import { TreamentWayComponent } from './treament-way/treament-way.component';
import { ScheduleStatusComponent } from './schedule-status/schedule-status.component';
import { ScheduleComponent } from './schedule/schedule.component';
import { ChemicalComponent } from './chemical/chemical.component';
import { InkComponent } from './ink/ink.component';
import { ScheduleDetailComponent } from './schedule-detail/schedule-detail.component';
import { SuppilerComponent } from './suppiler/suppiler.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GlueComponent } from './glue/glue.component';
import { IngredientComponent } from './ingredient/ingredient.component';
import { GlueModalComponent } from './glue/glue-modal/glue-modal.component';
import { IngredientModalComponent } from './ingredient/ingredient-modal/ingredient-modal.component';
import { GlueResolver } from '../../_core/_resolvers/glue.resolver';
import { IngredientResolver } from '../../_core/_resolvers/ingredient.resolver';
import { BuildingComponent } from './building/building.component';
import { BuildingUserComponent } from './building-user/building-user.component';
import { SummaryComponent } from './summary/summary.component';
import { AccountComponent } from './account/account.component';
import { PrintQRCodeComponent } from './ingredient/print-qrcode/print-qrcode.component';
import { GlueHistoryComponent } from './summary/glue-history/glue-history.component';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'ec',
      breadcrumb: 'Home'
    },
    children: [

      // setting
      {
        path: 'setting',
        data: {
          title: 'setting',
          breadcrumb: 'Setting'
        },
        children: [
          {
            path: 'ink',
            // component: IngredientComponent,
            data: {
              title: 'Ink',
              breadcrumb: 'Ink',
              url: 'ink/setting/ink'
            },
            children: [
              {
                path: '',
                component: InkComponent,
              },
              {
                path: 'print-qrcode/:id/:code/:name',
                component: PrintQRCodeComponent,
                data: {
                  title: 'Print QRCode',
                  breadcrumb: 'Print QRCode'
                }
              }
            ]
          },
          {
            path: 'treatment-way',
            component: TreamentWayComponent,
            data: {
              title: 'Treatment Way',
              breadcrumb: 'Treatment Way'
            }
          },
          {
            path: 'account-1',
            component: AccountComponent,
            data: {
              title: 'account',
              breadcrumb: 'Account'
            }
          },
          {
            path: 'account-2',
            component: BuildingUserComponent,
            data: {
              title: 'Account 2',
              breadcrumb: 'Account 2'
            }
          },
          {
            path: 'building',
            component: BuildingComponent,
            data: {
              title: 'Building',
              breadcrumb: 'Building'
            }
          },
          {
            path: 'supplier',
            component: SuppilerComponent,
            data: {
              title: 'Suppiler',
              breadcrumb: 'Suppiler'
            }
          },
          {
            path: 'chemical',
            component: ChemicalComponent,
            data: {
              title: 'Chemical',
              breadcrumb: 'Chemical'
            }
          },
          {
            path: 'glue',
            component: GlueComponent,
            resolve: { glues: GlueResolver },
            data: {
              title: 'Glue',
              breadcrumb: 'Glue'
            }
          },
        ]
      },
       // end setting

      // establish
      {
        path: 'establish',
        data: {
          title: 'Establish',
          breadcrumb: 'Establish'
        },
        children: [
          {
            path: 'schedule',
            data: {
              title: 'Schedule',
              breadcrumb: 'Schedule'
            },
            children: [
              {
                path: '',
                component: ScheduleComponent
              },
              {
                path: 'detail/:id',
                component: ScheduleDetailComponent,
                data: {
                  title: 'Detail',
                  breadcrumb: 'Detail'
                },
              }
            ]
          },
          {
            path: 'schedule-status',
            data: {
              title: 'Schedule Status',
              breadcrumb: 'Schedule Status'
            },
            component: ScheduleStatusComponent
          }

        ]
      },
      // end establish

      // manage
      {
        path: 'manage',
        data: {
          title: 'Manage',
          breadcrumb: 'Manage'
        },
        children: [
          {
            path: 'workplan',
            data: {
              title: 'Workplan',
              breadcrumb: 'Work Plan'
            }
          },
        ]
      },
      // end manage

      // execution
      {
        path: 'execution',
        data: {
          title: 'Execution',
          breadcrumb: 'Execution'
        },
        children: [

          {
            path: 'todolist',
            // component: SummaryComponent,
            data: {
              title: 'todolist',
              breadcrumb: 'Todolist'
            },
            children: [
              {
                path: '',
                component: SummaryComponent,
              },

              {
                path: 'history/:glueID',
                component: GlueHistoryComponent,
                data: {
                  title: 'History',
                  breadcrumb: 'History'
                }
              },
            ]
          },

        ]
      },
      // end execution

       // report
      {
        path: 'report',
        data: {
          title: 'Report',
          breadcrumb: 'Report'
        },
        children: [
          {
            path: 'comsumption',
            component: SummaryComponent,
            data: {
              title: 'Comsumption',
              breadcrumb: 'Comsumption'
            }
          },
        ]
      },
      // end report

    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ECRoutingModule { }
