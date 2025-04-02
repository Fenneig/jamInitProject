using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Jam.Scripts.Audio.SO
{
    [CreateAssetMenu(menuName = "Gameplay/Audio/SoundsFactorySO", fileName = "SoundsFactorySO")]
    public class SoundsFactorySo : ScriptableObject
    {
        [SerializeField] private float _intervalBetweenAmbientSfx;
        [SerializeField] private List<SoundData> _clips;
        
        public float IntervalBetweenAmbientSfx => _intervalBetweenAmbientSfx;

        public SoundElement GetRandomClipByType(SoundType type)
        {
            SoundElement clipToReturn = null;
            var audioList = _clips.FirstOrDefault(el => el.Type == type);
            if (audioList != null)
            {
                clipToReturn = audioList.Clips[Random.Range(0, audioList.Clips.Count)];
            }

            if (clipToReturn == null)
            {
                Debug.LogError("Clip not found: " + type);
            }

            return clipToReturn;
        }

        public SoundElement GetClipByTypeAndIndex(SoundType type, int index)
        {
            SoundElement clipToReturn = null;
            var audioList = _clips.FirstOrDefault(el => el.Type == type);

            if (audioList != null)
            {
                clipToReturn = audioList.Clips[index];
            }

            if (clipToReturn == null)
            {
                Debug.LogError("Clip not found: " + type);
            }

            return clipToReturn;
        }
    }
}