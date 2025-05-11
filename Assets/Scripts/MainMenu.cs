using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    [SerializeField] private GameObject _menu;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _filmScrollAudio;
    [SerializeField] private AudioSource _clickSound;

    [SerializeField] private InputAction _openMenuAction;
    [SerializeField] private InputAction _scrollAction;

    private void OnEnable()
    {
        _scrollAction.Enable();
        _scrollAction.performed += ButtonSwapper;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        _openMenuAction.performed += OpenMenu;
    }

    private void OpenMenu(InputAction.CallbackContext context)
    {
        _menu.SetActive(!_menu.activeSelf);
    }

    private void ButtonSwapper(InputAction.CallbackContext context)
    {
        Vector2 scrollVal = context.ReadValue<Vector2>();
        float verticalScroll = scrollVal.y;
        _animator.enabled = true;

        if (verticalScroll > 0f)
        {
            _animator.SetBool("forward", true);
            if (!_filmScrollAudio.isPlaying) _filmScrollAudio.Play();
        }
        else if (verticalScroll < 0f)
        {
            _animator.SetBool("forward", false);
            if (!_filmScrollAudio.isPlaying) _filmScrollAudio.Play();
        }
    }

    public void Play()
    {
        StartCoroutine(PlayWithSound());
    }

    private IEnumerator PlayWithSound()
    {
        _clickSound.Play();
        yield return new WaitForSeconds(_clickSound.clip.length);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        StartCoroutine(QuitWithSound());
    }

    private IEnumerator QuitWithSound()
    {
        _clickSound.Play();
        yield return new WaitForSeconds(_clickSound.clip.length);
        Application.Quit();
    }

    private void OnDestroy()
    {
        _openMenuAction.performed -= OpenMenu;
    }

    private void OnDisable()
    {
        _scrollAction.performed -= ButtonSwapper;
        _scrollAction.Disable();
    }
}
