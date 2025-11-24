using UnityEngine;

public class CollisionListener : MonoBehaviour
{
    private RicochetUI _ui;
    public void SetUI(RicochetUI ui)
    { 
        _ui = ui;
    }
    public void Bind(Bullet bullet)
    {
        bullet.OnCollision += HandleCollision;
        bullet.OnBulletDestroyed += Unbind;
    }

    private void Unbind(Bullet bullet)
    {
        bullet.OnCollision -= HandleCollision;
        bullet.OnBulletDestroyed -= Unbind;
    }

    private void HandleCollision(int count, Vector3 hitPoint)
    {
        _ui.ShowRicochet(count, hitPoint);
    }
}
