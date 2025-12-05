using UnityEngine;
using UnityEngine.Audio;

public class LevelCompleatHandlerBase : MonoBehaviour
{
    [SerializeField] private LevelProgressSystem _progressSystem;
    [SerializeField] private PlayerDetectorTrigger _actionSender;
    [SerializeField] private DialogueTrigger _trigger;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _mumbelSounds;
    [SerializeField] private bool _isCompleat;
    [SerializeField] private string _levelID;
    private Animator _animator;


    void Start()
    {
        if(_progressSystem == null)
            _progressSystem = FindFirstObjectByType<LevelProgressSystem>();

        _actionSender = GetComponentInChildren<PlayerDetectorTrigger>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        _progressSystem.OnLevelCompleat += LevelCompleat;

        //_actionSender.OnTriggerEnterAction += FinalAction;
        _trigger.OnDialogueStarted += PlayTalkSound;
        _trigger.OnDialogueEnded += FinalAction;

        _actionSender.gameObject.SetActive(false);
    }

    private void PlayTalkSound()
    {
        int rnd = Random.Range(0, _mumbelSounds.Length);
        _audioSource.PlayOneShot(_mumbelSounds[rnd]);
    }

    private void FinalAction()
    {
        _audioSource?.Stop();
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
