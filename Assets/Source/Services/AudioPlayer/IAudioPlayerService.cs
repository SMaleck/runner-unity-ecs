namespace Source.Services.AudioPlayer
{
    public interface IAudioPlayerService
    {
        void PlayMusic(MusicAudioClipType musicAudioClipType);
        void PlayEffect(EffectAudioClipType effectAudioClipType);
        void PlayEffectRandomized(EffectAudioClipType effectAudioClipType);
        void PauseMusic();
        void PauseEffects();
        void UnPauseMusic();
        void UnPauseEffects();
        void SetMusicVolume(float volume);
        void SetEffectsVolume(float volume);
    }
}