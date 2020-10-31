import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ScheduleStatusComponent } from './schedule-status.component';

describe('ScheduleStatusComponent', () => {
  let component: ScheduleStatusComponent;
  let fixture: ComponentFixture<ScheduleStatusComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ScheduleStatusComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ScheduleStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
