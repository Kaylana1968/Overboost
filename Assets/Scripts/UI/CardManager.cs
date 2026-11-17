using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [SerializeField]
    private RawImage Image;
    [SerializeField]
    private TMP_Text Title;
    [SerializeField]
    private TMP_Text Description;
    [SerializeField]
    private Button Button;

    public void SetCard(Boost boost, GameObject player, Canvas canvas)
    {
        Image.texture = boost.Image;
        Title.text = boost.BoostName;
        Description.text = boost.Description;
        Button.onClick.RemoveAllListeners();
        Button.onClick.AddListener(() =>
        {
            canvas.enabled = false;
            player.AddComponent(boost.Script.GetType());
            GameManager.Instance.GoOnNextRound();
        });
    }
}
