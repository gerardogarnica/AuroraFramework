import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModuleCardDetailComponent } from './module-card-detail.component';

describe('ModuleCardDetailComponent', () => {
  let component: ModuleCardDetailComponent;
  let fixture: ComponentFixture<ModuleCardDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModuleCardDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ModuleCardDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
