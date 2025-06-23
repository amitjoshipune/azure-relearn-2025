import { Component } from '@angular/core';
import { RequisitionService } from '../requisition.service';

@Component({
  selector: 'app-process',
  template: `
  <h2>Process Requisition</h2>
  <input [(ngModel)]="todoId" placeholder="Todo ID">
  <button (click)="run()">Process</button>
  <pre>{{result | json}}</pre>`
})
export class ProcessComponent {
  todoId!: number;
  result: any;
  constructor(private svc: RequisitionService) {}
  run() {
    this.svc.process(this.todoId).subscribe(r => this.result = r);
  }
}
