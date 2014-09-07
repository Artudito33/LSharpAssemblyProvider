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
        private ObservableCollectionEx<AssemblyEntity> _library;
        private ObservableCollectionEx<AssemblyEntity> _utility;
        private ObservableCollectionEx<AssemblyEntity> _champion;
        private ObservableCollectionEx<LogEntity> _log;
        private bool _init;

        public DataService()
        {
            Task.Factory.StartNew(() =>
            {
                _library = new ObservableCollectionEx<AssemblyEntity>();
                _utility = new ObservableCollectionEx<AssemblyEntity>();
                _champion = new ObservableCollectionEx<AssemblyEntity>();
                _log = new ObservableCollectionEx<LogEntity>();

                using (var client = new WebClient())
                {
                    var result = client.DownloadString(new Uri("https://raw.githubusercontent.com/h3h3/LSharpAssemblyProvider/master/Repository.json"));
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

        public ObservableCollectionEx<AssemblyEntity> GetChampionData()
        {
            return _champion;
        }

        public ObservableCollectionEx<AssemblyEntity> GetUtilityData()
        {
            return _utility;
        }

        public ObservableCollectionEx<AssemblyEntity> GetLibraryData()
        {
            return _library;
        }

        public ObservableCollectionEx<LogEntity> GetLogData()
        {
            return _log;
        }

        public bool IsInitComplete()
        {
            return _init;
        }
    }
}