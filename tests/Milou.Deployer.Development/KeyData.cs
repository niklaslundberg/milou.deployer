using System;

namespace Milou.Deployer.Development
{
    public class KeyData
    {
        public KeyData(byte[] key) => Key = key;

        public byte[] Key { get; }

        public string KeyAsBase64 => Convert.ToBase64String(Key);
    }
}