using System;
using UnityEngine;

namespace Jam.Scripts.Audio
{
    public class AudioSettings : MonoBehaviour
    {
        private const string BGM_PREFS_NAME = "BgmVolume";
        private const string SFX_PREFS_NAME = "SfxVolume";
        private const string MASTER_PREFS_NAME = "MasterSoundVolume";
        
        private const float BGM_DEFAULT_VOLUME = .5f;
        private const float SFX_DEFAULT_VOLUME = .5f;
        private const float MASTER_DEFAULT_VOLUME = 1f;
        
        public event Action<float> OnBgmVolumeChanged;
        public event Action<float> OnSfxVolumeChanged;
        public event Action OnMasterVolumeChanged;

        public float BgmVolume
        {
            get => PlayerPrefs.GetFloat(BGM_PREFS_NAME);
            set
            {
                PlayerPrefs.SetFloat(BGM_PREFS_NAME, value * MasterVolume);
                PlayerPrefs.Save();
                OnBgmVolumeChanged?.Invoke(value);
            }
        }
        
        public float SfxVolume
        {
            get => PlayerPrefs.GetFloat(SFX_PREFS_NAME);
            set
            {
                PlayerPrefs.SetFloat(SFX_PREFS_NAME, value * MasterVolume);
                PlayerPrefs.Save();
                OnSfxVolumeChanged?.Invoke(value);
            }
        }
        
        public float MasterVolume
        {
            get => PlayerPrefs.GetFloat(MASTER_PREFS_NAME);
            set
            {
                PlayerPrefs.SetFloat(MASTER_PREFS_NAME, value);
                PlayerPrefs.Save();
                OnMasterVolumeChanged?.Invoke();
            }
        }
        
        private void Awake()
        {
            BgmVolume = BGM_DEFAULT_VOLUME;
            SfxVolume = SFX_DEFAULT_VOLUME;
            MasterVolume = MASTER_DEFAULT_VOLUME;
        }
    }
}