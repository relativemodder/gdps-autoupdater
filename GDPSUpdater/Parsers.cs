using Newtonsoft.Json;

namespace GDPSUpdater
{
    public class Parsers
    {
        public LocalConfigModel ParseLocalConfig(string localConfigString)
        {
            return JsonConvert.DeserializeObject<LocalConfigModel>(localConfigString);
        }
        public RemoteManifestModel ParseRemoteManifest(string remoteManifestString)
        {
            return JsonConvert.DeserializeObject<RemoteManifestModel>(remoteManifestString);
        }

        public string SerializeLocalConfig(LocalConfigModel localConfig)
        {
            return JsonConvert.SerializeObject(localConfig);
        }
        public string SerializeRemoteManifest(RemoteManifestModel remoteManifest)
        {
            return JsonConvert.SerializeObject(remoteManifest);
        }
    }
}
