import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatFormFieldControl, MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatButtonModule } from '@angular/material/button';
import { TaskService } from '../../../core/services/task.service';
import { MatToolbarModule } from '@angular/material/toolbar';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/auth/auth.service';

@Component({
  selector: 'app-task-filters',
  standalone: true,
  templateUrl: './task-filters.component.html',
  styleUrls: ['./task-filters.component.scss'],
  imports: [
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatButtonModule,
    MatToolbarModule,
    CommonModule
],
})
export class TaskFiltersComponent {
  filter = { title: '', statusId: null, dueDate: null, userId: null };
  statuses: any[] = [];

  constructor(private taskService: TaskService, private router: Router, private authService: AuthService) {}

  ngOnInit(): void {
    this.taskService.getStatuses().subscribe(
      (s) => {
        this.statuses = s;
      }
    );
  }

  @Output() filterChanged = new EventEmitter<any>();

  onFilterChange(): void {
    this.filterChanged.emit(this.filter);
  }

  navigateToCreateTask(): void {
    this.router.navigate(['/tasks/create']);
  }

  isAdmin(): boolean {
    return this.authService.getCurrentUser()?.role === 'Admin';
  }
}