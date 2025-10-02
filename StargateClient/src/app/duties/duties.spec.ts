import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Duties } from './duties';

describe('Duties', () => {
  let component: Duties;
  let fixture: ComponentFixture<Duties>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [Duties]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Duties);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
