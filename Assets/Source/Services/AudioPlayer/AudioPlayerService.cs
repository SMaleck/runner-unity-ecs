using Source.Services.AudioPlayer.Config;
using UGF.Services.Audio;

namespace Source.Services.AudioPlayer
{
    public class AudioPlayerService : IAudioPlayerService
    {
        private readonly AudioClipsConfig _audioClipsConfig;
        private readonly IAudioService _audioService;

        public AudioPlayerService(
            AudioClipsConfig audioClipsConfig,
            IAudioService audioService)
        {
            _audioClipsConfig = audioClipsConfig;
            _audioService = audioService;
        }

        public void PlayMusic(MusicAudioClipType musicAudioClipType)
        {
            var audioClip = _audioClipsConfig.GetMusicAudioClip(musicAudioClipType);
            _audioService.PlayMusic(audioClip);
        }

        public void PlayEffect(EffectAudioClipType effectAudioClipType)
        {
            var audioClip = _audioClipsConfig.GetEffectAudioClip(effectAudioClipType);
            _audioService.PlayEffect(audioClip);
        }

        public void PlayEffectRandomized(EffectAudioClipType effectAudioClipType)
        {
            var audioClip = _audioClipsConfig.GetEffectAudioClip(effectAudioClipType);
            _audioService.PlayEffectRandomized(audioClip);
        }

        public void PauseMusic()
        {
            _audioService.PauseMusic();
        }

        public void PauseEffects()
        {
            _audioService.PauseEffects();
        }

        public void UnPauseMusic()
        {
            _audioService.UnPauseMusic();
        }

        public void UnPauseEffects()
        {
            _audioService.UnPauseMusic();
        }

        public void SetMusicVolume(float volume)
        {
            _audioService.SetMusicVolume(volume);
        }

        public void SetEffectsVolume(float volume)
        {
            _audioService.SetEffectsVolume(volume);
        }
    }
}
