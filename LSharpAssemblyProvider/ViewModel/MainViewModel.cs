using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using LSharpAssemblyProvider.Helpers;
using LSharpAssemblyProvider.Model;
using LSharpAssemblyProvider.Properties;
using MahApps.Metro.Controls.Dialogs;

namespace LSharpAssemblyProvider.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// The <see cref="LeagueSharpPath" /> property's name.
        /// </summary>
        public const string LeagueSharpPathPropertyName = "LeagueSharpPath";

        /// <summary>
        /// Sets and gets the LeagueSharpPath property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string LeagueSharpPath
        {
            get
            {
                return Settings.Default.LeagueSharpPath;
            }

            set
            {
                if (Settings.Default.LeagueSharpPath == value)
                {
                    return;
                }

                RaisePropertyChanging(() => LeagueSharpPath);
                Settings.Default.LeagueSharpPath = value;
                RaisePropertyChanged(() => LeagueSharpPath);
            }
        }

        /// <summary>
        /// The <see cref="StartPage" /> property's name.
        /// </summary>
        public const string StartPagePropertyName = "StartPage";

        private int _startPage = 3;

        /// <summary>
        /// Sets and gets the StartPage property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int StartPage
        {
            get
            {
                return _startPage;
            }
            set
            {
                Set(() => StartPage, ref _startPage, value);
            }
        }


        /// <summary>
        /// The <see cref="Champion" /> property's name.
        /// </summary>
        public const string ChampionPropertyName = "Champion";

        private ObservableCollection<AssemblyEntity> _champion;

        /// <summary>
        /// Sets and gets the Data property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<AssemblyEntity> Champion
        {
            get
            {
                return _champion;
            }
            set
            {
                Set(() => Champion, ref _champion, value);
            }
        }


        /// <summary>
        /// The <see cref="Utility" /> property's name.
        /// </summary>
        public const string UtilityPropertyName = "Utility";

        private ObservableCollection<AssemblyEntity> _utility;

        /// <summary>
        /// Sets and gets the Data property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<AssemblyEntity> Utility
        {
            get
            {
                return _utility;
            }
            set
            {
                Set(() => Utility, ref _utility, value);
            }
        }


        /// <summary>
        /// The <see cref="Library" /> property's name.
        /// </summary>
        public const string LibraryPropertyName = "Library";

        private ObservableCollection<AssemblyEntity> _library;

        /// <summary>
        /// Sets and gets the Data property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<AssemblyEntity> Library
        {
            get
            {
                return _library;
            }
            set
            {
                Set(() => Library, ref _library, value);
            }
        }

        /// <summary>
        /// The <see cref="Progress" /> property's name.
        /// </summary>
        public const string ProgressPropertyName = "Progress";

        private long _progress;

        /// <summary>
        /// Sets and gets the Progress property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public long Progress
        {
            get
            {
                return _progress;
            }
            set
            {
                Set(() => Progress, ref _progress, value);
            }
        }

        /// <summary>
        /// The <see cref="ProgressMax" /> property's name.
        /// </summary>
        public const string ProgressMaxPropertyName = "ProgressMax";

        private long _progressMax = 100;

        /// <summary>
        /// Sets and gets the Progress property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public long ProgressMax
        {
            get
            {
                return _progressMax;
            }
            set
            {
                Set(() => ProgressMax, ref _progressMax, value);
            }
        }

        /// <summary>
        /// The <see cref="Update" /> property's name.
        /// </summary>
        public const string UpdatePropertyName = "Update";

        private ObservableCollection<AssemblyEntity> _update;

        /// <summary>
        /// Sets and gets the Data property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<AssemblyEntity> Update
        {
            get
            {
                return _update;
            }
            set
            {
                Set(() => Update, ref _update, value);
            }
        }

        /// <summary>
        /// The <see cref="SelectedAssembly" /> property's name.
        /// </summary>
        public const string SelectedAssemblyPropertyName = "SelectedAssembly";

        private AssemblyEntity _selectedAssembly;

        /// <summary>
        /// Sets and gets the SelectedAssembly property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public AssemblyEntity SelectedAssembly
        {
            get
            {
                return _selectedAssembly;
            }
            set
            {
                Set(() => SelectedAssembly, ref _selectedAssembly, value);
            }
        }

        /// <summary>
        /// The <see cref="Log" /> property's name.
        /// </summary>
        public const string LogPropertyName = "Log";

        private ObservableCollection<LogEntity> _log;

        /// <summary>
        /// Sets and gets the Log property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<LogEntity> Log
        {
            get
            {
                return _log;
            }
            set
            {
                Set(() => Log, ref _log, value);
            }
        }

        private RelayCommand _installCommand;

        /// <summary>
        /// Gets the InstallCommand.
        /// </summary>
        public RelayCommand InstallCommand
        {
            get
            {
                return _installCommand
                    ?? (_installCommand = new RelayCommand(
                                          () =>
                                          {
                                              if (SelectedAssembly != null)
                                              {
                                                  InstallAssembly(SelectedAssembly);
                                              }
                                          }));
            }
        }

        private RelayCommand _deleteCommand;

        /// <summary>
        /// Gets the DeleteCommand.
        /// </summary>
        public RelayCommand DeleteCommand
        {
            get
            {
                return _deleteCommand
                    ?? (_deleteCommand = new RelayCommand(
                                          () =>
                                          {
                                              if (SelectedAssembly != null)
                                              {
                                                  var path = Path.Combine(Settings.Default.LeagueSharpPath, "Assemblies", SelectedAssembly.Name + ".exe");

                                                  if (File.Exists(path))
                                                      File.Delete(path);

                                                  SelectedAssembly.State = "";
                                              }
                                          }));
            }
        }

        private RelayCommand _updateCommand;

        /// <summary>
        /// Gets the UpdateCommand.
        /// </summary>
        public RelayCommand UpdateCommand
        {
            get
            {
                return _updateCommand
                       ?? (_updateCommand = new RelayCommand(UpdateAssembly));

            }
        }

        private RelayCommand _saveCommand;

        /// <summary>
        /// Gets the SaveCommand.
        /// </summary>
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand
                    ?? (_saveCommand = new RelayCommand(
                                          () =>
                                          {
                                              Cleanup();
                                              MessageBox.Show("Please restart Application!", "Restart",
                                                  MessageBoxButton.OK, MessageBoxImage.Information);
                                              Environment.Exit(0);
                                          }));
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService service)
        {
            if (string.IsNullOrEmpty(Settings.Default.LeagueSharpPath))
            {
                Console.WriteLine("Setup");
                StartPage = 4;
                return;
            }

            Task.Factory.StartNew(() =>
            {
                AssemblyEntity current = null;

                try
                {
                    Console.WriteLine("Init");

                    while (!service.IsInitComplete())
                        Thread.Sleep(100);

                    DispatcherHelper.RunAsync(() =>
                    {
                        Champion = service.GetChampionData();
                        Utility = service.GetUtilityData();
                        Library = service.GetLibraryData();
                        Log = service.GetLogData();
                        Update = new ObservableCollection<AssemblyEntity>();

                        LogFile.Write("", "Init Complete");

                        Progress = 0;
                        ProgressMax = Library.Count;
                        foreach (var lib in Library)
                        {
                            if (File.Exists(Path.Combine(Settings.Default.LeagueSharpPath, "Assemblies", "System", lib.Name + ".dll")))
                            {
                                lib.LocalVersion = Github.LocalVersion(lib);
                                lib.State = "Installed";
                                Update.Add(lib);
                            }
                            else
                            {
                                lib.LocalVersion = Github.LocalVersion(lib);
                                lib.State = "Available";
                            }

                            current = lib;
                            Progress++;
                        }

                        Progress = 0;
                        ProgressMax = Utility.Count;
                        foreach (var util in Utility)
                        {
                            if (File.Exists(Path.Combine(Settings.Default.LeagueSharpPath, "Assemblies", util.Name + ".exe")))
                            {
                                util.LocalVersion = Github.LocalVersion(util);
                                util.State = "Installed";
                                Update.Add(util);
                            }
                            else
                            {
                                util.LocalVersion = Github.LocalVersion(util);
                                util.State = "Available";
                            }

                            current = util;
                            Progress++;
                        }

                        Progress = 0;
                        ProgressMax = Champion.Count;
                        foreach (var champ in Champion)
                        {
                            if (File.Exists(Path.Combine(Settings.Default.LeagueSharpPath, "Assemblies", champ.Name + ".exe")))
                            {
                                champ.LocalVersion = Github.LocalVersion(champ);
                                champ.State = "Installed";
                                Update.Add(champ);
                            }
                            else
                            {
                                champ.LocalVersion = Github.LocalVersion(champ);
                                champ.State = "Available";
                            }

                            current = champ;
                            Progress++;
                        }

                        Progress = 0;
                        ProgressMax = 1;

                        var sort = new SortDescription("Name", ListSortDirection.Ascending);
                        var v = CollectionViewSource.GetDefaultView(Champion);
                        v.GroupDescriptions.Add(new PropertyGroupDescription("State"));
                        v.SortDescriptions.Add(sort);
                        v = CollectionViewSource.GetDefaultView(Utility);
                        v.GroupDescriptions.Add(new PropertyGroupDescription("State"));
                        v.SortDescriptions.Add(sort);
                        v = CollectionViewSource.GetDefaultView(Library);
                        v.GroupDescriptions.Add(new PropertyGroupDescription("State"));
                        v.SortDescriptions.Add(sort);
                        v = CollectionViewSource.GetDefaultView(Update);
                        v.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
                        v.SortDescriptions.Add(sort);
                    });
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error - " + current.Name + " - " + current.State, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        public override void Cleanup()
        {
            Console.WriteLine("Save: " + LeagueSharpPath);
            Settings.Default.LeagueSharpPath = LeagueSharpPath;
            Settings.Default.Save();
        }

        private void InstallAssembly(AssemblyEntity assembly)
        {
            Task.Factory.StartNew(async () =>
            {
                if (assembly.Name == "clipper_library")
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(new Uri(assembly.Url), Path.Combine(Settings.Default.LeagueSharpPath, "Assemblies", "System", assembly.Name + ".dll"));
                    }

                    assembly.State = "Complete";

                    return;
                }

                try
                {
                    LogFile.Write(assembly.Name, "Version Check");
                    assembly.State = "Version Check";
                    assembly.RepositroyVersion = Github.RepositorieVersion(assembly);
                    assembly.LocalVersion = Github.LocalVersion(assembly);

                    LogFile.Write(assembly.Name, "Downloading - " + assembly.Url);
                    assembly.State = "Downloading";
                    Github.Update(assembly.Url);
                    assembly.State = "";

                    LogFile.Write(assembly.Name, "Open Project");
                    assembly.State = "Open Project";
                    var project = assembly.GetProjectFile();

                    LogFile.Write(assembly.Name, "Compile");
                    assembly.State = "Compile";
                    var result = Github.Compile(project);

                    if (result != null && File.Exists(result))
                    {
                        LogFile.Write(assembly.Name, "Sucsesfull - " + result);
                        assembly.State = "Move";

                        if (result.EndsWith(".dll"))
                        {
                            var dll = Path.Combine(Settings.Default.LeagueSharpPath, "Assemblies", "System", assembly.Name + ".dll");
                            if (File.Exists(dll))
                                File.Delete(dll);
                            File.Move(result, dll);
                            LogFile.Write(assembly.Name, "Move - " + result + "\n->\n" + dll);
                        }

                        if (result.EndsWith(".exe"))
                        {
                            var exe = Path.Combine(Settings.Default.LeagueSharpPath, "Assemblies", assembly.Name + ".exe");
                            if (File.Exists(exe))
                                File.Delete(exe);
                            File.Move(result, exe);
                            LogFile.Write(assembly.Name, "Move - " + result + "\n->\n" + exe);
                        }

                        assembly.State = "Complete";
                        DispatcherHelper.CheckBeginInvokeOnUI(() => Update.Add(assembly));
                    }
                    else
                    {
                        LogFile.Write(assembly.Name, "Failed");
                        assembly.State = "Broken";
                    }

                    await DialogService.ShowMessage(assembly.Name, "Successful Installed", MessageDialogStyle.Affirmative);
                }
                catch (Exception e)
                {
                    LogFile.Write(assembly.Name, e.Message);
                    MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        private void UpdateAssembly()
        {
            Task.Factory.StartNew(async () =>
            {
                AssemblyEntity current = null;
                var build = 0;
                var failed = 0;

                try
                {
                    Progress = 0;
                    ProgressMax = Update.Count;

                    var repoUpdates = new List<AssemblyEntity>();
                    Parallel.ForEach(Update, repository =>
                    {
                        LogFile.Write(repository.Name, "Version Check");
                        repository.State = "Version Check";
                        repository.RepositroyVersion = Github.RepositorieVersion(repository);
                        repository.LocalVersion = Github.LocalVersion(repository);

                        if (repository.RepositroyVersion > repository.LocalVersion)
                        {
                            if (repoUpdates.All(r => r.Url != repository.Url))
                            {
                                LogFile.Write(repository.Name, "Download Queue");
                                repository.State = "Download Queue";
                                repoUpdates.Add(repository);
                            }
                            else
                            {
                                repository.State = "";
                            }
                        }
                        else
                        {
                            repository.State = "";
                        }

                        Progress++;
                    });

                    Progress = 0;
                    ProgressMax = repoUpdates.Count;

                    foreach (var repository in repoUpdates)
                    {
                        LogFile.Write(repository.Name, "Downloading - " + repository.Url);
                        repository.State = "Downloading";
                        Github.Update(repository.Url);
                        repository.State = "";
                        Progress++;
                    }

                    Progress = 0;
                    ProgressMax = Update.Count * 3;

                    foreach (var repository in Update)
                    {
                        if (repository.Name == "clipper_library")
                        {
                            repository.State = "Complete";
                            Progress += 3;
                            continue;
                        }
                            
                        current = repository;

                        LogFile.Write(repository.Name, "Open Project");
                        repository.State = "Open Project";
                        var project = repository.GetProjectFile();
                        Progress++;

                        LogFile.Write(repository.Name, "Compile");
                        repository.State = "Compile";
                        var result = Github.Compile(project);
                        Progress++;

                        if (result != null && File.Exists(result))
                        {
                            LogFile.Write(repository.Name, "Sucsesfull - " + result);
                            repository.State = "Move";

                            if (result.EndsWith(".dll"))
                            {
                                var dll = Path.Combine(Settings.Default.LeagueSharpPath, "Assemblies", "System", repository.Name + ".dll");
                                if (File.Exists(dll))
                                    File.Delete(dll);
                                File.Move(result, dll);
                                LogFile.Write(repository.Name, "Move - " + result + "\n->\n" + dll);
                            }

                            if (result.EndsWith(".exe"))
                            {
                                var exe = Path.Combine(Settings.Default.LeagueSharpPath, "Assemblies", repository.Name + ".exe");
                                if (File.Exists(exe))
                                    File.Delete(exe);
                                File.Move(result, exe);
                                LogFile.Write(repository.Name, "Move - " + result + "\n->\n" + exe);
                            }

                            repository.State = "Complete";
                            build++;
                        }
                        else
                        {
                            LogFile.Write(repository.Name, "Failed");
                            repository.State = "Broken";
                            failed++;
                        }

                        Progress++;
                    }

                    await DialogService.ShowMessage("Update", "Update Complete\n\n" + build + " Working\n" + failed + " Broken", MessageDialogStyle.Affirmative);
                }
                catch (Exception e)
                {
                    LogFile.Write(current.Name, e.Message);
                    MessageBox.Show(e.Message, "Error - " + current.Name + " - " + current.State, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }
    }
}