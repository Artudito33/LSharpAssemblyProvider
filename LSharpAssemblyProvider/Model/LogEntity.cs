using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSharpAssemblyProvider.Model
{
    public class LogEntity
    {
        public DateTime Date { get; set; }
        public string Assembly { get; set; }
        public string Message { get; set; }

        public LogEntity(string assembly, string message)
        {
            Assembly = assembly;
            Message = message;
            Date = DateTime.Now;
        }
    }
}
