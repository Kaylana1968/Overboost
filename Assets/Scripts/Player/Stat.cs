using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    private int _baseSpeed = 1;
    private readonly List<float> _speedMultipliers = new();
    private int _additionnalSpeed = 0;

    public int _turnCount = 3;
    public int Speed
    {
        get
        {
            int finalValue = _baseSpeed;
            foreach (float mult in _speedMultipliers)
            {
                finalValue = Mathf.FloorToInt(finalValue * mult);
            }
            finalValue += _additionnalSpeed;
            return finalValue;
        }
    }

    public void AddBaseValue(int value) => _baseSpeed += value;
    public void AddMultiplier(float multiplier) => _speedMultipliers.Add(multiplier);
    public void RemoveMultiplier(float multiplier) => _speedMultipliers.Remove(multiplier);
    public void AddAdditionnalValue(int value) => _additionnalSpeed += value;
}