import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TaskFormComponent } from '../../../shared/components/task-form/task-form.component';
import { TaskService } from '../../../core/services/task.service';
import { UserService } from '../../../core/services/user.service';
import { AuthService } from '../../../core/auth/auth.service';

@Component({
  selector: 'app-task-create',
  standalone: true,
  imports: [TaskFormComponent],
  templateUrl: './task-create.component.html',
  styleUrls: ['./task-create.component.scss'],
})
export class TaskCreateComponent implements OnInit {
  taskForm: FormGroup;
  users: any[] = [];
  statuses: any[] = [];

  constructor(private fb: FormBuilder, private taskService: TaskService, private userService: UserService, private authService: AuthService) {
    this.taskForm = this.fb.group({
      title: ['', Validators.required],
      description: [''],
      assignedUserId: ['', Validators.required],
      dueDate: ['', Validators.required],
      statusId: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.fetchUsers();
    this.fetchStatuses();
  }

  fetchUsers(): void {
    this.userService.getUsers().subscribe(
      (users) => (this.users = users),
      (error) => console.error('Error fetching users:', error)
    );
  }

  fetchStatuses(): void {
    this.taskService.getStatuses().subscribe(
      (statuses) => (this.statuses = statuses),
      (error) => console.error('Error fetching statuses:', error)
    );
  }

  onSubmit(taskData: any): void {
    const userId = this.authService.getCurrentUser()?.id; // Get userId from token
    const payload = {
      ...taskData,
      createdBy: userId, // Add createdBy field
    };
    this.taskService.createTask(payload).subscribe(
      () => {
        alert('Task created successfully!');
        this.taskForm.reset();
      },
      (error) => {
        console.error('Error creating task:', error);
        alert('Failed to create task. Please try again.');
      }
    );
  }
}