using System;
using UnityEngine;

namespace Jam.Scripts.Audio.SO
{
    [Serializable]
    public class SoundElement
    {
        [SerializeField] private float _volume = 100;
        [SerializeField] private AudioClip _clip;

        public float Volume => _volume;

        public AudioClip Clip => _clip;
    }
}