// dashboard-routing.module.ts
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from '../auth/auth.guard';

const routes: Routes = [
  {
    path: '', component: HomeComponent, canActivate: [AuthGuard], children: [
      { path: 'products', 
        loadChildren: () => 
          import('../products/products.module').then(m => m.ProductsModule) },
      { path: 'todos',
         loadChildren: () => 
          import('../todos/todos.module').then(m => m.TodosModule) },
      { path: 'stock', 
        loadChildren: () => 
          import('../stock/stock.module').then(m => m.StockModule) },
      { path: 'requisition', 
        loadChildren: () => 
          import('../requisition/requisition.module').then(m => m.RequisitionModule) }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
