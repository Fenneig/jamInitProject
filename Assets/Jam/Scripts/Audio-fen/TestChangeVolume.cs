using Jam.Scripts.Audio_fen.Data;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Jam.Scripts.Audio_fen
{
    public class TestChangeVolume : MonoBehaviour
    {
        [SerializeField] private Slider _masterSlider;
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _musicSlider;
        [Inject] private PersistentAudioSettings _settings;

        private void Awake()
        {
            _masterSlider.onValueChanged.AddListener(_settings.SetMasterVolume);
            _soundSlider.onValueChanged.AddListener(_settings.SetSoundVolume);
            _musicSlider.onValueChanged.AddListener(_settings.SetMusicVolume);
        }
    }
}
