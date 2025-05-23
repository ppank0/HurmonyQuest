﻿namespace ContestService.DAL.Repositories.Interfaces;

public interface IHasTimestamps
{
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
}
