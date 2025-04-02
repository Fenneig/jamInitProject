using UnityEngine;

namespace Jam.Scripts.Audio.Base.BaseAudioSource
{
    public abstract class GameAudioSource
    {
        public abstract AudioSource GetAudioSource();
        public abstract void SetVolume(float volume);

        protected GameObject AudioManagerGameObject;
        protected GameAudioSource(GameObject audioManagerGameObject)
        {
            AudioManagerGameObject = audioManagerGameObject;
        }
    }
}