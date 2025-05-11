using UnityEngine;
using static UnityEditor.PlayerSettings;

public class VehicleMovement : MonoBehaviour
{
    [SerializeField] private Transform _frontControlPointPosition;
    [SerializeField] private Transform _backControlPointPosition;
    [SerializeField] private float _checkRange;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private Transform _groundObject;
    [SerializeField] public float destroyTime = 30f;
    public float movementSpeed;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    private void FixedUpdate()
    {
        MoveForward();
        Vector3 origin = _frontControlPointPosition.position;
        Vector3 direction = _frontControlPointPosition.forward;
        bool isTouchingGround = Physics.Raycast(origin, direction, _checkRange, _ground);

        if (!isTouchingGround)
        {
            transform.Rotate(Vector3.left, 0.2f, Space.Self);
        }
        transform.Rotate(Vector3.right, 0.1f, Space.Self);
    }

    public void MoveForward()
    {
        transform.position += (movementSpeed * -transform.up) * Time.fixedDeltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector3 frontPos = _frontControlPointPosition.position;
        Vector3 direction = _frontControlPointPosition.forward * _checkRange;
        Gizmos.DrawLine(frontPos, frontPos + direction);

        Vector3 backPos = _backControlPointPosition.position;
        Vector3 directionBack = _backControlPointPosition.forward * _checkRange;
        Gizmos.DrawLine(backPos, backPos + directionBack);
    }
}
