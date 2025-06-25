// app-routing.module.ts
import { NgModule } from '@angular/core';

import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './pages/home/home.component';
import { ProductListComponent } from './pages/products/product-list/product-list.component';
import { ProductAddComponent } from './pages/products/product-add/product-add.component';
import { ProductEditComponent } from './pages/products/product-edit/product-edit.component';
import { LoginComponent } from './components/login/login.component';
import { AuthGuard } from './auth/auth.guard';
import { TodoListComponent } from './pages/todos/todo-list/todo-list.component';

const routes: Routes = [
   { path: '', component: HomeComponent,canActivate:[AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'products', component: ProductListComponent, canActivate: [AuthGuard] },
   { path: 'products/add', component: ProductAddComponent, canActivate: [AuthGuard] },
  { path: 'products/edit/:id', component: ProductEditComponent, canActivate: [AuthGuard] },
  { path: 'todos', component: TodoListComponent, canActivate: [AuthGuard] },
  //{ path: 'todos/add', component: TodoAddComponent, canActivate: [AuthGuard] },
//{ path: 'todos/edit/:id', component: TodoEditComponent, canActivate: [AuthGuard] },
  { path: '**', redirectTo: 'login' }

  // Later (optional, lazy)
  // { path: 'auth', loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule) }
]; // empty routes = valid

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
