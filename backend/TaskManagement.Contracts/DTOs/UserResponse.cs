﻿namespace TaskManagement.Contracts.DTOs
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}
