using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcQueueManager : MonoBehaviour
{
    [SerializeField] private List<NPCMover> jumpers; // только персонажи
    [SerializeField] private float moveDuration = 1f;
    [SerializeField] private float delayBetweenJumps = 1.5f;

    private List<Vector3> queuePositions = new List<Vector3>();
    NPCMover _first;
    private void Start()
    {
        // Сохраняем стартовые позиции всех персонажей
        foreach (var j in jumpers)
            queuePositions.Add(j.transform.position);

        StartCoroutine(RunQueueLoop());
    }

    private IEnumerator RunQueueLoop()
    {
        while (true)
        {
            // 1. Первый прыгает
            _first = jumpers[0];
            TriggerJump();

            // 2. Ждём
            yield return new WaitForSeconds(delayBetweenJumps);

            // 3. Двигаем всех на позицию вперёд
            for (int i = 0; i < jumpers.Count; i++)
            {
                Vector3 targetPos = queuePositions[i];
                jumpers[i].MoveTovards(targetPos, moveDuration);
            }
            yield return new WaitForSeconds(moveDuration);
        }
    }

    private IEnumerator SmoothMove(Transform obj, Vector3 target, float time)
    {
        Vector3 start = obj.position;
        float t = 0;

        while (t < moveDuration)
        {
            t += Time.deltaTime / time;
            obj.position = Vector3.Lerp(start, target, t);
            yield return null;
        }
    }

    IEnumerator JumpRoutine()
    {
        jumpers.Remove(_first);
        yield return new WaitForSeconds(delayBetweenJumps);
        _first.transform.position = queuePositions[queuePositions.Count - 1];
        jumpers.Add(_first);
    }

    private void TriggerJump()
    {
        _first.JumpForward();
        Debug.Log(_first.name + " прыгает!");
        StartCoroutine(JumpRoutine());
    }
}
