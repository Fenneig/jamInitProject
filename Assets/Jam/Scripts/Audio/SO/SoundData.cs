using System;
using System.Collections.Generic;
using UnityEngine;

namespace Jam.Scripts.Audio.SO
{
    [Serializable]
    public class SoundData
    {
        [field:SerializeField] public SoundType Type { get; private set; } = SoundType.None;
        [field:SerializeField] public List<SoundElement> Clips { get; private set; }
    }
}