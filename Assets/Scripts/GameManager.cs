using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private List<PlayerController> Players;

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
        PlayerController _activePlayer = Players[_activePlayerIndex];

        foreach (PlayerController _player in Players)
        {
            _player.OnStartRound.AddListener(() => _currentTurn = 1);
            _player.OnEndRound.AddListener(() =>
            {
                _activePlayerIndex = (_activePlayerIndex + 1) % Players.Count;
                Players[_activePlayerIndex].OnStartRound.Invoke();

            });
            _player.OnEndTurn.AddListener(() =>
            {
                if (_currentTurn >= Players[_activePlayerIndex].PlayerStat._turnCount)
                {
                    _player.OnEndRound.Invoke();
                }
                else
                {
                    _currentTurn++;
                }
            });
        }
        _activePlayer.OnStartRound.Invoke();
    }
}