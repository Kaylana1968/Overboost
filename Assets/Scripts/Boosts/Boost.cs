using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Boost", menuName = "Scriptable Objects/Boost")]
public class Boost : ScriptableObject
{
    public string BoostName;
    public string Description;
    public Texture2D Image;
    public MonoBehaviour Script;
}
