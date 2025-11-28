using System.Collections;
using UnityEngine;

public class PluhNPSAnimController : MonoBehaviour
{
    [SerializeField] LevelProgressSystem levelProgressSystem;
    [SerializeField] private NPCAnimationHandler npcAnimationHandler;
    [SerializeField] private DialogueTrigger dialogueTrigger;


    private void Awake()
    {
        if (levelProgressSystem == null)
            levelProgressSystem = FindFirstObjectByType<LevelProgressSystem>();
        npcAnimationHandler = GetComponent<NPCAnimationHandler>();
    }

    private void Start()
    {
        levelProgressSystem.OnLevelCompleat += StartGreeting;
        dialogueTrigger.OnDialogueStarted += SetApplauseAnim;
        npcAnimationHandler.SetIdleAnim();
    }

    private void OnDisable()
    {
        levelProgressSystem.OnLevelCompleat -= StartGreeting;
        dialogueTrigger.OnDialogueStarted -= SetApplauseAnim;
    }

    private void StartGreeting()
    {
        npcAnimationHandler.SetGreetingAnim();
    }

    private void SetApplauseAnim()
    {
        StartCoroutine(StartApplauseAndTalk());
    }

    private IEnumerator StartApplauseAndTalk()
    {
        npcAnimationHandler.SetisApplauseAnim();
        yield return new WaitForSeconds(3);
        npcAnimationHandler.SetTalkAnim();
    }
}
