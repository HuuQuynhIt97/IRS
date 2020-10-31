import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { ModalName } from './../../../_core/_model/modal-name';
import { ModalNameService } from './../../../_core/_service/modal-name.service';
import { AlertifyService } from 'src/app/_core/_service/alertify.service';
import { PageSettingsModel, GridComponent } from '@syncfusion/ej2-angular-grids';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { EditService, ToolbarService, PageService } from '@syncfusion/ej2-angular-grids';
import { ActivatedRoute } from '@angular/router';
import { IngredientService } from 'src/app/_core/_service/ingredient.service';
import { GlueService } from 'src/app/_core/_service/glue.service';
import { ICommentModelName } from 'src/app/_core/_model/comment';
import { CommentService } from 'src/app/_core/_service/comment.service';
import { BuildingUserService } from 'src/app/_core/_service/building.user.service';
import { CalendarsService } from 'src/app/_core/_service/calendars.service';
import { AuthService } from 'src/app/_core/_service/auth.service';
import { BPFCEstablishService } from 'src/app/_core/_service/bpfc-establish.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ScheduleService } from 'src/app/_core/_service/schedule.service';

@Component({
  selector: 'app-schedule-status',
  templateUrl: './schedule-status.component.html',
  styleUrls: ['./schedule-status.component.css']
})
export class ScheduleStatusComponent implements OnInit {

  public pageSettings: PageSettingsModel;
  public editSettings: object;
  public toolbar: object;
  users: any[] = [];
  BPFCEstablishID: number;
  ScheduleID: number;
  public editparams: object;
  public grid: GridComponent;
  modalReference: NgbModalRef;
  modalReferenceDetail: NgbModalRef;
  public data: object[];
  searchSettings: any = { hierarchyMode: 'Parent' };
  public name: string;
  pageSize: number;
  page: number;
  ingredients: any;
  glues: any;
  glueid: any;
  content: any;
  @ViewChild('gridModel')
  public gridModel: GridComponent;
  @ViewChild('gridInkChemical')
  public gridInkChemical: GridComponent;
  modalname: ModalName = {
    id: 0,
    name: '',
    modelNo: '',
    createdBy: JSON.parse(localStorage.getItem('user')).User.ID
  };
  comment: ICommentModelName;
  comments: [];
  setFocus: any;
  level: any;
  newglueID: any;
  RoleName: string ;
  constructor(
    private modalNameService: ModalNameService,
    private bPFCEstablishService: BPFCEstablishService,
    public modalService: NgbModal,
    private route: ActivatedRoute,
    private alertify: AlertifyService,
    private ingredientService: IngredientService,
    private glueService: GlueService,
    private commentService: CommentService,
    private calendarsService: CalendarsService,
    private buildingUserService: BuildingUserService,
    private authService: AuthService,
    private spinner: NgxSpinnerService,
    private scheduleService: ScheduleService
  ) { }

  ngOnInit(): void {
    this.pageSettings = { currentPage: 1, pageSize: 10, pageCount: 20 };
    this.editparams = { params: { popupHeight: '300px' } };
    this.editSettings = { showDeleteConfirmDialog: true, allowEditing: false, allowAdding: false, allowDeleting: false, mode: 'Normal' };
    this.toolbar = ['ExcelExport',
     'Search',
    //  'Default',
    //   { text: 'Approved', tooltipText: 'Approved', prefixIcon: 'fa fa-check', id: 'Approved' },
    //   { text: 'Not Approved', tooltipText: 'Not Approved', prefixIcon: 'fa fa-times', id: 'Not Approved' },
    //   'All'
    ];
    this.getAllUsers();
    this.RoleName = JSON.parse(localStorage.getItem('role')).name
  }

  ngAfterViewInit() {
    this.getBuilding();
  }

  getAllUsers() {
    this.buildingUserService.getAllUsers(1, 1000).subscribe((res: any) => {
      this.users = res.result;
      this.getAll();
    });
  }

  tooltips(data) {
    if (data) {
      return data;
    } else {
      return '';
    }
  }

  createdSearch(args) {
    var gridElement = this.gridModel.element;
    var span = document.createElement("span");
    span.className = "e-clear-icon";
    span.id = gridElement.id + "clear";
    span.onclick = this.cancelBtnClick.bind(this);
    gridElement.querySelector(".e-toolbar-item .e-input-group").appendChild(span);
  }

  public cancelBtnClick(args) {
    this.gridModel.searchSettings.key = "";
    (this.gridModel.element.querySelector(".e-input-group.e-search .e-input") as any).value = "";
  }

  // getAllUsers() {
  //   this.buildingUserService.getAllUsers(1, 1000).subscribe((res: any) => {
  //     this.users = res.result;
  //     this.filterByFinishedStatus();
  //   });
  // }

  getAll() {
    this.spinner.show();
      setTimeout(() =>{
        this.scheduleService.getAll().subscribe( (res: any) => {
          this.data = res.map(item => {
            return {
              status: false,
              id: item.id,
              approvalStatus: item.approvalStatus,
              articleNo: item.articleNo,
              createdBy: item.createdBy,
              createdDate: item.createdDate,
              establishDate: item.establishDate,
              finishedStatus: item.finishedStatus,
              modelName: item.modelName,
              modelNo: item.modelNo,
              modifiedDate: item.modifiedDate,
              part: item.part,
              parts: item.parts,
              process: item.process,
              productionDate: item.productionDate,
              treatment: item.treatment,
              approvalBy: this.createdBy(item.approvalBy)
            }
          }) ; ;
          this.spinner.hide();
        });
      }, 500)
  }

  getBuilding() {
    const userID = JSON.parse(localStorage.getItem('user')).User.ID;
    this.authService.getBuildingByUserID(userID).subscribe((res: any) => {
      res = res || {};
      if (res !== {}) {
        this.level = res.level;
      }
    });
  }

  dataBound() {
  }

  no(item: any): number {
    return (this.pageSettings.currentPage - 1) * this.pageSettings.pageSize + Number(item.index) + 1;
  }

  actionBegin(args) {
    if (args.requestType === 'save') {
      this.modalname.id = args.data.id || 0;
      this.modalname.name = args.data.name;
      this.modalname.modelNo = args.data.modelNo;
      if (args.data.id > 0) {
        this.update(this.modalname);
      } else {
        this.add(this.modalname);
      }
    }
    if (args.requestType === 'delete') {
      this.delete(args.data[0].id);
    }
  }

  actionComplete(e: any): void {
    if (e.requestType === 'add') {
      (e.form.elements.namedItem('name') as HTMLInputElement).focus();
      (e.form.elements.namedItem('id') as HTMLInputElement).disabled = true;
      (e.form.elements.namedItem('tool') as HTMLInputElement).disabled = true;
    }
  }

  onDoubleClick(args: any): void {
    this.setFocus = args.column;  // Get the column from Double click event
  }

  openaddModalName(addModalName) {
    this.modalReference = this.modalService.open(addModalName);
  }

  getAllBPFCStatus() {
    this.bPFCEstablishService.getAllBPFCStatus().subscribe((res: any) => {
      this.data = res.map(item => {
        return  {
          id: item.id,
          modelNameID: item.modelNameID,
          modelNoID: item.modelNoID,
          articleNoID: item.articleNoID,
          artProcessID: item.artProcessID,
          modelName: item.modelName,
          modelNo: item.modelNo,
          articleNo: item.articleNo,
          artProcess: item.artProcess,
          approvalStatus: item.approvalStatus,
          finishedStatus: item.finishedStatus,
          approvalBy:  this.createdBy(item.approvalBy),
          createdBy:  this.createdBy(item.createdBy),
          season: item.season,
          createdDate: item.createdDate,
          modifiedDate: item.modifiedDate,
          updateTime: item.updateTime,
          bpfcName: `${item.modelName } - ${item.modelNo } - ${item.articleNo } - ${item.artProcess }`,
        };
      });
    });
  }

  filterByApprovedStatus() {
    this.bPFCEstablishService.filterByApprovedStatus().subscribe((res: any) => {
      this.data = res.map(item => {
        return  {
          id: item.id,
          modelNameID: item.modelNameID,
          modelNoID: item.modelNoID,
          articleNoID: item.articleNoID,
          artProcessID: item.artProcessID,
          modelName: item.modelName,
          modelNo: item.modelNo,
          articleNo: item.articleNo,
          artProcess: item.artProcess,
          approvalStatus: item.approvalStatus,
          finishedStatus: item.finishedStatus,
          approvalBy:  this.createdBy(item.approvalBy),
          createdBy:  this.createdBy(item.createdBy),
          season: item.season,
          createdDate: item.createdDate,
          modifiedDate: item.modifiedDate,
          updateTime: item.updateTime,
          bpfcName: `${item.modelName } - ${item.modelNo } - ${item.articleNo } - ${item.artProcess }`,
        };
      });
    });
  }

  filterByFinishedStatus() {
    this.bPFCEstablishService.filterByFinishedStatus().subscribe((res: any) => {
      this.data = res.map(item => {
        return  {
          id: item.id,
          modelNameID: item.modelNameID,
          modelNoID: item.modelNoID,
          articleNoID: item.articleNoID,
          artProcessID: item.artProcessID,
          modelName: item.modelName,
          modelNo: item.modelNo,
          articleNo: item.articleNo,
          artProcess: item.artProcess,
          approvalStatus: item.approvalStatus,
          finishedStatus: item.finishedStatus,
          approvalBy:  this.createdBy(item.approvalBy),
          createdBy:  this.createdBy(item.createdBy),
          season: item.season,
          createdDate: item.createdDate,
          modifiedDate: item.modifiedDate,
          updateTime: item.updateTime,
          bpfcName: `${item.modelName } - ${item.modelNo } - ${item.articleNo } - ${item.artProcess }`,
        };
      });
    });
  }

  filterByNotApprovedStatus() {
    this.bPFCEstablishService.filterByNotApprovedStatus().subscribe((res: any) => {
      this.data = res.map(item => {
        return  {
          id: item.id,
          modelNameID: item.modelNameID,
          modelNoID: item.modelNoID,
          articleNoID: item.articleNoID,
          artProcessID: item.artProcessID,
          modelName: item.modelName,
          modelNo: item.modelNo,
          articleNo: item.articleNo,
          artProcess: item.artProcess,
          approvalStatus: item.approvalStatus,
          finishedStatus: item.finishedStatus,
          approvalBy:  this.createdBy(item.approvalBy),
          createdBy:  this.createdBy(item.createdBy),
          season: item.season,
          createdDate: item.createdDate,
          modifiedDate: item.modifiedDate,
          updateTime: item.updateTime,
          bpfcName: `${item.modelName } - ${item.modelNo } - ${item.articleNo } - ${item.artProcess }`,
        };
      });
    });
  }

  update(modelname) {
    this.modalNameService.update(modelname).subscribe(() => {
      this.alertify.success('Update Modal Name Successfully');
    });
  }

  delete(id) {
    this.alertify.confirm('Delete Modal Name', 'Are you sure you want to delete this ModalName ID "' + id + '" ?', () => {
      this.modalNameService.delete(id).subscribe(() => {
        this.getAllBPFCStatus();
        this.alertify.success('Modal Name has been deleted');
      }, error => {
        this.alertify.error('Failed to delete the Modal Name');
      });
    });
  }

  add(modalname) {
    this.modalNameService.create(modalname).subscribe(() => {
      this.alertify.success('Add Modal Name Successfully');
      this.getAllBPFCStatus();
    });
  }

  approval(BPFCEstablishID) {
    const userid = JSON.parse(localStorage.getItem('user')).User.ID;
    this.bPFCEstablishService.approval(BPFCEstablishID, userid).subscribe(() => {
      this.alertify.success('The model name - model no has been approved!');
      this.getAllBPFCStatus();
    });
  }

  done(BPFCEstablishID) {
    const userid = JSON.parse(localStorage.getItem('user')).User.ID;
    this.bPFCEstablishService.done(BPFCEstablishID, userid).subscribe(() => {
      this.alertify.success('The model name - model no has been finished!');
      this.getAllBPFCStatus();
    });
  }

  release() {
    const userid = JSON.parse(localStorage.getItem('user')).User.ID;
    this.scheduleService.release(this.ScheduleID, userid).subscribe(() => {
      this.alertify.success('The Schedule has been released!');
      this.getAllUsers();
      this.modalReferenceDetail.close();
    });
  }

  reject() {
    const userid = JSON.parse(localStorage.getItem('user')).User.ID;
    this.scheduleService.reject(this.ScheduleID, userid).subscribe((res: any) => {
      if (res.status === true) {
        this.alertify.success(res.message);
        // this.filterByNotApprovedStatus();
        this.getAllUsers();
        this.modalReferenceDetail.close();
        // const email = this.users.filter(item => item.ID === res.userId)[0].Email || '';

        // this.bPFCEstablishService.sendMailForPIC(email).subscribe(() => { });
      } else {
        this.alertify.error(res.message);
      }
    });
  }

  openModalDetail(detail, ScheduleID) {
    this.modalReferenceDetail = this.modalService.open(detail, { size: 'xxl' });
    setTimeout(() => {
      this.getAllGlueByScheduleID(ScheduleID);
    }, 300);
    this.ScheduleID = ScheduleID;
    this.getComments();
  }

  getAllGlueByScheduleID(scheduleID) {
    this.glueService.getGlueByScheduleID(scheduleID).subscribe((res: any) => {
      this.glues = res ;
      if (this.glues.length === 0) {
        this.glueid = 0;
        this.getInkChemicalByglueID(this.glueid);
      } else {
        this.glueid = this.glues[0].id;
        this.getInkChemicalByglueID(this.glueid);
      }
    })

  }

  getInkChemicalByglueID(glueid) {
    this.glueService.GetInkChemicalByGlueID(glueid).subscribe((res: any) => {
      this.ingredients = res;
    })
  }

  rowSelected(args: any) {
    const newGlueID = args.data.id;
    this.getInkChemicalByglueID(newGlueID);
  }

  toolbarClick(args: any): void {
    switch (args.item.text) {
      case 'Approved':
        this.filterByApprovedStatus();
        break;
      case 'Not Approved':
        this.filterByNotApprovedStatus();
        break;
      case 'All':
        this.getAllBPFCStatus();
        break;
      case 'Excel Export':
        this.gridModel.excelExport();
        break;
      case 'Default':
        this.filterByFinishedStatus();
        break;
    }
  }

  /// comment
  createComment() {
    this.comment = {
      id: 0,
      content: this.content,
      createdBy: JSON.parse(localStorage.getItem('user')).User.ID,
      createdByName: '',
      scheduleID: this.ScheduleID,
      createdDate: new Date()
    };
    if (this.content !== undefined) {
      this.commentService.create(this.comment).subscribe(() => {
        this.alertify.success('The comment has been created!');
        this.content = '';
        this.getComments();
      });
    } else {
      this.alertify.error('Comments can not be Empty !');
    }
  }

  updateComment() {
    this.commentService.update(this.comment).subscribe(() => {
      this.alertify.success('The comment has been updated!');
      this.getComments();
    });
  }

  deleteComment() {
    this.commentService.delete(this.comment.id).subscribe(() => {
      this.alertify.success('The comment has been deleted!');
      this.getComments();
    });
  }

  getComments() {
    this.commentService.getAllCommentByScheduleID(this.ScheduleID).subscribe((res: any) => {
      this.comments = res ;
    });
  }

  datetime(d) {
    return this.calendarsService.JSONDateWithTime(d);
  }

  username(id) {
    return (this.users.filter((item: any) => item.ID === id)[0] as any).Username;
  }

  createdBy(id) {
    if (id === 0) {
      return '#N/A';
    }
    const result = (this.users.filter((item: any) => item.ID === id)[0] as any);
    if (result !== undefined) {
      return result.Username;
    } else {
      return '#N/A';
    }
  }

}
