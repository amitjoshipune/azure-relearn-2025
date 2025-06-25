import { Component, OnInit } from '@angular/core';
import { ProductService ,Product } from '../../../services/product.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-list',
  // standalone: true,
  // imports: [],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.scss'
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];

  constructor(private productService: ProductService, private router: Router) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts() {
    this.productService.getAll().subscribe(data => {
      this.products = data.items;
    });
  }

  deleteProduct(id: number) {
    if (confirm('Delete product?')) {
      this.productService.delete(id).subscribe(() => this.loadProducts());
    }
  }

  navigateToAdd(): void {
  this.router.navigate(['/products/add']);
  }
  navigateToEdit(id: number): void {
  this.router.navigate(['/products/edit', id]);
  }
}