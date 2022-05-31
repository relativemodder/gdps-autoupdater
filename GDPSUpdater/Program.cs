namespace GDPSUpdater
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "--skip-updates")
            {
                return;
            }
            Updater updater = new Updater();
            bool haveUpdates = updater.CheckForUpdates();
            if (!haveUpdates)
            {
                GDLauncher.LaunchGD();
                return;
            }
            updater.Update();
            GDLauncher.LaunchGD();
        }
    }
}
