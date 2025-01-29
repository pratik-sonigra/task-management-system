import { Component, OnInit } from '@angular/core';
import { DashboardService } from '../../../core/services/dashboard.service';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatBadgeModule } from '@angular/material/badge';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
  imports: [ MatCardModule, MatListModule, MatIconModule, MatBadgeModule, CommonModule ]
})
export class DashboardComponent implements OnInit {
  totalTasks = 0;
  tasksByStatus: { status: string, count: number }[] = [];
  tasksPerUser: { userName: string; taskCount: number }[] = [];

  constructor(private dashboardService: DashboardService) {}

  ngOnInit(): void {
    this.loadDashboardData();
  }

  loadDashboardData(): void {
    this.dashboardService.getDashboardSummary().subscribe(
      (data) => {
        this.totalTasks = data.totalTasks;
        this.tasksByStatus = data.tasksByStatus;
        this.tasksPerUser = data.tasksPerUser;
      },
      (error) => {
        console.error('Error loading dashboard data:', error);
      }
    );
  }
}
