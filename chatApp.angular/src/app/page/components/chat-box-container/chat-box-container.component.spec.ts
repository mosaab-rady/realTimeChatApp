import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatBoxContainerComponent } from './chat-box-container.component';

describe('ChatBoxContainerComponent', () => {
  let component: ChatBoxContainerComponent;
  let fixture: ComponentFixture<ChatBoxContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChatBoxContainerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChatBoxContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
