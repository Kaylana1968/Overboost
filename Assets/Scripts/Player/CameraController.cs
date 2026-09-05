using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float Offset;

    public Transform ActivePlayer;

    void Update()
    {
        transform.position = new(ActivePlayer.position.x + Offset, transform.position.y, transform.position.z);
    }
}
