using UnityEngine;

public class Messages {
    public struct Audio
    {
        public AudioClip clip;
        public enum COMMAND {
            PLAY_SOUND,
            PLAY_MUSIC,
            PAUSE_MUSIC,
            STOP_SOUND,
            STOP_MUSIC
        };
        public COMMAND commandType;
    }
    public enum GAME_STATE {
        PLAYING,
        PAUSED,
        QUIT
    };
}
