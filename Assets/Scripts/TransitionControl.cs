using UnityEngine;

public class TransitionControl : MonoBehaviour
{
    private Animator _animator;
    private UIManager _uiManager;
    private static readonly int FadeIn = Animator.StringToHash("FadeIn");
    private static readonly int FadeIn2 = Animator.StringToHash("FadeIn2");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _uiManager = FindObjectOfType<UIManager>();
    }

    public void StartFadeInTransition(int value)
    {
        switch (value)
        {
            case 1:
                _animator.SetTrigger(FadeIn);
                break;
            case 2:
                if (_uiManager.GameIsPaused)
                {
                    _uiManager.SetGamePause();
                    _animator.SetTrigger(FadeIn2);
                }
                else
                {
                    _animator.SetTrigger(FadeIn2);
                }
                break;
        }
    }

    public void OnCompleteTransition(int value) => _uiManager.LoadScene(value);
}
