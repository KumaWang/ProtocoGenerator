namespace iuiu.VirtualMachine
{
    public class Package
    {
        /// <summary>
        /// 包文件名
        /// </summary>
        public string                   FileName        { get; set; }

        /// <summary>
        /// 启动文件名
        /// </summary>
        public string                   StartupFileName { get; set; }

        /// <summary>
        /// 项目目录
        /// </summary>
        public string                   Root            { get; set; }

        /// <summary>
        /// 项目输出目录
        /// </summary>
        public string                   OutputRoot      { get; set; }

        /// <summary>
        /// 函数集
        /// </summary>
        public Function[]               Functions       { get; set; }

        internal Package()
        {
        }

        internal Package(string fileName, string startupFileName, string root, Function[] functions)
        {
            FileName = fileName;
            Functions = functions;
            StartupFileName = startupFileName;
            Root = root;
        }
    }
}
