import { Component, OnInit } from '@angular/core';
import { TaskService } from '../../../core/services/task.service';
import { Task } from '../../../core/models/task.model';
import { MatTableModule } from '@angular/material/table';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../core/auth/auth.service';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { TaskFiltersComponent } from '../../../shared/components/task-filters/task-filters.component';
import { filter, take } from 'rxjs';
import { Router } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [ 
    MatTableModule,
    MatToolbarModule,
    MatFormFieldModule,
    MatSelectModule,
    MatOptionModule,
    MatDatepickerModule,
    MatInputModule,
    FormsModule,
    CommonModule,
    TaskFiltersComponent,
    MatIconModule
   ],
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.scss'],
})
export class TaskListComponent implements OnInit {
  tasks: Task[] = [];
  isLoading = true;
  currentUser: any;
  isAdmin: boolean = false;
  filter = { title: '', statusId: null, dueDate: null, userId: null };

  constructor(private taskService: TaskService, private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    console.log('Inside task list ngOnInit');
    this.currentUser = this.authService.getCurrentUser();
    this.isAdmin = this.currentUser.role === 'Admin';
    this.loadTasks(this.filter);
  }

  onFilterChanged(filter: any): void {
    this.loadTasks(filter);
  }

  loadTasks(filter: any): void {
    if (this.currentUser.role === 'User') {
      filter = { ...filter, userId: this.currentUser.id };
    }

    this.filter = filter;

    this.taskService.getAllTasks(filter).subscribe(
      (tasks) => {
        this.tasks = tasks;
        this.isLoading = false;
      },
      (error) => {
        console.error('Error fetching tasks:', error);
        this.isLoading = false;
      }
    );
  }

  editTask(taskId: number): void {
    this.router.navigate(['/tasks/edit', taskId]);
  }

  deleteTask(taskId: number): void {
    if (confirm('Are you sure you want to delete this task?')) {
      this.taskService.deleteTask(taskId).subscribe(() => {
        console.log('Task deleted successfully');
        this.loadTasks(this.filter); // Reload task list
      });
    }
  }
}