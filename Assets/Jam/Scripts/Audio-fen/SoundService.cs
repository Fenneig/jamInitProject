using System.Collections.Generic;
using System.Linq;
using Jam.Scripts.Audio_fen.Data;
using UnityEngine;
using Zenject;

namespace Jam.Scripts.Audio_fen
{
    public class SoundService : MonoBehaviour
    {
        [Inject] private SoundRepository _soundRepository;
        [Inject] private PersistentAudioSettings _persistentAudioSettings;
        
        private AudioSource _musicSource;
        private List<AudioSource> _soundSources = new List<AudioSource>();
        private Dictionary<string, SoundElement> _cachedClips = new Dictionary<string, SoundElement>();
        
        private SoundElement _nextMusicClip;

        public void PlaySound(string clipName)
        {
            SoundElement clip = FindClip(clipName, SoundType.Effect);
            
            if (clip == null)
                return;

            AudioSource source = GetSource();
            
            SetSoundClip(source, clip);
        }
        
        private AudioSource GetSource()
        {
            foreach (AudioSource soundSource in _soundSources.Where(soundSource => !soundSource.isPlaying))
                return soundSource;

            return AddNewSoundSource();
        }

        public void PlayMusic(string clipName, bool instant = false)
        {
            SoundElement clip = FindClip(clipName, SoundType.Music);

            if (clip == null)
                return;
            
            if (instant || _musicSource.isPlaying == false)
            {
                SetMusicClip(clip);
            }
            else
            {
                _musicSource.loop = false;
                _nextMusicClip = clip;
            }
        }
        
        private SoundElement FindClip(string clipName, SoundType soundType)
        {
            SoundElement clip;

            if (_cachedClips.TryGetValue(clipName, out SoundElement cachedClip))
            {
                clip = cachedClip;
            }
            else
            {
                clip = _soundRepository.GetClip(clipName, soundType);
                
                if (clip == null)
                    return null;
                
                _cachedClips.Add(clipName, clip);
            }
            return clip;
        }

        private void SetSoundClip(AudioSource soundSource, SoundElement clip)
        {
            soundSource.clip = clip.Clip;
            soundSource.volume = _persistentAudioSettings.SoundVolume * clip.Volume / 100f;
            soundSource.Play();
        }
        
        private void SetMusicClip(SoundElement clip)
        {
            _musicSource.clip = clip.Clip;
            _musicSource.volume = _persistentAudioSettings.MusicVolume * clip.Volume / 100f;
            _musicSource.loop = true;
            _musicSource.Play();
        }

        private AudioSource AddNewSoundSource()
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            _soundSources.Add(source);
            return source;
        }
        
        private void Awake()
        {
            _musicSource = gameObject.AddComponent<AudioSource>();
            _musicSource.loop = true;
            _musicSource.playOnAwake = false;

            for (int i = 0; i < 5; i++)
            {
                AddNewSoundSource();
            }
        }

        private void Update()
        {
            if (_musicSource.loop ||
                _musicSource.isPlaying ||
                _nextMusicClip == null)
                return;
            
            SetMusicClip(_nextMusicClip);
            _nextMusicClip = null;
        }
    }
}
