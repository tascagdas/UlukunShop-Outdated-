import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChooseusComponent } from './chooseus.component';

describe('ChooseusComponent', () => {
  let component: ChooseusComponent;
  let fixture: ComponentFixture<ChooseusComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChooseusComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChooseusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
