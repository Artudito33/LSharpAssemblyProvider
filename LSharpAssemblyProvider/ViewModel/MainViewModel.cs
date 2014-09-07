using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using LSharpAssemblyProvider.Helpers;
using LSharpAssemblyProvider.Model;
using LSharpAssemblyProvider.Properties;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;

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

        private ObservableCollectionEx<AssemblyEntity> _champion;

        /// <summary>
        /// Sets and gets the Data property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollectionEx<AssemblyEntity> Champion
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

        private ObservableCollectionEx<AssemblyEntity> _utility;

        /// <summary>
        /// Sets and gets the Data property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollectionEx<AssemblyEntity> Utility
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

        private ObservableCollectionEx<AssemblyEntity> _library;

        /// <summary>
        /// Sets and gets the Data property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollectionEx<AssemblyEntity> Library
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

        private ObservableCollectionEx<AssemblyEntity> _update;

        /// <summary>
        /// Sets and gets the Data property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollectionEx<AssemblyEntity> Update
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

        private ObservableCollectionEx<LogEntity> _log;

        /// <summary>
        /// Sets and gets the Log property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollectionEx<LogEntity> Log
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

        /// <summary>
        /// The <see cref="IsOverlay" /> property's name.
        /// </summary>
        public const string IsOverlayPropertyName = "IsOverlay";

        private bool _isOverlay = false;

        /// <summary>
        /// Sets and gets the LockInterface property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsOverlay
        {
            get
            {
                return _isOverlay;
            }
            set
            {
                Set(() => IsOverlay, ref _isOverlay, value);
            }
        }

        /// <summary>
        /// The <see cref="OverlayText" /> property's name.
        /// </summary>
        public const string OverlayTextPropertyName = "OverlayText";

        private string _overlayText = "";

        /// <summary>
        /// Sets and gets the OverlayText property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string OverlayText
        {
            get
            {
                return _overlayText;
            }
            set
            {
                Set(() => OverlayText, ref _overlayText, value);
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
                    ?? (_saveCommand = new RelayCommand(async () =>
                                          {
                                              var dia = new OpenFileDialog
                                              {
                                                  DefaultExt = ".exe",
                                                  Filter = "LeagueSharp.Loader.exe|*.exe"
                                              };
                                              var result = dia.ShowDialog();

                                              if (result == true)
                                              {
                                                  if (dia.FileName.EndsWith("LeagueSharp.Loader.exe"))
                                                  {
                                                      LeagueSharpPath = dia.FileName.Replace("LeagueSharp.Loader.exe", "");
                                                      await DialogService.ShowMessage("Restart", "Please restart Application!", MessageDialogStyle.Affirmative);
                                                      Cleanup();
                                                      Environment.Exit(0);
                                                  }
                                                  else
                                                  {
                                                      await DialogService.ShowMessage("Error", "LeagueSharp.Loader.exe not found @ " + dia.FileName, MessageDialogStyle.Affirmative);
                                                  }
                                              }
                                          }));
            }
        }

        public DataGrid UpdateGrid { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService service)
        {
            if (string.IsNullOrEmpty(Settings.Default.LeagueSharpPath))
            {
                StartPage = 5;
                return;
            }

            if (!IsInDesignMode)
                IsOverlay = true;

            Task.Factory.StartNew(() =>
            {
                try
                {
                    OverlayText = "Loading";

                    while (!service.IsInitComplete())
                        Thread.Sleep(100);

                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        Champion = service.GetChampionData();
                        Utility = service.GetUtilityData();
                        Library = service.GetLibraryData();
                        Log = service.GetLogData();
                        Update = new ObservableCollectionEx<AssemblyEntity>();

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

                    Thread.Sleep(100);

                    Progress = 0;
                    ProgressMax = Library.Count + Utility.Count + Champion.Count;

                    using (var update = Update.DelayNotifications())
                    {
                        foreach (var lib in Library)
                        {
                            lib.LocalVersion = Github.LocalVersion(lib);

                            if (File.Exists(Path.Combine(Settings.Default.LeagueSharpPath, "Assemblies", "System", lib.Name + ".dll")))
                            {
                                lib.State = "Installed";
                                update.Add(lib);
                            }
                            else
                            {
                                lib.State = "Available";
                            }

                            Progress++;
                            OverlayText = string.Format("{0}/{1} Loading", Progress, ProgressMax);
                        }


                        foreach (var util in Utility)
                        {
                            util.LocalVersion = Github.LocalVersion(util);

                            if (File.Exists(Path.Combine(Settings.Default.LeagueSharpPath, "Assemblies", util.Name + ".exe")))
                            {
                                util.State = "Installed";
                                update.Add(util);
                            }
                            else
                            {
                                util.State = "Available";
                            }

                            Progress++;
                            OverlayText = string.Format("{0}/{1} Loading", Progress, ProgressMax);
                        }

                        foreach (var champ in Champion)
                        {
                            champ.LocalVersion = Github.LocalVersion(champ);

                            if (File.Exists(Path.Combine(Settings.Default.LeagueSharpPath, "Assemblies", champ.Name + ".exe")))
                            {
                                champ.State = "Installed";
                                update.Add(champ);
                            }
                            else
                            {
                                champ.State = "Available";
                            }

                            Progress++;
                            OverlayText = string.Format("{0}/{1} Loading", Progress, ProgressMax);
                        }

                        Update = update;
                    }

                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        Update = new ObservableCollectionEx<AssemblyEntity>(Update.OrderBy(a => a.Name));
                        CollectionViewSource.GetDefaultView(Update).Refresh();
                        CollectionViewSource.GetDefaultView(Champion).Refresh();
                        CollectionViewSource.GetDefaultView(Utility).Refresh();
                        CollectionViewSource.GetDefaultView(Library).Refresh();
                    });
                }
                catch (Exception e)
                {
                    DialogService.ShowMessage("Error", e.Message, MessageDialogStyle.Affirmative);
                }

                LogFile.Write("", "Init Complete");
                IsOverlay = false;
            });
        }

        public override void Cleanup()
        {
            Console.WriteLine("Save: " + LeagueSharpPath);
            Settings.Default.LeagueSharpPath = LeagueSharpPath;
            Settings.Default.Save();
        }

        private List<AssemblyEntity> VersionCheck(IList<AssemblyEntity> list)
        {
            Progress = 0;
            ProgressMax = list.Count;
            OverlayText = string.Format("{0}/{1} Version Check", Progress, ProgressMax);
            var updates = new List<AssemblyEntity>();

            Parallel.ForEach(list, assembly =>
            {
                var old = assembly.State;
                LogFile.Write(assembly.Name, "Version Check");
                assembly.State = "Version Check";
                assembly.LocalVersion = Github.LocalVersion(assembly);
                assembly.RepositroyVersion = Github.RepositorieVersion(assembly);

                if (assembly.LocalVersion == 0 || assembly.LocalVersion != assembly.RepositroyVersion)
                {
                    updates.Add(assembly);
                }

                assembly.State = old;
                Progress++;
                OverlayText = string.Format("{0}/{1} Version Check", Progress, ProgressMax);
            });

            Progress = 0;
            ProgressMax = 1;

            return updates;
        }

        private void Download(IList<AssemblyEntity> list)
        {
            Progress = 0;
            ProgressMax = list.Count;
            OverlayText = string.Format("{0}/{1} Download", Progress, ProgressMax);
            var complete = new List<String>();

            Parallel.ForEach(list, repository =>
            {
                var download = false;

                lock (complete)
                {
                    if (!complete.Contains(repository.Url))
                    {
                        complete.Add(repository.Url);
                        download = true;
                    }
                }

                if (download)
                {
                    var old = repository.State;
                    LogFile.Write(repository.Name, "Downloading");
                    repository.State = "Downloading";
                    Github.Update(repository.Url);
                    repository.State = old;
                    repository.LocalVersion = Github.LocalVersion(repository);
                }

                Progress++;
                OverlayText = string.Format("{0}/{1} Download", Progress, ProgressMax);
            });

            Progress = 0;
            ProgressMax = 1;
        }

        private List<String> Compile(IList<AssemblyEntity> list)
        {
            Progress = 0;
            ProgressMax = list.Count;
            OverlayText = string.Format("{0}/{1} Compile", Progress, ProgressMax);
            var complete = new List<String>();

            foreach (var assembly in list)
            {
                assembly.State = "Compile";
                var project = assembly.GetProjectFile();

                if (project != null)
                {
                    var result = Github.Compile(project);

                    if (result != null && File.Exists(result))
                    {
                        LogFile.Write(assembly.Name, "Compile Sucsesfull - " + result);
                        assembly.State = "Updated";
                        complete.Add(result);
                    }
                    else
                    {
                        LogFile.Write(assembly.Name, "Compile Failed - Compiler Error");
                        assembly.State = "Broken";
                    }
                }
                else
                {
                    LogFile.Write(assembly.Name, "Project File not Found - " + assembly.Name + ".csproj");
                    assembly.State = "Broken";
                }

                Progress++;
                OverlayText = string.Format("{0}/{1} Compile", Progress, ProgressMax);
            }

            Progress = 0;
            ProgressMax = 1;

            return complete;
        }

        private void Copy(IList<string> list)
        {
            Progress = 0;
            ProgressMax = list.Count;
            OverlayText = string.Format("{0}/{1} Copy", Progress, ProgressMax);

            Parallel.ForEach(list, file =>
            {
                var info = new FileInfo(file);

                if (info.Extension == ".dll")
                {
                    var dll = Path.Combine(Settings.Default.LeagueSharpPath, "Assemblies", "System", info.Name);
                    if (File.Exists(dll))
                        File.Delete(dll);
                    File.Move(file, dll);

                    LogFile.Write(info.Name.Replace(".dll", ""), "Move - " + info.FullName + " -> " + dll);
                }

                if (info.Extension == ".exe")
                {
                    var exe = Path.Combine(Settings.Default.LeagueSharpPath, "Assemblies", info.Name);
                    if (File.Exists(exe))
                        File.Delete(exe);
                    File.Move(file, exe);

                    LogFile.Write(info.Name.Replace(".exe", ""), "Move - " + info.FullName + " -> " + exe);
                }

                Progress++;
                OverlayText = string.Format("{0}/{1} Copy", Progress, ProgressMax);
            });
        }

        private void InstallAssembly(AssemblyEntity assembly)
        {
            Task.Factory.StartNew(async () =>
            {
                IsOverlay = true;
                try
                {
                    Download(new[] { assembly });
                    var complete = Compile(new[] { assembly });
                    Copy(complete);
                    assembly.State = "Installed";

                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        Update.Add(assembly);
                    });

                    OverlayText = "Complete";
                    await DialogService.ShowMessage("Install", "Install Complete\n\n" + complete.Count + " Assemblies Installed.", MessageDialogStyle.Affirmative);
                    OverlayText = "";
                }
                catch (Exception e)
                {
                    LogFile.Write("Error", e.Message);
                    DialogService.ShowMessage("Error", e.Message, MessageDialogStyle.Affirmative);
                }
                IsOverlay = false;
            });
        }

        private void UpdateAssembly()
        {
            Task.Factory.StartNew(async () =>
            {
                IsOverlay = true;
                try
                {
                    var updates = VersionCheck(Update);
                    Download(updates);
                    var complete = Compile(updates);
                    Copy(complete);

                    OverlayText = "Complete";
                    await DialogService.ShowMessage("Update", "Update Complete\n\n" + updates.Count + " Assemblies Updated.", MessageDialogStyle.Affirmative);
                    OverlayText = "";
                }
                catch (Exception e)
                {
                    LogFile.Write("Error", e.Message);
                    MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                IsOverlay = false;
            });
        }
    }
}