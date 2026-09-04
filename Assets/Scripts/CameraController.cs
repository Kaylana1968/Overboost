using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float offset;

    public Transform activePlayer;

    void Update()
    {
        transform.position = new(activePlayer.position.x + offset, transform.position.y, transform.position.z);
    }
}
