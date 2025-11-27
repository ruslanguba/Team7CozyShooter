using UnityEngine;

public class Lobby1Progress : MonoBehaviour
{
    [SerializeField] private EnemyHealth[] first_Enemies;

    [SerializeField] private PillowGun pillowGun;

    [SerializeField] private DialogueTrigger first_Dilogue;
    [SerializeField] private DialogueTrigger second_Dilogue;

    [SerializeField] private Animator first_door;
    [SerializeField] private Animator second_door;

    [SerializeField] private int _counter;

    private void Start()
    {
        _counter = 0;
        foreach (EnemyHealth enemy in first_Enemies)
        {
            enemy.OnDeath += CalculateFirstQuestProgress;
        }

        first_Dilogue.OnDialogueEnded += OpenFirstDoor;
        second_Dilogue.OnDialogueEnded += OpenSecondDoor;

        pillowGun.enabled = false;
        second_Dilogue.gameObject.SetActive(false);
    }

    private void CalculateFirstQuestProgress(EnemyHealth enemy, int collisions)
    {
        _counter++;
        if(_counter >= 2)
        {
            second_Dilogue.gameObject.SetActive(true);
        }
    }
    private void OpenFirstDoor()
    {
        pillowGun.enabled = true;
        first_door.SetTrigger("open");
    }

    private void OpenSecondDoor()
    {
        second_door.SetTrigger("open");
    }
}
