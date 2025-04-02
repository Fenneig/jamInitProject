using Jam.Scripts.Audio.Base.BaseAudioSource;
using UnityEngine;

namespace Jam.Scripts.Audio.Base.BaseAudioManager
{
    public abstract class AudioManager : MonoBehaviour, IAudioManager
    {
        protected GameAudioSource GameAudioSource;
        protected string PlayerPrefsName;
        protected abstract void SetAudioSource();
        protected abstract void InitVolumeLevel();
        protected abstract void SetPlayerPrefsName();

        public float Volume { get; protected set; }
        
        private void Awake()
        {
            SetPlayerPrefsName();
            SetAudioSource();
            InitVolumeLevel();
        }

        public virtual void ChangeVolumeLevel(float volume, float masterVolume)
        {
            Volume = volume;
            if (GameAudioSource.GetAudioSource() != null)
            {
                GameAudioSource.SetVolume(Volume * masterVolume);
            }
        }

        public void SaveCurrentValue()
        {
            PlayerPrefs.SetFloat(PlayerPrefsName, Volume);
            PlayerPrefs.Save();
        }

        public void RestorePrevValue(float masterVolume)
        {
            var prevVolume = PlayerPrefs.GetFloat(PlayerPrefsName, Volume);
            ChangeVolumeLevel(prevVolume, masterVolume);
            SaveCurrentValue();
        }
    }
}