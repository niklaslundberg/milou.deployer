namespace Milou.Deployer.Web.Core.Deployment
{
    public sealed class DeployStatus
    {
        public static readonly DeployStatus Latest = new("latest", "Latest");

        public static readonly DeployStatus NoPackagesAvailable = new("no-packages", "No packages available");

        public static readonly DeployStatus UpdateAvailable = new("update-available", "Update available");

        public static readonly DeployStatus Unavailable = new("unavailable", "Unavailable");

        public static readonly DeployStatus Unknown = new("unknown", "unknown");

        public static readonly DeployStatus NoLaterAvailable = new("no-later-available", "No later version available");

        private DeployStatus(string key, string displayName)
        {
            Key = key;
            DisplayName = displayName;
        }

        public string Key { get; }

        public string DisplayName { get; }
    }
}