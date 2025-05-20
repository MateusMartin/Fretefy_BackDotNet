import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegioesComponent } from './regioes.component';

describe('RegioesComponent', () => {
  let component: RegioesComponent;
  let fixture: ComponentFixture<RegioesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegioesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RegioesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
