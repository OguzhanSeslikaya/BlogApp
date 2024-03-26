import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateRoleComponent } from './create-role/create-role.component';
import { ListRolesComponent } from './list-roles/list-roles.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button'
import { AssignPermissionDialogComponent } from './dialogs/assign-permission-dialog/assign-permission-dialog.component'
import { DeleteRoleDirective } from 'src/app/directives/delete-role.directive';


@NgModule({
  declarations: [
    CreateRoleComponent,
    ListRolesComponent,
    AssignPermissionDialogComponent,
    DeleteRoleDirective
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatButtonModule
    
  ]
})
export class RoleModule { }
