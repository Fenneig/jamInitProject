using Jam.Scripts.Audio.Base.BaseAudioManager;
using Jam.Scripts.Audio.Base.BaseAudioSource;
using Jam.Scripts.Audio.SO;
using UnityEngine;

namespace Jam.Scripts.Audio.Bgm
{
    public class BgmAudioManager : AudioManager, IBgmAudioManager
    {
        private const string PLAYER_PREFS_NAME = "MusicVolume";
        private const float DEFAULT_VOLUME = .5f;
        private const float PERCENT_VALUE = 100f;

        [SerializeField] private SoundsFactorySo _soundsFactory;

        private bool _isIntroPlaying = false;
        private BgmAudioType _currentClipType = BgmAudioType.None;
        private BgmAudioType _nextClipTypeToPlay = BgmAudioType.None;

        protected override void SetPlayerPrefsName()
        {
            PlayerPrefsName = PLAYER_PREFS_NAME;
        }

        protected override void SetAudioSource()
        {
            GameAudioSource = new SingleGameAudioSource(gameObject);
        }

        protected override void InitVolumeLevel()
        {
            Volume = PlayerPrefs.GetFloat(PLAYER_PREFS_NAME, DEFAULT_VOLUME);
            GameAudioSource.SetVolume(Volume);
        }

        private void Update()
        {
            var isClipNotEnded =
                GameAudioSource.GetAudioSource().isPlaying || GameAudioSource.GetAudioSource().time != 0;
            if (isClipNotEnded || _currentClipType == BgmAudioType.None) return;

            if (_isIntroPlaying)
            {
                _isIntroPlaying = false;
                StartBgm();
                return;
            }

            SelectNextClipAndPlayInLoop();
        }

        public void PlayBgmWithIntro(BgmAudioType audioType)
        {
            _currentClipType = audioType;
            var intro = GetIntroByType(audioType);
            StartIntro(intro, audioType);
        }

        public void SetNextClipToPlay(BgmAudioType audioType) => _nextClipTypeToPlay = audioType;

        private void SelectNextClipAndPlayInLoop()
        {
            var clipTypeToPlay = _nextClipTypeToPlay != BgmAudioType.None && _currentClipType != _nextClipTypeToPlay
                ? _nextClipTypeToPlay
                : _currentClipType;
            var clipToPlay = GetBgmByType(clipTypeToPlay);
            PlaySoundByType(clipToPlay, 0);
        }

        private void StartIntro(SoundElement soundElement, BgmAudioType type)
        {
            _isIntroPlaying = true;
            PlaySoundByType(soundElement, type);
        }

        private void StartBgm()
        {
            var bgmAudio = GetBgmByType(_currentClipType);
            PlaySoundByType(bgmAudio, _currentClipType);
        }

        private void PlaySoundByType(SoundElement soundElement, BgmAudioType bgmAudioType)
        {
            var audioSource = GameAudioSource.GetAudioSource();
            audioSource.clip = soundElement.Clip;
            audioSource.volume = Volume * (soundElement.Volume / PERCENT_VALUE);
            audioSource.loop = false;
            audioSource.Play();
            _currentClipType = bgmAudioType;
        }

        private SoundElement GetIntroByType(BgmAudioType bgmAudioType)
        {
            return bgmAudioType switch
            {
                BgmAudioType.Menu => _soundsFactory.GetClipByTypeAndIndex(SoundType.MenuIntro, 0),
                BgmAudioType.Gameplay => _soundsFactory.GetClipByTypeAndIndex(SoundType.GameplayIntro, 0),
                _ => null
            };
        }

        private SoundElement GetBgmByType(BgmAudioType bgmAudioType)
        {
            return bgmAudioType switch
            {
                BgmAudioType.Menu => _soundsFactory.GetClipByTypeAndIndex(SoundType.MenuBgm, 0),
                BgmAudioType.Gameplay => _soundsFactory.GetClipByTypeAndIndex(SoundType.GameplayBgm, 0),
                _ => null
            };
        }
    }
}