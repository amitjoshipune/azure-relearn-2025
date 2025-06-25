// src/app/services/todo.service.ts
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface Todo {
  id?: number;
  title: string;
  description?: string;
  productId?: number;
  status?: string;
  createdAtUtc?: string;
}

@Injectable({ providedIn: 'root' })
export class TodoService {
  //private api = 'http://localhost:5209/api/todo';
  private api = environment.api.todos;

/**
 *
 */
constructor(private http :HttpClient) {
  this.api = environment.api.todos;
  
}
/*  
getAll(page = 1, pageSize = 10, from?: string, to?: string): Observable<{items: Todo[], total: number}> {
    let params = new HttpParams()
      .set('page', page)
      .set('pageSize', pageSize);

    if (from) params = params.set('from', from);
    if (to) params = params.set('to', to);

    return this.http.get<{items: Todo[], total: number}>(`${this.api}/all`, { params });
  }
*/
getAll(
    page = 1,
    pageSize = 10,
    from?: string,
    to?: string
  ): Observable<{ items: Todo[]; total: number }> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    if (from) params = params.set('from', from);
    if (to) params = params.set('to', to);

    return this.http.get<{ items: Todo[]; total: number }>(
      `${this.api}/all`,
      { params }
    );
  }
  
  create(todo: Todo) {
    return this.http.post(this.api, todo);
  }

  update(todo: Todo) {
    return this.http.put(`${this.api}/${todo.id}`, todo);
  }

  delete(id: number) {
    return this.http.delete(`${this.api}/${id}`);
  }
}
