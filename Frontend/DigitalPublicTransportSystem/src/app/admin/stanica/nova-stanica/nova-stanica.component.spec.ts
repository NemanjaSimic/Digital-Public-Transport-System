import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NovaStanicaComponent } from './nova-stanica.component';

describe('NovaStanicaComponent', () => {
  let component: NovaStanicaComponent;
  let fixture: ComponentFixture<NovaStanicaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NovaStanicaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NovaStanicaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
