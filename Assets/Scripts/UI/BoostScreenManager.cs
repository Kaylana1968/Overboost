using System.Collections.Generic;
using UnityEngine;

public class BoostScreenManager : MonoBehaviour
{
    public static BoostScreenManager Instance;

    [SerializeField]
    List<Boost> Boosts;
    [SerializeField]
    List<CardManager> Cards;
    [SerializeField]
    Canvas Canvas;
    [SerializeField]
    PanelManager Panel;

    GameObject player1;
    Boost selectedBoost1;
    Boost selectedBoost2;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GameManager.Instance.OnEndRound.AddListener((player) => GenerateBoostFor(player.gameObject));
    }

    List<Boost> GetThreeRandomBoosts()
    {
        List<Boost> tempList = new(Boosts);

        // shuffle
        for (int i = 0; i < tempList.Count; i++)
        {
            int randomIndex = Random.Range(i, tempList.Count);
            (tempList[randomIndex], tempList[i]) = (tempList[i], tempList[randomIndex]);
        }

        return tempList.GetRange(0, 3);
    }

    void GenerateBoostFor(GameObject player)
    {
        Canvas.enabled = true;
        player1 = player;
        selectedBoost1 = null;
        selectedBoost2 = null;

        List<Boost> playerChoices = GetThreeRandomBoosts();
        List<Boost> opponentChoices = GetThreeRandomBoosts();
        for (int i = 0; i < Cards.Count; i++)
        {
            Cards[i].SetCard(playerChoices[i], opponentChoices[i]);
        }
    }

    public void SetBoosts(Boost boost1, Boost boost2)
    {
        selectedBoost1 = boost1;
        selectedBoost2 = boost2;
        Panel.SetBoosts(boost1, boost2);
    }

    public void ValidateBoosts()
    {
        if (!selectedBoost1 || !selectedBoost2)
        {
            return;
        }

        GameObject player2 = GameManager.Instance.Players.Find(player => player.gameObject != player1).gameObject;
        player1.AddComponent(selectedBoost1.Script.GetType());
        player2.AddComponent(selectedBoost2.Script.GetType());
        Canvas.enabled = false;

        GameManager.Instance.GoOnNextRound();
    }
}
