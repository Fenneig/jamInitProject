using System;

namespace Jam.Scripts.Audio.Sfx
{
    public interface ISfxAudioManager
    {
        void PlaySoundByType(SoundType type, int soundIndex);

        void PlayRandomSoundByType(
            SoundType type,
            bool isRandomPitch,
            bool isRandomChanceToPlay
        );
    }
}