using UnityEngine;

public class CollisionListener : MonoBehaviour
{
    private RicochetUI _ui;
    private Bullet _bullet;

    public void SetUI(RicochetUI ui)
    {
        _ui = ui;
    }

    public void Bind(Bullet bullet)
    {
        _bullet = bullet;
        _bullet.OnCollision += HandleCollision;
        _bullet.OnBulletDestroyed += Unbind;
    }

    private void Unbind(Bullet bullet)
    {
        if (bullet == null || bullet.Equals(null)) return;

        bullet.OnCollision -= HandleCollision;
        bullet.OnBulletDestroyed -= Unbind;

        if (_bullet == bullet)
            _bullet = null;
    }

    private void HandleCollision(int count, Vector3 hitPoint)
    {
        if (_ui == null || _ui.Equals(null))
            return;

        _ui.ShowRicochet(count, hitPoint);
    }

    private void OnDestroy()
    {
        if (_bullet != null)
            Unbind(_bullet);
    }
}
