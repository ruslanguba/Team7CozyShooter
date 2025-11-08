using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{  
    public void PauseMenu()
    {
        Time.timeScale = 0;
    }

    public void ClosePauseMenu()
    {
        Time.timeScale = 1;
    }
}
