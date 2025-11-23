using UnityEngine;

public class DisableText : MonoBehaviour
{
    [SerializeField] private GameObject[] _textsToDisable;

    public void DisableAll()
    {
        foreach (GameObject obj in _textsToDisable)
        {
            obj.SetActive(false);
        }
    }
}
