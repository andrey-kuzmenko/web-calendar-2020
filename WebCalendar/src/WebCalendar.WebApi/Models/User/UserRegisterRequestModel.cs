﻿namespace WebCalendar.WebApi.Models.User
{
    public class UserRegisterRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}