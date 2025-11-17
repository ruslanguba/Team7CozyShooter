using UnityEngine;

public class SceneTransitionTrigger : MonoBehaviour
{
    [SerializeField] private string _sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerSafePosition>() != null)
        {
            var request = new SceneTransitionRequest(_sceneToLoad);
            SceneTransitionManager.Instance.Transition(request);
            Debug.Log("trigger");
        }
    }
}
