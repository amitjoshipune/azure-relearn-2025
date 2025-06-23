import { Component, OnInit } from '@angular/core';
import { ProductService } from '../product.service';
import { Product } from '../product.service';

@Component({
  selector: 'app-product-list',
  template: `
  <h2>Products</h2>
  <button (click)="new()">New</button>
  <ul>
    <li *ngFor="let p of products">
      {{p.name}} 
      <button (click)="edit(p.id)">Edit</button>
      <button (click)="del(p.id)">Del</button>
    </li>
  </ul>`
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  constructor(private svc: ProductService) {}
  ngOnInit() { this.load(); }
  load() { this.svc.list().subscribe(ps => this.products = ps); }
  new() { /* navigate to detail with id=0 */ }
  edit(id: number) { /* navigate to detail/id */ }
  del(id: number) { this.svc.delete(id).subscribe(() => this.load()); }
}
