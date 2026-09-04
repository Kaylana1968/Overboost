using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float stepSize;

    [SerializeField]
    private float jumpPower;

    [SerializeField]
    private float jumpDuration;

    private Vector3 _initialPosition;
    private int _currentSquare;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentSquare = 1;
        _initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnWait(InputValue ctx)
    {
        print("ZZZ");
    }

    public void OnWalk(InputValue ctx)
    {
        WalkOneSquare();
    }

    public void WalkOneSquare()
    {
        Vector3 targetPosition = new(_initialPosition.x + stepSize * _currentSquare, _initialPosition.y, _initialPosition.z);
        transform.DOComplete();
        transform.DOJump(targetPosition, jumpPower, 1, jumpDuration);
        _currentSquare++;
    }
}
