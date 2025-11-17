using System.Collections.Generic;
using UnityEngine;

public class DynamicObjectsRegistry : MonoBehaviour
{
    private readonly List<Rigidbody> _dynamicObjects = new List<Rigidbody>();

    public IReadOnlyList<Rigidbody> DynamicObjects => _dynamicObjects;

    // Один раз собираем все объекты
    public void CollectAll()
    {
        _dynamicObjects.Clear();

        foreach (Rigidbody rb in Object.FindObjectsByType<Rigidbody>(0))
        {
            if (rb.gameObject.TryGetComponent(out Bullet bullet))
                continue; // исключаем снаряды
            _dynamicObjects.Add(rb);
        }
    }

    // Добавление объекта вручную (если появятся новые)
    public void Register(Rigidbody rb)
    {
        if (!_dynamicObjects.Contains(rb))
            _dynamicObjects.Add(rb);
    }

    // Удаление объекта из списка (например, при смерти врага)
    public void Unregister(Rigidbody rb)
    {
        _dynamicObjects.Remove(rb);
    }
}
