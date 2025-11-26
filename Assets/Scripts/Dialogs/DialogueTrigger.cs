using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public event Action OnDialogueEnded;
    [SerializeField] private bool deleteOnEnd;
    private PlayerDetectorTrigger _actionSender;
    [SerializeField] private List<Dialogue> sentence;

    private void OnEnable()
    {
        _actionSender = GetComponentInChildren<PlayerDetectorTrigger>();
        _actionSender.OnTriggerEnterAction += TriggerDialog;
    }

    private void OnDisable()
    {
        _actionSender.OnTriggerEnterAction -= TriggerDialog;
    }

    public void TriggerDialog()
    {
        DialogueManager.Instance.StartDialog(sentence, this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerSafePosition>() != null)
        {
            TriggerDialog();
        }
    }

    public void DialogueCompleted()
    {
        OnDialogueEnded?.Invoke();  // ≈сли событи€ нет - ничего не произойдет
        if (deleteOnEnd)
        {
            Destroy(gameObject);
        }
    }
}
