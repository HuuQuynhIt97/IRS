import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InkModalComponent } from './ink-modal.component';

describe('InkModalComponent', () => {
  let component: InkModalComponent;
  let fixture: ComponentFixture<InkModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InkModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InkModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
