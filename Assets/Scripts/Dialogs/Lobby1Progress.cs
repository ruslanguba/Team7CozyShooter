using System.Collections;
using UnityEngine;

public class Lobby1Progress : MonoBehaviour
{
    [SerializeField] private EnemyHealth[] first_Enemies;
    [SerializeField] private EnemyHealth[] second_Enemies;

    //[SerializeField] private PillowGun pillowGun;

    [SerializeField] private DialogueTrigger first_Dilogue;
    [SerializeField] private DialogueTrigger second_Dilogue;
    //[SerializeField] private DialogueTrigger third_Dilogue;

    [SerializeField] private Animator first_door;
    [SerializeField] private Animator second_door;

    [SerializeField] private NPCAnimationHandler npcAnimationHandler;

    [SerializeField] private int _counter;

    private void Start()
    {
        _counter = 0;
        foreach (EnemyHealth enemy in first_Enemies)
        {
            enemy.OnDeath += CalculateFirstQuestProgress;
        }

        foreach (EnemyHealth enemy in second_Enemies)
        {
            enemy.OnDeath += CalculateSecondQuestProgress;
        }

        first_Dilogue.OnDialogueStarted += SetTalkAnim;
        first_Dilogue.OnDialogueEnded += OpenFirstDoor;

        second_Dilogue.OnDialogueStarted += SetApplauseAnim;

        //third_Dilogue.OnDialogueStarted += SetApplauseAnim;

        //pillowGun.Deactivate();
        second_Dilogue.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        first_Dilogue.OnDialogueStarted -= SetTalkAnim;
        first_Dilogue.OnDialogueEnded -= OpenFirstDoor;

        second_Dilogue.OnDialogueStarted -= SetApplauseAnim;

        //third_Dilogue.OnDialogueStarted -= SetApplauseAnim;
    }
    private void CalculateFirstQuestProgress(EnemyHealth enemy, int collisions)
    {
        _counter++;
        if(_counter >= 2)
        {
            second_Dilogue.gameObject.SetActive(true);
            npcAnimationHandler.SetGreetingAnim();
            _counter = 0;
        }
    }
    private void CalculateSecondQuestProgress(EnemyHealth enemy, int collisions)
    {
        _counter++;
        if (_counter >= second_Enemies.Length)
        {
            npcAnimationHandler.SetGreetingAnim();
            OpenSecondDoor();
        }
    }

    private void OpenFirstDoor()
    {
        //pillowGun.Activate();
        first_door.SetTrigger("open");
        npcAnimationHandler.SetIdleAnim();
    }

    private void OpenSecondDoor()
    {
        second_door.SetTrigger("open");
        npcAnimationHandler.SetIdleAnim();
    }

    private void SetTalkAnim()
    {
        npcAnimationHandler.SetTalkAnim();
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
