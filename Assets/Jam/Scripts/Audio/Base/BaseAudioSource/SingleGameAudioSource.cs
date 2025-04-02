using UnityEngine;

namespace Jam.Scripts.Audio.Base.BaseAudioSource
{
    public class SingleGameAudioSource: GameAudioSource
    {
        private AudioSource _audioSource;
        public SingleGameAudioSource(GameObject audioManagerGameObject) : base(audioManagerGameObject)
        {
            _audioSource = audioManagerGameObject.GetComponent<AudioSource>();
        }

        public override AudioSource GetAudioSource() => _audioSource;

        public override void SetVolume(float volume) => _audioSource.volume = volume;
    }
}