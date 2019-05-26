using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CML.ToolKit.ToolTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketExTest.Instance.ExecuteTest();
            Console.Read();
        }
    }
}
