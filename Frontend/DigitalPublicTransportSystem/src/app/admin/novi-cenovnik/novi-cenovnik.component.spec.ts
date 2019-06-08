import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NoviCenovnikComponent } from './novi-cenovnik.component';

describe('NoviCenovnikComponent', () => {
  let component: NoviCenovnikComponent;
  let fixture: ComponentFixture<NoviCenovnikComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NoviCenovnikComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NoviCenovnikComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
