using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float StepSize;

    [SerializeField]
    private float JumpPower;

    [SerializeField]
    private float JumpDuration;

    private Vector3 _initialPosition;
    private int _currentSquare;

    public Stat PlayerStat;
    public UnityEvent OnStartRound;
    public UnityEvent OnEndRound;
    public UnityEvent OnStartTurn;
    public UnityEvent OnEndTurn;

    void Awake()
    {
        OnStartRound = new();
        OnEndRound = new();
        OnStartTurn = new();
        OnEndTurn = new();
    }

    void Start()
    {
        _currentSquare = 1;
        _initialPosition = transform.position;
    }

    public void Wait()
    {
        print("ZZZ");
        OnEndTurn.Invoke();
    }

    public void Walk()
    {
        StartCoroutine(WalkBy(PlayerStat.Speed));
    }

    private IEnumerator WalkBy(int squares)
    {
        transform.DOComplete();
        for (int i = 0; i < squares; i++)
        {
            Vector3 targetPosition = new(_initialPosition.x + StepSize * _currentSquare, _initialPosition.y, _initialPosition.z);
            transform.DOJump(targetPosition, JumpPower, 1, JumpDuration);
            _currentSquare++;

            yield return new WaitForSeconds(JumpDuration);
        }
        OnEndTurn.Invoke();
    }
}
