﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace StreamingPlatform.Domain.Models;

public partial class Singer
{
    public int Id { get; set; }

    public string AccountNo { get; set; }

    public string Pwd { get; set; }

    public string Email { get; set; }

    public string Name { get; set; }

    public string DisplayName { get; set; }

    public bool GoogleOta { get; set; }
}