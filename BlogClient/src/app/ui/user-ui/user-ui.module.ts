import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { GuideComponent } from './guide/guide.component';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    HomeComponent,
    GuideComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule
  ]
})
export class UserUiModule { }
