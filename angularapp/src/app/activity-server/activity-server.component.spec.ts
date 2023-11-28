import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActivityServerComponent } from './activity-server.component';

describe('ActivityServerComponent', () => {
  let component: ActivityServerComponent;
  let fixture: ComponentFixture<ActivityServerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ActivityServerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ActivityServerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
