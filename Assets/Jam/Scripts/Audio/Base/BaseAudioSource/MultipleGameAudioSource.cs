using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Jam.Scripts.Audio.Base.BaseAudioSource
{
    public class MultipleGameAudioSource : GameAudioSource
    {
        private List<AudioSource> _audioSources;
        private float _volume;

        public MultipleGameAudioSource(GameObject audioManagerGameObject) : base(audioManagerGameObject)
        {
            _audioSources = new List<AudioSource>();
        }

        public override AudioSource GetAudioSource()
        {
            foreach (AudioSource source in _audioSources.Where(source => !source.isPlaying))
            {
                return source;
            }
            
            AudioSource newAudioSource = AudioManagerGameObject.AddComponent<AudioSource>();
            newAudioSource.volume = _volume;
            _audioSources.Add(newAudioSource);
            return newAudioSource;
        }

        public override void SetVolume(float volume)
        {
            _volume = volume;
            _audioSources.ForEach(source => source.volume = _volume);
        } 
        
    }
}