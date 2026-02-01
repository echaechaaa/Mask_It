using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [Header("Files")]
    [SerializeField] private AudioClip _music1;
    [SerializeField] private AudioClip _music2;
    [SerializeField] private AudioClip _selectSFX;

    [Header("Settings")]
    [SerializeField][Range(0.0f, 1.0f)] private float _musicVolume = 0.5f;
    [SerializeField][Range(0.0f, 1.0f)] private float _sfxVolume = 1f;

    [SerializeField] private float _fadeTimeBetweenSongs = 3.0f;
    private float _volumeBeforeFade = 0.0f;
    private bool _isFading = false;

    //Mute related
    private bool _isMusicMuted = false;
    private bool _isSFXMuted = false;
    private float _musicVolumeBeforeMute = 0.0f;
    private float _sfxVolumeBeforeMute = 0.0f;

    //Slider Related
    private float _musicVolumeRatioComparedToSFX = 0.2f;

    private AudioClip _currentPlayingMusic = null;


    #region Callable Methods
    public void PlayMusic(AudioClip musicToPlay)
    {
        if (musicToPlay == _currentPlayingMusic) { return; }

        if (_currentPlayingMusic != null)
        {
            StopMusic();
        }

        _currentPlayingMusic = musicToPlay;
        _musicSource.clip = musicToPlay;
        _musicSource.Play();
    }
    public void StopMusic()
    {
        _musicSource.Stop();
        _currentPlayingMusic = null;
    }

    [EasyButtons.Button]
    public void SwitchMusic()
    {
        //Call fadeout
        //Play new song when volume is zero
        //call fadein

        StartCoroutine(FadeOutMusic());
    }

    public void PlaySFX(AudioClip sfx)
    {
        _sfxSource.PlayOneShot(sfx, _sfxVolume);
    }

  
    public void ToggleMusic()  //Mute unmute
    {
        if (_isMusicMuted)
        {
            _musicVolume = _musicVolumeBeforeMute;
        }
        else
        {
            _musicVolumeBeforeMute = _musicVolume;
            _musicVolume = 0f;
        }
        _isMusicMuted = !_isMusicMuted;
    }

    public void ToggleSFX()  //Mute unmute
    {
        if (_isSFXMuted)
        {
            _sfxVolume = _sfxVolumeBeforeMute;
        }
        else
        {
            _sfxVolumeBeforeMute = _sfxVolume;

            _sfxVolume = 0f;
        }
        _isSFXMuted = !_isSFXMuted;
    }

    public void SetVolumeTo(float value)
    {
        if(_isFading)
        {
            return;
        }
        _sfxVolume = value;
        _musicVolume = value * _musicVolumeRatioComparedToSFX;
    }
    #endregion

    //Debug method
    [EasyButtons.Button]
    public void PlaySelectSFX()
    {
        _sfxSource.PlayOneShot(_selectSFX, _sfxVolume);
    }

    [EasyButtons.Button]
    public void GoToMusicEnd()
    {
        float secondsBeforeEnd = 5f;
        if (_musicSource.clip == null) return;

        float targetTime = _musicSource.clip.length - secondsBeforeEnd;
        _musicSource.time = Mathf.Max(0f, targetTime);
    }

    #region Singleton
    public static AudioManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }

        _musicSource = GetComponent<AudioSource>();
        _musicSource.loop = false;

        _musicSource.clip = _music1;
        _musicSource.volume = _musicVolume;
        _sfxSource.volume = _sfxVolume;

        _musicSource.Play();


    }
    #endregion

    private void Update()
    {
        _musicSource.volume = _musicVolume;
        _sfxSource.volume = _sfxVolume;

        if (_musicSource.clip == null) return;

        if (!_isFading && DoesMusicRequireFade())
        {
            _isFading = true;
            StartCoroutine(FadeOutMusic());
        }
    }

    public bool DoesMusicRequireFade()
    {
        bool startFade = _musicSource.time >= _musicSource.clip.length - _fadeTimeBetweenSongs;
        return startFade;
    }

    #region Coroutines
    private IEnumerator FadeOutMusic()
    {
        float timeElapsed = 0f;
        _volumeBeforeFade = _musicSource.volume;

        while (timeElapsed < _fadeTimeBetweenSongs)
        {
            _isFading = true;
            timeElapsed += Time.deltaTime;
            _musicSource.volume = Mathf.Lerp(_volumeBeforeFade, 0, timeElapsed / _fadeTimeBetweenSongs); //fade in fadeInTrack
            yield return null;
        }

        StartCoroutine(FadeInMusic());

        //Switch here music when volume is zero
        if (_musicSource.clip == _music1)
        {
            PlayMusic(_music2);
        }
        else
        {
            PlayMusic(_music1);
        }
    }

    private IEnumerator FadeInMusic()
    {
        float timeElapsed = 0f;

        while (timeElapsed < _fadeTimeBetweenSongs)
        {
            timeElapsed += Time.deltaTime;
            _musicSource.volume = Mathf.Lerp(0, _volumeBeforeFade, timeElapsed / _fadeTimeBetweenSongs); //fade in fadeInTrack
            yield return null;
        }
        _isFading = false;
    }
    #endregion
}
