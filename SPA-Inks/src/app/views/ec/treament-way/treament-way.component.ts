import { ITreatmentWay } from './../../../_core/_model/TreatmentWay';
import { TreatmentService } from './../../../_core/_service/treatment.service';
import { ISupplier } from './../../../_core/_model/Supplier';
import { IngredientService } from './../../../_core/_service/ingredient.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { AlertifyService } from 'src/app/_core/_service/alertify.service';
import { GridComponent } from '@syncfusion/ej2-angular-grids';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-treament-way',
  templateUrl: './treament-way.component.html',
  styleUrls: ['./treament-way.component.css']
})
export class TreamentWayComponent implements OnInit {

  public pageSettings = { pageCount: 20, pageSizes: true, currentPage: 1, pageSize: 10 };
  public toolbarOptions = ['ExcelExport', 'Add', 'Edit', 'Delete', 'Cancel', 'Search'];
  public editSettings = { showDeleteConfirmDialog: false, allowEditing: true, allowAdding: true, allowDeleting: true, mode: 'Normal' };
  public data: object[];
  filterSettings = { type: 'Excel' };
  modalTreatmentWay: ITreatmentWay = {
    id: 0,
    name: '',
    Process: ''
  };
  public ProcessData: any = [];
  public textProcess = 'Select Treament';
  public fieldsSup: object = { text: 'name', value: 'name' };
  @ViewChild('grid') grid: GridComponent;
  public textGlueLineName = 'Select ';
  public treatmentWay: object[];
  public setFocus: any;
  constructor(
    private alertify: AlertifyService,
    public modalService: NgbModal,
    private ingredientService: IngredientService,
    private treatmentWayService: TreatmentService
  ) { }

  ngOnInit(): void {
    this.getAll();
    this.getAllProcess();
  }

  onChangeTreatment(args) {
    this.modalTreatmentWay.Process = args.value;
  }

  getAll() {
    this.treatmentWayService.getAll().subscribe((res: any) => {
      this.treatmentWay = res;
    });
  }
  getAllProcess() {
    this.treatmentWayService.getAllProcess().subscribe((res: any) => {
      this.ProcessData = res;
    });
  }

  actionBegin(args) {
    if (args.requestType === 'save') {
      if (args.action === 'edit') {
        this.modalTreatmentWay.id = args.data.id || 0;
        this.modalTreatmentWay.name = args.data.name;
        this.update(this.modalTreatmentWay);
      }
      if (args.action === 'add') {
        this.modalTreatmentWay.id = 0;
        this.modalTreatmentWay.name = args.data.name;
        this.add(this.modalTreatmentWay);
      }
    }
    if (args.requestType === 'delete') {
      this.delete(args.data[0].id);
    }
  }

  toolbarClick(args): void {
    switch (args.item.text) {
      /* tslint:disable */
      case 'Excel Export':
        this.grid.excelExport();
        break;
      /* tslint:enable */
      default:
        break;
    }
  }

  actionComplete(e: any): void {
    if (e.requestType === 'add') {
      (e.form.elements.namedItem('name') as HTMLInputElement).focus();
      (e.form.elements.namedItem('id') as HTMLInputElement).disabled = true;
    }
  }

  onDoubleClick(args: any): void {
    this.setFocus = args.column;  // Get the column from Double click event
  }

  delete(id) {
    this.alertify.confirm('Delete Treatment Way', 'Are you sure you want to delete this Treatment Way "' + id + '" ?', () => {
      this.treatmentWayService.delete(id).subscribe(() => {
        this.getAll();
        this.alertify.success('Treatment Way has been deleted');
      }, error => {
          this.alertify.error('Failed to delete the Treatment Way');
      });
    });
  }

  update(modalSup) {
    this.treatmentWayService.update(modalSup).subscribe(res => {
      this.alertify.success('Updated successfully!');
      this.getAll();
    });
  }

  add(modalSup) {
    this.treatmentWayService.create(modalSup).subscribe(() => {
      this.alertify.success('Add Treatment way successfully');
      this.getAll();
      this.modalTreatmentWay.name = '';
    });
  }

  save() {
    this.treatmentWayService.create(this.modalTreatmentWay).subscribe(() => {
      this.alertify.success('Add Treatment way successfully');
      this.getAll();
      this.modalTreatmentWay.name = '';
    });
  }

  NO(index) {
    return (this.grid.pageSettings.currentPage - 1) * this.grid.pageSettings.pageSize + Number(index) + 1;
  }


}
