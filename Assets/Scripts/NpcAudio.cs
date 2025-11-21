using UnityEngine;
using UnityEngine.Playables;

public class NpcAudio : MonoBehaviour
{
    private PlayableDirector pd;

    void Start()
    {
        pd = GetComponent<PlayableDirector>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pd.Play();
        }
    }
}
