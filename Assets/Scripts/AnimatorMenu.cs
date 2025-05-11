using UnityEngine;
using UnityEngine.UI;

public class AnimatorMenu : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
        _quitButton.gameObject.SetActive(false);
    }

    public void PlayButtonEnable()
    {
        _playButton.gameObject.SetActive(true);
        _quitButton.gameObject.SetActive(false);
    }

    public void ExitButtonEnable() 
    {
        _quitButton.gameObject.SetActive(true);
        _playButton.gameObject.SetActive(false);
    }

}
