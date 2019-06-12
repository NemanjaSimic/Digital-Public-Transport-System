import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ValidacijaKartaComponent } from './validacija-karta.component';

describe('ValidacijaKartaComponent', () => {
  let component: ValidacijaKartaComponent;
  let fixture: ComponentFixture<ValidacijaKartaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ValidacijaKartaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ValidacijaKartaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
