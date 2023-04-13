import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateBlogentryComponent } from './create-blogentry.component';

describe('CreateBlogentryComponent', () => {
  let component: CreateBlogentryComponent;
  let fixture: ComponentFixture<CreateBlogentryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateBlogentryComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateBlogentryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
