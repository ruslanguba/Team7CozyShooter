using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField] private DialogueUI dialogueUIPrefab;
    private DialogueUI dialogueUI;

    private Queue<Dialogue> sentenses;
    private DialogueTrigger _currentTrigger;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        CreateEventSystemIfNoExist();
        dialogueUI = Instantiate(dialogueUIPrefab);
        sentenses = new Queue<Dialogue>();
    }

    private void CreateEventSystemIfNoExist()
    {
        if (EventSystem.current == null)
        {
            GameObject esGO = new GameObject("EventSystem");
            esGO.AddComponent<EventSystem>();
            esGO.AddComponent<StandaloneInputModule>();
        }
    }

    public void StartDialog(List<Dialogue> dialogs, DialogueTrigger trigger)
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        PlayerManager.Instance.DisableInput();
        sentenses.Clear();
        dialogueUI.ShowPanel();
        _currentTrigger = trigger;
        foreach (Dialogue sentence in dialogs)
        {
            sentenses.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentenses.Count == 0)
        {
            EndDialog();
            return;
        }
        Dialogue sentence = sentenses.Dequeue();
        dialogueUI.ShowNexSentence(sentence.IsPluh, sentence.Sentence);
    }

    private void EndDialog()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        PlayerManager.Instance.EnableInput();
        _currentTrigger.DialogueCompleted();
        dialogueUI.HidePanel();
        Debug.Log("End Dialogue");
    }
}
