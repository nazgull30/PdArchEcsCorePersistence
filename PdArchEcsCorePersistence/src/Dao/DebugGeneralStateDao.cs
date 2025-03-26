namespace PdArchEcsCorePersistence.Dao;

using System.Threading.Tasks;
using Godot;
using VContainer;

[GenerateInjector]
public class DebugGeneralStateDao : IGeneralStateDao
{
    private readonly string _file;
    private readonly IGeneralStateByteConverter _generalStateByteConverter;

    public DebugGeneralStateDao(string file, IGeneralStateByteConverter generalStateByteConverter)
    {
        _file = file;
        _generalStateByteConverter = generalStateByteConverter;
    }

    public async Task Save(IGeneralState generalState)
    {
        await Task.Run(() =>
                {
                    GD.Print("[DebugGeneralStateDao] Saving...");
                    var bytes = _generalStateByteConverter.ToBytes(generalState);
                    GD.Print($"[DebugGeneralStateDao] Saving... bytes: {bytes.Length}");
                    System.IO.File.WriteAllBytes(_file, bytes);
                    GD.Print($"[DebugGeneralStateDao] Saved... {_file}");
                });
    }

    public IGeneralState Load()
    {
        if (!System.IO.File.Exists(_file))
            return null;

        var bytes = System.IO.File.ReadAllBytes(_file);
        return _generalStateByteConverter.FromBytes(bytes);
    }
}

