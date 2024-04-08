import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShoppingCartCompleteDialogComponent } from './shopping-cart-complete-dialog.component';

describe('ShoppingCartCompleteDialogComponent', () => {
  let component: ShoppingCartCompleteDialogComponent;
  let fixture: ComponentFixture<ShoppingCartCompleteDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShoppingCartCompleteDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShoppingCartCompleteDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
