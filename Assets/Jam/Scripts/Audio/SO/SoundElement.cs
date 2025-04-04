using System;
using UnityEngine;

namespace Jam.Scripts.Audio.SO
{
    [Serializable]
    public class SoundElement
    {
        [field: SerializeField] public float Volume { get; private set; } = 100;
        [field:SerializeField] public AudioClip Clip { get; private set; }
    }
}