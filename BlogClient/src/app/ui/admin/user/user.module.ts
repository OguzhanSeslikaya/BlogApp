import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListUsersComponent } from './list-users/list-users.component';
import { AssignRoleDialogComponent } from './dialog/assign-role-dialog/assign-role-dialog.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { DeleteUserDirective } from 'src/app/directives/delete-user.directive';




@NgModule({
  declarations: [
    ListUsersComponent,
    AssignRoleDialogComponent,
    DeleteUserDirective
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatButtonModule
  ]
})
export class UserModule { }
