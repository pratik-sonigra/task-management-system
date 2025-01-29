import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TaskListComponent } from './task-list/task-list.component';
import { TaskCreateComponent } from './task-create/task-create.component';
import { AuthGuard } from '../../core/guards/auth.guard';
import { TaskEditComponent } from './task-edit/task-edit.component';

const routes: Routes = [
  {
    path: '',
    component: TaskListComponent,
    canActivate: [AuthGuard],
    data: { roles: ['Admin', 'User'] },
  },
  {
    path: 'create',
    component: TaskCreateComponent,
    canActivate: [AuthGuard],
    data: { roles: ['Admin'] },
  },
  {
    path: 'edit/:id',
    component: TaskEditComponent,
    canActivate: [AuthGuard],
    data: { roles: ['Admin'] },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TasksRoutingModule {}