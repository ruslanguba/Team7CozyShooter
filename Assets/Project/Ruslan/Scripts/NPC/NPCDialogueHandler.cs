using System.Collections;
using UnityEngine;

public class NPCDialogueHandler : MonoBehaviour
{
    [SerializeField] private float _timer;
    [SerializeField] private NPCHandler[] _handler;

    private void Start()
    {
        _handler[0].SetTalking(true);
        _handler[1].SetTalking(false);

        StartCoroutine(DialogueRutine());
    }

    IEnumerator DialogueRutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timer);
            for (int i = 0; i < _handler.Length; i++)
            {
                _handler[i].SwitchTalking();
            }
        }
    }
}
