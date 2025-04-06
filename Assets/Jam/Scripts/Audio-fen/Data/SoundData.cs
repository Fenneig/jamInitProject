using System;
using System.Collections.Generic;
using UnityEngine;

namespace Jam.Scripts.Audio_fen.Data
{
    [Serializable]
    public class SoundData
    {
        [SerializeField] private string _name;
        [SerializeField] private SoundType _type;
        [SerializeField] private List<SoundElement> _clips;

        public SoundData(string name, SoundType type, List<SoundElement> clips)
        {
            _name = name;
            _type = type;
            _clips = clips;
        }
        public string Name => _name;
        public SoundType Type => _type;
        public List<SoundElement> Clips => _clips;
    }
}