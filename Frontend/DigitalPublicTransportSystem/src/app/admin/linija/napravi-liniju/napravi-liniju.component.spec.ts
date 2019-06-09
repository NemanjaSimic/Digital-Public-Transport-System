import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NapraviLinijuComponent } from './napravi-liniju.component';

describe('NapraviLinijuComponent', () => {
  let component: NapraviLinijuComponent;
  let fixture: ComponentFixture<NapraviLinijuComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NapraviLinijuComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NapraviLinijuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
