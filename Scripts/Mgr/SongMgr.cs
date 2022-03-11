using UnityEngine;

public class SongMgr : UnitySingleton<SongMgr>
{
    const int Max_Sounds = 8;
    [HideInInspector]
    public AudioSource music;
    [HideInInspector]
    public AudioSource[] sounds = new AudioSource[Max_Sounds];

    private int music_muted;
    private int sounds_muted;

    private float music_volumn;
    private float sounds_volumn;

    private int soundid;
    private int soundid_up;
    private bool soundchange;
    private bool allsounds;
    private bool singlesound;
    private int playing_soundid = 0;
    public bool clearEndSounds;

    public LerpZeroToOne songlerp;
    public LerpZeroToOne soundlerp;

    private void Update()
    {
        if (songlerp.start)
        {
            songlerp.Update();
            music.volume = songlerp.value* music_volumn;
            //Debug.Log(music.volume);
            //if(songlerp.value > 1)
            //{
            //    //EventMgr.Instance.dispatch_event("SongLerp", 1);
            //}
            //if(songlerp.value < 0)
            //{
            //    //EventMgr.Instance.dispatch_event("SongLerp", 0);
            //}
        }
        if(soundlerp.start)
        {
            soundlerp.Update();
            if(allsounds)
            {
                SlowChangeAllSounds(soundlerp.value);
                
            }
            if(singlesound)
            {
                switch (soundchange)
                {
                    case true:
                        sounds[soundid].volume = soundlerp.value * sounds_volumn;
                        sounds[soundid_up].volume = (1 - soundlerp.value) * sounds_volumn;
                        break;
                    case false:
                        sounds[soundid].volume = soundlerp.value * sounds_volumn;
                        break;
                }
                if (soundlerp.value < 0 || soundlerp.value > 1)
                {
                    singlesound = false;
                    if (soundlerp.value < 0)
                    {
                        StopSingleSound(soundid);
                        soundchange = false;
                    }
                }
            }
        }
        if(clearEndSounds)
        {
            for (int i = 0; i < Max_Sounds; i++)
            {
                if(sounds[i].clip != null)
                {
                    if(sounds[i].isPlaying == false)
                    {                       
                        StopSingleSound(i);                    
                    }                                   
                }
            }
        }
    }
    public void SetAudio()
    {
        music_muted = PlayerPrefs.GetInt("music_muted", 0);
        sounds_muted = PlayerPrefs.GetInt("sounds_muted", 0);
        music_volumn = PlayerPrefs.GetFloat("music_volumn", 1.0f);
        sounds_volumn = PlayerPrefs.GetFloat("sounds_volumn", 1.0f);

        music = this.gameObject.AddComponent<AudioSource>();

        this.music.mute = (this.music_muted == 1);
        this.music.volume = (this.music_volumn);
          
        for(int i = 0; i< Max_Sounds; i++)
        {
            sounds[i] = this.gameObject.AddComponent<AudioSource>();
            this.sounds[i].mute = (this.sounds_muted == 1);
            this.sounds[i].volume = (this.sounds_volumn);
        }
        songlerp = new LerpZeroToOne
        {
            Speed = 1.0f,
            deltaT = 0.01f,
        };
        soundlerp = new LerpZeroToOne
        {
            Speed = 1.0f,
            deltaT = 0.01f,
        };
    }
    public void PlayMusic(string url,bool loop = true)
    {
        AudioClip clip = Resources.Load<AudioClip>("Musics/" + url);
        //if(clip != null)
        //{
        //    Debug.Log(clip.name);
        //}
        this.music.clip = clip;
        this.music.loop = loop;
        this.music.Play();
    }
    public void MusicVolumnDown(float speed = 0.01f)
    {
        songlerp.value = 1;
        songlerp.deltaT = -1;
        songlerp.Speed = speed;
        songlerp.start = true;
    }

    public void MusicVolumnUp(float speed = 0.01f)
    {
        songlerp.value = 0;
        songlerp.deltaT = 1;
        songlerp.Speed = speed;
        songlerp.start = true;
    }

    public int PlaySounds(string url,bool loop = false)
    {
        int soundid = this.playing_soundid;
        AudioClip clip = Resources.Load<AudioClip>("Sounds/" + url);
        for(int i = 0;i<Max_Sounds;i++)
        {
            if(this.sounds[i].clip == null)
            {
                this.playing_soundid = i;
                soundid = this.playing_soundid;
                break;
            }
            if(this.sounds[i].clip != null)
            {
                if(i < Max_Sounds)
                {
                    continue;
                }
                Debug.LogError("SoundsNotEnough");
            }
        }
        this.sounds[this.playing_soundid].clip = clip;
        this.sounds[this.playing_soundid].loop = loop;
        this.sounds[this.playing_soundid].volume = soundsVolumn;
        this.sounds[this.playing_soundid].Play();      
        return soundid;      
    }

    public void SoundVolumnDown(int down_id, float speed = 0.05f)
    {
        singlesound = true;
        soundid = down_id;
        soundlerp.value = 1;
        soundlerp.deltaT = -1;
        soundlerp.Speed = speed;
        soundlerp.start = true;
    }
    public void SoundVolumnUp(int up_id, float speed = 0.05f)
    {
        singlesound = true;
        soundid = up_id;
        soundlerp.value = 0;
        soundlerp.deltaT = 1;
        soundlerp.Speed = speed;
        soundlerp.start = true;
    }
    public void SoundVolumnChange(int down_id , int up_id, float speed = 0.05f)
    {
        singlesound = true;
        soundchange = true;
        soundid = down_id;
        soundid_up = up_id;
        soundlerp.value = 1;
        soundlerp.deltaT = -1;
        soundlerp.Speed = speed;
        soundlerp.start = true;
    }

    public void StopSingleSound(int sound_id)
    {
        if(sound_id < 0 || sound_id > this.sounds.Length)
        {
            return;
        }
        this.sounds[sound_id].Stop();
        this.sounds[sound_id].clip = null;
    }
    public void StopMusic()
    {
        this.music.Stop();
        this.music.clip = null;
    }

    public void StopAllSound()
    {
        for (int i = 0; i < this.sounds.Length; i++)
        {
            this.sounds[i].Stop();
            this.sounds[i].clip = null;
        }
    }
    public void AllSoundsDown(float speed = 0.01f)
    {
        allsounds = true;
        soundlerp.value = soundsVolumn;
        soundlerp.deltaT = -1;
        soundlerp.Speed = speed;
        soundlerp.start = true;
    }
    public void AllSoundsUp(float speed = 0.01f)
    {
        allsounds = true;
        soundlerp.value = 0;
        soundlerp.deltaT = 1;
        soundlerp.Speed = speed;
        soundlerp.start = true;
    }

    void SlowChangeAllSounds(float value)
    {
        for (int i = 0; i < this.sounds.Length; i++)
        {
            this.sounds[i].volume = value*sounds_volumn;
        }
        if (value < 0)
        {
            allsounds = false;
            soundlerp.start = false;
            StopAllSound();
        }
    }
    public void SetMusicMute(bool mute)
    {
        if (mute == (this.music_muted == 1))
        {
            return;
        }
        //Debug.Log(mute);
        this.music_muted = mute ? 1 : 0;
        this.music.mute = mute;
        PlayerPrefs.SetInt("music_muted", this.music_muted);
    }

    public void SetSoundsMute(bool mute)
    {
        if (mute == (this.sounds_muted == 1))
        {
            return;
        }
        this.sounds_muted = mute ? 1 : 0;
        for (int i = 0; i < this.sounds.Length; i++)
        {
            this.sounds[i].mute = mute;
        }
        PlayerPrefs.SetInt("sounds_muted", this.sounds_muted);
    }

    public void SetMusicVolumn(float value)
    {
        if(value < 0 || value>1.0f)
        {
            return;
        }
        this.music_volumn = value;
        this.music.volume = this.music_volumn;
        PlayerPrefs.SetFloat("music_volumn", this.music_volumn);
    }

    public void SetSoundsVolumn(float value)
    {
        if (value < 0 || value > 1.0f)
        {
            return;
        }
        this.sounds_volumn = value;
        for (int i = 0; i < this.sounds.Length; i++)
        {        
            this.sounds[i].volume = this.sounds_volumn;
        }
        PlayerPrefs.SetFloat("sounds_volumn", this.sounds_volumn);
    }

    public bool musicMuted
    {
        get
        {
            return (this.music_muted == 1);
        }
    }
    public float musicVolumn
    {
        get
        {
            return this.music_volumn;
        }
    }
    public bool soundsMuted
    {
        get
        {
            return (this.sounds_muted == 1);
        }
    }
    public float soundsVolumn
    {
        get
        {
            return this.sounds_volumn;
        }
    }
}
