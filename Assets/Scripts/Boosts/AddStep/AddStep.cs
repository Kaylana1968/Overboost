using UnityEngine;
using UnityEngine.InputSystem;

public class AddStep : MonoBehaviour
{
    private PlayerController _playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }


    public void OnWalk(InputValue ctx)
    {
        _playerController.OnWalk(ctx);
    }
}
