using UnityEngine;

public class LevelCompleatHandlerBase : MonoBehaviour
{
    [SerializeField] private LevelProgressSystem _progressSystem;
    [SerializeField] private PlayerDetectorTrigger _actionSender;
    [SerializeField] private DialogueTrigger _trigger;
    [SerializeField] private bool _isCompleat;
    [SerializeField] private string _levelID;
    private Animator _animator;


    void Start()
    {
        if(_progressSystem == null)
            _progressSystem = FindFirstObjectByType<LevelProgressSystem>();

        _actionSender = GetComponentInChildren<PlayerDetectorTrigger>();
        _animator = GetComponent<Animator>();

        _progressSystem.OnLevelCompleat += LevelCompleat;

        //_actionSender.OnTriggerEnterAction += FinalAction;
        _trigger.OnDialogueEnded += FinalAction;
        _actionSender.gameObject.SetActive(false);
    }

    private void FinalAction()
    {
        _animator.SetTrigger("start");
    }

    private void OnDisable()
    {
        _progressSystem.OnLevelCompleat -= LevelCompleat;
        _trigger.OnDialogueEnded -= FinalAction;
    }

    protected virtual void LevelCompleat()
    {
        _actionSender.gameObject.SetActive(true);
        _isCompleat = true;

        PlayerPrefs.SetInt(_levelID + "_Completed", 1);
        PlayerPrefs.Save();
    }
}
