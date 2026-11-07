using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float JumpPower;
    [SerializeField]
    private float JumpDuration;

    private Vector3 _initialPosition;
    private readonly List<bool> moves = new(); // true = Walk; false = Wait
    private bool _isMoving;

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
        _initialPosition = transform.position;
    }

    public void Wait()
    {
        moves.Add(false);

        if (_isMoving) return;

        StartCoroutine(WalkBy(0));
    }

    public void Walk()
    {
        moves.Add(true);

        if (_isMoving) return;

        StartCoroutine(WalkBy(PlayerStat.Speed));
    }

    private IEnumerator WalkBy(int squares)
    {
        transform.DOComplete();
        _isMoving = true;
        moves.RemoveAt(0);

        float duration = JumpDuration / (1f + Mathf.Floor(squares / 10));

        for (int i = 0; i < squares; i++)
        {
            PlayerStat.CurrentSquare++;

            Vector3 targetPosition = new(_initialPosition.x + BoardManager.Instance.PlatformSpacing * PlayerStat.CurrentSquare, _initialPosition.y, _initialPosition.z);
            BoardManager.Instance.EnablePlatform(PlayerStat.CurrentSquare, duration);
            transform.DOJump(targetPosition, JumpPower, 1, duration);

            yield return new WaitForSeconds(duration);
        }

        OnEndTurn.Invoke();

        if (moves.Count == 0) // no next move
        {
            _isMoving = false;
        }
        else if (moves[0]) // next move is walk
        {
            StartCoroutine(WalkBy(PlayerStat.Speed));
        }
        else // next move is wait
        {
            StartCoroutine(WalkBy(0));
        }
    }
}
