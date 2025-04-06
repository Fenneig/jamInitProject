using System;
using UnityEngine;

namespace Jam.Scripts.Audio_fen.Data
{
    public class PersistentAudioSettings
    {
        public float SoundVolume { get; private set; }
        public float MusicVolume { get; private set; }
        
        private const string SOUND_VOLUME_KEY = "SoundVolume";
        private const string MUSIC_VOLUME_KEY = "MusicVolume";
        
        private const float DEFAULT_SOUND_VOLUME = .5f;
        private const float DEFAULT_MUSIC_VOLUME = .5f;

        public event Action<float> OnSoundVolumeChanged, OnMusicVolumeChanged; 
        
        public PersistentAudioSettings()
        {
            SoundVolume = PlayerPrefs.HasKey(SOUND_VOLUME_KEY) ? PlayerPrefs.GetFloat(SOUND_VOLUME_KEY) : DEFAULT_SOUND_VOLUME;
            MusicVolume = PlayerPrefs.HasKey(MUSIC_VOLUME_KEY) ? PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY) : DEFAULT_MUSIC_VOLUME;
        }

        public void SaveSettings()
        {
            PlayerPrefs.SetFloat(SOUND_VOLUME_KEY, SoundVolume);
            PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, MusicVolume);
        }

        public void SetSoundVolume(float volume)
        {
            SoundVolume = volume;
            OnSoundVolumeChanged?.Invoke(SoundVolume);
        }

        public void SetMusicVolume(float volume)
        {
            MusicVolume = volume;
            OnMusicVolumeChanged?.Invoke(MusicVolume);
        }
    }
}
