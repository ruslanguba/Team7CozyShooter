using UnityEngine;

public class ActorAudio : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip _deathClip;
    [SerializeField] private AudioClip _attackClip;
    [SerializeField] private AudioClip _jumpClip;

    [Header("Footsteps")]
    [SerializeField] private AudioClip[] _footstepClips;
    [SerializeField] private float _stepInterval = 0.5f;
    [SerializeField] private float _pitchRandom = 0.1f;

    private int _lastFootstepIndex = -1;

    [Header("Random Clips")]
    public AudioClip[] randomClips;
    public Vector2 randomInterval = new Vector2(3f, 7f);
    public float pitchRandom = 0.15f;

    private float _nextRandomTime;
    private float _stepTimer;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        ScheduleNextRandom();
    }
    private void Update()
    {
        // Вызов случайного звука таймером
        if (Time.time >= _nextRandomTime)
        {
            PlayRandomClip();
            ScheduleNextRandom();
        }
    }
    private void PlayRandomClip()
    {
        if (randomClips.Length == 0) return;

        audioSource.pitch = 1f + Random.Range(-pitchRandom, pitchRandom);
        var clip = randomClips[Random.Range(0, randomClips.Length)];
        audioSource.PlayOneShot(clip);
    }

    private void ScheduleNextRandom()
    {
        _nextRandomTime = Time.time + Random.Range(randomInterval.x, randomInterval.y);
    }
    public void PlayDeath()
    {
        if (_deathClip != null)
            audioSource.PlayOneShot(_deathClip);
    }

    public void PlayAttack()
    {
        if (_attackClip != null)
            audioSource.PlayOneShot(_attackClip);
    }

    public void PlayJump()
    {
        if (_jumpClip != null)
            audioSource.PlayOneShot(_jumpClip);
    }
    public void TickFootsteps(bool isMoving)
    {
        if (!isMoving)
        {
            _stepTimer = 0;
            return;
        }

        _stepTimer -= Time.deltaTime;
        if (_stepTimer <= 0f)
        {
            PlayStep();
            _stepTimer = _stepInterval;
        }
    }

    private void PlayStep()
    {
        if (_footstepClips == null || _footstepClips.Length == 0)
            return;

        // Случайный индекс, но не повторяем тот же самый звук дважды подряд
        int index;
        do
        {
            index = Random.Range(0, _footstepClips.Length);
        }
        while (index == _lastFootstepIndex && _footstepClips.Length > 1);

        _lastFootstepIndex = index;

        audioSource.pitch = 1f + Random.Range(-_pitchRandom, _pitchRandom);
        audioSource.PlayOneShot(_footstepClips[index]);
    }

}
