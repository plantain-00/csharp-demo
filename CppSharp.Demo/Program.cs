using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CppSharp.AST;
using CppSharp.Generators;

namespace CppSharp.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleDriver.Run(new SampleLibrary());
            Console.Read();
        }
    }

    internal class SampleLibrary : ILibrary
    {
        public void Preprocess(Driver driver, ASTContext ctx)
        {
        }

        public void Postprocess(Driver driver, ASTContext ctx)
        {
        }

        public void Setup(Driver driver)
        {
            var options = driver.Options;
            options.GeneratorKind = GeneratorKind.CSharp;
            options.LibraryName = "Sample";
            options.Headers.Add("C:\\Users\\York Yao\\Documents\\Sample.h");
            //options.Libraries.Add("Sample.lib");
        }

        public void SetupPasses(Driver driver)
        {
        }
    }
}
