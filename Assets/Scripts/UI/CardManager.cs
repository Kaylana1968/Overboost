using TMPro;
using Unity.VisualScripting;
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

    public void SetCard(Boost boost, PlayerController player)
    {
        image.texture = boost.Image;
        title.text = boost.BoostName;
        description.text = boost.Description;
        _button.onClick.RemoveAllListeners();
        // _button.onClick.AddListener(() => );
    }
}
