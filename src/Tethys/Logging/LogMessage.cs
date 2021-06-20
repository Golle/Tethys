namespace Tethys.Logging
{
    internal readonly struct LogMessage
    {
        public readonly string Message;
        public readonly LogLevel Level;

        public LogMessage(LogLevel level, string message)
        {
            Level = level;
            Message = message;
        }
    }
}
