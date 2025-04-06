using Jam.Scripts.Audio_fen.Data;
using UnityEngine;
using Zenject;

namespace Jam.Scripts.Audio_fen
{
    public class SoundInstaller : MonoInstaller
    {
        [SerializeField] private AudioService _audioServicePrefab;
        [SerializeField] private SoundRepository _soundRepository;
        
        public override void InstallBindings()
        {
            AudioSettingsInstall();
            SoundServiceInstall();
        }
        
        private void AudioSettingsInstall()
        {
            Container.Bind<PersistentAudioSettings>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
        
        private void SoundServiceInstall()
        {
            Container.Bind<AudioService>()
                .FromComponentInNewPrefab(_audioServicePrefab)
                .AsSingle()
                .WithArguments(_soundRepository)
                .NonLazy();
        }
    }
}
