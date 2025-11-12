using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmoury : MonoBehaviour
{
    [SerializeField] private Guns[] _guns;
    [SerializeField] private int _currentGunIndex;
    [SerializeField] private InputReader input;

    void Start()
    {       
        TakeGun(_currentGunIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TakeGun(0);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TakeGun(1);
        }
    }

    public void TakeGun(int gunIndex)
    {
        _currentGunIndex = gunIndex;

        for (int i = 0; i < _guns.Length; i++)
        {
            if (i == gunIndex)
            {
                _guns[i].SetInputReader(input);
                _guns[i].Activate();
            }

            else
            {
                _guns[i].Deactivate();
            }
        }
    }

    public void AddBulletsForGun(int gunIndex, int numberOfBullets)
    {
        _guns[gunIndex].AddBullets(numberOfBullets);
    }
}
