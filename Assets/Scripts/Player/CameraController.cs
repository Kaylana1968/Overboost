using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float Offset;
    [SerializeField]
    private float Smoothing;

    private Transform _activePlayer;

    void Start()
    {
        GameManager.Instance.OnStartRound.AddListener((player) => _activePlayer = player.transform);
    }

    void Update()
    {
        Vector3 targetPosition = new(_activePlayer.position.x + Offset, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Smoothing);
    }
}
