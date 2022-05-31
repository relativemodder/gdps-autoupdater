using System;
using System.IO;

namespace GDPSUpdater
{
    public class GDLauncher
    {
        public static void LaunchGD()
        {
            Parsers parser = new Parsers();

            string localConfigString = File.ReadAllText("config.json");
            LocalConfigModel localConfig = parser.ParseLocalConfig(localConfigString);

            Console.WriteLine("Launching GD...");
            System.Diagnostics.Process.Start(localConfig.GDExecutableFile);
        }
    }
}
