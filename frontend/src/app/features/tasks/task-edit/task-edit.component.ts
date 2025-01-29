import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { TaskFormComponent } from '../../../shared/components/task-form/task-form.component';
import { TaskService } from '../../../core/services/task.service';
import { UserService } from '../../../core/services/user.service';
import { AuthService } from '../../../core/auth/auth.service';

@Component({
  selector: 'app-task-edit',
  standalone: true,
  imports: [TaskFormComponent],
  templateUrl: './task-edit.component.html',
  styleUrls: ['./task-edit.component.scss'],
})
export class TaskEditComponent implements OnInit {
  taskForm: FormGroup;
  users: any[] = [];
  statuses: any[] = [];
  taskId!: number;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private taskService: TaskService,
    private userService: UserService,
    private authService: AuthService
  ) {
    this.taskForm = this.fb.group({
      title: ['', Validators.required],
      description: [''],
      assignedUserId: ['', Validators.required],
      dueDate: ['', Validators.required],
      statusId: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.taskId = +this.route.snapshot.params['id'];
    this.fetchTask();
    this.fetchUsers();
    this.fetchStatuses();
  }

  fetchTask(): void {
    this.taskService.getTaskById(this.taskId).subscribe(
      (task) => {
        this.taskForm.patchValue({
          title: task.title,
          description: task.description,
          assignedUserId: task.assignedUserId, // Ensure this matches the API response
          dueDate: task.dueDate,
          statusId: task.statusId, // Ensure this matches the API response
        });
        Object.keys(this.taskForm.controls).forEach((controlName) => {
          this.taskForm.get(controlName)?.markAsDirty();
        });
      },
      (error) => console.error('Error fetching task:', error)
    );
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
    console.log(payload);
    this.taskService.updateTask(this.taskId, payload).subscribe(
      () => {
        alert('Task updated successfully!');
      },
      (error) => {
        console.error('Error updating task:', error);
        alert('Failed to update task. Please try again.');
      }
    );
  }
}