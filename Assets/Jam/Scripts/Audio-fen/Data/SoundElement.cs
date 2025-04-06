using System;
using UnityEngine;

namespace Jam.Scripts.Audio_fen.Data
{
    [Serializable]
    public class SoundElement
    {
        [SerializeField] private float _volume = 100f;
        [SerializeField] private AudioClip _clip;

        public SoundElement(AudioClip clip)
        {
            _clip = clip;
        }
        
        public float Volume => _volume;

        public AudioClip Clip => _clip;
    }
}