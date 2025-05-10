using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    [SerializeField] private Transform _controlPointPosition;
    [SerializeField] private float _checkRange;
    [SerializeField] private LayerMask ground;

    private void FixedUpdate()
    {
        var pos = _controlPointPosition.localPosition;
        bool isTouchingGround = Physics.Raycast(pos, new Vector3(pos.x, pos.y, pos.z), ground);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        var pos = _controlPointPosition.localPosition;
        Gizmos.DrawLine(pos, new Vector3(pos.x, pos.y-_checkRange, pos.z));
    }
}
