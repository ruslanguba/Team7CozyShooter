using UnityEngine;

public class LevelCompleatHandlerBase : MonoBehaviour
{
    [SerializeField] private LevelProgressSystem _progressSystem;
    [SerializeField] private bool _isCompleat;

    void Start()
    {
        if(_progressSystem == null)
            _progressSystem = FindFirstObjectByType<LevelProgressSystem>();

        _progressSystem.OnLevelCompleat += LevelCompleat;
    }

    protected virtual void LevelCompleat()
    {
        _isCompleat = true;
    }
}
