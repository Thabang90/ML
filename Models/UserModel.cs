﻿using System.ComponentModel.DataAnnotations;

namespace QuestionExplorer.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int AdminId { get; set; }
        public string Token { get; set; }
        public bool IsAdmin { get; set; }
    }
}
