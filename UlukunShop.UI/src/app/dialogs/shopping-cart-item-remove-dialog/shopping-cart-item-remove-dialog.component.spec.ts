import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShoppingCartItemRemoveDialogComponent } from './shopping-cart-item-remove-dialog.component';

describe('ShoppingCartItemRemoveDialogComponent', () => {
  let component: ShoppingCartItemRemoveDialogComponent;
  let fixture: ComponentFixture<ShoppingCartItemRemoveDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShoppingCartItemRemoveDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShoppingCartItemRemoveDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
