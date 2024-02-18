import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuizzezComponent } from './quizzez.component';

describe('QuizzezComponent', () => {
  let component: QuizzezComponent;
  let fixture: ComponentFixture<QuizzezComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [QuizzezComponent]
    });
    fixture = TestBed.createComponent(QuizzezComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
