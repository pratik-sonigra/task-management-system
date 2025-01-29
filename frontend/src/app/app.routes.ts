import { Routes } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';
import { UnauthorizedComponent } from './features/unauthorized/unauthorized.component';

export const routes: Routes = [
  { path: '', redirectTo: '/auth/login', pathMatch: 'full' },
  {
    path: 'auth',
    loadChildren: () =>
      import('./features/auth/auth.module').then((m) => m.AuthModule),
  },
  {
    path: 'dashboard',
    loadChildren: () =>
      import('./features/dashboard/dashboard.module').then(
        (m) => m.DashboardModule
      ),
      canActivate: [AuthGuard], // Protect the route
      data: { roles: ['Admin'] }, // Only Admins can access
  },
  {
    path: 'tasks',
    loadChildren: () =>
      import('./features/tasks/tasks.module').then((m) => m.TasksModule),
      canActivate: [AuthGuard], // Protect the route
      data: { roles: ['Admin', 'User'] }, // Admins and Users can access
  },
  {
    path: 'dashboard',
    loadChildren: () =>
      import('./features/dashboard/dashboard.module').then((m) => m.DashboardModule),
      canActivate: [AuthGuard], // Protect with AuthGuard
      data: { roles: ['Admin'] }, // Restrict to Admins
  },
  { path: 'unauthorized', component: UnauthorizedComponent },
  { path: '**', redirectTo: '/auth/login' }, // Fallback route
];