import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductsRoutingModule } from './products-routing.module';
import { FormsModule } from '@angular/forms';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { ProductListComponent } from './product-list/product-list.component';


@NgModule({
  declarations: [ProductListComponent,ProductDetailComponent],
  imports: [
    CommonModule,
    ProductsRoutingModule,
    FormsModule
    //NgModule
  ]
})
export class ProductsModule { }
