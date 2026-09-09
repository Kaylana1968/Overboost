using UnityEngine;

public class AddSpeedMultiplier : MonoBehaviour
{
    private PlayerController _playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _playerController.PlayerStat.AddMultiplier(1.5f);
    }
}
