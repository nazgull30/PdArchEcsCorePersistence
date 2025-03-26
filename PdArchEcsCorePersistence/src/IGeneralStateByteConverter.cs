namespace PdArchEcsCorePersistence;

public interface IGeneralStateByteConverter
{
    public byte[] ToBytes(IGeneralState gameState);
    public IGeneralState FromBytes(byte[] bytes);
}
