using System;

namespace Sematext.Helpers
{
    public static class TimeHelper
    {
        public static long EpochFromUtc => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }
}