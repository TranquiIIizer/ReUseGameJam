using UnityEngine;
using UnityEngine.InputSystem;

public class SummaryCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _summaryCanvas;
    [SerializeField] private InputAction _summaryOpened;

    private void Start()
    {
        _summaryCanvas.SetActive(false);

    }
}
