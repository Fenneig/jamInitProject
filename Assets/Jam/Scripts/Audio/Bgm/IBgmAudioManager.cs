namespace Jam.Scripts.Audio.Bgm
{
    public interface IBgmAudioManager
    {
        void PlayBgmWithIntro(BgmAudioType audioType);
        void SetNextClipToPlay(BgmAudioType audioType);
    }
}