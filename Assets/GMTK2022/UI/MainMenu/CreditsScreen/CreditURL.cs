using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreditURL : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string url;
    
    private TextMeshProUGUI textUGUI;
    private readonly Color baseColor = new (1f, 0.97f, 0.92f);
    private readonly Color hoverColor = new(0f, 0.2f, 0.4f);
    

    private void Awake()
    {
        textUGUI = GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!string.IsNullOrWhiteSpace(url))
            Application.OpenURL(url);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textUGUI.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textUGUI.color = baseColor;
    }
}
