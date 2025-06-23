import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockEventsComponent } from './stock-events.component';

describe('StockEventsComponent', () => {
  let component: StockEventsComponent;
  let fixture: ComponentFixture<StockEventsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StockEventsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(StockEventsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
