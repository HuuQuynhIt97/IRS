import { ChemicalModalComponent } from './chemical/chemical-modal/chemical-modal.component';
// Angular
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { NgxSpinnerModule } from 'ngx-spinner';
// Components Routing
import { ECRoutingModule } from './ec-routing.module';
import { NgSelectModule } from '@ng-select/ng-select';

import { GlueComponent } from './glue/glue.component';
import { IngredientComponent } from './ingredient/ingredient.component';
import { GlueModalComponent } from './glue/glue-modal/glue-modal.component';
import { IngredientModalComponent } from './ingredient/ingredient-modal/ingredient-modal.component';
import { DropDownListModule } from '@syncfusion/ej2-angular-dropdowns';
import { NgbModalModule, NgbModule } from '@ng-bootstrap/ng-bootstrap';
// Import ngx-barcode module
import { BarcodeGeneratorAllModule, DataMatrixGeneratorAllModule } from '@syncfusion/ej2-angular-barcode-generator';
import { ChartAllModule, AccumulationChartAllModule, RangeNavigatorAllModule } from '@syncfusion/ej2-angular-charts';
import { ChartsModule } from 'ng2-charts';
import { SwitchModule, RadioButtonModule } from '@syncfusion/ej2-angular-buttons';
import { GridAllModule } from '@syncfusion/ej2-angular-grids';
import { TreeGridAllModule } from '@syncfusion/ej2-angular-treegrid';
import { ButtonModule } from '@syncfusion/ej2-angular-buttons';

import { SuppilerComponent } from './suppiler/suppiler.component';
import { BuildingComponent } from './building/building.component';
import { BuildingUserComponent } from './building-user/building-user.component';
import { SummaryComponent } from './summary/summary.component';
import { DatePickerModule } from '@syncfusion/ej2-angular-calendars';
import { AccountComponent } from './account/account.component';
import { BuildingModalComponent } from './building/building-modal/building-modal.component';
import { QRCodeGeneratorAllModule } from '@syncfusion/ej2-angular-barcode-generator';
import { PrintQRCodeComponent } from './ingredient/print-qrcode/print-qrcode.component';
import { MaskedTextBoxModule } from '@syncfusion/ej2-angular-inputs';
import { HttpClient } from '@angular/common/http';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { ToolbarModule } from '@syncfusion/ej2-angular-navigations';
// AoT requires an exported function for factories
export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

import { AutofocusDirective } from './focus.directive';
import { AutoSelectDirective } from './select.directive';
import { SearchDirective } from './search.directive';
import { GlueHistoryComponent } from './summary/glue-history/glue-history.component';
import { SelectTextDirective } from './select.text.directive';
import { TooltipModule } from '@syncfusion/ej2-angular-popups';
import { TagInputModule } from 'ngx-chips';
import { TimePickerModule } from '@syncfusion/ej2-angular-calendars';
import { DateTimePickerModule } from '@syncfusion/ej2-angular-calendars';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { SelectQrCodeDirective } from './select.qrcode.directive';
import { ScheduleDetailComponent } from './schedule-detail/schedule-detail.component';
import { InkComponent } from './ink/ink.component';
import { InkModalComponent } from './ink/ink-modal/ink-modal.component';
import { ChemicalComponent } from './chemical/chemical.component';
import { ScheduleComponent } from './schedule/schedule.component';
import { CheckBoxModule } from '@syncfusion/ej2-angular-buttons';
import { NgxPrettyCheckboxModule } from 'ngx-pretty-checkbox';
import { ScheduleStatusComponent } from './schedule-status/schedule-status.component';
import { TreamentWayComponent } from './treament-way/treament-way.component';

const lang = localStorage.getItem('lang');
let defaultLang: any;
if (lang) {
  defaultLang = lang;
} else {
  defaultLang = 'vi';
}
@NgModule({
  imports: [
    ToolbarModule,
    TagInputModule,
    NgxPrettyCheckboxModule,
    ButtonModule,
    CheckBoxModule ,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NgxSpinnerModule,
    ECRoutingModule,
    NgSelectModule,
    DropDownListModule,
    NgbModule,
    ChartsModule,
    ChartAllModule,
    AccumulationChartAllModule,
    RangeNavigatorAllModule,
    BarcodeGeneratorAllModule,
    QRCodeGeneratorAllModule,
    DataMatrixGeneratorAllModule,
    SwitchModule,
    MaskedTextBoxModule,
    DatePickerModule,
    TreeGridAllModule,
    GridAllModule,
    RadioButtonModule,
    TooltipModule,
    TimePickerModule ,
    Ng2SearchPipeModule,
    DateTimePickerModule,
    TranslateModule.forChild({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      },
      defaultLanguage: defaultLang
    }),
  ],
  declarations: [
    GlueComponent,
    IngredientComponent,
    GlueModalComponent,
    IngredientModalComponent,
    SuppilerComponent,
    BuildingComponent,
    BuildingModalComponent,
    BuildingUserComponent,
    AccountComponent,
    SummaryComponent,
    PrintQRCodeComponent,
    AutofocusDirective,
    SelectTextDirective,
    AutoSelectDirective,
    SearchDirective,
    GlueHistoryComponent,
    SelectQrCodeDirective,
    ScheduleDetailComponent,
    InkComponent,
    InkModalComponent,
    ChemicalComponent,
    ChemicalModalComponent,
    ScheduleComponent,
    ScheduleStatusComponent,
    TreamentWayComponent
  ]
})
export class ECModule { }
