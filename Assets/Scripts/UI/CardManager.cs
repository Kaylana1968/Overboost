using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [SerializeField]
    Button Button;
    [SerializeField]
    RawImage Image1;
    [SerializeField]
    RawImage Image2;

    public void SetCard(Boost boost1, Boost boost2)
    {
        Image1.texture = boost1.Image;
        Image2.texture = boost2.Image;
        Button.onClick.RemoveAllListeners();
        Button.onClick.AddListener(() =>
        {
            BoostScreenManager.Instance.SetBoosts(boost1, boost2);
        });
    }
}
