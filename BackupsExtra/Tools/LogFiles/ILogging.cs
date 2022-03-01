namespace BackupsExtra.Tools.LogFiles
{
    public interface ILogging
    {
        void Info(string message);
        void Warning(string message);
        void BeginMakingTimeAlert();
        void EndMakingTimeAlert();
    }
}