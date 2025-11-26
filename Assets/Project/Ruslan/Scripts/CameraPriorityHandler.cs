using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraPriorityHandler : MonoBehaviour
{
    CinemachineCamera _cinCamera;
    DialogueTrigger _dialogueTrigger;

    private void Start()
    {
        _cinCamera = GetComponent<CinemachineCamera>();
        _dialogueTrigger = GetComponentInParent<DialogueTrigger>();
        _dialogueTrigger.OnDialogueStarted += SetCameraPriority;
        _dialogueTrigger.OnDialogueEnded += ResetCameraPriority;
        _cinCamera.Priority = -1;
    }

    private void OnDisable()
    {
        _dialogueTrigger.OnDialogueStarted -= SetCameraPriority;
        _dialogueTrigger.OnDialogueEnded -= ResetCameraPriority;
    }
    private void SetCameraPriority()
    {
        _cinCamera.Priority = 1;
    }

    private void ResetCameraPriority()
    {
        _cinCamera.Priority = -1;
    }
}
