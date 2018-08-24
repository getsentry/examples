namespace AspNetCore21Serilog
{
    public interface IGameService
    {
        System.Threading.Tasks.Task<(int dungeonsIds, int userMana)> FetchNextPhaseDataAsync();
    }
}