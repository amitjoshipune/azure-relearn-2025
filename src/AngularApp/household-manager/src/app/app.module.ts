// app.module.ts
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgxPaginationModule } from 'ngx-pagination';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { AuthInterceptor } from './interceptors/AuthInterceptor';
import { ProductAddComponent } from './pages/products/product-add/product-add.component';
import { ProductEditComponent } from './pages/products/product-edit/product-edit.component';
import { ProductListComponent } from './pages/products/product-list/product-list.component';
import { LoginComponent } from './components/login/login.component';
import { TodoListComponent } from './pages/todos/todo-list/todo-list.component';

@NgModule({
  declarations: [AppComponent,
    HomeComponent,
    ProductListComponent,
  ProductAddComponent,
  ProductEditComponent,
    LoginComponent,
    TodoListComponent
],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
    NgxPaginationModule
  ],
  providers: [
    {
      provide:HTTP_INTERCEPTORS, 
      useClass: AuthInterceptor, 
      multi:true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
