import { Component } from '@angular/core';
import { ProductService, Product } from '../../../services/product.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-add',
  templateUrl: './product-add.component.html'
})
export class ProductAddComponent {
  product: Product = {
    name: '', description: '',
    id: 0
  };
  constructor(private svc: ProductService, private router: Router) {}

  save() {
    this.svc.create(this.product).subscribe(() => this.router.navigate(['/products']));
  }
}
