import { ChemicalService } from './../../../../_core/_service/chemical.service';
import { IChemical } from './../../../../_core/_model/Chemical';
import { InkService } from './../../../../_core/_service/ink.service';
import { IInk } from './../../../../_core/_model/Ink';
import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { IIngredient } from 'src/app/_core/_model/Ingredient';
import { AlertifyService } from 'src/app/_core/_service/alertify.service';
import { IngredientService } from 'src/app/_core/_service/ingredient.service';
import { GridComponent } from '@syncfusion/ej2-angular-grids';

@Component({
  selector: 'app-chemical-modal',
  templateUrl: './chemical-modal.component.html',
  styleUrls: ['./chemical-modal.component.css']
})
export class ChemicalModalComponent implements OnInit {

  @Input() title: '';
  @Input() chemical: IChemical = {
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
    modify: false,
    unit: 0
  };
  supplier: any [] = [];
  public ProcessData = [
    {
      id: 1,
      name: 'Spray',
    },
    {
      id: 2,
      name: 'Print',
    },
  ];
  public ModifyData = [
    {
      id: true,
      name: 'Yes',
    },
    {
      id: false,
      name: 'No',
    },
  ];
  public fieldsProcess: object = { text: 'name', value: 'id' };
  public fieldsGlue: object = { text: 'name', value: 'id' };
  public fieldsModify: object = { text: 'name', value: 'id' };
  public textGlue = 'Select Supplier name';
  public textProcess = 'Select Process';
  public textModify = 'Select Modify';
  showBarCode: boolean;
  constructor(
    public activeModal: NgbActiveModal,
    private alertify: AlertifyService,
    private ingredientService: IngredientService,
    private InkService: InkService,
    private chemicalService: ChemicalService,
  ) { }

  ngOnInit() {
    // if (this.chemical.id === 0) {
    //   this.showBarCode = false;
    //   this.genaratorIngredientCode();
    // } else {
    //   this.showBarCode = true;
    // }
    // this.getSupllier();
    this.chemical.createdBy = JSON.parse(localStorage.getItem('user')).User.ID as number ;
    this.getSupllier(this.chemical.processID);
  }
  create() {
    this.chemicalService.create(this.chemical).subscribe( () => {
      this.alertify.success('Created successed!');
      this.activeModal.dismiss();
      this.chemicalService.changeChemical(300);
    },
    (error) => {
      this.alertify.error(error);
      // this.genaratorIngredientCode();
    });
  }

  update() {
    this.chemicalService.update(this.chemical).subscribe( res => {
      this.alertify.success('Updated successed!');
      this.activeModal.dismiss();
      this.chemicalService.changeChemical(300);
    });
  }

  onChangeSup(args) {
    this.chemical.supplierID = args.value;
  }

  onChangeProcess(args) {
    this.chemical.processID = args.value;
    this.getSupllier(args.value);
  }

  onChangeModify(args) {
    this.chemical.modify = args.value;
  }

  save() {
    if (this.chemical.id === 0) {
      if (this.chemical.code === '') {
        this.genaratorIngredientCode();
      }
      this.create();
    } else {
      this.update();
    }
  }

  onBlur($event) {
    this.showBarCode = true;
  }

  getSupllier(id) {
    this.ingredientService.getAllSupplierByTreatment(id).subscribe(res => {
      this.supplier = res ;
    });
  }

  genaratorIngredientCode() {
    this.chemical.code = this.makeid(8);
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
