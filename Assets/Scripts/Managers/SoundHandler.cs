using UniRx;
using UnityEngine;

public class SoundHandler : MonoBehaviour{
    [SerializeField]
    AudioSource sfxSource;
    [SerializeField]
    AudioSource musicSource;

    [Header("Looped music clip")]
    AudioClip musicClip;

    private void Start() {
        MessageBroker.Default.Receive<Messages.Audio>()
        .Where(x => x.commandType == Messages.Audio.COMMAND.PLAY_SOUND)
        .Subscribe(x => PlaySound(x.clip));

        MessageBroker.Default.Receive<Messages.Audio>()
        .Where(x => x.commandType == Messages.Audio.COMMAND.STOP_SOUND)
        .Subscribe(x => PlaySound(x.clip));


        MessageBroker.Default.Receive<Messages.Audio>()
        .Where(x => x.commandType == Messages.Audio.COMMAND.PLAY_MUSIC)
        .Subscribe(x => PlayMusic());

        MessageBroker.Default.Receive<Messages.Audio>()
        .Where(x => x.commandType == Messages.Audio.COMMAND.PAUSE_MUSIC)
        .Subscribe(x => PauseMusic());

        MessageBroker.Default.Receive<Messages.Audio>()
        .Where(x => x.commandType == Messages.Audio.COMMAND.STOP_MUSIC)
        .Subscribe(x => StopMusic());
    }
    public void PlaySound(AudioClip clip) {
        sfxSource.clip = clip;
        sfxSource.Play();
    }

    public void StopMusic() {
        musicSource.Stop();
    }

    public void PlayMusic() {
        if (musicSource.clip != null) {
            musicSource.Play();
        }
    }

    public void PauseMusic() {
        if (musicSource.isPlaying) {
            musicSource.Pause();
        }
    }
}