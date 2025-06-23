import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class RequisitionService {
  base = environment.api.requisition;
  constructor(private http: HttpClient) {}
  process(todoId: number) {
    return this.http.post(`${this.base}/${todoId}/process`, {});
  }
}
