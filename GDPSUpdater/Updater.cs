using System;
using System.IO;
using System.Windows.Forms;

namespace GDPSUpdater
{
    public class Updater
    {
        private Network Networker;
        private Parsers Parser;

        public Updater()
        {
            this.Networker = new Network();
            this.Parser = new Parsers();
        }

        public void Update()
        {
            string localConfigString = File.ReadAllText("config.json");
            LocalConfigModel localConfig = Parser.ParseLocalConfig(localConfigString);

            string remoteManifestString = Networker.DoGetRequest($"{localConfig.updaterDirectoryURI}/manifest.json");
            RemoteManifestModel remoteManifest = Parser.ParseRemoteManifest(remoteManifestString);

            foreach(string file in remoteManifest.files)
            {
                string[] fullpath = file.Split('/');
                if(fullpath.Length > 1)
                {
                    for (int i = 0; i < fullpath.Length - 1; i++)
                        Directory.CreateDirectory(fullpath[i]);
                }
                try
                {
                    Networker.DownloadFile($"{localConfig.updaterDirectoryURI}/{remoteManifest.directory}/{file}", file);
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Error downloading file at url: {localConfig.updaterDirectoryURI}/{remoteManifest.directory}/{file}");
                    Console.WriteLine(e.Message);
                }
            }
            localConfig.localVersion = remoteManifest.version;
            File.WriteAllText("config.json", Parser.SerializeLocalConfig(localConfig));
        }
        public bool CheckForUpdates()
        {
            if (!File.Exists("config.json"))
            {
                LocalConfigModel zeroLocalConfig = new LocalConfigModel();
                zeroLocalConfig.localVersion = "0.1";
                zeroLocalConfig.updaterDirectoryURI = "https://awesomegdps.com/updates";
                zeroLocalConfig.GDExecutableFile = "GeometryDash.exe";

                string zeroLocalConfigString = Parser.SerializeLocalConfig(zeroLocalConfig);
                File.WriteAllText("config.json", zeroLocalConfigString);

                MessageBox.Show("No local config file found. Updater created you a config.json file with template filler. Please fill in a config.json correctly and try again.");
                Application.Exit();
            }

            string localConfigString = File.ReadAllText("config.json");
            LocalConfigModel localConfig = Parser.ParseLocalConfig(localConfigString);

            string remoteManifestString = "";

            try
            {
                remoteManifestString = Networker.DoGetRequest($"{localConfig.updaterDirectoryURI}/manifest.json");
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error getting manifest at url: {localConfig.updaterDirectoryURI}/ manifest.json");
                Console.WriteLine(e.Message);
                Console.ReadLine();
                Application.Exit();
            }
            RemoteManifestModel remoteManifest = Parser.ParseRemoteManifest(remoteManifestString);

            if (localConfig.localVersion != remoteManifest.version)
                return true;
            return false;
        }
    }
}
