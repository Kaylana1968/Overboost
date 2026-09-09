using System.Collections.Generic;
using UnityEngine;

public class BoostScreenManager : MonoBehaviour
{
    [SerializeField]
    private List<Boost> Boosts;
    [SerializeField]
    private List<PlayerController> Players;

    private Canvas _canvas;

    void Start()
    {
        _canvas = GetComponent<Canvas>();

        foreach (PlayerController player in Players)
        {
            player.OnEndRound.AddListener(() => GenerateBoostFor(player));
        }
    }

    void GenerateBoostFor(PlayerController player)
    {
        _canvas.enabled = true;

    }
}
