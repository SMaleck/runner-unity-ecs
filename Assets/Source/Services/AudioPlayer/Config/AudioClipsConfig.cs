using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source.Services.AudioPlayer.Config
{
    [CreateAssetMenu(fileName = nameof(AudioClipsConfig), menuName = Constants.UMenuRoot + nameof(AudioClipsConfig))]
    public class AudioClipsConfig : ScriptableObject
    {
        [Serializable]
        private class MusicAudioClipItem
        {
            [SerializeField] private MusicAudioClipType _musicAudioClipType;
            public MusicAudioClipType MusicAudioClipType => _musicAudioClipType;

            [SerializeField] private AudioClip _audioClip;
            public AudioClip AudioClip => _audioClip;
        }

        [Serializable]
        private class EffectAudioClipItem
        {
            [SerializeField] private EffectAudioClipType _effectAudioClipType;
            public EffectAudioClipType EffectAudioClipType => _effectAudioClipType;

            [SerializeField] private AudioClip _audioClip;
            public AudioClip AudioClip => _audioClip;
        }

        [SerializeField] private List<MusicAudioClipItem> _musicAudioClipItems;
        [SerializeField] private List<EffectAudioClipItem> _effectAudioClipItems;

        public AudioClip GetMusicAudioClip(MusicAudioClipType musicAudioClipType)
        {
            return _musicAudioClipItems
                .First(item => item.MusicAudioClipType == musicAudioClipType)
                .AudioClip;
        }

        public AudioClip GetEffectAudioClip(EffectAudioClipType effectAudioClipType)
        {
            return _effectAudioClipItems
                .First(item => item.EffectAudioClipType == effectAudioClipType)
                .AudioClip;
        }
    }
}
