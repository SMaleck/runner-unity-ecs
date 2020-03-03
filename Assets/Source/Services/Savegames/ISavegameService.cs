using Source.Services.Savegames.Models;

namespace Source.Services.Savegames
{
    public interface ISavegameService
    {
        Savegame Savegame { get; }
        void Reset();
    }
}
