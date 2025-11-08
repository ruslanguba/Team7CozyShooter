using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text _totalCoinsText;
    [SerializeField] private Text _totalNightmareText;

    public static ScoreManager Instance;
    public static int TotalCoins;
    public static int TotalNightmare;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        PlayerPrefs.SetInt("TotalCoins", TotalCoins);
        _totalCoinsText.text = PlayerPrefs.GetInt("TotalCoins", 0).ToString();
        PlayerPrefs.SetInt("TotalNightmare", TotalNightmare);
        _totalNightmareText.text = PlayerPrefs.GetInt("TotalNightmare", 0).ToString();
    }

    public void AddCoins(int digit)
    {
        TotalCoins += digit;

        PlayerPrefs.SetInt("TotalCoins", TotalCoins);
        _totalCoinsText.text = TotalCoins.ToString();      
    }

    public void AddNightmare(int score)
    {
        TotalNightmare += score;

        PlayerPrefs.SetInt("TotalNightmare", TotalNightmare);
        _totalNightmareText.text = TotalNightmare.ToString();
    }
}
