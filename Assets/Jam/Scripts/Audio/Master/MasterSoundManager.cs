using Jam.Scripts.Audio.Base.BaseAudioManager;
using UnityEngine;

namespace Jam.Scripts.Audio.Master
{
    public class MasterSoundManager : AudioManager
    {
        private const string PLAYER_PREFS_NAME = "MasterSoundVolume";
        private const float DEFAULT_VOLUME = 1f;

        protected override void InitVolumeLevel()
        {
            Volume = PlayerPrefs.GetFloat(PLAYER_PREFS_NAME, DEFAULT_VOLUME);
        }

        protected override void SetPlayerPrefsName()
        {
            PlayerPrefsName = PLAYER_PREFS_NAME;
        }

        public override void ChangeVolumeLevel(float volume, float masterVolume)
        {
            Volume = volume;
        }

        protected override void SetAudioSource()
        {
            // no-op
        }
    }
}