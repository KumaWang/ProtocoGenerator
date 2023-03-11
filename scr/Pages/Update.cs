using iuiu.Common;
using iuiu.server;
using iuiu.server.Net.Protocol.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iuiu.service.Pages
{
    class Update : IHttpRequest
    {
        private string mTotalMD5;
        private string mRoot = Path.Combine(FileService.ApplicationRootPath, "iuiu");
        private Dictionary<string, RemoteFile> sRemoteFiles;
        private Encoding mEncoding;

        public Update()
        {
            mTotalMD5 = GetTotalMD5();
            mEncoding = Encoding.GetEncoding("gb2312");
        }

        public HttpResult GetResponse(HttpMessage msg, string postData)
        {
            try
            {
                var obj = fastJSON.JSON.Parse(postData);
                var total = obj.Object["total"].String;
                var returnStr = string.Empty;
                if (total != mTotalMD5)
                {
                    Dictionary<string, LocalFile> localFiles = new Dictionary<string, LocalFile>();
                    var files = obj.Object["files"].String.Split(';');
                    if (!(files.Length == 1 && string.IsNullOrEmpty(files[0])))
                    {
                        foreach (var file in files)
                        {
                            var strs = file.Split(':');
                            localFiles.Add(strs[0], new LocalFile() { Inculde = strs[0], MD5 = strs[1] });
                        }
                    }

                    // 对比新增
                    var added = sRemoteFiles.Where(X => !localFiles.ContainsKey(X.Value.Inculde));
                    foreach (var value in added)
                    {
                        returnStr += string.Format("add:{0};", value.Value.Inculde);
                    }

                    // 对比移除
                    var removed = localFiles.Where(X => !sRemoteFiles.ContainsKey(X.Value.Inculde));
                    foreach (var value in removed)
                    {
                        returnStr += string.Format("remove:{0};", value.Value.Inculde);
                    }

                    // 对比替换
                    var replaces = sRemoteFiles.Where(X => localFiles.ContainsKey(X.Value.Inculde) && localFiles[X.Value.Inculde].MD5 != X.Value.MD5);
                    foreach (var value in replaces)
                    {
                        returnStr += string.Format("replace:{0};", value.Value.Inculde);
                    }

                    return new HttpResult(mEncoding.GetBytes(returnStr));
                }
                else
                {
                    // 已是最新版
                    return new HttpResult(mEncoding.GetBytes("0"));
                }
            }
            catch
            {
                Console.WriteLine("请求update时发生异常");

                // 返回null不提示
                return null;
            }
        }

        private string GetTotalMD5()
        {
            sRemoteFiles = new Dictionary<string, RemoteFile>();

            var str = string.Empty;
            foreach (var fileName in FileService.SearchDirectory(Path.Combine(mRoot, "bin"), "*.*", true))
            {
                var md5 = ConvertUtility.GetMD5HashFromFile(fileName);
                str = str + ";" + md5;
                var inculde = FileService.GetRelativePath(mRoot, fileName).Replace("\\", "/");
                sRemoteFiles[inculde] = new RemoteFile() { Inculde = inculde, FileName = fileName, MD5 = md5 };
            }

            foreach (var fileName in FileService.SearchDirectory(Path.Combine(mRoot, "data"), "*.*", true))
            {
                var md5 = ConvertUtility.GetMD5HashFromFile(fileName);
                str = str + ";" + md5;
                var inculde = FileService.GetRelativePath(mRoot, fileName).Replace("\\", "/");
                sRemoteFiles[inculde] = new RemoteFile() { Inculde = inculde, FileName = fileName, MD5 = md5 };
            }

            return ConvertUtility.GenerateKey(str);
        }

        class LocalFile
        {
            public string Inculde { get; set; }
            public string MD5 { get; set; }
        }

        class RemoteFile
        {
            public string Inculde { get; set; }
            public string FileName { get; set; }
            public string MD5 { get; set; }
        }
    }
}
