using System.Collections.Generic;

namespace GDPSUpdater
{
    public class LocalConfigModel
    {
        public string updaterDirectoryURI { get; set; }
        public string localVersion { get; set; }
        public string GDExecutableFile { get; set; }
    }
    public class RemoteManifestModel
    {
        public string version { get; set; }
        public string directory { get; set; }
        public List<string> files { get; set; }
    }
}
