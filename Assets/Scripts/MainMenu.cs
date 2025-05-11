using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private GameObject _menu;

    [SerializeField] private InputAction _openMenuAction;

    private void Start()
    {
        _openMenuAction.performed += OpenMenu;
        Time.timeScale = 0f;

        _playButton.gameObject.SetActive(true);
        _quitButton.gameObject.SetActive(false);
    }

    private void OpenMenu(InputAction.CallbackContext context)
    {
        _menu.SetActive(!_menu.activeSelf);
    }

    public void Play()
    {
        _menu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        _openMenuAction.performed -= OpenMenu;
    }
}
