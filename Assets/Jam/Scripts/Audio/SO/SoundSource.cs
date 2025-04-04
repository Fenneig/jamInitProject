using UnityEngine;

namespace Jam.Scripts.Audio.SO
{
    public class SoundSource
    {
        private const float PERCENT_VALUE = 100f;

        public AudioSource AudioSource { get; }
        private SoundElement SoundElement { get; set; }

        public SoundSource(AudioSource audioSource)
        {
            AudioSource = audioSource;
        }

        public void SetSoundElement(SoundElement soundElement)
        {
            SoundElement = soundElement;
            AudioSource.clip = soundElement.Clip;
        }

        public void UpdateSettingsVolume(float settingsVolume)
        {
            if (SoundElement == null) return;
            AudioSource.volume = settingsVolume * (SoundElement.Volume / PERCENT_VALUE);
        }
    }
}