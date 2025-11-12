using UnityEngine;
using UnityEngine.UI;

public class PaintGun : Guns
{
    [SerializeField] private int _numberOfBullets = 10;
    [SerializeField] private Text _textOfBullets;
    [SerializeField] private PlayerArmoury _playerArmoury;

    public override void Shot()
    {
        if (_numberOfBullets > 0)
        {
            base.Shot();
            _numberOfBullets--;
            UpdateText();
        }
        

        if ( _numberOfBullets <= 0)
        {
            _playerArmoury.TakeGun(0);
        }
    }

    public override void Activate()
    {
        base.Activate();
        _textOfBullets.gameObject.SetActive(true);
        UpdateText();
    }

    public override void Deactivate()
    {
        base.Deactivate();
        _textOfBullets.gameObject.SetActive(false);
    }

    public override void AddBullets(int numberOfBullets)
    {
        base.AddBullets(numberOfBullets);
        _numberOfBullets += numberOfBullets;
        UpdateText();
    }

    void UpdateText()
    {
        _textOfBullets.text = _numberOfBullets.ToString();
    }
}
