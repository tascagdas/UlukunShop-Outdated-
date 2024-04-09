import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderDetailDialogComponent } from './order-detail-dialog.component';

describe('OrderDetailDialogComponent', () => {
  let component: OrderDetailDialogComponent;
  let fixture: ComponentFixture<OrderDetailDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrderDetailDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrderDetailDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
