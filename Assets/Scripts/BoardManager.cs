using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;

[Serializable]
public struct PlatformData
{
    public int Number;
    public GameObject GameObject;
}

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { get; private set; }
    public float PlatformSpacing;
    public float CreationDuration;

    [SerializeField]
    private List<PlayerController> Players;
    [SerializeField]
    private float SpawnHeight;
    [SerializeField]
    private float TargetHeight;
    [SerializeField]
    private float NeighborHeight;
    [SerializeField]
    private GameObject Platform;
    [SerializeField]
    private List<PlatformData> Platforms;

    void Awake()
    {
        Instance = this;
    }

    private bool HasPlayerOnSquare(int number)
    {
        return Players.Any(p => p.PlayerStat.CurrentSquare == number);
    }

    private bool HasPlayerOnOrAroundSquare(int number)
    {
        return Players.Any(p =>
        {
            int playerSquare = p.PlayerStat.CurrentSquare;
            return playerSquare >= number - 1 && playerSquare <= number + 1;
        });
    }

    public void EnablePlatform(int number)
    {
        PlatformData platform = Platforms.Find(p => p.Number == number);
        PlatformData previousPreviousPlatform = Platforms.Find(p => p.Number == number - 2);
        PlatformData previousPlatform = Platforms.Find(p => p.Number == number - 1);
        PlatformData nextPlatform = Platforms.Find(p => p.Number == number + 1);

        if (previousPreviousPlatform.GameObject != null && !HasPlayerOnOrAroundSquare(number - 2))
        {
            previousPreviousPlatform.GameObject.transform.DOComplete();
            previousPreviousPlatform.GameObject.transform.DOMoveY(SpawnHeight, CreationDuration);
            Destroy(previousPreviousPlatform.GameObject, CreationDuration + 0.1f);
            Platforms.Remove(previousPreviousPlatform);
        }

        if (previousPlatform.GameObject != null && !HasPlayerOnSquare(number - 1))
        {
            previousPlatform.GameObject.transform.DOComplete();
            previousPlatform.GameObject.transform.DOMoveY(NeighborHeight, CreationDuration);
        }

        if (nextPlatform.GameObject == null)
        {
            nextPlatform = new PlatformData
            {
                Number = number + 1,
                GameObject = Instantiate(Platform, new Vector3(number * PlatformSpacing, SpawnHeight, 0f), Quaternion.identity, transform)
            };
            nextPlatform.GameObject.GetComponentInChildren<TMP_Text>().text = (number + 1).ToString();
            Platforms.Add(nextPlatform);
            nextPlatform.GameObject.transform.DOMoveY(NeighborHeight, CreationDuration);
        }

        platform.GameObject.transform.DOComplete();
        platform.GameObject.transform.DOMoveY(TargetHeight, CreationDuration);

    }
}
