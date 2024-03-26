import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreatePageComponent } from './create-page/create-page.component';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [CreatePageComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule
  ]
})
export class PageModule { }
