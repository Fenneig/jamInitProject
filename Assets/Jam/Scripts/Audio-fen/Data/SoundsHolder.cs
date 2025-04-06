using System.Collections.Generic;
using UnityEngine;

namespace Jam.Scripts.Audio_fen.Data
{
    [CreateAssetMenu(fileName = "SoundsHolder", menuName = "Audio/SoundsHolder")]
    public class SoundsHolder : ScriptableObject
    {
        [field: SerializeField] public List<AudioClip> Sounds { get; private set; }
    }
}
