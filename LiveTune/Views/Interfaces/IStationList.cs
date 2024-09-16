using System.Threading.Tasks;

namespace LiveTune.Views.Interfaces
{
    public interface IStationList
    {
        int PageSize { get; }
        int Offset { get; set; }
        bool IsError { get; set; }
        bool IsLoading { get; set; }
        Task LoadFirstAsync();

        Task LoadNextAsync();

        Task ReloadAsync();
    }
}
