using iuiu.server.Net.Protocol.Http;
using System;
using System.IO;
using System.Text;
using System.Web;

namespace iuiu.service.Pages
{
    class Request404 : IHttpRequest
    {
        public OneServer Server { get; private set; }

        public Request404(OneServer server)
        {
            Server = server;
        }

        public HttpResult GetResponse(HttpMessage msg, string postData)
        {
            var path = HttpUtility.UrlDecode(Path.Combine(Server.WWWRoot, msg.RequestUrl), UTF8Encoding.UTF8);
            if (File.Exists(path))
            {
                return new HttpResult(File.ReadAllBytes(path));
            }
            else
            {
                Console.WriteLine("not found file: {0}", path);
                return null;
            }
        }
    }
}
