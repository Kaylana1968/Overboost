using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [SerializeField]
    private RawImage image;
    [SerializeField]
    private TMP_Text title;
    [SerializeField]
    private TMP_Text description;

    private Button _button;

    void Start()
    {
        _button = GetComponent<Button>();
    }

    public void SetCard(Boost boost, GameObject player, Canvas canvas)
    {
        image.texture = boost.Image;
        title.text = boost.BoostName;
        description.text = boost.Description;
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() =>
        {
            canvas.enabled = false;
            player.AddComponent(boost.Script.GetType());
            GameManager.Instance.GoOnNextRound();
        });
    }
}
