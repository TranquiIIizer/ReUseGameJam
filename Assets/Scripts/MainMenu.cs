using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    [SerializeField] private GameObject _menu;
    [SerializeField] private Animator _animator;
    
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

        if (verticalScroll > 0f)
        {
            _animator.SetBool("forward", true);
        } 
        else if (verticalScroll < 0f) 
        {
            _animator.SetBool("forward", false);
        }
    }

    public void Play()
    {
        Debug.Log("Played");
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

    private void OnDisable()
    {
        _scrollAction.performed -= ButtonSwapper;
        _scrollAction.Disable();
    }
}
