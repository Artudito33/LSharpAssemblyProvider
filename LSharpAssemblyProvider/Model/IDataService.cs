using System.Collections.ObjectModel;

namespace LSharpAssemblyProvider.Model
{
    public interface IDataService
    {
        ObservableCollectionEx<AssemblyEntity> GetChampionData();
        ObservableCollectionEx<AssemblyEntity> GetUtilityData();
        ObservableCollectionEx<AssemblyEntity> GetLibraryData();
        ObservableCollectionEx<LogEntity> GetLogData();
        bool IsInitComplete();
    }
}
