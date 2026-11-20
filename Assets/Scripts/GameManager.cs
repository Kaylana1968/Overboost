using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayerController ActivePlayer { get; private set; }
    public int FinishSquare;
    public List<PlayerController> Players;

    public UnityEvent<PlayerController> OnStartRound;
    public UnityEvent<PlayerController> OnEndRound;

    void Awake()
    {
        Instance = this;
        OnStartRound = new();
        OnEndRound = new();

        OnStartRound.AddListener((player) =>
        {
            ActivePlayer = player;
        });
    }

    void Start()
    {
        OnStartRound.Invoke(Players[0]);
    }

    public void GoOnNextRound()
    {
        OnStartRound.Invoke(Players[0] == ActivePlayer ? Players[1] : Players[0]);
    }

    public void OnFinishReached()
    {
        if (ActivePlayer != Players[1])
        {
            GoOnNextRound();
            return;
        }

        bool hasPlayer1Finished = Players[0].PlayerStat.CurrentSquare >= FinishSquare;
        bool hasPlayer2Finished = Players[1].PlayerStat.CurrentSquare >= FinishSquare;

        if (hasPlayer1Finished)
        {
            if (hasPlayer2Finished)
            {
                EndScreenManager.Instance.ToggleWinner(0);
            }
            else
            {
                EndScreenManager.Instance.ToggleWinner(1);
            }
        }
        else if (hasPlayer2Finished)
        {
            EndScreenManager.Instance.ToggleWinner(2);
        }
    }
}