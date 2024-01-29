using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using TatRat.API;
using UDebug = UnityEngine.Debug;

namespace TatRat
{
    [UsedImplicitly]
    public sealed class UnityLogService : ILogService
    {
        private readonly string LOG_CHANNEL_FORMAT = "{0}:{1}";

        public void Log(string message)
            => UDebug.Log(message);

        public void Log(LogChannel channel, string message)
            => UDebug.Log(CombineMessageWithChannel(channel, message));

        public void Warn(string message)
            => UDebug.LogWarning(message);

        public void Warn(LogChannel channel, string message)
            => UDebug.LogWarning(CombineMessageWithChannel(channel, message));

        public void Error(string message)
            => UDebug.LogError(message);

        public void Error(LogChannel channel, string message)
            => UDebug.LogError(CombineMessageWithChannel(channel, message));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string CombineMessageWithChannel(LogChannel channel, string message)
            => string.Format(LOG_CHANNEL_FORMAT, channel, message);
    }
}