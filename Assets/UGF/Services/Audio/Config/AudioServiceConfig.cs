using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UGF.Services.Audio.Config
{
    [CreateAssetMenu(fileName = nameof(AudioServiceConfig), menuName = UgfConstants.UMenuRoot + nameof(AudioServiceConfig))]
    public class AudioServiceConfig : ScriptableObject
    {
        [Serializable]
        private class AudioChannelConfig
        {
            [SerializeField] private AudioChannel _audioChannel;
            public AudioChannel AudioChannel => _audioChannel;

            [SerializeField] private float _defaultVolume;
            public float DefaultVolume => _defaultVolume;
        }

        [SerializeField] private AudioSource _audioSourcePrefab;
        public AudioSource AudioSourcePrefab => _audioSourcePrefab;

        [SerializeField] private float _minPitch;
        public float MinPitch => _minPitch;

        [SerializeField] private float _maxPitch;
        public float MaxPitch => _maxPitch;

        [SerializeField] private List<AudioChannelConfig> _audioChannelConfigs;

        public float GetDefaultVolume(AudioChannel audioChannel)
        {
            return _audioChannelConfigs
                .First(config => config.AudioChannel == audioChannel)
                .DefaultVolume;
        }
    }
}
