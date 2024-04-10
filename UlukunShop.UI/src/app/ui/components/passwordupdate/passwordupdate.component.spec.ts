import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PasswordupdateComponent } from './passwordupdate.component';

describe('PasswordupdateComponent', () => {
  let component: PasswordupdateComponent;
  let fixture: ComponentFixture<PasswordupdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PasswordupdateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PasswordupdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
