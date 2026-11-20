using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    [Header("First boost")]
    [SerializeField]
    TMP_Text FirstTitle;
    [SerializeField]
    RawImage FirstImage;
    [SerializeField]
    TMP_Text FirstDescription;
    
    [Header("Second boost")]
    [SerializeField]
    TMP_Text SecondTitle;
    [SerializeField]
    RawImage SecondImage;
    [SerializeField]
    TMP_Text SecondDescription;

    public void SetBoosts(Boost boost1, Boost boost2)
    {
        FirstTitle.text = boost1.BoostName;
        FirstImage.texture = boost1.Image;
        FirstDescription.text = boost1.Description;
        
        SecondTitle.text = boost2.BoostName;
        SecondImage.texture = boost2.Image;
        SecondDescription.text = boost2.Description;
    }
}
