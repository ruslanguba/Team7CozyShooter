using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    public event Action<bool> OnDialogueActive;
    public event Action OnDialogueEnded;

    public static DialogueManager Instance;

    [SerializeField] private DialogueUI dialogueUIPrefab;
    private DialogueUI dialogueUI;

    private Queue<Dialogue> sentenses;
    private DialogueTrigger _currentTrigger;



    public bool IsDialogActive { get; private set; }

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

    private void Update()
    {
        if(!IsDialogActive)
            return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }
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
        IsDialogActive = true;
        OnDialogueActive?.Invoke(IsDialogActive);
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
        Debug.Log("next Sentence");
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
        IsDialogActive = false;
        OnDialogueActive?.Invoke(IsDialogActive);
        _currentTrigger.DialogueCompleted();
        dialogueUI.HidePanel();
        Debug.Log("End Dialogue");
    }
}
