import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShoppingcartsComponent } from './shoppingcarts.component';

describe('ShoppingcartsComponent', () => {
  let component: ShoppingcartsComponent;
  let fixture: ComponentFixture<ShoppingcartsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShoppingcartsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShoppingcartsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
