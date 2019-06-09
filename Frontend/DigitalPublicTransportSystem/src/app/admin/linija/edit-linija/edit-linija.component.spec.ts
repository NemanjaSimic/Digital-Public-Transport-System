import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditLinijaComponent } from './edit-linija.component';

describe('EditLinijaComponent', () => {
  let component: EditLinijaComponent;
  let fixture: ComponentFixture<EditLinijaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditLinijaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditLinijaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
