using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject
        mainMenu,
        pauseMenu,
        mainEven,
        plane;

    [SerializeField] private GameObject wall;

    private FlightControl _control;
    private SoundManager _sound;
    private TransitionControl _transition;
    private NoDestroy _noDestroy;

    private bool
        _gameIsPaused,
        _gameIsStarted,
        _gameFinishing,
        _otherButtonIsPressed;
    
    public bool GameIsPaused => _gameIsPaused;

    private void Awake() => Assignations();

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _otherButtonIsPressed = false;
        if(SceneManager.GetActiveScene().buildIndex != 0)
            _control.StopRumble();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Pause") && _gameIsStarted)
            PauseMenu();
    }

    private void Assignations()
    {
        _noDestroy = FindObjectOfType<NoDestroy>();
        _control = FindObjectOfType<FlightControl>();
        _sound = GetComponent<SoundManager>();
        _transition = FindObjectOfType<TransitionControl>();
    }

    private void PauseMenu()
    {
        _gameIsPaused = !_gameIsPaused;
        if (_gameIsPaused)
        {
            mainEven.SetActive(false);
            _sound.PauseMusic();
            _control.StopRumble();
            _control.eventActive = false;
            Time.timeScale = 0f;
            _control.enabled = false;
            pauseMenu.SetActive(true);

        }else
        {
            if (!_gameFinishing)
            {
                _sound.PlayMusic();
                Time.timeScale = 1f;
                _control.enabled = true;
                pauseMenu.SetActive(false);
                mainEven.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
                mainEven.SetActive(true);
                _gameFinishing = false;
            }
        }
    }

    private IEnumerator StartCoroutine()
    {
        yield return new WaitForSeconds(_sound.startClip.length);
        mainMenu.SetActive(false);
        plane.GetComponent<LaunchControl>().enabled = true;
        _sound.PlayMusic();
        _gameIsStarted = true;
    }

    private IEnumerator ViewFlightCoroutine()
    {
        yield return new WaitForSeconds(_sound.startClip.length);
        mainMenu.SetActive(false);
        plane.GetComponent<FlightControl>().enabled = true;
        _sound.PlayMusic();
        _gameIsStarted = true;
        wall.SetActive(false);
    }
    
    private IEnumerator QuitCoroutine()
    {
        yield return new WaitForSeconds(_sound.quitClip.length);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void ChangeAxisDirection(string axis)
    {
        switch (axis)
        {
            case "y":
                _control.SetYAxis();
                break;
            case "x":
                _control.SetXAxis();
                break;
        }
    }
    
    private void CallDrawFlight() => _noDestroy.DrawFlights();
    
    private void CallResetPositions() => _noDestroy.ResetTrailsPositions();

    public void StartTransition(int value) => _transition.StartFadeInTransition(value);

    public void LoadScene(int value) => SceneManager.LoadScene(value, LoadSceneMode.Single);

    public void StartGame()
    {
        if (_otherButtonIsPressed) return;
        _otherButtonIsPressed = true;
        _sound.PlaySound(_sound.startClip);
        StartCoroutine(nameof(StartCoroutine));
    }

    public void QuitGame()
    {
        if (_otherButtonIsPressed) return;
        _otherButtonIsPressed = true;
        _sound.PlaySound(_sound.quitClip);
        StartCoroutine(nameof(QuitCoroutine));
    }

    public void SetGamePause()
    {
        _gameFinishing = true;
        PauseMenu();
    }

    public void WantToViewFlight()
    {
        if (_otherButtonIsPressed) return;
        _otherButtonIsPressed = true;
        _sound.PlaySound(_sound.startClip);
        CallDrawFlight();
        StartCoroutine(nameof(ViewFlightCoroutine));
    }

    public void DontWantToViewFlight()
    {
        if (_otherButtonIsPressed) return;
        _otherButtonIsPressed = true;
        _sound.PlaySound(_sound.quitClip);
        StartTransition(2);
        CallResetPositions();
    }
}
