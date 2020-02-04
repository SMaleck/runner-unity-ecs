using UnityEngine;

namespace UGF.Services.Audio
{
    public interface IAudioService
    {
        void PlayMusic(AudioClip audioClip, bool loop = true);
        void PlayEffect(AudioClip audioClip, bool loop = false);
        void PlayEffectRandomized(AudioClip audioClip, bool loop = false);
        void ResetAllPausedItems();
        void PauseAll();
        void PauseMusic();
        void PauseEffects();
        void UnPauseAll();
        void UnPauseMusic();
        void UnPauseEffects();
        void SetMusicVolume(float volume);
        void SetEffectsVolume(float volume);
    }
}