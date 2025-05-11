using UnityEngine;
using UnityEngine.UI;

public class AnimatorMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;
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
