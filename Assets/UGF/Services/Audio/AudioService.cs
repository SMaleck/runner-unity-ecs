using System.Collections.Generic;
using System.Linq;
using UGF.Services.Audio.Config;
using UGF.Util;
using UnityEngine;

namespace UGF.Services.Audio
{
    public class AudioService : IAudioService
    {
        private const float DefaultPitch = 1;

        private readonly AudioServiceConfig _audioServiceConfig;
        private readonly AudioSourceItem.Pool _audioSourceItemPool;

        private readonly Dictionary<AudioChannel, List<AudioSourceItem>> _audioChannels;
        private readonly Dictionary<AudioChannel, float> _channelVolumes;

        public AudioService(
            AudioServiceConfig audioServiceConfig,
            AudioSourceItem.Pool audioSourceItemPool)
        {
            _audioServiceConfig = audioServiceConfig;
            _audioSourceItemPool = audioSourceItemPool;

            _audioChannels = EnumHelper<AudioChannel>.Iterator
                .ToDictionary(
                    audioChannel => audioChannel,
                    audioChannel => new List<AudioSourceItem>());

            _channelVolumes = EnumHelper<AudioChannel>.Iterator
                .ToDictionary(
                    audioChannel => audioChannel,
                    _audioServiceConfig.GetDefaultVolume);
        }

        private void PlayClip(AudioChannel audioChannel, AudioClip audioClip, float pitch, bool loop)
        {
            CleanupChannels();

            var audioSourceItem = _audioSourceItemPool.Spawn(
                audioClip,
                _channelVolumes[audioChannel],
                pitch,
                loop);

            audioSourceItem.Play();
            _audioChannels[audioChannel].Add(audioSourceItem);
        }

        private void CleanupChannels()
        {
            _audioChannels.Values
                .ToList()
                .ForEach(DespawnCompletedAudioSourceItems);
        }

        private void DespawnCompletedAudioSourceItems(List<AudioSourceItem> audioSourceItems)
        {
            audioSourceItems
                .Where(audioSourceItem => !audioSourceItem.IsPlaying)
                .ToList()
                .ForEach(audioSourceItem =>
                {
                    _audioSourceItemPool.Despawn(audioSourceItem);
                    audioSourceItems.Remove(audioSourceItem);
                });
        }

        #region PLAY INTERFACE

        public void PlayMusic(AudioClip audioClip, bool loop = true)
        {
            PlayClip(AudioChannel.Music, audioClip, DefaultPitch, loop);
        }

        public void PlayEffect(AudioClip audioClip, bool loop = false)
        {
            PlayClip(AudioChannel.Effects, audioClip, DefaultPitch, loop);
        }

        public void PlayEffectRandomized(AudioClip audioClip, bool loop = false)
        {
            float pitch = UnityEngine.Random.Range(_audioServiceConfig.MinPitch, _audioServiceConfig.MaxPitch);
            PlayClip(AudioChannel.Effects, audioClip, pitch, loop);
        }

        #endregion


        #region PAUSE / UNPAUSE INTERFACE

        public void ResetAllPausedItems()
        {
            _audioChannels.Values
                .SelectMany(audioSourceItems => audioSourceItems)
                .Where(audioSourceItem => audioSourceItem.IsPaused)
                .ToList()
                .ForEach(audioSourceItem => audioSourceItem.Stop());
        }

        public void PauseAll()
        {
            _audioChannels.Values
                .SelectMany(audioSourceItems => audioSourceItems)
                .ToList()
                .ForEach(audioSourceItem => audioSourceItem.Pause());
        }

        public void PauseMusic()
        {
            PauseChannel(AudioChannel.Music);
        }

        public void PauseEffects()
        {
            PauseChannel(AudioChannel.Effects);
        }

        private void PauseChannel(AudioChannel audioChannel)
        {
            _audioChannels[audioChannel]
                .ForEach(audioSourceItem => audioSourceItem.Pause());
        }

        public void UnPauseAll()
        {
            _audioChannels.Values
                .SelectMany(audioSourceItems => audioSourceItems)
                .ToList()
                .ForEach(audioSourceItem => audioSourceItem.UnPause());
        }

        public void UnPauseMusic()
        {
            UnPauseChannel(AudioChannel.Music);
        }

        public void UnPauseEffects()
        {
            UnPauseChannel(AudioChannel.Effects);
        }

        private void UnPauseChannel(AudioChannel audioChannel)
        {
            _audioChannels[audioChannel]
                .ForEach(audioSourceItem => audioSourceItem.UnPause());
        }

        #endregion


        #region VOLUME INTERFACE

        public void SetMusicVolume(float volume)
        {
            UpdateChannelVolume(AudioChannel.Music, volume);
        }

        public void SetEffectsVolume(float volume)
        {
            UpdateChannelVolume(AudioChannel.Effects, volume);
        }

        private void UpdateChannelVolume(AudioChannel audioChannel, float volume)
        {
            _channelVolumes[audioChannel] = volume;

            _audioChannels[audioChannel]
                .ForEach(audioSourceItem => audioSourceItem.SetVolume(volume));
        }

        #endregion
    }
}
