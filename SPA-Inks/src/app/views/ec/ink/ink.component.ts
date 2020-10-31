import { InkModalComponent } from './ink-modal/ink-modal.component';
import { InkService } from './../../../_core/_service/ink.service';
import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';

import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ActivatedRoute } from '@angular/router';
import { Pagination, PaginatedResult } from 'src/app/_core/_model/pagination';
import { IngredientService } from 'src/app/_core/_service/ingredient.service';
import { AlertifyService } from 'src/app/_core/_service/alertify.service';
import { IIngredient } from 'src/app/_core/_model/Ingredient';
import { ModalNameService } from 'src/app/_core/_service/modal-name.service';
import { environment } from '../../../../environments/environment';
import { QRCodeGenerator, DisplayTextModel } from '@syncfusion/ej2-angular-barcode-generator';
import { GridComponent, RowDDService } from '@syncfusion/ej2-angular-grids';
import { DatePipe } from '@angular/common';
import { IInk } from 'src/app/_core/_model/Ink';
declare let $: any;
const CURRENT_DATE = new Date();
@Component({
  selector: 'app-ink',
  templateUrl: './ink.component.html',
  styleUrls: ['./ink.component.css'],
  providers: [DatePipe, RowDDService]
})
export class InkComponent implements OnInit {

  editSettings = { showDeleteConfirmDialog: false, allowEditing: true, mode: 'Normal' };
  pageSettings = { pageCount: 20, pageSizes: true, currentPage: 1, pageSize: 20 };
  data: any;
  dataInk: any;
  destData: object[] = [];
  modalReference: NgbModalRef;
  excelDownloadUrl: string;
  defaultDate = new Date(null);
  currentDate = new Date();
  srcDropOptions = { targetID: 'DestGrid' };
  public displayTextMethod: DisplayTextModel = {
    visibility: false
  };
  destDropOptions = { targetID: 'Grid' };
  public mfg = this.datePipe.transform(new Date(), 'yyyyMMdd');
  public exp = this.datePipe.transform(new Date(new Date().setMonth(new Date().getMonth() + 4)), 'yyyyMMdd');
  ink: IInk = {
    id: 0,
    name: '',
    code: '',
    createdDate: new Date(),
    supplierID: 0,
    allow: 0,
    voc: 0,
    processID: 0,
    createdBy: 0,
    expiredTime: 0,
    daysToExpiration: 0,
    materialNO: '',
    unit: 0
  };
  pagination: Pagination;
  page = 1;
  currentPage = 1;
  itemsPerPage = 15;
  totalItems: any;
  file: any;
  toolbar = ['Search'];
  text: any;
  filterSettings: any;
  toolbarOptions: any;
  @ViewChild('grid', { static: true })
  public grid: GridComponent;
  show: boolean;
  pd: Date;
  constructor(
    private modalNameService: ModalNameService,
    public modalService: NgbModal,
    private ingredientService: IngredientService,
    private alertify: AlertifyService,
    private datePipe: DatePipe,
    private route: ActivatedRoute,
    private inkService: InkService
  ) { }

  ngOnInit() {
    this.filterSettings = { type: 'Excel' };
    this.toolbarOptions = ['ExcelExport', 'Search'];
    this.excelDownloadUrl = `${environment.apiUrlEC}Ink/ExcelExport`;
    this.inkService.currentInk.subscribe(res => {
      if (res === 300) {
        this.getAll();
        this.ink = {
          id: 0,
          name: '',
          code: '',
          createdDate: new Date(),
          supplierID: 0,
          allow: 0,
          voc: 0,
          processID: 0,
          createdBy: 0,
          expiredTime: 0,
          daysToExpiration: 0,
          materialNO: '',
          unit: 0
        };
      }
    });
    this.getAll();
  }

  createdSearch(args) {
    var gridElement = this.grid.element;
    var span = document.createElement("span");
    span.className = "e-clear-icon";
    span.id = gridElement.id + "clear";
    span.onclick = this.cancelBtnClick.bind(this);
    gridElement.querySelector(".e-toolbar-item .e-input-group").appendChild(span);
  }

  public cancelBtnClick(args) {
    this.grid.searchSettings.key = "";
    (this.grid.element.querySelector(".e-input-group.e-search .e-input") as any).value = "";
  }

  ngAfterViewInit() {
    $('[data-toggle="tooltip"]').tooltip();
  }

  dataBound() {
  }

  makeid(length) {
    let result = '';
    const characters = '0123456789';
    const charactersLength = characters.length;
    for ( let i = 0; i < length; i++ ) {
      result += characters.charAt(Math.floor(Math.random() * charactersLength));
    }
    return result;
   // return '59129032';
  }

  toolbarClick(args): void {
    switch (args.item.text) {
      /* tslint:disable */
      case 'Excel Export':
        this.grid.excelExport();
        break;
      /* tslint:enable */
      case 'PDF Export':
        break;
    }
  }

  actionBegin(args) {
    if (args.requestType === 'save' && args.action === 'edit') {
    }
  }

  getAll() {
    this.inkService.getInks().subscribe(res => {
      this.dataInk = res;
    });
  }

  delete(data) {
    this.alertify.confirm('Delete Ink', 'Are you sure you want to delete this Ink "' + data.id + '" ?', () => {
      this.inkService.delete(data.id).subscribe(() => {
        this.getAll();
        this.alertify.success('Ink has been deleted');
      }, error => {
        this.alertify.error('Failed to delete the Ink');
      });
    });
  }

  tooltip(data) {
    if (data) {
      return data;
    } else {
      return '';
    }
  }

  openInkModalComponent() {
    const modalRef = this.modalService.open(InkModalComponent, { size: 'md' });
    modalRef.componentInstance.ink = this.ink;
    modalRef.componentInstance.title = 'Add Ink';
    modalRef.result.then((result) => {
    }, (reason) => {
    });
  }

  openInkEditModalComponent(item) {
    const modalRef = this.modalService.open(InkModalComponent, { size: 'md' });
    modalRef.componentInstance.ink = item;
    modalRef.componentInstance.title = 'Edit Ink';
    modalRef.result.then((result) => {
    }, (reason) => {
    });
  }

  fileProgress(event) {
    this.file = event.target.files[0];
  }

  uploadFile() {
    const createdBy = JSON.parse(localStorage.getItem('user')).User.ID;
    this.inkService.import(this.file, createdBy)
      .subscribe((res: any) => {
        this.getAll();
        this.alertify.success('The excel has been imported into system!');
        this.modalReference.close();
      });
  }

  showModal(name) {
    this.modalReference = this.modalService.open(name, { size: 'xl' });
  }

  NO(index) {
    return (this.grid.pageSettings.currentPage - 1) * this.grid.pageSettings.pageSize + Number(index) + 1;
  }

}
