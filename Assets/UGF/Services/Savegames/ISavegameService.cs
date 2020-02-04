using UGF.Services.Savegames.Models;

namespace UGF.Services.Savegames
{
    public interface ISavegameService
    {
        Savegame Savegame { get; }
        void Reset();
    }
}
