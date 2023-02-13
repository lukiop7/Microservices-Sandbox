﻿namespace PlatformService.Models;

public sealed class Platform
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Publisher { get; set; } = default!;
    public string Cost { get; set; } = default!;
}