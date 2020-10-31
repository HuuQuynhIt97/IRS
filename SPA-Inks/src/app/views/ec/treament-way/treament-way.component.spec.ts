import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TreamentWayComponent } from './treament-way.component';

describe('TreamentWayComponent', () => {
  let component: TreamentWayComponent;
  let fixture: ComponentFixture<TreamentWayComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TreamentWayComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TreamentWayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
