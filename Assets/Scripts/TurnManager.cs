using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;

    private int _currentTurn;
    private bool _canMove;
    private readonly List<MoveType> moves = new();
    private bool _isMoving;
    private int _currentMove;

    public UnityEvent<PlayerController> OnStartTurn;
    public UnityEvent<PlayerController> OnEndTurn;

    void Awake()
    {
        Instance = this;
        OnStartTurn = new();
        OnEndTurn = new();
    }

    void Start()
    {
        GameManager.Instance.OnStartRound.AddListener((player) =>
        {
            _currentTurn = 0;
            _currentMove = 0;
            _canMove = true;
            _isMoving = false;
            moves.Clear();
        });

        OnStartTurn.AddListener((player) =>
        {
           GameUIManager.Instance.UpdateMoveText(string.Format("{0} / {1}", _currentTurn, GameManager.Instance.ActivePlayer.PlayerStat.TurnCount)); 
           GameUIManager.Instance.UpdateSpeedText(GameManager.Instance.ActivePlayer.PlayerStat.Speed.ToString());
        });

        OnEndTurn.AddListener((player) =>
        {
            _isMoving = false;
            _currentMove++;
        });
    }

    public void OnWalk(InputValue ctx)
    {
        AddMoveToQueue(MoveType.Walk);
    }

    public void OnWait(InputValue ctx)
    {
        AddMoveToQueue(MoveType.Wait);
    }

    public void OnMovementEnded()
    {
        OnEndTurn.Invoke(GameManager.Instance.ActivePlayer);

        if (!_canMove && _currentMove == moves.Count)
        {
            GameManager.Instance.OnEndRound.Invoke(GameManager.Instance.ActivePlayer);
            return;
        }

        OnStartTurn.Invoke(GameManager.Instance.ActivePlayer);

        MakeNextMove();
    }

    void AddMoveToQueue(MoveType move)
    {
        if (!_canMove)
        {
            return;
        }

        moves.Add(MoveType.Wait);
        IncrementTurn();

        if (_isMoving)
        {
            return;
        }

        MakeMove(move);
    }

    void CheckCanMove()
    {
        if (_currentTurn == GameManager.Instance.ActivePlayer.PlayerStat.TurnCount)
        {
            _canMove = false;
        }
    }

    void IncrementTurn()
    {
        _currentTurn++;
        CheckCanMove();
    }

    void MakeNextMove()
    {
        if (_currentMove >= moves.Count)
        {
            return;
        }

        MakeMove(moves[_currentMove]);
    }

    void MakeMove(MoveType move)
    {
        _isMoving = true;

        if (move == MoveType.Walk)
        {
            GameManager.Instance.ActivePlayer.Walk();
        }
        else if (move == MoveType.Wait)
        {
            GameManager.Instance.ActivePlayer.Wait();
        }
    }
}
