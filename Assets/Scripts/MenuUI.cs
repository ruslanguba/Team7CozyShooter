using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] GameObject _mainMenuPanel;
    [SerializeField] GameObject _settingsPanel;
    [SerializeField] GameObject _exitPanel;

    [SerializeField] GameObject _menuParent;

    [SerializeField] GameObject _comics;

    private void Start()
    {
        GameManager.Instance.OnCloseMenu += CloseMenu;
        GameManager.Instance.OnOpenComics += OpenCommics;
        GameManager.Instance.OnOpenMenu += OpenMenu;
        _menuParent.SetActive(false);
        _mainMenuPanel.SetActive(false);
        _settingsPanel.SetActive(false);
        _comics.SetActive(false);
        _exitPanel.SetActive(false);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnCloseMenu -= CloseMenu;
        GameManager.Instance.OnOpenComics -= OpenCommics;
        GameManager.Instance.OnOpenMenu -= OpenMenu;
    }

    private void OpenMenu()
    {
        _menuParent.SetActive(true);
        _mainMenuPanel.gameObject.SetActive(true);
        _settingsPanel.gameObject.SetActive(false);
        _exitPanel.gameObject.SetActive(false);
        _comics.SetActive(false);
    }

    private void OpenCommics()
    {
        _menuParent.SetActive(false);
        _comics.SetActive(true);
    }

    private void CloseMenu() 
    {
        _menuParent.SetActive(false);
        _mainMenuPanel.SetActive(false);
        _comics.SetActive(false);
    }
}
