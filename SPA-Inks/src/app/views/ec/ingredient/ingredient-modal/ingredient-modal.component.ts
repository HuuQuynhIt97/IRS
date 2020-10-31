import { InkService } from './../../../../_core/_service/ink.service';
import { IInk } from './../../../../_core/_model/Ink';
import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { IIngredient } from 'src/app/_core/_model/Ingredient';
import { AlertifyService } from 'src/app/_core/_service/alertify.service';
import { IngredientService } from 'src/app/_core/_service/ingredient.service';

@Component({
  selector: 'app-ingredient-modal',
  templateUrl: './ingredient-modal.component.html',
  styleUrls: ['./ingredient-modal.component.scss']
})
export class IngredientModalComponent implements OnInit {
  @Input() title: '';
  @Input() ink: IInk = {
    id: 0,
    name: '',
    code: this.makeid(8),
    createdDate: new Date(),
    supplierID: 0,
    allow: 0,
    voc: 0,
    processID: 0,
    expiredTime: 0,
    daysToExpiration: 0,
    materialNO: '',
    unit: 0,
    createdBy: 0
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
  public fieldsGlue: object = { text: 'name', value: 'id' };
  public textGlue = 'Select Supplier name';
  public textProcess = 'Select Process';
  showBarCode: boolean;
  constructor(
    public activeModal: NgbActiveModal,
    private alertify: AlertifyService,
    private ingredientService: IngredientService,
    private InkService: InkService,
  ) { }

  ngOnInit() {
    if (this.ink.id === 0) {
      this.showBarCode = false;
      this.genaratorIngredientCode();
    } else {
      this.showBarCode = true;
    }
    this.getSupllier();
  }
  create() {
    console.log('create',this.ink);
    this.InkService.create(this.ink).subscribe( () => {
      this.alertify.success('Created successed!');
      this.activeModal.dismiss();
      this.InkService.changeInk(300);
    },
    (error) => {
      this.alertify.error(error);
      this.genaratorIngredientCode();
    });
  }

  update() {
    // this.ingredientService.update(this.ink).subscribe( res => {
    //   this.alertify.success('Updated successed!');
    //   this.activeModal.dismiss();
    //   this.ingredientService.changeIngredient(300);
    // });
  }

  onChangeSup(args) {
    this.ink.supplierID = args.value;
  }

  onChangeProcess(args) {
    this.ink.processID = args.value;
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

  getSupllier() {
    this.ingredientService.getAllSupplier().subscribe(res => {
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
