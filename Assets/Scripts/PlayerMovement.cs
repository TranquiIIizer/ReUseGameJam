using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    InputAction moveAction;
    [SerializeField] private bool moveOnZAxis;
    [SerializeField] private float moveSpeed;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Debug.Log(moveValue);
        if (moveOnZAxis)
        {
            transform.position += new Vector3(moveValue.x, 0, 0) * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(0, 0, moveValue.x) * Time.deltaTime;
        }
    }
}
