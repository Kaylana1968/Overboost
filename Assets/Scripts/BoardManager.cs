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

    public readonly void SetHeight(float height, float duration)
    {
        if (GameObject.transform.position.y == height)
        {
            return;
        }

        GameObject.transform.DOComplete();
        GameObject.transform.DOMoveY(height, duration);
    }
}

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { get; private set; }
    public float PlatformSpacing;

    [SerializeField]
    private List<PlayerController> Players;
    [SerializeField]
    private List<float> HeightPoints;
    [SerializeField]
    private GameObject Platform;
    [SerializeField]
    private List<PlatformData> Platforms;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Initialize platforms
        EnablePlatform(0, 0f);
    }

    private void CreatePlatform(int number, float height)
    {
        PlatformData platform = new()
        {
            Number = number,
            GameObject = Instantiate(
                Platform,
                new Vector3(number * PlatformSpacing, height, 0f),
                Quaternion.identity,
                transform
            )
        };
        string text = (number + 1).ToString();
        platform.GameObject.GetComponentInChildren<TMP_Text>().SetText(text);
        Platforms.Add(platform);
    }

    private void DestroyPlatform(int number)
    {
        PlatformData platform = Platforms.Find(p => p.Number == number);

        if (platform.GameObject == null)
        {
            return;
        }

        Platforms.Remove(platform);
        Destroy(platform.GameObject);
    }

    private int DistanceFromPlayer(int number)
    {
        return Players.Min(p => Math.Abs(p.PlayerStat.CurrentSquare - number));
    }


    public void EnablePlatform(int number, float duration)
    {
        for (int i = 0; i < HeightPoints.Count; i++)
        {
            if (i == 0) // player's platform
            {
                PlatformData platform = Platforms.Find(p => p.Number == number);
                platform.SetHeight(HeightPoints[0], duration);

                continue;
            }

            int previousNumber = number - i;
            int nextNumber = number + i;

            PlatformData previousPlatform = Platforms.Find(p => p.Number == previousNumber);
            PlatformData nextPlatform = Platforms.Find(p => p.Number == nextNumber);

            if (previousPlatform.GameObject != null)
            {
                previousPlatform.SetHeight(HeightPoints[DistanceFromPlayer(previousNumber)], duration);
            }

            if (nextPlatform.GameObject == null)
            {
                CreatePlatform(nextNumber, HeightPoints[i]);
            }
            else
            {
                nextPlatform.SetHeight(HeightPoints[DistanceFromPlayer(nextNumber)], duration);
            }
        }

        int outOfRangePlatformNumber = number - HeightPoints.Count;
        if (DistanceFromPlayer(outOfRangePlatformNumber) >= HeightPoints.Count)
        {
            DestroyPlatform(outOfRangePlatformNumber);
        }
    }
}
