// src/app/pages/todos/todo-list.component.ts
import { Component, OnInit } from '@angular/core';
import { TodoService, Todo } from '../../../services/todo.service';
import { ProductService, Product } from '../../../services/product.service';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html'
})
export class TodoListComponent implements OnInit {

  todos: Todo[] = [];
  products: Product[] = [];
  page = 1;
  pageSize = 10;
  total = 0;
  filterFrom = '';
  filterTo = '';

  constructor(private svc: TodoService, private ps: ProductService) {}

  ngOnInit() {
    this.load();
    this.ps.getAll().subscribe(x => this.products = x.items);
  }

  load() {
    this.svc.getAll(this.page, this.pageSize, this.filterFrom, this.filterTo)
      .subscribe(resp => {
        this.todos = resp.items;
        this.total = resp.total;
      });
  }

  pageChanged(page: number) {
    this.page = page;
    this.load();
  }

  delete(id: number) {
    if (confirm('Delete?')) this.svc.delete(id).subscribe(() => this.load());
  }

  statusChanged(todo: Todo, event: Event) {
    todo.status = (event.target as HTMLInputElement).value;
    this.svc.update(todo).subscribe();
  }

  findProductName(productId: number | undefined): string {
  if (!productId || !this.products) return '';
  const prod = this.products.find(p => p.id === productId);
  return prod ? prod.name : '';
  }
}
