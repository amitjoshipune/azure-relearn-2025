import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; 

import { RequisitionRoutingModule } from './requisition-routing.module';
import { ProcessComponent } from './process/process.component';


@NgModule({
  declarations: [ProcessComponent],
  imports: [
    CommonModule,
    FormsModule,
    RequisitionRoutingModule
  ]
})
export class RequisitionModule { }
