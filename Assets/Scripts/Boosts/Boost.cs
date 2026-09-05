using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Boost", menuName = "Scriptable Objects/Boost")]
public class Boost : ScriptableObject
{
    public string boostName;
    public string description;
    public MonoScript script;
}
