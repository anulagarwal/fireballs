using UniRx;
using UnityEngine;
using System.Collections.Generic;
public class SoundHandler : MonoBehaviour{

    [System.Serializable]
    public class Sound
    {
        public AudioClip clip;
        public SoundType type;
    }

    public static SoundHandler Instance = null;

    [SerializeField]
    AudioSource sfxSource;
    [SerializeField]
    AudioSource musicSource;

    [Header("Looped music clip")]
    [SerializeField] List<Sound> sounds;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }
    private void Start() {
       /* MessageBroker.Default.Receive<AudioMessage>()
        .Where(x => x.commandType == AudioMessage.COMMAND.PAUSE_MUSIC)
        .Subscribe(x => PlaySound(x.clip));

        MessageBroker.Default.Receive<AudioMessage>()
        .Where(x => x.commandType == AudioMessage.COMMAND.PLAY_SOUND)
        .Subscribe(x => PlaySound(x.clip));


        MessageBroker.Default.Receive<AudioMessage>()
        .Where(x => x.commandType == AudioMessage.COMMAND.PLAY_MUSIC)
        .Subscribe(x => PlayMusic());

        MessageBroker.Default.Receive<AudioMessage>()
        .Where(x => x.commandType == AudioMessage.COMMAND.PAUSE_MUSIC)
        .Subscribe(x => PauseMusic());

        MessageBroker.Default.Receive<AudioMessage>()
        .Where(x => x.commandType == AudioMessage.COMMAND.STOP_MUSIC)
        .Subscribe(x => StopMusic());*/
    }
    public void PlaySound(SoundType type) {
        
        sfxSource.clip = sounds.Find( x=> x.type == type).clip;
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