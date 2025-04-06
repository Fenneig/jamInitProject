using UnityEngine;
using Zenject;

namespace Jam.Scripts.Audio_fen
{
    public class TestSound : MonoBehaviour
    {
        [Inject] private AudioService _audioService;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                _audioService.PlaySound("MiniGame");
        }
    }
}
