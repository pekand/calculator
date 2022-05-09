using System;
using System.Windows.Forms;

using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using System.IO;
using System.Drawing;
using IronPython.Modules;
using System.Reflection;

namespace Calculator
{
    public class Script //UID6870742772
    {
        private ScriptEngine pyEngine = null;
        private dynamic pyScope = null;
        private string StandardLibraryPath;

        public Tools tools = null;

        public string script = "";

        /*************************************************************************************************************************/
        // ENGINE

        public Script(string StandardLibraryPath = "")
        {
            this.StandardLibraryPath = StandardLibraryPath;
            this.tools = new Tools();
        }

        private dynamic CompileSourceAndExecute(String code)
        {
            ScriptSource source = pyEngine.CreateScriptSourceFromString(code, SourceCodeKind.Statements);
            CompiledCode compiled = source.Compile();
            return compiled.Execute(pyScope);
        }

        class Scriptresult {
            public string exression;
            public string result;
            public string result2;
        }

        public string RunScript(String script)
        {
            string output = null;

            try
            {
                if (pyEngine == null)
                {
                    pyEngine = Python.CreateEngine();

                    var paths = pyEngine.GetSearchPaths();
                    if (File.Exists(this.StandardLibraryPath))
                    {
                        paths.Add(this.StandardLibraryPath);
                    }
                    pyEngine.SetSearchPaths(paths);
                    pyScope = pyEngine.CreateScope();

                    /// add items to scope
                    pyScope.Tools = this.tools;
                    pyScope.F = this.tools;
                }

                

            
                /// set streams
                MemoryStream ms = new MemoryStream();
                StreamWriter outputWr = new StreamWriter(ms);
                pyEngine.Runtime.IO.SetOutput(ms, outputWr);
                pyEngine.Runtime.IO.SetErrorOutput(ms, outputWr);

                /// execute script
                this.CompileSourceAndExecute(script);

                /// read script output
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                output = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return output;
        }

    }
}
