export interface Task {
    taskId: number;
    title: string;
    description: string;
    assignedUserId: number; // User's ID
    dueDate: string; // ISO date string
    statusId: number;
  }