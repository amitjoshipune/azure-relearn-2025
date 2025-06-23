import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

export interface Product { id: number; name: string; description?: string; }
@Injectable({ providedIn: 'root' })
export class ProductService {
  base = environment.api.products;
  constructor(private http: HttpClient) {}
  list(): Observable<Product[]> { return this.http.get<Product[]>(this.base); }
  get(id: number) { return this.http.get<Product>(`${this.base}/${id}`); }
  create(p: Product) { return this.http.post<Product>(this.base, p); }
  update(p: Product) { return this.http.put(`${this.base}/${p.id}`, p); }
  delete(id: number) { return this.http.delete(`${this.base}/${id}`); }
}




