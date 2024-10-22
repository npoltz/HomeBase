﻿using HomeBase.Core.Configuration;

namespace HomeBase.Data.Configuration
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}