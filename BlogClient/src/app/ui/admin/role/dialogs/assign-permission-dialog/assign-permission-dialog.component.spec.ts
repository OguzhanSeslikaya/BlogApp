import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignPermissionDialogComponent } from './assign-permission-dialog.component';

describe('AssignPermissionDialogComponent', () => {
  let component: AssignPermissionDialogComponent;
  let fixture: ComponentFixture<AssignPermissionDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AssignPermissionDialogComponent]
    });
    fixture = TestBed.createComponent(AssignPermissionDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
