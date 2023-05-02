﻿namespace BookStoreAPI.Models;

public class BookStoreDatabaseSetting
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string BookCollectionName { get; set; } = null!;
}