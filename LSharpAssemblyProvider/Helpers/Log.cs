using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Threading;
using LSharpAssemblyProvider.Model;
using LSharpAssemblyProvider.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace LSharpAssemblyProvider.Helpers
{
    public static class LogFile
    {
        public static void Write(string assembly, string message)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() => ServiceLocator.Current.GetInstance<MainViewModel>().Log.Add(new LogEntity(assembly, message)));
        }
    }
}
