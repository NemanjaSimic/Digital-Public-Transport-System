import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ValidacijaDokumenataComponent } from './validacija-dokumenata.component';

describe('ValidacijaDokumenataComponent', () => {
  let component: ValidacijaDokumenataComponent;
  let fixture: ComponentFixture<ValidacijaDokumenataComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ValidacijaDokumenataComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ValidacijaDokumenataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
