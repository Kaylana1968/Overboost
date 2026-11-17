using System.Collections.Generic;
using UnityEngine;

public class BoostScreenManager : MonoBehaviour
{
    [SerializeField]
    private List<Boost> Boosts;
    [SerializeField]
    private List<CardManager> Cards;
    [SerializeField]
    private Canvas Canvas;

    void Start()
    {
        GameManager.Instance.OnEndRound.AddListener((player) => GenerateBoostFor(player.gameObject));
    }

    List<Boost> GetThreeRandomBoosts()
    {
        List<Boost>tempList = new(Boosts);

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
        
        List<Boost> selectedBoosts = GetThreeRandomBoosts();
        for (int i = 0; i < Cards.Count; i++)
        {
            Cards[i].SetCard(selectedBoosts[i], player, Canvas);
        }
    }
}
