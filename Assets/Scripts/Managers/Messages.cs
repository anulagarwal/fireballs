using UnityEngine;

public class Messages {
    public enum COMMAND{};

    public COMMAND commandType {get;set;}
}
    
public class AudioMessage: Messages
{
    public AudioClip clip;
    public enum COMMAND {
        PLAY_SOUND,
        PLAY_MUSIC,
        PAUSE_MUSIC,
        STOP_SOUND,
        STOP_MUSIC
    };

    public AudioMessage(COMMAND _command, AudioClip _clip = null) {
        commandType = _command;

        if (_clip != null) {
            clip = _clip;
        }
    }
    public COMMAND commandType {get;set;}
}
public class GamePlayMessage: Messages
{
    public enum COMMAND {
        PLAYING,
        RESTART,
        PAUSED,
        CONTINUE,
        QUIT
    };
    public COMMAND commandType {get;set;}
    public GamePlayMessage(COMMAND _command) {
        commandType = _command;
    }
}
