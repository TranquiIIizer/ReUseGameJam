using UnityEngine;
using static UnityEditor.PlayerSettings;

public class VehicleMovement : MonoBehaviour
{
    [SerializeField] private Transform _controlPointPosition;
    [SerializeField] private float _checkRange;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private float _movementSpeed;

    private void FixedUpdate()
    {
        MoveForward();

        Vector3 origin = _controlPointPosition.position;
        Vector3 direction = _controlPointPosition.forward;
        bool isTouchingGround = Physics.Raycast(origin, direction, _checkRange, _ground);
        if (!isTouchingGround)
        {
            transform.Rotate(Vector3.left, 1f, Space.Self);
        }
    }

    public void MoveForward()
    {
        transform.position += (_movementSpeed * -transform.up) * Time.fixedDeltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 pos = _controlPointPosition.position;
        Vector3 direction = _controlPointPosition.forward * _checkRange;
        Gizmos.DrawLine(pos, pos + direction);
    }
}
