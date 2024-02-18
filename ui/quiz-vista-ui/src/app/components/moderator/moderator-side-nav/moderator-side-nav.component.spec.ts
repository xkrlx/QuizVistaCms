import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModeratorSideNavComponent } from './moderator-side-nav.component';

describe('ModeratorSideNavComponent', () => {
  let component: ModeratorSideNavComponent;
  let fixture: ComponentFixture<ModeratorSideNavComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ModeratorSideNavComponent]
    });
    fixture = TestBed.createComponent(ModeratorSideNavComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
