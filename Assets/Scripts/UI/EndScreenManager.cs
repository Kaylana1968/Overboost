using TMPro;
using UnityEngine;

public class EndScreenManager : MonoBehaviour
{
    public static EndScreenManager Instance { get; private set; }

    [SerializeField]
    Canvas canvas;
    [SerializeField]
    TMP_Text WinnerText;

    void Awake()
    {
        Instance = this;
    }

    public void ToggleWinner(int winner)
    {
        canvas.enabled = true;

        if (winner == 0)
        {
            WinnerText.text = "Draw";
        }
        else if (winner == 1)
        {
            WinnerText.text = "Player 1 wins";
        }
        else
        {
            WinnerText.text = "Player 2 wins";
        }
    }
}
