﻿namespace MultaqaTech.Core.Entities.Identity;

public class JwtResponseVM
{
    [Required]
    public string Token { get; set; }
}
