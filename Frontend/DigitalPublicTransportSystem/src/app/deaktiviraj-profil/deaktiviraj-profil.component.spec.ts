import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeaktivirajProfilComponent } from './deaktiviraj-profil.component';

describe('DeaktivirajProfilComponent', () => {
  let component: DeaktivirajProfilComponent;
  let fixture: ComponentFixture<DeaktivirajProfilComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeaktivirajProfilComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeaktivirajProfilComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
