using UnityEngine;

public class CursorActivatorInMenu : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        //QualitySettings.vSyncCount = 0;   // Отключаем vSync
        Application.targetFrameRate = 60; // Ограничиваем FPS
    }
}
