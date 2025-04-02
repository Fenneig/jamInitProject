using Jam.Scripts.Audio.Base.BaseAudioManager;
using Jam.Scripts.Audio.Base.BaseAudioSource;
using Jam.Scripts.Audio.SO;
using UnityEngine;

namespace Jam.Scripts.Audio.Sfx
{
    public class SfxAudioManager : AudioManager, ISfxAudioManager
    {
        private const string PLAYER_PREFS_NAME = "SoundEffectVolume";
        private const float DEFAULT_VOLUME = .5f;
        private const float PERCENT_VALUE = 100f;

        [SerializeField] private SoundsFactorySo _soundsFactory;

        protected override void SetPlayerPrefsName()
        {
            PlayerPrefsName = PLAYER_PREFS_NAME;
        }

        protected override void SetAudioSource()
        {
            GameAudioSource = new MultipleGameAudioSource(gameObject);
        }

        protected override void InitVolumeLevel()
        {
            Volume = PlayerPrefs.GetFloat(PLAYER_PREFS_NAME, DEFAULT_VOLUME);
            GameAudioSource.SetVolume(Volume);
        }

        private SoundElement GetRandomSoundByType(SoundType type)
        {
            return _soundsFactory.GetRandomClipByType(type);
        }

        public void PlaySoundByType(SoundType type, int soundIndex)
        {
            var soundElement = _soundsFactory.GetClipByTypeAndIndex(type, soundIndex);
            PlaySound(soundElement.Clip, soundElement.Volume);
        }

        public void PlayRandomSoundByType(
            SoundType type,
            bool isRandomPitch,
            bool isRandomChanceToPlay
        )
        {
            if (isRandomChanceToPlay)
            {
                var chance = Random.Range(0f, 1f);
                if (chance < .5f) return;
            }

            var soundElement = _soundsFactory.GetRandomClipByType(type);
            var pitch = isRandomPitch ? Random.Range(1, 2) : 1;
            PlaySoundWithPitch(soundElement.Clip, soundElement.Volume, pitch);
        }

        private void PlaySoundWithPitch(AudioClip clip, float clipVolume, int pitch)
        {
            var audioSource = GameAudioSource.GetAudioSource();
            audioSource.clip = clip;
            audioSource.volume = Volume * (clipVolume / PERCENT_VALUE);
            audioSource.pitch = pitch;
            audioSource.Play();
        }

        private void PlaySound(AudioClip clip, float clipVolume)
        {
            var audioSource = GameAudioSource.GetAudioSource();
            audioSource.clip = clip;
            audioSource.volume = Volume * (clipVolume / PERCENT_VALUE);
            audioSource.Play();
        }
    }
}