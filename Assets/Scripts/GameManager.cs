using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private List<PlayerController> Players;
    [SerializeField]
    private Canvas BoostScreen;
    [SerializeField]
    TMP_Text MoveText;
    [SerializeField]
    TMP_Text SpeedText;

    public UnityEvent<PlayerController> OnStartRound;
    public UnityEvent<PlayerController> OnEndRound;
    public UnityEvent<PlayerController> OnStartTurn;
    public UnityEvent<PlayerController> OnEndTurn;

    private PlayerController _activePlayer;
    private int _currentTurn;
    private bool _canMove;
    private readonly List<bool> moves = new(); // true = Walk; false = Wait
    private bool _isMoving;
    private int _currentMove;

    void Awake()
    {
        Instance = this;
        OnStartRound = new();
        OnEndRound = new();
        OnStartTurn = new();
        OnEndTurn = new();
    }

    void Start()
    {
        _activePlayer = Players[0];

        OnStartRound.AddListener((player) =>
        {
            _currentTurn = 0;
            _currentMove = 0;
            _canMove = true;
            moves.Clear();
            UpdateMoveText();
            UpdateSpeedText();
        });

        OnStartTurn.AddListener((player) =>
        {
            UpdateSpeedText();
        });

        OnStartRound.Invoke(_activePlayer);
    }

    private void CheckCanMove()
    {
        if (_currentTurn == _activePlayer.PlayerStat.TurnCount)
        {
            _canMove = false;
        }
    }

    public void OnWalk(InputValue ctx)
    {
        if (!_canMove) return;

        moves.Add(true);
        IncrementTurn();

        if (!_isMoving)
        {
            MakeMove(true);
        }
    }

    public void OnWait(InputValue ctx)
    {
        if (!_canMove) return;

        moves.Add(false);
        IncrementTurn();

        if (!_isMoving)
        {
            MakeMove(false);
        }
    }

    public void OnMovementEnded()
    {
        OnEndTurn.Invoke(_activePlayer);
        _isMoving = false;
        _currentMove++;

        if (!_canMove && _currentMove == moves.Count)
        {
            OnEndRound.Invoke(_activePlayer);
            return;
        }

        OnStartTurn.Invoke(_activePlayer);
        UpdateSpeedText();

        if (_currentMove < moves.Count)
        {
            MakeMove(moves[_currentMove]);
        }
    }

    public void GoOnNextRound()
    {
        _activePlayer = Players[0] == _activePlayer ? Players[1] : Players[0];
        OnStartRound.Invoke(_activePlayer);
    }

    void IncrementTurn()
    {
        _currentTurn++;
        UpdateMoveText();
        CheckCanMove();
    }

    void MakeMove(bool move)
    {
        _isMoving = true;

        if (move)
        {
            _activePlayer.Walk();
        }
        else
        {
            _activePlayer.Wait();
        }
    }

    void UpdateMoveText()
    {
        MoveText.text = string.Format("Move : {0} / {1}", _currentTurn, _activePlayer.PlayerStat.TurnCount);
    }

    void UpdateSpeedText()
    {
        SpeedText.text = string.Format("Speed : {0}", _activePlayer.PlayerStat.Speed);
    }
}