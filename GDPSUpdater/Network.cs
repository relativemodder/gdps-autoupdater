using System.Net;

namespace GDPSUpdater
{
    public class Network
    {
        public string DoGetRequest(string URI)
        {
            WebClient webClient = new WebClient();
            string responseText = webClient.DownloadString(URI);
            return responseText;
        }
        public void DownloadFile(string URI, string path)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFile(URI, path);
        }
    }
}
