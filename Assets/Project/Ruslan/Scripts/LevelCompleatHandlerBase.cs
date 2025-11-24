using UnityEngine;

public class LevelCompleatHandlerBase : MonoBehaviour
{
    [SerializeField] private LevelProgressSystem _progressSystem;
    [SerializeField] private FinalTriggerActionSender _actionSender;
    [SerializeField] private bool _isCompleat;
    private Animator _animator;


    void Start()
    {
        if(_progressSystem == null)
            _progressSystem = FindFirstObjectByType<LevelProgressSystem>();

        _actionSender = GetComponentInChildren<FinalTriggerActionSender>();
        _animator = GetComponent<Animator>();
        _actionSender.gameObject.SetActive(false);
        _progressSystem.OnLevelCompleat += LevelCompleat;
        _actionSender.OnTriggerEnterAction += FinalAction;
    }

    private void FinalAction(bool obj)
    {
        _animator.SetTrigger("start");
    }

    private void OnDisable()
    {
        _progressSystem.OnLevelCompleat -= LevelCompleat;
    }

    protected virtual void LevelCompleat()
    {
        _actionSender.gameObject.SetActive(true);
        _isCompleat = true;
    }
}
