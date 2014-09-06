using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LSharpAssemblyProvider.Model
{
    public class DataService : IDataService
    {
        private ObservableCollection<AssemblyEntity> _library;
        private ObservableCollection<AssemblyEntity> _utility;
        private ObservableCollection<AssemblyEntity> _champion;
        private ObservableCollection<LogEntity> _log;
        private bool _init;

        public DataService()
        {
            Task.Factory.StartNew(() =>
            {
                _library = new ObservableCollection<AssemblyEntity>();
                _utility = new ObservableCollection<AssemblyEntity>();
                _champion = new ObservableCollection<AssemblyEntity>();
                _log = new ObservableCollection<LogEntity>();

                using (var client = new WebClient())
                {
                    var result = client.DownloadString(new Uri("https://dl.dropboxusercontent.com/u/54555251/Data.json"));
                    var data = JsonConvert.DeserializeObject<List<AssemblyEntity>>(result);

                    foreach (var entity in data)
                    {
                        switch (entity.Category)
                        {
                            case "Library":
                                _library.Add(entity);
                                break;

                            case "Utility":
                                _utility.Add(entity);
                                break;

                            case "Champion":
                                _champion.Add(entity);
                                break;
                        }
                    }

                    _init = true;
                }
            });
        }

        public ObservableCollection<AssemblyEntity> GetChampionData()
        {
            return _champion;
        }

        public ObservableCollection<AssemblyEntity> GetUtilityData()
        {
            return _utility;
        }

        public ObservableCollection<AssemblyEntity> GetLibraryData()
        {
            return _library;
        }

        public ObservableCollection<LogEntity> GetLogData()
        {
            return _log;
        }

        public bool IsInitComplete()
        {
            return _init;
        }
    }
}