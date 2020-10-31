import { InkService } from './../../../../_core/_service/ink.service';
import { IInk } from './../../../../_core/_model/Ink';
import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { IIngredient } from 'src/app/_core/_model/Ingredient';
import { AlertifyService } from 'src/app/_core/_service/alertify.service';
import { IngredientService } from 'src/app/_core/_service/ingredient.service';
import { GridComponent } from '@syncfusion/ej2-angular-grids';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-ink-modal',
  templateUrl: './ink-modal.component.html',
  styleUrls: ['./ink-modal.component.css']
})
export class InkModalComponent implements OnInit {
  inkForm: FormGroup;
  @Input() title: '';
  @Input() ink: IInk = {
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

  public fieldsProcess: object = { text: 'name', value: 'id' };
  @ViewChild('chemicalGrid') chemicalGrid: GridComponent;
  public fieldsGlue: object = { text: 'name', value: 'id' };
  public textGlue = 'Select Supplier name';
  public textProcess = 'Select Process';
  showBarCode: boolean;
  constructor(
    public activeModal: NgbActiveModal,
    private alertify: AlertifyService,
    private ingredientService: IngredientService,
    private inkService: InkService,
    private formBuilder: FormBuilder,
  ) {
    this.inkForm = this.formBuilder.group({
      name : [null, Validators.required],
      code : [null],
      supplierID : [null, Validators.required],
      allow : [0],
      voc : [0],
      materialNO : [null, Validators.required],
      expiredTime : [0],
      processID : [null, Validators.required],
      createdBy : [0],
      daysToExpiration : [0],
      unit : [0],
      createdDate : [new Date()],
    })
  }

  ngOnInit() {

    this.ink.createdBy = JSON.parse(localStorage.getItem('user')).User.ID as number ;
    setTimeout(() => {
      this.getSupllier(this.ink.processID);
    }, 300);
  }
  create() {
    console.log(this.inkForm.value)
    this.inkService.create(this.ink).subscribe( () => {
      this.alertify.success('Created successed!');
      this.activeModal.dismiss();
      this.inkService.changeInk(300);
    },
    (error) => {
      this.alertify.error(error);
    });
  }

  update() {
    this.inkService.update(this.ink).subscribe( res => {
      this.alertify.success('Updated successed!');
      this.activeModal.dismiss();
      this.inkService.changeInk(300);
    });
  }

  onChangeSup(args) {
    this.ink.supplierID = args.value;
  }

  onChangeProcess(args) {
    this.ink.processID = args.value;
    this.getSupllier(args.value);
  }

  save() {
    if (this.ink.id === 0) {
      if (this.ink.code === '') {
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
    this.ink.code = this.makeid(8);
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
