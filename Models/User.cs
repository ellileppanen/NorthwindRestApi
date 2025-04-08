using System;
using System.Collections.Generic;

namespace NorthwindRestApi.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int AccesslevelId { get; set; }
}
