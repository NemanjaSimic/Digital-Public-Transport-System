import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardKontrolorComponent } from './dashboard-kontrolor.component';

describe('DashboardKontrolorComponent', () => {
  let component: DashboardKontrolorComponent;
  let fixture: ComponentFixture<DashboardKontrolorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DashboardKontrolorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DashboardKontrolorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
