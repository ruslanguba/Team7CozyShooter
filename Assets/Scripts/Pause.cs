using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    public void PauseMenu()
    {
        Time.timeScale = 0;
    }

    public void ClosePauseMenu()
    {
        Time.timeScale = 1;
        _inputReader.enabled = true;
    }
}
