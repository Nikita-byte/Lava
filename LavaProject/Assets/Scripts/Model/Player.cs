using UnityEngine;
using UnityEngine.AI;


public class Player : MonoBehaviour
{
    [SerializeField] private Transform _cameraPosition;
    private NavMeshAgent _agent;
    private Animator _animator;

    public Transform CameraPosition { get; }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _agent.speed = GamePreferences.Instance.PlayerSpeed;
    }

    private void FixedUpdate()
    {
        if (_agent.velocity.magnitude == 0)
        {
            _animator.SetBool("IsMoving", false);
        }
        else
        {
            _animator.SetBool("IsMoving", true);
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        _agent.SetDestination(point);
    }

    public void StopMoving()
    {
        _agent.isStopped = true;
    }

    public void StartMoving()
    {
        _agent.isStopped = false;
    }

    public Transform GetCameraPosition()
    {
        return _cameraPosition;
    }

    public void Fire(float shotingPower)
    {
        var bullet = ObjectPool.Instance.GetBullet();
        bullet.transform.position = _cameraPosition.position;
        bullet.gameObject.SetActive(true);
        bullet.GetComponent<Bullet>().Fire(shotingPower);
    }
}
