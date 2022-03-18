using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    #region 싱글톤
    private static SoundManager _instance = null;

    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (SoundManager)FindObjectOfType(typeof(SoundManager));
                if (_instance == null)
                {
                    Debug.Log("활성화된 매니저 오브젝트가 없습니다.");
                }
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            _instance = this;
            //DontDestroyOnLoad(this.gameObject);
            AwakeAfter();
        }
    }
    #endregion

    AudioSource[] _audioSources = new AudioSource[3];
    Dictionary<string, AudioClip> _sounds;
    AudioClip[] BGMClips;

    private void AwakeAfter()
    {
        _audioSources[0] = GetComponent<AudioSource>();
        _audioSources[1] = gameObject.transform.Find("BGM").GetComponent<AudioSource>();
        _audioSources[2] = gameObject.transform.Find("RepeatSound").GetComponent<AudioSource>();

        _sounds = new Dictionary<string, AudioClip>();
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Sounds/");
        for (int i = 0; i < clips.Length; i++)
        {
            _sounds.Add(clips[i].name, clips[i]);
        }
        BGMClips = Resources.LoadAll<AudioClip>("Sounds/BGM");
        //딕셔너리와 BGM 배열에 음악파일 넣기

    }

    public void SoundChange(float Effect, float BGM)
    {
        _audioSources[0].volume = Effect;
        _audioSources[2].volume = Effect;
        _audioSources[1].volume = BGM;
    }

    public void PlaySound(string path = "", bool isSpecial = false)
    {
        AudioClip clip = null;

        if (path == "")
        {
            _audioSources[0].Stop();
        }
        else if (isSpecial)
        {
            clip = Resources.Load<AudioClip>("Sounds/Special/" + path);
        }
        else
        {
            clip = _sounds[path];
        }
        _audioSources[0].clip = clip;
        _audioSources[0].Play();
    }


    public void PlayBGM(int index)
    {
        StartCoroutine(BGMChange(index));
    }

    IEnumerator BGMChange(int index)
    {
        if (_audioSources[1].isPlaying)
        {
            float sound = PlayerPrefs.GetFloat("BGM");

            while (sound > 0)
            {
                _audioSources[1].volume = sound;
                yield return new WaitForSeconds(0.01f);
                sound -= 0.01f;
            }
            _audioSources[1].Stop();
            _audioSources[1].volume = PlayerPrefs.GetFloat("BGM");
        }

        if (index != -1)
        {
            _audioSources[1].clip = BGMClips[index];
            _audioSources[1].Play();
        }
    }

    public void PlayRepeatSound(string path = "", bool isSpecial = false)
        //PlaySound의 것과 다른 오디오소스로 소리 충돌 X
        //Loop가 켜져있기에 계속 반복
    {
        AudioClip clip = null;

        if (path == "")
        {
            _audioSources[2].Stop();
        }
        else if (isSpecial)
        {
            clip = Resources.Load<AudioClip>("Sounds/Special/" + path);
        }
        else
        {
            clip = _sounds[path];
        }
        _audioSources[2].clip = clip;
        _audioSources[2].Play();
    }

    public void SoundPause(bool OnOff = false)
    {
        if (!OnOff)
        {
            for (int i = 0; i < _audioSources.Length; i++)
            {
                _audioSources[i].Pause();
            }
        }
        else
        {
            for (int i = 0; i < _audioSources.Length; i++)
            {
                _audioSources[i].UnPause();
            }
        }
    }

}
