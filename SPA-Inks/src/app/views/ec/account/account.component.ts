import { environment } from "./../../../../environments/environment";
import { Component, OnInit, ViewChild } from "@angular/core";
import { AccountService } from "src/app/_core/_service/account.service";
import { AlertifyService } from "src/app/_core/_service/alertify.service";
import {
  EditService,
  ToolbarService,
  PageService,
  PageSettingsModel,
  ToolbarItems,
  GridComponent,
} from "@syncfusion/ej2-angular-grids";
import { DataService } from 'src/app/_core/_service/data.service';
import { L10n, loadCldr, setCulture, setCurrencyCode } from '@syncfusion/ej2-base';


@Component({
  selector: "app-account",
  templateUrl: "./account.component.html",
  styleUrls: ["./account.component.css"],
  providers: [ToolbarService, EditService, PageService],
})
export class AccountComponent implements OnInit {
  userData: object;
  buildings: [];
  roles: [];
  roleData: [];
  fieldsBuilding: object = { text: "name", value: "name" };
  fieldsRole: object = { text: "name", value: "name" };
  editSettings = {
    showDeleteConfirmDialog: false,
    allowEditing: true,
    allowAdding: true,
    allowDeleting: true,
    mode: "Normal",
  };
  buildingUsers: [];
  user: any;
  password = "";
  userID: number;
  buildingID: number;
  roleID: number = 0;
  locale = 'de-DE'
  public toolbarAccount : string[] ;
  passwordFake = `aRlG8BBHDYjrood3UqjzRl3FubHFI99nEPCahGtZl9jvkexwlJ`;
  pageSettings = { pageCount: 20, pageSizes: true, pageSize: 10 };
  @ViewChild("grid") public grid: GridComponent;

  constructor(
    private accountService: AccountService,
    private alertify: AlertifyService,
    private dataService: DataService,
  ) {

  }

  ngOnInit() {
    this.getAllUsers();

    setCulture('de-DE');
    L10n.load({
      'de-DE': {
        grid: {
          Add: 'Them123',
          Delete: 'Xoa'
        }
      },
    });
    this.toolbarAccount = [
      "Add",
      "Delete",
      "Cancel",
      "ExcelExport",
      "Search",
    ];
  }

  tooltips(data) {
    if (data) {
      return data;
    } else {
      return "";
    }
  }
  // life cycle ejs-grid
  createdUsers() {}

  actionBegin(args) {
    if (args.requestType === "save" && args.action === "add") {
      this.user = {
        id: 0,
        username: args.data.Username,
        password: this.password,
        email: args.data.Email,
        roleid: 2,
        employeeID: args.data.EmployeeID,
        isLeader: false,
        systemCode: 4,
      };
      if (this.roleID !== 0) {
        this.create();
      } else {
        this.alertify.error("Please Select Role !");
        args.cancel = true;
      }
    }
    if (args.requestType === "save" && args.action === "edit") {
      this.user = {
        id: args.data.ID,
        username: args.data.Username,
        password: this.password || "",
        email: args.data.Email || "",
        roleid: 2,
        systemCode: 4,
        employeeID: args.data.EmployeeID,
        isLeader: false,
      };
      // this.roleID = args.data.RoleID;
      this.mapRoleUser(this.userID, this.roleID);
      this.update();
      this.mapBuildingUser(this.userID, this.buildingID);
    }
    if (args.requestType === "delete") {
      this.delete(args.data[0].ID);
    }
  }

  toolbarClick(args) {
    console.log(args)
    switch (args.item.text) {
      case "Excel Export":
        this.grid.excelExport({ hierarchyExportMode: "All" });
        break;
      default:
        break;
    }
  }

  actionComplete(args) {
    if (args.requestType === "edit") {
      (args.form.elements.namedItem("ID") as HTMLInputElement).disabled = true;
      (args.form.elements.namedItem(
        "Password"
      ) as HTMLInputElement).disabled = true;
    }
    if (args.requestType === "add") {
      (args.form.elements.namedItem("ID") as HTMLInputElement).disabled = true;
    }
  }

  dataBound() {
    // document.querySelectorAll(
    //   "button[aria-label=Update] > span.e-tbar-btn-text"
    // )[0].innerHTML = "Save";
  }

  // end life cycle ejs-grid

  // api
  onChangeBuilding(args, data) {
    this.userID = data.ID;
    this.buildingID = args.itemData.id;
  }

  onChangeRole(args, data) {
    this.userID = data.ID;
    this.roleID = args.itemData.id;
  }

  getBuildings() {
    return new Promise((res, rej) => {
      this.accountService.getBuildings().subscribe(
        (result: any) => {
          this.buildings = result || [];
          res(result);
        },
        (error) => {
          rej(error);
        }
      );
    });
  }

  getRoleUser() {
    return new Promise((res, rej) => {
      this.accountService.getAllRoleUser().subscribe(
        (result: any) => {
          this.roles = result || [];
          res(result);
        },
        (error) => {
          rej(error);
        }
      );
    });
  }

  getRoles() {
    return new Promise((res, rej) => {
      this.accountService.getAllRole().subscribe(
        (result: any) => {
          this.roleData = result || [];
          res(result);
        },
        (error) => {
          rej(error);
        }
      );
    });
  }

  async getAllUsers() {
    try {
      await this.getBuildings();
      await this.getRoleUser();
      await this.getRoles();
      await this.getBuildingUsers();
      this.accountService.getAllUsers(1, 10000).subscribe((res) => {
        const users = res.result.map((item: any) => {
          return {
            ID: item.ID,
            Username: item.Username,
            Password: this.passwordFake + item.ID,
            Email: item.Email,
            EmployeeID: item.EmployeeID,
            Status: this.StatusTemplate(item.ID),
            RoleID: this.RoleIDTempate(item.ID),
            RoleName: this.RoleTempate(item.ID),
            BuildingName: this.buildingTempate(item.ID),
          };
        });
        this.userData = users;
      });
    } catch (error) {
      this.alertify.error(error + "");
    }
  }

  getBuildingUsers() {
    return new Promise((res, rej) => {
      this.accountService.getBuildingUsers().subscribe(
        (result) => {
          this.buildingUsers = result as any;
          res(result);
        },
        (err) => {
          rej(err);
        }
      );
    });
  }

  mapBuildingUser(userid, buildingid) {
    if (userid !== undefined && buildingid !== undefined) {
      this.accountService
        .mapBuildingUser(userid, buildingid)
        .subscribe((res: any) => {
          if (res.status) {
            this.alertify.success(res.message);
            this.getBuildingUsers();
            this.getAllUsers();
          } else {
            this.alertify.success(res.message);
          }
        });
    }
  }

  mapRoleUser(userid, roleid) {
    if (userid !== undefined && roleid !== undefined) {
      this.accountService.mapRoleUser(userid, roleid).subscribe((res: any) => {
        if (res.status) {
          //  this.alertify.success(res.message);
          //  this.getBuildingUsers();
          this.getAllUsers();
        } else {
          this.alertify.success(res.message);
        }
      });
    }
  }

  delete(id) {
    this.accountService.deleteUser(id).subscribe((res) => {
      this.alertify.success("The user has been deleted!");
      this.getAllUsers();
    });
  }

  create() {
    this.accountService.createUser(this.user).subscribe((res: number) => {
      this.alertify.success("The user has been created!");
      if (res > 0) {
        this.mapRoleUser(res, this.roleID);
        this.mapBuildingUser(res, this.buildingID);
        this.getAllUsers();
        this.password = "";
      }
    });
  }

  update() {
    this.accountService.updateUser(this.user).subscribe((res) => {
      this.alertify.success("The user has been updated!");
      this.getAllUsers();
      this.password = "";
    });
  }
  // end api

  // template ejs-grid
  buildingTempate(userid): string {
    const buildingUser = this.buildingUsers.filter(
      (item: any) => item.userID === userid
    ) as any[];
    if (buildingUser.length === 0) {
      return "#N/A";
    }
    const buildingID = buildingUser[0].buildingID || 0;
    const building = this.buildings.filter(
      (item: any) => item.id === buildingID
    ) as any[];
    if (building.length === 0) {
      return "#N/A";
    }
    const buildingName = building[0].name;
    return buildingName || "#N/A";
  }

  RoleTempate(userid): string {
    const roleUser = this.roles.filter(
      (item: any) => item.userID === userid
    ) as any[];
    if (roleUser.length === 0) {
      return "#N/A";
    }
    const roleID = roleUser[0].roleID || 0;
    const roel = this.roleData.filter(
      (item: any) => item.id === roleID
    ) as any[];
    if (roel.length === 0) {
      return "#N/A";
    }

    const roleName = roel[0].name;
    return roleName || "#N/A";
  }

  RoleIDTempate(userid): string {
    const roleUser = this.roles.filter(
      (item: any) => item.userID === userid
    ) as any[];
    if (roleUser.length === 0) {
      return "#N/A";
    }
    const roleID = roleUser[0].roleID || 0;
    const roleName = roleID;
    return roleName || "#N/A";
  }

  StatusTemplate(userid): string {
    const roleUser = this.roles.filter(
      (item: any) => item.userID === userid
    ) as any[];
    if (roleUser.length === 0) {
      return "#N/A";
    }
    const stt = roleUser[0].status || 0;

    const Status = stt;
    return Status || "#N/A";
  }

  // end template ejs-grid
  NO(index) {
    return (
      (this.grid.pageSettings.currentPage - 1) * this.pageSettings.pageSize +
      Number(index) +
      1
    );
  }

  blockAccount(data) {
    this.accountService.blockAccount(data.ID).subscribe((res: any) => {
      if (res.status) {
        this.alertify.success(res.message);
        this.getAllUsers();
      } else {
        this.alertify.success(res.message);
      }
    });
  }

}
