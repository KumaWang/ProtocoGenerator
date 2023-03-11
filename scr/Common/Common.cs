using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iuiu.service
{
    static class Common
    {
        public static uint IpToInt(string ip)
        {
            char[] separator = new char[] { '.' };
            string[] items = ip.Split(separator);
            return uint.Parse(items[0])
                    | uint.Parse(items[1]) << 8
                    | uint.Parse(items[2]) << 16
                    | uint.Parse(items[3]) << 24;
        }

        public static string ReadString(this BinaryReader br, int count)
        {
            var str = string.Empty;
            foreach (var c in br.ReadChars(count))
            {
                str = str + c;
            }

            return str;
        }
    }
}
