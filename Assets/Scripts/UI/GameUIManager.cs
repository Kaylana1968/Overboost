using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;

    [SerializeField]
    TMP_Text MoveText;
    [SerializeField]
    TMP_Text SpeedText;

    void Awake()
    {
        Instance = this;
    }

    public void UpdateMoveText(string value)
    {
        MoveText.text = string.Format("Move : {0}", value);
    }

    public void UpdateSpeedText(string value)
    {
        SpeedText.text = string.Format("Speed : {0}", value);
    }
}
