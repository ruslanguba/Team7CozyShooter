using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text tooltipText;
    
    void Start()
    {
        tooltipText.alpha = 0f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipText.alpha = 1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipText.alpha = 0f;
    }
}
