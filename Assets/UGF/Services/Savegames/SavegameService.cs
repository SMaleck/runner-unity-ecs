using System;
using UGF.Services.Savegames.Config;
using UGF.Services.Savegames.Models;
using UGF.Util.DataStorageStrategies;
using UGF.Util.UniRx;
using UniRx;

namespace UGF.Services.Savegames
{
    public class SavegameService : AbstractDisposable, ISavegameService, ISavegamePersistenceService
    {
        private readonly SavegamesConfig _savegamesConfig;
        private readonly IDataStorageStrategy _dataStorageStrategy;

        private readonly SerialDisposable _savegameDisposer;
        private readonly SerialDisposable _saveDisposer;
        private readonly TimeSpan _requestSaveTimeout;

        private SavegameData _savegameData;
        private Savegame _savegame;

        Savegame ISavegameService.Savegame => GetSavegame();

        private ISavegamePersistenceService SavegamePersistenceService => this;
        private string FileName => _savegamesConfig.SavegameFilename;

        public SavegameService(
            SavegamesConfig savegamesConfig,
            IDataStorageStrategy dataStorageStrategy)
        {
            _savegamesConfig = savegamesConfig;
            _dataStorageStrategy = dataStorageStrategy;

            _savegameDisposer = new SerialDisposable().AddTo(Disposer);
            _saveDisposer = new SerialDisposable().AddTo(Disposer);
            _requestSaveTimeout = TimeSpan.FromSeconds(_savegamesConfig.RequestSaveTimeoutSeconds);
        }

        private Savegame GetSavegame()
        {
            if (_savegame == null)
            {
                SavegamePersistenceService.Load();
            }

            return _savegame;
        }

        void ISavegamePersistenceService.Load()
        {
            _savegameData = _dataStorageStrategy.Load<SavegameData>(FileName);
            _savegameData = _savegameData ?? SavegameDataFactory.CreateSavegameData();

            SetupSavegame();
        }

        void ISavegamePersistenceService.EnqueueSaveRequest()
        {
            Logger.Log($"Savegame dirty, saving in {_requestSaveTimeout.TotalSeconds}s");

            _saveDisposer.Disposable = Observable.Timer(_requestSaveTimeout)
                .Subscribe(_ => SavegamePersistenceService.Save());
        }

        void ISavegamePersistenceService.Save()
        {
            _saveDisposer.Disposable?.Dispose();
            _dataStorageStrategy.Save(FileName, _savegameData);
        }

        void ISavegameService.Reset()
        {
            _savegameData = SavegameDataFactory.CreateSavegameData();
            SetupSavegame();

            SavegamePersistenceService.Save();
        }

        private void SetupSavegame()
        {
            _savegameDisposer.Disposable?.Dispose();

            _savegame = new Savegame(_savegameData);
            _savegameDisposer.Disposable = _savegame;
        }
    }
}
