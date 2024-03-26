import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminComponent } from './admin.component';
import { RouterModule } from '@angular/router';
import { RoleModule } from './role/role.module';
import { UserModule } from './user/user.module';
import { PageModule } from './page/page.module';



@NgModule({
  declarations: [
    AdminComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    RoleModule,
    UserModule,
    PageModule
  ]
})
export class AdminModule { }
