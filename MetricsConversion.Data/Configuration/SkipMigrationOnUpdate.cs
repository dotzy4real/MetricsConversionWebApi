﻿using System;

namespace MetricsConversion.Data.Configuration
{
    /// <summary>
    /// Attribute to exclude migration from the list for use during the upgrade process
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class SkipMigrationOnUpdateAttribute : Attribute
    {
    }
}
