// app-routing.module.ts
import { NgModule } from '@angular/core';

import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './pages/home/home.component';
import { ProductListComponent } from './pages/products/product-list/product-list.component';
import { ProductAddComponent } from './pages/products/product-add/product-add.component';
import { ProductEditComponent } from './pages/products/product-edit/product-edit.component';
import { LoginComponent } from './components/login/login.component';

const routes: Routes = [
  { path: '', component:HomeComponent},
    { path: 'products', component: ProductListComponent },
  { path: 'products/add', component: ProductAddComponent },
  { path: 'products/edit/:id', component: ProductEditComponent },
   { path: 'login', component: LoginComponent }

  // Later (optional, lazy)
  // { path: 'auth', loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule) }
]; // empty routes = valid

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
