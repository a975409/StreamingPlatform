﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace StreamingPlatform.Domain.Models;

public partial class SongAndAlbumRelation
{
    public int Id { get; set; }

    public int SongId { get; set; }

    public int AlbumId { get; set; }

    public string AlbumName { get; set; }
}