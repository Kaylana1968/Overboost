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

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        if (Players.Count < 2)
        {
            return;
        }

        _activePlayerIndex = 0;
        PlayerController activePlayer = Players[_activePlayerIndex];

        foreach (PlayerController player in Players)
        {
            player.OnStartRound.AddListener(() => _currentTurn = 1);
            player.OnEndTurn.AddListener(() =>
            {
                if (_currentTurn >= Players[_activePlayerIndex].PlayerStat._turnCount)
                {
                    player.OnEndRound.Invoke();
                    BoostScreen.enabled = true;
                }
                else
                {
                    _currentTurn++;
                }
            });
        }
        activePlayer.OnStartRound.Invoke();
    }

    public void OnWalk(InputValue ctx)
    {
        Players[_activePlayerIndex].Walk();
    }

    public void OnWait(InputValue ctx)
    {
        Players[_activePlayerIndex].Wait();
    }

    public void GoOnNextRound()
    {
        _activePlayerIndex = (_activePlayerIndex + 1) % Players.Count;
        Players[_activePlayerIndex].OnStartRound.Invoke();
    }
}