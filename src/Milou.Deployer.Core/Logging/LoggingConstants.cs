﻿using Arbor.KVConfiguration.Core.Metadata;

namespace Milou.Deployer.Core.Logging
{
    public static class LoggingConstants
    {
        [Metadata]
        public const string PlainOutputFormatEnabled = "--plain-console";

        public const string LoggingCategoryFormatEnabled = "--logging-category-format-enabled";

        public const string PlainFormat = "{Message:lj}{NewLine}{Exception}";

        public const string DefaultFormat = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";
    }
}
