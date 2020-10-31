import { ChemicalModalComponent } from './chemical-modal/chemical-modal.component';
import { ChemicalService } from './../../../_core/_service/chemical.service';
import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Pagination, PaginatedResult } from 'src/app/_core/_model/pagination';
import { AlertifyService } from 'src/app/_core/_service/alertify.service';
import { IIngredient } from 'src/app/_core/_model/Ingredient';
import { environment } from '../../../../environments/environment';
import { GridComponent, RowDDService } from '@syncfusion/ej2-angular-grids';
import { IChemical } from 'src/app/_core/_model/Chemical';
declare let $: any;
const CURRENT_DATE = new Date();

@Component({
  selector: 'app-chemical',
  templateUrl: './chemical.component.html',
  styleUrls: ['./chemical.component.css']
})
export class ChemicalComponent implements OnInit {

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
  editChemical = {
    showDeleteConfirmDialog: false,
    allowEditing: false,
    allowAdding: false,
    allowDeleting: false,
    mode: 'Normal',
  };
  destDropOptions = { targetID: 'Grid' };
  chemical: IChemical = {
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
    unit: 0,
    modify: false
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
  @ViewChild('chemicalGrid') chemicalGrid: GridComponent;
  show: boolean;
  pd: Date;
  dataChemical: Object;
  ingredientService: any;
  constructor(
    public modalService: NgbModal,
    private alertify: AlertifyService,
    private chemicalService: ChemicalService
  ) { }

  ngOnInit() {
    this.filterSettings = { type: 'Excel' };
    this.toolbarOptions = ['ExcelExport', 'Search'];
    this.excelDownloadUrl = `${environment.apiUrlEC}Chemical/ExcelExport`;
    this.chemicalService.currentChemical.subscribe(res => {
      if (res === 300) {
        this.getAll();
        this.chemical = {
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
          unit: 0,
          modify: false
        };
      }
    });
    this.getAll();

  }

  ngAfterViewInit() {
    $('[data-toggle="tooltip"]').tooltip();
  }

  createdSearch(args) {
    var gridElement = this.chemicalGrid.element;
    var span = document.createElement("span");
    span.className = "e-clear-icon";
    span.id = gridElement.id + "clear";
    span.onclick = this.cancelBtnClick.bind(this);
    gridElement.querySelector(".e-toolbar-item .e-input-group").appendChild(span);
  }

  public cancelBtnClick(args) {
    this.chemicalGrid.searchSettings.key = "";
    (this.chemicalGrid.element.querySelector(".e-input-group.e-search .e-input") as any).value = "";
  }

  actionBeginChemical(args) {
  }

  tooltip(data) {
    if (data) {
      return data;
    } else {
      return '';
    }
  }

  dataBound() {
  }

  toolbarClick(args): void {
    switch (args.item.text) {
      /* tslint:disable */
      case 'Excel Export':
        this.chemicalGrid.excelExport();
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
    this.chemicalService.getChemicals().subscribe(res => {
      this.dataChemical = res;
    });
  }

  delete(data) {
    this.alertify.confirm('Delete Chemical', 'Are you sure you want to delete this Chemical "' + data.id + '" ?', () => {
      this.chemicalService.delete(data.id).subscribe(() => {
        this.getAll();
        this.alertify.success('Chemical has been deleted');
      }, error => {
        this.alertify.error('Failed to delete the Chemical');
      });
    });
  }

  openChemicalModalComponent() {
    const modalRef = this.modalService.open(ChemicalModalComponent, { size: 'md' });
    modalRef.componentInstance.chemical = this.chemical;
    modalRef.componentInstance.title = 'Add Chemical';
    modalRef.result.then((result) => {
    }, (reason) => {
    });
  }

  openChemicalEditModalComponent(item) {
    const modalRef = this.modalService.open(ChemicalModalComponent, { size: 'md' });
    // this.getSupllier(item.supplierID);
    modalRef.componentInstance.chemical = item;
    modalRef.componentInstance.title = 'Edit Chemical';
    modalRef.result.then((result) => {
    }, (reason) => {
    });
  }

  // getSupllier(id) {
  //   this.ingredientService.getAllSupplierByTreatment(id).subscribe(res => {
  //     this.supplier = res ;
  //   });
  // }

  fileProgress(event) {
    this.file = event.target.files[0];
  }

  uploadFile() {
    const createdBy = JSON.parse(localStorage.getItem('user')).User.ID;
    this.chemicalService.import(this.file, createdBy)
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
    return (this.chemicalGrid.pageSettings.currentPage - 1) * this.chemicalGrid.pageSettings.pageSize + Number(index) + 1;
  }

  makeid(length) {
    let result           = '';
    const characters       = '0123456789';
    const charactersLength = characters.length;
    for ( let i = 0; i < length; i++ ) {
       result += characters.charAt(Math.floor(Math.random() * charactersLength));
    }
    return result;
   // return '59129032';
  }

}
