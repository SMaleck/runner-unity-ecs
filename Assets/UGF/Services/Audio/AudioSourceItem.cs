using UGF.Util.Mono;
using UnityEngine;
using Zenject;

namespace UGF.Services.Audio
{
    public class AudioSourceItem : AbstractDisposableMonoBehaviour
    {
        public class Pool : MonoMemoryPool<AudioClip, float, float, bool, AudioSourceItem>
        {
            protected override void Reinitialize(
                AudioClip audioClip,
                float volume,
                float pitch,
                bool loop, 
                AudioSourceItem audioSourceItem)
            {
                audioSourceItem.SetIsActive(true);
                audioSourceItem.Construct(
                    audioClip,
                    volume,
                    pitch,
                    loop);
            }

            protected override void OnDespawned(AudioSourceItem audioSourceItem)
            {
                audioSourceItem.Stop();
                base.OnDespawned(audioSourceItem);
            }
        }

        [SerializeField] private AudioSource _audioSource;

        public bool IsPlaying => _audioSource.isPlaying;
        public bool IsPaused { get; private set; }

        public void Construct(
            AudioClip audioClip,
            float volume,
            float pitch,
            bool loop)
        {
            _audioSource.clip = audioClip;
            _audioSource.volume = volume;
            _audioSource.pitch = pitch;
            _audioSource.loop = loop;

            IsPaused = false;
        }

        public void Play()
        {
            IsPaused = false;
            _audioSource.Play();
        }

        public void Stop()
        {
            IsPaused = false;
            _audioSource.Stop();
        }

        public void Pause()
        {
            if (!IsPlaying || IsPaused) return;

            IsPaused = true;
            _audioSource.Pause();
        }

        public void UnPause()
        {
            if (!IsPaused) return;

            IsPaused = false;
            _audioSource.UnPause();
        }

        public void SetVolume(float volume)
        {
            _audioSource.volume = volume;
        }
    }
}
