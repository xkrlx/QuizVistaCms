import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResetPasswordInitComponent } from './reset-password-init.component';

describe('ResetPasswordInitComponent', () => {
  let component: ResetPasswordInitComponent;
  let fixture: ComponentFixture<ResetPasswordInitComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ResetPasswordInitComponent]
    });
    fixture = TestBed.createComponent(ResetPasswordInitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
