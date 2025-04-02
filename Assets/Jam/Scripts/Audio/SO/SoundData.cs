using System;
using System.Collections.Generic;
using UnityEngine;

namespace Jam.Scripts.Audio.SO
{
    [Serializable]
    public class SoundData
    {
        [SerializeField] private SoundType _type;
        [SerializeField] private List<SoundElement> _clips;

        public SoundType Type => _type;
        public List<SoundElement> Clips => _clips;
    }
}