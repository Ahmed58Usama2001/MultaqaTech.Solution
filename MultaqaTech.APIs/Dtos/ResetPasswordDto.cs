﻿namespace MultaqaTech.APIs.Dtos
{
    public class ResetPasswordDto
    {
        [Required]
        public string Password { get; set; }
        public string Email { get; set; }
        public string Token {  get; set; }
    }
}
