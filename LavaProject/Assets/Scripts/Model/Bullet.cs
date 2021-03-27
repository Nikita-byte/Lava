using System;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    private float _shotingPower;
    private float _lifeTime = 5;
    private float _currentLifeTime;

    public void Fire(float shotingPower)
    {
        gameObject.GetComponent<Rigidbody>().AddForce( Camera.main.transform.forward * shotingPower, ForceMode.Impulse);
        _shotingPower = shotingPower;
    }

    private void Start()
    {
        _currentLifeTime = _lifeTime;
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            _currentLifeTime -= Time.deltaTime;

            if (_currentLifeTime <= 0)
            {
                _currentLifeTime = _lifeTime;
                ObjectPool.Instance.ReturnBullet(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyPart enemyPart;

        if (collision.gameObject.TryGetComponent<EnemyPart>(out enemyPart))
        {
            enemyPart.OffAnimator();
            enemyPart.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * _shotingPower*3, ForceMode.Impulse);
            Debug.Log(collision.transform.gameObject.name);
        }
    }
}
