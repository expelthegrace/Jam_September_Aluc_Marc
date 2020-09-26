using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
 
 public class ChangeText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private Text myText;

    void Start()
    {
        myText = GetComponentInChildren<Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        myText.color = Color.grey;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        myText.color = Color.black;
    }
}

