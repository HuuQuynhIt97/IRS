import { IGlues } from './../../../_core/_model/glues';
import { GlueService } from './../../../_core/_service/glue.service';
import { PartService } from 'src/app/_core/_service/part.service';
import { IPart } from './../../../_core/_model/Part';
import { Component, OnInit, ViewChild } from '@angular/core';
import { GridComponent } from '@syncfusion/ej2-angular-grids';
import { ScheduleService } from 'src/app/_core/_service/schedule.service';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from 'src/app/_core/_service/alertify.service';
import { CheckBoxComponent } from '@syncfusion/ej2-angular-buttons';
import { DropDownListComponent } from '@syncfusion/ej2-angular-dropdowns';
@Component({
  selector: 'app-schedule-detail',
  templateUrl: './schedule-detail.component.html',
  styleUrls: ['./schedule-detail.component.css']
})
export class ScheduleDetailComponent implements OnInit {
  partData: any = [];
  treatmentWayData: any = [];
  glueData: [];
  supData: [];
  previewData: any = [];
  RoleName: string ;
  textPreview: string;
  chemicalData: any;
  chemicalDataDefault: any =  [];
  saveData: any = [];
  saveDataTamp: any = [];
  editSettings = {
    showDeleteConfirmDialog: false,
    allowEditing: false,
    allowAdding: true,
    allowDeleting: true,
    autoFit: true,
    mode: 'Normal',
  };
  editSettingss = {
    showDeleteConfirmDialog: false,
    allowEditing: true,
    allowAdding: true,
    allowDeleting: true,
    autoFit: true,
    mode: 'Normal',
  };
  editSettingsCheckbox = {
    showDeleteConfirmDialog: false,
    allowEditing: false,
    allowAdding: false,
    allowDeleting: false,
    autoFit: false,
    mode: 'Normal',
  };
  editSettingsDefault = {
    showDeleteConfirmDialog: false,
    allowEditing: false,
    allowAdding: false,
    allowDeleting: false,
    autoFit: true,
    mode: 'Normal',
  };
  ScheduleID: number;
  toolbar = [
    {
      text: 'Add New',
      tooltipText: 'Add new row',
      prefixIcon: 'e-add',
      id: 'AddNew',
    },
    'Delete',
    'Cancel',
    'Search',
  ];
  editparamsChemical: any = { params: { format: 'n' } };
  toolbarChemical = [
    // {
    //   text: 'Add New',
    //   tooltipText: 'Add new row',
    //   prefixIcon: 'e-add',
    //   id: 'AddNew',
    // },
    // 'Delete',
    'Cancel',
    'Search',
  ];
  toolbarCheckbox = [
    // 'Search',
  ];
  modalPart: IPart = {
    id: 0,
    name: '',
    objectID: 0,
    status: false,
    scheduleID: 0,
  };
  modalGlues: IGlues = {
    id: 0,
    name: '',
    glueID: 0,
    partID: 0,
    treatmentWayID: 0,
    scheduleID: 0,
  };
  public fieldsPosition: object = { text: 'name', value: 'id' };
  pageSettings = { pageCount: 20, pageSizes: true, pageSize: 10 };
  modelName: string;
  modelNo: string;
  articleNo: string;
  ProcessName: string;
  ObjectName: string;
  public rowIndex: any = '';
  isShow: boolean = false;
  isShowDefault: boolean = false;
  @ViewChild('gridPart')
  public gridPart: GridComponent;
  @ViewChild('gridChemical')
  public gridChemical: GridComponent;

  @ViewChild('positionDropdownlist')
  public dropdObj: DropDownListComponent;
  B: any;
  C: any;
  D: any;
  E: any;
  partID: number;
  treatmentID: number;
  glueID: number;
  namePart: any = '';
  public checked: boolean = false;
  public approveStatus: boolean = false;
  public finishStatus: boolean = false;
  public toolbarOptions = ['Add', 'Delete', 'Cancel', 'Search'];
  filterSettings = { type: 'Excel' };
  public checkedwifi: boolean = true;

  public checkModify: boolean = false;
  public textModify: string ;
  public partModify: string ;
  selectedRow = [];
  selIndex: number[];
  public checkInkChemicalExist: boolean = false;
  @ViewChild('checkbox')
  public checkbox: CheckBoxComponent;
  constructor(
    private route: ActivatedRoute,
    private alertify: AlertifyService,
    private scheduleService: ScheduleService,
    private glueService: GlueService,
    private partService: PartService
  ) { }

  ngOnInit(): void {
    this.removeLocalStore("details") ;
    this.ScheduleID = this.route.snapshot.params.id ;
    this.GetDetailSchedule();
    setTimeout(() => {
      this.getAllGlue();
    }, 400);
    this.RoleName = JSON.parse(localStorage.getItem('role')).name
  }

  CheckInkChemicalExist(id) {
    for (const item of this.chemicalDataDefault) {
      if (item.id === id) {
        this.checkInkChemicalExist = true;
        break;
      } else {
        this.checkInkChemicalExist = false;
      }
    }
  }

  public changeHandler(args,data) : void {
    this.checkModify = true ;
    if (args.checked) {
      for (let i in this.chemicalData) {
        if(this.chemicalData[i].id == data.id && this.chemicalData[i].subname === data.subname) {
          this.chemicalData[i].status = true;
          break;
        }
      }
    } else
    {
      for (let i in this.chemicalData) {
        if(this.chemicalData[i].id == data.id  && this.chemicalData[i].subname === data.subname) {
          this.chemicalData[i].status = false;
          break;
        }
      }
    }
    if (args.checked) {
      this.previewData = [] ;
      this.saveData.push(data) ;
      const details = this.getLocalStore('details') ;
      details.push(data) ;
      this.setLocalStore('details', details) ;

    } else {
      const details = this.getLocalStore('details') ;
      for(var i = 0; i < details.length; i++) {
        if(details[i].id == data.id) {
          details.splice(i, 1);
          break;
        }
      }
      this.setLocalStore('details', details);
    }
  }

  ClickSub(item) {
    if (this.gridPart.getSelectedRowIndexes().length === 0) {
      this.alertify.warning('Please select Part first!', true) ;
    } else {
      if (item === 0) {
        this.previewData = [] ;
        this.isShow = true ;
        this.getInkChemicalByglueID() ;
      } else {
        this.isShow = true ;
        this.GetChemicalBySupplier(item.id) ;
      }
    }
  }

  getAllGlue() {
    this.glueService.getGlueByScheduleID(this.ScheduleID).subscribe((res: any) => {
      this.glueData = res ;
    })
  }

  GetChemicalBySupplier(id) {
    this.scheduleService.GetChemicalBySupplier(id).subscribe((res: any) => {
      this.chemicalData = res.map(item => {
        return {
          status: false,
          id: item.id,
          name: item.name,
          subname: item.subname,
          percentage: item.percentage,
          modify: item.modify,
          code: item.code
        }
      }) ;
      let details = this.getLocalStore("details");
      if (details.length > 0 ) {
        for (let item in this.chemicalData) {
          for ( let item2 in details) {
            if (this.chemicalData[item].id == details[item2].id) {
              this.chemicalData[item].status = true;
              this.chemicalData[item].percentage = details[item2].percentage;
            }
          }
        }
      }
      this.isShow = true ;
    });
  }

  done() {
    this.scheduleService.done(this.ScheduleID).subscribe((res) => {
      this.alertify.success('success');
      this.GetDetailSchedule();
    });
  }

  approve() {
    const userid = JSON.parse(localStorage.getItem('user')).User.ID;
    this.scheduleService.approval(this.ScheduleID, userid).subscribe((res) => {
      this.alertify.success('success');
      this.GetDetailSchedule();
    });
  }

  GetDetailSchedule() {
    this.scheduleService.GetDetailSchedule(this.ScheduleID).subscribe((res: any) => {
      this.modelName = res[0].modelName;
      this.modelNo = res[0].modelNo;
      this.articleNo = res[0].articleNo;
      this.ProcessName = res[0].treatment;
      this.ObjectName = res[0].process;
      this.partData = res[0].parts;
      this.treatmentWayData = res[0].treatmentWay;
      this.supData = res[0].supplier;
      this.finishStatus = res[0].finishedStatus;
      this.approveStatus = res[0].approvalStatus;
      this.modalPart.objectID = res[0].inkTblObjectID;
    });
  }

  dataBoundPart() {
    if (this.selectedRow.length) {
      this.selectedRow = [this.glueData.length - 1]
      this.gridPart.selectRows(this.selectedRow);
      this.selectedRow = [];
    }
  }

  dataBoundChemical() {

  }

  actionComplete(e: any): void {

  }

  toolbarClick(args: any): void {
    switch (args.item.text) {
      case 'Add':
        // this.dropObj.enabled  = false;
        if (this.checkModify === true) {
          this.alertify.valid('Warning',`Are you sure you want to discard these changes ? <br> Cảnh báo ! Bạn có chắc rằng muốn bỏ qua những thay đổi chưa được lưu của ${this.textModify}`)
          .then((result) => {
          }).catch((err) => {
            args.cancel = true;
            this.gridPart.refresh();
          });
        } else {
          break;
        }
    }
  }

  ChemicalToolbarClick(args: any): void {

  }

  rowSelected(args: any) {
    this.selIndex = [args.rowIndex];
    if (!args.isInteracted && args.previousRowIndex) {
      this.selIndex = [args.previousRowIndex];
    }
    if (args.isInteracted === false ) {
      this.removeLocalStore("details") ;
      this.loadPreview() ;
      this.partID = args.data[0].partID ;
      this.treatmentID = args.data[0].treatmentID ;
      this.glueID = args.data[0].id ;
      this.isShow = true ;
      this.partModify = args.data[0].part ;
      this.getInkChemicalByglueID();
    }
    if (this.checkModify === true) {
      this.alertify.valid('Warning','Are you sure you want to discard these changes ? Cảnh báo ! Bạn có chắc rằng muốn bỏ qua những thay đổi chưa được lưu của ' + this.textModify)
      .then((result) => {
        if(result) {
          this.removeLocalStore("details") ;
          this.loadPreview() ;
          this.partID = args.data.partID ;
          this.treatmentID = args.data.treatmentID ;
          this.glueID = args.data.id;
          this.isShow = true ;
          this.partModify = args.data.part  ;
          this.getInkChemicalByglueID() ;
          this.checkModify = false ;
        }
      })
    }
    if (args.isInteracted) {
      this.removeLocalStore("details") ;
      this.loadPreview() ;
      this.partID = args.data.partID ;
      this.treatmentID = args.data.treatmentID ;
      this.glueID = args.data.id;
      this.isShow = true ;
      this.partModify = args.data.part  ;
      this.getInkChemicalByglueID() ;
    }

  }

  setLocalStore(key: string, value: any) {
    localStorage.removeItem(key) ;
    let details = value || [] ;
    for (let key in details) {
      details[key].status = true ;
    }
    const result = JSON.stringify(details) ;
    localStorage.setItem(key, result) ;
    this.loadPreview() ;
  }

  getLocalStore(key: string) {
    const data = JSON.parse(localStorage.getItem(key)) || [];
    return data ;
  }

  getInkChemicalByglueID() {
    const details = this.getLocalStore('details');
    if (details.length === 0) {
      this.glueService.GetInkChemicalByGlueID(this.glueID).subscribe((res: any) => {
        this.chemicalData = res.map((item, index) => {
          return {
            index: index,
            status: true,
            id: item.id,
            name: item.name,
            subname: item.subname,
            percentage: item.percentage,
            modify: item.modify,
            code: item.code
          }
        }) ;
        this.setLocalStore('details', this.chemicalData);
      })
    }
     else {
      const details = this.getLocalStore('details');
      this.chemicalData = details
     }
  }

  loadPreview() {
    this.previewData = [] ;
    this.textPreview = null ;
    let details = this.getLocalStore("details") ;
    for (let i = 0; i < details.length; i++) {
      this.previewData.push(details[i].percentage + '%' + details[i].name) ;
      this.textPreview = this.previewData.join(' + ') ;
      this.textModify = this.partModify + ' - ' + this.textPreview ;
    }
  }

  rowSelectedChemical(args: any) {
  }

  actionBeginPart(args) {

    if (args.requestType === 'delete') {
      this.delete(args.data[0].id);
    }
  }

  tooltips(data) {
    if (data) {
      return data;
    } else {
      return '';
    }
  }

  actionBeginChemical(args) {

    if (args.requestType === 'beginEdit') {
      if (args.rowData.status === false ) {
        this.alertify.warning('Please select the ink/chemical ! <br> Vui lòng chọn mực/hóa chất' , true);
        args.cancel = true ;
        return ;
      }
      if (args.rowData.status) {
        if(args.rowData.modify === false) {
          this.alertify.warning('Can not modify this chemical <br> Không thể sửa đổi hóa chất này' , true);
          args.cancel = true ;
          return ;
        }
      }
    }

    if (args.requestType === 'save' && args.action === 'edit') {
      let details = this.getLocalStore("details") ;
      for (let i in this.chemicalData) {
        if(this.chemicalData[i].id == args.data.id  && this.chemicalData[i].subname == args.data.subname ) {
          this.chemicalData[i].percentage = args.data.percentage ;
          break ;
        }
      }
      for (let i in details) {
        if(details[i].id == args.data.id && details[i].subname == args.data.subname) {
          details[i].percentage = args.data.percentage ;
          break ;
        }
      }
      this.setLocalStore("details", details) ;
    }

  }

  delete(id) {
    this.alertify.confirm('Delete Glues', 'Are you sure you want to delete this Glues "' + id + '" ?', () => {
      this.glueService.delete(id).subscribe(() => {
        this.getAllGlue();
        this.isShow = false;
        this.alertify.success('Glues has been deleted');
      }, error => {
        this.alertify.error('Failed to delete the Glues');
        this.gridPart.refresh();
      });
    });
  }

  save() {
    this.partService.create(this.modalGlues).subscribe((res) => {
      this.alertify.success('Create successfully!');
      this.getAllGlue();
      this.selectedRow = [0];
      this.selIndex = [0];
    });
  }

  onChangePosition(args, data, index) {
    if (this.checkModify === true) {
      this.alertify.valid('Warning',`Are you sure you want to discard these changes ? <br> Cảnh báo ! Bạn có chắc rằng muốn bỏ qua những thay đổi chưa được lưu của ${this.textModify}`)
      .then((result) => {
        if(result) {
          this.modalGlues.name = '1';
          this.modalGlues.scheduleID = this.ScheduleID;
          this.modalGlues.partID = args.value;
          // this.save();
          this.checkModify = false ;
        }
      }).catch((err) => {
        args.cancel = true;
      });
    } else {
      this.modalGlues.name = '1';
      this.modalGlues.scheduleID = this.ScheduleID;
      this.modalGlues.partID = args.value;
      // this.save();
    }
  }

  onChangeTreatmentWay(args, data, index) {
    this.modalGlues.treatmentWayID = args.value ;
    if (data.id) {
      this.modalGlues.id = data.id || 0 ;
      this.modalGlues.name = data.name ;
      this.modalGlues.partID = data.partID ;
      this.modalGlues.scheduleID = Number(this.ScheduleID) ;
      this.updateGlue(this.modalGlues) ;
    } else {
      this.save();
    }
  }

  updateGlue(modal) {
    this.partService.updateGlue(modal).subscribe((res: any) => {
      this.alertify.success('Update Treatment successfully!') ;
      this.getAllGlue() ;
      // this.selectedRow = [0];
      // this.selIndex = [0];
    })
  }

  finished() {
    let DataSave = JSON.parse(localStorage.getItem("details"));
    const obj = {
      name: this.textPreview,
      glueID: this.glueID,
      partID: this.partID,
      treatmentID: this.treatmentID,
      listAdd: DataSave
    }
    this.glueService.saveGlue(obj).subscribe((res: any) => {
      if (res.status) {
        this.alertify.success('successfully!');
        this.previewData = [] ;
        this.getAllGlue() ;
        this.isShow = false ;
        this.checkModify  = false ;
        this.gridPart.refresh() ;
        this.removeLocalStore("details") ;
        // this.selectedRow = [0]
      } else {
        this.alertify.error(res.message) ;
      }
    });
  }

  removeLocalStore(key: string) {
    localStorage.removeItem(key);
  }

  update(modal) {
    this.partService.update(modal).subscribe(res => {
      this.alertify.success('Updated successfully!');
      this.GetDetailSchedule();
    });
  }

  rowDeselected(args) {

  }

  rowDeselectedPart(args) {

  }

  NO(index) {
    return (this.gridPart.pageSettings.currentPage - 1) * this.gridPart.pageSettings.pageSize + Number(index) + 1;
  }

}
