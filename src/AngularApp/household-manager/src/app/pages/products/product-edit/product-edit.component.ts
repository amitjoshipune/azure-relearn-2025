import { Component, OnInit } from '@angular/core';
import { ProductService, Product } from '../../../services/product.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html'
})
export class ProductEditComponent implements OnInit {
  product: Product = { id: 0, name: '', description: '' };

  constructor(
    private svc: ProductService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.svc.getById(id).subscribe(x => this.product = x);
  }

  save() {
    this.svc.update(this.product).subscribe(() => this.router.navigate(['/products']));
  }
}
