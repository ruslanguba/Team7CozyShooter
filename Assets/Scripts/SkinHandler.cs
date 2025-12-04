using UnityEngine;

public class SkinHandler : MonoBehaviour
{
    [SerializeField] private Animator[] _skins;
    [SerializeField] private PlayerContext _playerContext;
    private int _skinIndex;

    private void Awake()
    {
        _playerContext = GetComponent<PlayerContext>();
        _playerContext.SetAnimator(_skins[_skinIndex]);
    }

    private void ChangeSkin()
    {
        foreach (var skin in _skins) 
        {
            skin.gameObject.SetActive(false);
        }
        _skins[_skinIndex].gameObject.SetActive(true);
        _playerContext.SetAnimator(_skins[_skinIndex]);
    }

    public void SetSkin(int skinIndex)
    {
        if (_skinIndex < _skins.Length)
        {
            _skinIndex = skinIndex;
            ChangeSkin();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeSkin();
            _skinIndex++;

            if (_skinIndex % _skins.Length == 0)
                _skinIndex = 0;
        }
    }
}
