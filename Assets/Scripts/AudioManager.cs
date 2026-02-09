using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public int channelCount = 5;
    public AudioSource[] sfxChannels;
    public AudioSource[] musicChannels = new AudioSource[2];
    public static AudioManager Instance { get; private set; }
    public AudioClip[] sfxClips;
    public AudioClip[] musicClips;

    public const int SFX_BUTTON = 0;
    public const int SFX_CLAPS = 1;
    public const int SFX_SUCCESS = 2;
    public const int SFX_FAIL = 3;
    public const int SFX_TIMER_START = 4;
    public const int SFX_EXPLOSION = 5;
    public const int SFX_SPOTLIGHT = 6;
    public const int SFX_BOARD_SPAWN = 7;
    public const int SFX_TV_SWITCH = 8;
    public const int SFX_STAB = 9;
    public const int SFX_CURTAIN = 10;
    public const int SFX_START = 11;
    public const int SFX_ROPE_RIPPING = 12;
    public const int SFX_SWORD_TAKE = 13;
    public const int SFX_SWORD_GIVE = 14;
    public const int SFX_RAIL = 15;
    public const int SFX_RAIL_UP = 16;
    public const int SFX_GRUNT = 17;
    public const int SFX_CARDBOARD = 18;

    public const int MUSIC_DEFAULT = 0;
    public const int MUSIC_CLIMAX = 1;

    void Awake()
    {
        if (Instance != null)
        {
            throw new System.Exception("Only one AudioManager is allowed");
        }

        Instance = this;
        sfxChannels = new AudioSource[channelCount];
        for (int i = 0; i < channelCount; i++)
        {
            sfxChannels[i] = gameObject.AddComponent<AudioSource>();
        }
        for(int i = 0; i < musicChannels.Length; i++){
            musicChannels[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    public void Start()
    {
        CrossFadeMusic(musicClips[MUSIC_DEFAULT]);
    }

    public void PlaySound(AudioClip clip, float volume = 0.5f)
    {
        for (int i = 0; i < channelCount; i++)
        {
            if (!sfxChannels[i].isPlaying)
            {
                sfxChannels[i].volume = volume;
                sfxChannels[i].clip = clip;
                sfxChannels[i].Play();
                return;
            }
        }
    }

    public float musicFadeDuration = 1f;
    
    public void CrossFadeMusic(AudioClip clip, float volume = 0.3f) {
        AudioSource oldMusic;
        AudioSource newMusic;
        if(musicChannels[0].isPlaying){
            oldMusic = musicChannels[0];
            newMusic = musicChannels[1];
        } else if (musicChannels[1].isPlaying) {
            oldMusic = musicChannels[1];
            newMusic = musicChannels[0];
        }
        else {
            newMusic = musicChannels[0];
            newMusic.clip = clip;
            newMusic.volume = volume;
            newMusic.loop = true;
            newMusic.Play();
            return;
        }
        newMusic.clip = clip;
        newMusic.loop = true;
        newMusic.volume = 0f;
        newMusic.loop = true;
        newMusic.Play();
        
        // Fade out la vieille musique
        LeanTween.value(gameObject, oldMusic.volume, 0f, musicFadeDuration)
            .setOnUpdate((float val) => {
                oldMusic.volume = val;
            })
            .setOnComplete(() => {
                oldMusic.Stop();
            });
        
        // Fade in de la nueva música
        LeanTween.value(gameObject, 0f, volume, musicFadeDuration)
            .setOnUpdate((float val) => {
                newMusic.volume = val;
            });
    }

    public void StopMusic(){
        musicChannels[0].Stop();
        musicChannels[1].Stop();
    }
}
