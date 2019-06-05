import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CenovnikPrikazComponent } from './cenovnik-prikaz.component';

describe('CenovnikPrikazComponent', () => {
  let component: CenovnikPrikazComponent;
  let fixture: ComponentFixture<CenovnikPrikazComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CenovnikPrikazComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CenovnikPrikazComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
