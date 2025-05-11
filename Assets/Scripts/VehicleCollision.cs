using UnityEngine;

public class VehicleCollision : MonoBehaviour
{
    private Collider _selfCollider;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            CollidedWithPlayer();
        }
    }

    public void CollidedWithPlayer()
    {
        //Audio poniżej

        Debug.Log("Śmierć");
    }
}
