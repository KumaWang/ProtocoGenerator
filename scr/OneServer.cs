using iuiu.server.Database;
using iuiu.server.Net;
using iuiu.server.Net.Protocol.Http;
using iuiu.service.Pages;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;

namespace iuiu.service
{
    class OneServer
    {
        public AsyncSocketServer<UserClinet> WebServer  { get; private set; }

        //public AsyncDatabaseServer Database { get; private set; }

        public string WWWRoot { get; set; }

        public OneServer(string wwwroot)
        {
            WWWRoot = wwwroot;
        }

        public void Start()
        {
            var mysqlFileName = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)), "tools\\mysql", "bin", "startup.bat");

            //Database = new AsyncDatabaseServer();
            //Database.Init("ro", "root", "x5", "127.0.0.1", 8080, mysqlFileName);

            var httpProtocol = new HttpProtocol<UserClinet>();
            httpProtocol.Enable404 = true;
            httpProtocol.NotFoundRequest = new Request404(this);
            httpProtocol.Register("update.aspx", new Update());

            WebServer = new AsyncSocketServer<UserClinet>(httpProtocol, 1000);

            ThreadPool.QueueUserWorkItem(new WaitCallback(StartWebServer));
        }

        private void StartWebServer(object sender)
        {
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }

            WebServer.Start(AddressIP, 8080);
        }

    }
}
