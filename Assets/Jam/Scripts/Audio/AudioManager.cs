using System.Collections.Generic;
using System.Linq;
using Jam.Scripts.Audio.SO;
using UnityEngine;

namespace Jam.Scripts.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private SoundsRepositorySo soundsRepository;
        [SerializeField] private AudioSettings _audioSettings;

        private List<SoundSource> _sfxSoundSources;
        private SoundSource _bgmSoundSource;

        private bool _isIntroPlaying = false;
        private BgmAudioType _currentBgmClipType = BgmAudioType.None;
        private BgmAudioType _nextBgmClipTypeToPlay = BgmAudioType.None;

        private void Awake()
        {
            _sfxSoundSources = new List<SoundSource>();
            _bgmSoundSource = GetAvailableSoundSource();
            _audioSettings.OnMasterVolumeChanged += OnMasterVolumeChanged;
            _audioSettings.OnSfxVolumeChanged += OnSfxVolumeChanged;
            _audioSettings.OnBgmVolumeChanged += OnBgmVolumeChanged;
        }

        private void OnDestroy()
        {
            _audioSettings.OnMasterVolumeChanged -= OnMasterVolumeChanged;
            _audioSettings.OnSfxVolumeChanged -= OnSfxVolumeChanged;
            _audioSettings.OnBgmVolumeChanged -= OnBgmVolumeChanged;
        }

        private void OnMasterVolumeChanged()
        {
            OnSfxVolumeChanged(_audioSettings.SfxVolume);
            OnBgmVolumeChanged(_audioSettings.BgmVolume);
        }

        private void OnSfxVolumeChanged(float volume) =>
            _sfxSoundSources.ForEach(soundSource => soundSource.UpdateSettingsVolume(volume));

        private void OnBgmVolumeChanged(float volume) => _bgmSoundSource.UpdateSettingsVolume(volume);

        private void Update()
        {
            var isClipNotEnded = _bgmSoundSource.AudioSource.isPlaying || _bgmSoundSource.AudioSource.time != 0;
            if (isClipNotEnded || _currentBgmClipType == BgmAudioType.None) return;

            if (_isIntroPlaying)
            {
                _isIntroPlaying = false;
                StartBgm();
                return;
            }

            SelectNextBgmSoundAndPlayInLoop();
        }

        public void PlaySfx(SoundType type, int soundIndex)
        {
            var soundSource = GetAvailableSoundSource();
            var soundElement = soundsRepository.GetSoundElementByTypeAndIndex(type, soundIndex);
            soundSource.SetSoundElement(soundElement);
            soundSource.UpdateSettingsVolume(_audioSettings.SfxVolume);
            soundSource.AudioSource.Play();
        }

        public void PlayBgm(BgmAudioType type)
        {
            _currentBgmClipType = type;
            var intro = GetIntroByType(type);
            _isIntroPlaying = true;
            PlayBgmSoundInLoop(intro, _currentBgmClipType);
        }

        public void SetNextBgmClipToPlay(BgmAudioType type) => _nextBgmClipTypeToPlay = type;

        private void PlayBgmSoundInLoop(SoundElement soundElement, BgmAudioType bgmAudioType)
        {
            var soundSource = _bgmSoundSource;
            soundSource.SetSoundElement(soundElement);
            soundSource.UpdateSettingsVolume(_audioSettings.BgmVolume);
            soundSource.AudioSource.Play();
            _currentBgmClipType = bgmAudioType;
        }

        private void StartBgm()
        {
            var bgmAudio = GetBgmByType(_currentBgmClipType);
            PlayBgmSoundInLoop(bgmAudio, _currentBgmClipType);
        }

        private void SelectNextBgmSoundAndPlayInLoop()
        {
            var hasNewClipToPlay = _nextBgmClipTypeToPlay != BgmAudioType.None &&
                                   _currentBgmClipType != _nextBgmClipTypeToPlay;
            var clipTypeToPlay = hasNewClipToPlay ? _nextBgmClipTypeToPlay : _currentBgmClipType;
            var clipToPlay = GetBgmByType(clipTypeToPlay);
            PlayBgmSoundInLoop(clipToPlay, 0);
        }

        private SoundSource GetAvailableSoundSource()
        {
            foreach (SoundSource instance in _sfxSoundSources.Where(instance => !instance.AudioSource.isPlaying))
            {
                return instance;
            }

            AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
            SoundSource soundSource = new SoundSource(newAudioSource);
            _sfxSoundSources.Add(soundSource);
            return soundSource;
        }

        private SoundElement GetIntroByType(BgmAudioType bgmAudioType)
        {
            return bgmAudioType switch
            {
                BgmAudioType.Menu => soundsRepository.GetSoundElementByTypeAndIndex(SoundType.MenuIntro, 0),
                BgmAudioType.Gameplay => soundsRepository.GetSoundElementByTypeAndIndex(SoundType.GameplayIntro, 0),
                _ => null
            };
        }

        private SoundElement GetBgmByType(BgmAudioType bgmAudioType)
        {
            return bgmAudioType switch
            {
                BgmAudioType.Menu => soundsRepository.GetSoundElementByTypeAndIndex(SoundType.MenuBgm, 0),
                BgmAudioType.Gameplay => soundsRepository.GetSoundElementByTypeAndIndex(SoundType.GameplayBgm, 0),
                _ => null
            };
        }
    }
}