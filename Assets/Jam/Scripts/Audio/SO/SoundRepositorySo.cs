using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Jam.Scripts.Audio.SO
{
    [CreateAssetMenu(menuName = "Gameplay/Audio/SoundsRepositorySO", fileName = "SoundsRepositorySO")]
    public class SoundsRepositorySo : ScriptableObject
    {
        [SerializeField] private List<SoundData> _soundDataList;

        public SoundElement GetRandomSoundElementByType(SoundType type)
        {
            SoundElement clipToReturn = null;
            var audioList = _soundDataList.FirstOrDefault(el => el.Type == type);
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

        public SoundElement GetSoundElementByTypeAndIndex(SoundType type, int index)
        {
            SoundElement clipToReturn = null;
            var audioList = _soundDataList.FirstOrDefault(el => el.Type == type);

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