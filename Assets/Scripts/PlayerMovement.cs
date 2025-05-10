using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> _endPoints;
    InputAction moveAction;
    [SerializeField] private bool _moveOnZAxis;
    [SerializeField] private float _moveSpeed;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        Vector2 moveValue = -moveAction.ReadValue<Vector2>();
        Debug.Log(moveValue);
        if (_moveOnZAxis)
        {
            transform.position += new Vector3(moveValue.x * _moveSpeed, 0, 0) * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(0, 0, moveValue.x * _moveSpeed) * Time.deltaTime;
        }
    }
}
