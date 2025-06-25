import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface Product {
  id: number;
  name: string;
  description: string;
  categoryId?: number;
}


@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = environment.api.products; // 'http://localhost:5076/api/product'; // Adjust port

  constructor(private http: HttpClient) {
    this.apiUrl = environment.api.products; // 'http://localhost:5076/api/product'; // Adjust port
  }

  getAll(): Observable<any> {
    return this.http.get(`${this.apiUrl}/All`);
  }

  getById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/${id}`);
  }

  create(product: Product): Observable<any> {
    return this.http.post(this.apiUrl, { ...product, category: { id: 20 } });
  }

  update(product: Product): Observable<any> {
    return this.http.put(`${this.apiUrl}/${product.id}`, { ...product, category: { id: 20 } });
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
