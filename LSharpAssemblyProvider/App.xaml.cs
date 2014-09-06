using System;
using System.IO;
using System.Windows;
using GalaSoft.MvvmLight.Threading;

namespace LSharpAssemblyProvider
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        static App()
        {
            DispatcherHelper.Initialize();
            Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log"));
            Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Repositories"));
        }
    }
}
