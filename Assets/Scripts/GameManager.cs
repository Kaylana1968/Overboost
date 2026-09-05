using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private List<PlayerController> players;

    private PlayerController _activePlayer;

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
        if (players.Count < 2)
        {
            return;
        }

        _activePlayer = players[0];
        _activePlayer.OnStartTurn.Invoke();
    }
}