﻿namespace Employee.Api.Models;

public class RefreshToken
{
    public Guid Id { get; set; }
    public required string Token { get; set; }
    public Guid UserId { get; set; }
    public required ApplicationUser User { get; set; }
}