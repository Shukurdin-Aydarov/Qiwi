using System;

namespace Qiwi.Helpers
{
    internal static class ThrowHelper
    {
        internal static void ThrowInvalidOperationException(string message)
        {
            throw new InvalidOperationException(message);
        }

        internal static void ThrowArgumentNullException(string arg)
        {
            throw new ArgumentNullException($"The {arg} cannot be null.");
        }
    }
}
