namespace TatRat.API
{
    public interface ILogService : IService
    {
        public void Log(string message);
        public void Log(LogChannel channel, string message);
        public void Warn(string message);
        public void Warn(LogChannel channel, string message);
        public void Error(string message);
        public void Error(LogChannel channel, string message);
    }
}