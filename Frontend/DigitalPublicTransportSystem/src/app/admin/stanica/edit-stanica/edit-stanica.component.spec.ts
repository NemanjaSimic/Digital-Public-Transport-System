import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditStanicaComponent } from './edit-stanica.component';

describe('EditStanicaComponent', () => {
  let component: EditStanicaComponent;
  let fixture: ComponentFixture<EditStanicaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditStanicaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditStanicaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
