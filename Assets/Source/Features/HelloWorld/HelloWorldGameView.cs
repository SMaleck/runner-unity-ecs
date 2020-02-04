using Source.Services.AudioPlayer;
using UGF.Views;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Features.HelloWorld
{
    public class HelloWorldGameView : AbstractView
    {
        public class Factory : PlaceholderFactory<UnityEngine.Object, HelloWorldGameView> { }

        [Header("Audio Test")]
        [SerializeField] private Button _playSoundEffectButton;
        [SerializeField] private Button _playSoundEffectRandomizedButton;
        [SerializeField] private Button _toggleMusicButton;

        private IAudioPlayerService _audioPlayerService;

        [Inject]
        private void Inject(IAudioPlayerService audioPlayerService)
        {
            _audioPlayerService = audioPlayerService;
        }

        public override void OnInitialize()
        {
            _playSoundEffectButton.OnClickAsObservable()
                .Subscribe(_ => _audioPlayerService.PlayEffect(EffectAudioClipType.Default))
                .AddTo(Disposer);

            _playSoundEffectRandomizedButton.OnClickAsObservable()
                .Subscribe(_ => _audioPlayerService.PlayEffectRandomized(EffectAudioClipType.Default))
                .AddTo(Disposer);

            _toggleMusicButton.OnClickAsObservable()
                .Subscribe(_ => _audioPlayerService.PlayMusic(MusicAudioClipType.Default))
                .AddTo(Disposer);
        }
    }
}
