using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float Offset;

    [SerializeField]
    private float Smoothing;

    [SerializeField]
    private List<PlayerController> Players;

    private Transform ActivePlayer;

    void Start()
    {
        foreach (PlayerController player in Players)
        {
            player.OnStartRound.AddListener(() => ActivePlayer = player.transform);
        }
    }

    void Update()
    {
        Vector3 targetPosition = new(ActivePlayer.position.x + Offset, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Smoothing);
    }
}
