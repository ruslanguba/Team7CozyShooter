using System.Collections;
using UnityEngine;

public class NPCHandler : MonoBehaviour
{
    [SerializeField] private GameObject _dialogCanvas;
    [SerializeField] private float _rotationSpeed = 90;
    private HintActivatorTrigger _dialogActivatorTrigger;
    private Animator _animator;

    private Coroutine _lookCoroutine;
    private bool _isTurning;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _dialogCanvas.SetActive(false);
    }

    private void OnEnable()
    {
        if (GetComponentInChildren<HintActivatorTrigger>() != null)
        {
            _dialogActivatorTrigger = GetComponentInChildren<HintActivatorTrigger>();
            _dialogActivatorTrigger.OnTriggerActivated += SwitchHint;
        }
    }

    private void OnDisable()
    {
        if (_dialogActivatorTrigger != null)
            _dialogActivatorTrigger.OnTriggerActivated -= SwitchHint;
    }

    private void SwitchHint(bool isShowing, Vector3 direction)
    {
        if (_lookCoroutine != null)
            StopCoroutine(_lookCoroutine);
        _lookCoroutine = StartCoroutine(SmoothLookAt(direction));
        _dialogCanvas.SetActive(isShowing);
        SwitchTalking(isShowing);
    }

    public void SwitchTalking(bool isTalking)
    {
        _animator.SetBool("isTalk", isTalking);
    }

    private IEnumerator SmoothLookAt(Vector3 targetPos)
    {
        _isTurning = true;
        Vector3 direction = targetPos - transform.position;
        direction.y = 0;
        if (direction.sqrMagnitude < 0.01f)
        {
            _isTurning = false;
            yield break;
        }

        Quaternion targetRot = Quaternion.LookRotation(direction);

        while (_isTurning)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRot,
                _rotationSpeed * Time.deltaTime
            );

            // Поворот закончен
            if (Quaternion.Angle(transform.rotation, targetRot) < 0.5f)
            {
                _isTurning = false;
                break;
            }
            yield return null;
        }
    }
}
