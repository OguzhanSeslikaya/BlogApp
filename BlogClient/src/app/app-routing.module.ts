import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LogInComponent } from './ui/account/log-in/log-in.component';
import { SignInComponent } from './ui/account/sign-in/sign-in.component';
import { HomeComponent } from './ui/user-ui/home/home.component';
import { authGuard } from './guards/auth.guard';
import { AdminComponent } from './ui/admin/admin.component';
import { CreatePageComponent } from './ui/admin/page/create-page/create-page.component';
import { CreateRoleComponent } from './ui/admin/role/create-role/create-role.component';
import { ListRolesComponent } from './ui/admin/role/list-roles/list-roles.component';
import { ListUsersComponent } from './ui/admin/user/list-users/list-users.component';
import { GuideComponent } from './ui/user-ui/guide/guide.component';

const routes: Routes = [
  {path:"",component:HomeComponent},
  {path:"home",component:HomeComponent},
  {path:"guide/:name",component:GuideComponent},
  {path:"admin",component:AdminComponent,canActivate:[authGuard],children:[
    {path:"page-actions",children:[
      {path:"create-page",component:CreatePageComponent}
    ]},
    {path:"role-actions",children:[
      {path:"create-role",component:CreateRoleComponent},
      {path:"list-roles",component:ListRolesComponent}
    ]},
    {path:"user-actions",children:[
      {path:"list-users",component:ListUsersComponent}
    ]}
  ]},
  {path:"log-in",component:LogInComponent},
  {path:"sign-in",component:SignInComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
