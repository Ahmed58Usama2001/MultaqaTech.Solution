﻿namespace MultaqaTech.Core.Entities.Identity;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string? Bio { get; set; }

    public string? JobTitle { get; set; }

    public bool IsInstructor { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public DateTime RegistrationDate { get; set; }
}
