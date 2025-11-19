using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _enemiesToKill;

    public void UpdateEnemiesToKillText(string text)
    {
        _enemiesToKill.text = text;
    }

}
