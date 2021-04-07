namespace AspNetCoreSerilog
{
    public interface IGameService
    {
        System.Threading.Tasks.Task<(int dungeonsIds, int userMana)> FetchNextPhaseDataAsync();
    }
}