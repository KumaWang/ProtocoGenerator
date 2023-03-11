using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iuiu.VirtualMachine
{
    /// <summary>
    /// 函数
    /// </summary>
    public class Function
    {
        /// <summary>
        /// 获取函数名(唯一名)
        /// </summary>
        public string           FunctionName    { get; private set; }

        /// <summary>
        /// 获取来源文件名
        /// </summary>
        public string           FileName         { get; private set; }

        /// <summary>
        /// 获取堆栈大小
        /// </summary>
        public int              StackSize       { get; private set; }

        /// <summary>
        /// 获取参数
        /// </summary>
        public string[]         Formals         { get; private set; }

        /// <summary>
        /// 获取函数指令集
        /// </summary>
        public Instruction[]    Instructions    { get; private set; }

        /// <summary>
        /// 应用程序包
        /// </summary>
        public Package          Package         { get; private set; }


        public Function(Package package, string name, string fileName, int stackSize, string[] formals,  Instruction[] ins)
        {
            Package = package;
            FunctionName = name;
            FileName = fileName;
            StackSize = stackSize;
            Instructions = ins;
            Formals = formals;
        }

        public void Compile()
        {

        }
        
    }
}
