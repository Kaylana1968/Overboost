using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    private int _turnCount = 3;
    private int _baseSpeed = 1;
    private readonly List<float> _speedMultipliers = new();
    private readonly List<int> _additionnalSpeed = new();

    public int Speed
    {
        get
        {
            int finalValue = _baseSpeed;
            foreach (float mult in _speedMultipliers)
            {
                finalValue = Mathf.FloorToInt(finalValue * mult);
            }
            return finalValue;
        }
    }

    public void AddBaseValue(int count) => _baseSpeed += count;
    public void AddMultiplier(float multiplier) => _speedMultipliers.Add(multiplier);
    public void RemoveMultiplier(float multiplier) => _speedMultipliers.Remove(multiplier);
    public void AddAdditionnalValue(int value) => _additionnalSpeed.Add(value);
    public void RemoveAdditionnalValue(int value) => _additionnalSpeed.Remove(value);
}