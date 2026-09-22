using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private List<PlayerController> Players;
    [SerializeField]
    private Canvas BoostScreen;

    private int _activePlayerIndex;
    private int _currentTurn;
    private int _currentEndTurn;
    private bool _canMove;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _activePlayerIndex = 0;
        PlayerController activePlayer = Players[_activePlayerIndex];

        foreach (PlayerController player in Players)
        {
            player.OnStartRound.AddListener(() =>
            {
                _currentTurn = 1;
                _currentEndTurn = 1;
                _canMove = true;
            });
            player.OnEndTurn.AddListener(() =>
            {
                if (_currentEndTurn == player.PlayerStat.TurnCount)
                {
                    player.OnEndRound.Invoke();
                    BoostScreen.enabled = true;
                }
                else
                {
                    _currentEndTurn++;
                }
            });
        }
        activePlayer.OnStartRound.Invoke();
    }

    private void CheckCanMove()
    {
        if (_currentTurn <= Players[_activePlayerIndex].PlayerStat.TurnCount)
        {
            return;
        }

        _canMove = false;

    }

    public void OnWalk(InputValue ctx)
    {
        if (!_canMove) return;
        Players[_activePlayerIndex].Walk();
        _currentTurn++;
        CheckCanMove();
    }

    public void OnWait(InputValue ctx)
    {
        if (!_canMove) return;
        Players[_activePlayerIndex].Wait();
        _currentTurn++;
        CheckCanMove();
    }

    public void GoOnNextRound()
    {
        _activePlayerIndex = (_activePlayerIndex + 1) % Players.Count;
        Players[_activePlayerIndex].OnStartRound.Invoke();
    }
}