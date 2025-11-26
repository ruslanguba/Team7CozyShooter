using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hint : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TextMeshPro;
    [SerializeField] private List<string> HintText;

    private void Start()
    {
        m_TextMeshPro.text = HintText[0];
    }
}
