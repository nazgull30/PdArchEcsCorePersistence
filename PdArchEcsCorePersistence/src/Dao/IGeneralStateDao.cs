namespace PdArchEcsCorePersistence.Dao;

using System.Threading.Tasks;

public interface IGeneralStateDao
{
    public Task Save(IGeneralState generalState);
    public IGeneralState Load();
}
