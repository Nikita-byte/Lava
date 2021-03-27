using System;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPool
{
    private static ObjectPool _objectPool = new ObjectPool();

    public static ObjectPool Instance { get { return _objectPool; } }

    private readonly string POOLNAME = "[ObjectPool]";
    private int _bulletCount = 20;
    private Queue<GameObject> _bullets;
    private GameObject Pool;

    public void Initialize()
    {
        Pool = new GameObject(POOLNAME);
        _bullets = new Queue<GameObject>();
        GameObject _temp = Resources.Load<GameObject>(AssetsPath.Path[GameObjectType.Bullet]);

        for (int i = 0; i < _bulletCount; i++)
        {
            var go = GameObject.Instantiate(_temp, Pool.transform);
            go.gameObject.SetActive(false);
            _bullets.Enqueue(go);
        }
    }

    public GameObject GetBullet()
    {
        return _bullets.Dequeue();
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        bullet.gameObject.SetActive(false);
        _bullets.Enqueue(bullet);
    }
}
