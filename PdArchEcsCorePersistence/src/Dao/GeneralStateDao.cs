namespace PdArchEcsCorePersistence.Dao;

using System.Threading.Tasks;
using Godot;
using VContainer;

[GenerateInjector]
public class GeneralStateDao : IGeneralStateDao
{
    private readonly string _filePath;
    private readonly IGeneralStateByteConverter _generalStateByteConverter;

    public GeneralStateDao(string filePath, IGeneralStateByteConverter generalStateByteConverter)
    {
        _filePath = filePath;
        _generalStateByteConverter = generalStateByteConverter;
    }

    public async Task Save(IGeneralState generalState)
    {
        await Task.Run(() =>
                {
                    GD.Print("[GeneralStateDao] Saving...");
                    var bytes = _generalStateByteConverter.ToBytes(generalState);
                    GD.Print($"[GeneralStateDao] Saving... bytes: {bytes.Length}");
                    using var file = FileAccess.Open(_filePath, FileAccess.ModeFlags.Write);
                    GD.Print($"[GeneralStateDao] Saved... {file}");
                    file.StoreBuffer(bytes);
                });
    }

    public IGeneralState Load()
    {
        using var file = FileAccess.Open(_filePath, FileAccess.ModeFlags.Read);
        if (file == null)
            return null;
        if (file.GetError() != Error.Ok)
        {
            GD.PrintErr($"[GeneralStateDao] -> Load: {file.GetError()}");
            return null;
        }
        var bytes = file.GetBuffer((int)file.GetLength());
        return _generalStateByteConverter.FromBytes(bytes);
    }
}
