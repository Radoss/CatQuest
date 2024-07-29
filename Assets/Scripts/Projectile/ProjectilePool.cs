using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool
{
    private Transform _prefab;
    private Transform _parent;

    private List<Transform> _pool;

    public ProjectilePool(Transform prefab, Transform parent, int count)
    {
        _prefab = prefab;
        _parent = parent;
        CreatePool(count);
    }

    private void CreatePool(int count)
    {
        _pool = new List<Transform>();

        for (int i = 0; i < count; i++)
        {
            CreateAndAddObject(false);
        }
    }

    private Transform CreateAndAddObject(bool isActivated)
    {
        var newPoolObj = Object.Instantiate(_prefab, _parent);
        newPoolObj.gameObject.SetActive(isActivated);
        _pool.Add(newPoolObj);
        return newPoolObj;
    }

    public Transform GetFreeElement()
    {
        foreach(var mono in _pool)
        {
            if(!mono.gameObject.activeSelf)
            {
                mono.gameObject.SetActive(true);
                return mono;
            }
        }
        return CreateAndAddObject(true);
    }

    public void ReturnElement(Transform element)
    {
        if (_pool.Contains(element))
        {
            element.gameObject.SetActive(false);
        }
    }
}
