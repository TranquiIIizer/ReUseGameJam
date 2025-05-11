using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject GameOverUI;
    private void Start()
    {
        VehicleCollision.GameEnded += OpenUI;
    }

    public void OpenUI(bool ended)
    {
        GameOverUI.SetActive(ended);
    }

    private void OnDestroy()
    {
        VehicleCollision.GameEnded -= OpenUI;
    }
}
