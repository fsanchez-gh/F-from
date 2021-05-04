using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip musicClip;

    [SerializeField] private Trail
        rightTrail,
        leftTrail;

     public AudioClip
        startClip,
        quitClip,
        yesClip,
        noClip;

    private AudioSource
    _musicAu,
    _soundsAu;

    private UIManager _uiManager;
    private FlightControl _control;
    private NoDestroy _noDestroy;
    private bool _musicPaused = true;

    private void Awake() => Assignations();

    private void Start()
    {
        SetMusic();
        SetAudioClips();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0) return;
        if (_musicAu.isPlaying || _musicPaused) return;
        rightTrail.StopSetPosition();
        leftTrail.StopSetPosition();
        _noDestroy.CallSetSetLineRendererPositions();
        _uiManager.StartTransition(1);
    }

    private void Assignations()
    {
        _noDestroy = FindObjectOfType<NoDestroy>();
        _musicAu = gameObject.AddComponent<AudioSource>();
        _soundsAu = gameObject.AddComponent<AudioSource>();
        _uiManager = FindObjectOfType<UIManager>();
        _control = FindObjectOfType<FlightControl>();
    }

    private void SetMusic()
    {
        _musicAu.clip = musicClip;
        _musicAu.volume = 1;
        if (SceneManager.GetActiveScene().buildIndex == 1)
            _musicAu.loop = true;
    }

    private void SetAudioClips()
    {
        _soundsAu.volume = 0.6f;
        _soundsAu.pitch = Random.Range(0.9f, 1.0f);
    }
    
    public void PlayMusic()
    {
        _musicAu.Play();
        _musicPaused = false;
    }

    public void PauseMusic()
    {
        _musicAu.Pause();
        _musicPaused = !_musicPaused;
    }

    public void PlaySound(AudioClip clip) => _soundsAu.PlayOneShot(clip); 
}
