using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;

public class DialogueTrigger : MonoBehaviour
{
    public event Action OnDialogueStarted;
    public event Action OnDialogueEnded;

    [SerializeField] private bool deleteOnEnd;
    private PlayerDetectorTrigger _actionSender;
    [SerializeField] private List<Dialogue> sentence;
    [SerializeField] private List<Dialogue> sentenceEn;

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
        OnDialogueStarted?.Invoke();

        var selectedLocale = LocalizationSettings.SelectedLocale.Identifier;

        if (selectedLocale == "en")
        {
            Debug.Log("Selected English locale");
            DialogueManager.Instance.StartDialog(sentenceEn, this);
        }

        else
        {
            Debug.Log("Selected Russian or another locale");
            DialogueManager.Instance.StartDialog(sentence, this);
        }
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
            Destroy(this);
        }
    }
}
