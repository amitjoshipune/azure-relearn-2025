// dashboard.module.ts
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { DashboardRoutingModule } from './dashboard-routing.module'; // ✅ THIS WAS MISSING

@NgModule({
  declarations: [HomeComponent],
  imports: [
    CommonModule,
    RouterModule, // Required for router-outlet
    DashboardRoutingModule // ✅ CRUCIAL IMPORT
  ]
})
export class DashboardModule { }
