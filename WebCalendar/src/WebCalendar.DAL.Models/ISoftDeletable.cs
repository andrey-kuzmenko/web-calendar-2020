﻿namespace WebCalendar.DAL.Models
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}