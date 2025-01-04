import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserRawComponent } from './user-raw.component';

describe('UserRawComponent', () => {
  let component: UserRawComponent;
  let fixture: ComponentFixture<UserRawComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserRawComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserRawComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
