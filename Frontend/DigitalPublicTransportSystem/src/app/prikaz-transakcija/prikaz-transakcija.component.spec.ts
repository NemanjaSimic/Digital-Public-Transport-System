import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrikazTransakcijaComponent } from './prikaz-transakcija.component';

describe('PrikazTransakcijaComponent', () => {
  let component: PrikazTransakcijaComponent;
  let fixture: ComponentFixture<PrikazTransakcijaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrikazTransakcijaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrikazTransakcijaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
