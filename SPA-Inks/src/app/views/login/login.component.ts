import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../_core/_service/auth.service';
import { AlertifyService } from '../../_core/_service/alertify.service';
import { Router, ActivatedRoute, RouterStateSnapshot } from '@angular/router';
const ADMIN = 1;
const SUPERVISOR = 2;
const STAFF = 3;
const WORKER = 5;
const SYSTEM_CODE = 4;
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  user: any = {};
  uri: any;
  level: number;

  routerLinkAdmin = [
    '/setting/account-1',
    '/setting/account-2',
    '/setting/building',
    '/setting/supplier',
    '/setting/ingredient',
    '/setting/ink',
    '/setting/chemical',
    '/setting/material',
    '/establish/schedule',
    '/manage/bpfc-status',
    '/manage/bpfc-schedule',
    '/manage/workplan',
    '/manage/establish-record',

    '/execution/todolist',
    '/execution/addition',
    '/report/consumption',
  ];
  routerLinkSupervisor = [
    '/setting/account-1',
    '/setting/account-2',
    '/setting/building',
    '/setting/supplier',
    '/setting/ingredient',
    '/setting/ink',
    '/setting/chemical',
    '/setting/material',
    //
    '/establish/schedule',
    //
    '/manage/bpfc-status',
    '/manage/bpfc-schedule',
    '/manage/workplan',
    '/manage/establish-record',
    //
    '/report/consumption',

    '/execution/todolist',
    '/execution/addition',
  ];
  routerLinkStaff = [
    '/setting/building',
    '/setting/supplier',
    '/setting/ingredient',
    '/setting/ink',
    '/setting/chemical',
    '/setting/account-1',
    '/establish/schedule',
    //
    '/manage/bpfc-status',
    '/manage/bpfc-schedule',
    '/manage/workplan',
    '/manage/establish-record',
    //
    '/report/consumption',

    '/execution/todolist',
    '/execution/addition',
  ];
  routerLinkWorker = [
    '/ink/establish/schedule',
    '/execution/addition',
  ];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private alertifyService: AlertifyService
  ) {
    this.route.queryParams.subscribe(params => {
      this.uri = params.uri;
    });
  }
  role: number;
  ngOnInit(): void {
  }
  login(): void {
    this.authService.login(this.user).subscribe((next: any) => {
      this.authService.CheckBlockUser(JSON.parse(localStorage.getItem('user')).User.ID).subscribe((res: any) => {
        if(!JSON.parse(localStorage.getItem('user')).User.Systems.includes(SYSTEM_CODE)) {
          return this.alertifyService.warning('Your account does not exist in this system!');
        }
        else if (res === false) {
          return this.alertifyService.warning('Your account has been Blocked! Please contact Admin to unlock Account!');
        }
        else {
          this.role = JSON.parse(localStorage.getItem('user')).User.Role;
          this.alertifyService.success('Login Success!!');
          this.authService.getRoleByUserID(JSON.parse(localStorage.getItem('user')).User.ID).subscribe((res: any) => {
            res = res || {};
            localStorage.setItem('role', JSON.stringify(res));
            this.level = res.id;
            if (this.level === WORKER) {
              const currentLang = localStorage.getItem('lang');
              if (currentLang) {
                localStorage.setItem('lang', currentLang);
              } else {
                localStorage.setItem('lang', 'vi');
              }
            } else {
              const currentLang = localStorage.getItem('lang');
              if (currentLang) {
                localStorage.setItem('lang', currentLang);
              } else {
                localStorage.setItem('lang', 'vi');
              }
            }
            this.checkRole();
          });
          }
        });
      },
      error => {
        this.alertifyService.error('Login failed!!');
      },
      () => {
      }
    );
  }
  checkRouteAdmin(uri) {
    let flag = false;
    this.routerLinkAdmin.forEach(element => {
      if (uri.includes(element)) {
        flag = true;
      }
    });
    return flag;
  }
  CheckLogin() {
    this.authService.CheckBlockUser(JSON.parse(localStorage.getItem('user')).User.ID).subscribe((res: any) => {
      if (res === false) {
        return this.alertifyService.warning('Account has Bock! Please contact Admin to able Account!');
      }
    });
  }
  checkRouteSupervisor(uri) {
    let flag = false;
    this.routerLinkSupervisor.forEach(element => {
      if (uri.includes(element)) {
        flag = true;
      }
    });
    return flag;
  }
  checkRouteStaff(uri) {
    let flag = false;
    this.routerLinkStaff.forEach(element => {
      if (uri.includes(element)) {
        flag = true;
      }
    });
    return flag;
  }
  checkRouteWorker(uri) {
    let flag = false;
    this.routerLinkWorker.forEach(element => {
      if (uri.includes(element)) {
        flag = true;
      }
    });
    return flag;
  }
  checkRole() {
    const uri = decodeURI(this.uri);
    if (this.level === ADMIN) {
      if (uri !== 'undefined') {
        if (this.checkRouteAdmin(uri)) {
          this.router.navigate([uri]);
        } else {
          this.router.navigate(['/ink/establish/schedule']);
        }
      } else {
        this.router.navigate(['/ink/establish/schedule']);
      }
    } else if (this.level === SUPERVISOR) {
      if (uri !== 'undefined') {
        if (this.checkRouteSupervisor(uri)) {
          this.router.navigate([uri]);
        } else {
          this.router.navigate(['/ink/establish/schedule']);
        }
      } else {
        this.router.navigate(['/ink/establish/schedule']);
      }
    } else if (this.level === STAFF) {
      if (uri !== 'undefined') {
        if (this.checkRouteStaff(uri)) {
          this.router.navigate([uri]);
        } else {
          this.router.navigate(['/ink/establish/schedule']);
        }
      } else {
        this.router.navigate(['/ink/establish/schedule']);
      }
    } else if (this.level === WORKER) {
      if (uri !== 'undefined') {
        if (this.checkRouteWorker(uri)) {
          this.router.navigate([uri]);
        } else {
          this.router.navigate(['/ink/establish/schedule']);
        }
      } else {
        this.router.navigate(['/ink/establish/schedule']);
      }
    }
  }
}
