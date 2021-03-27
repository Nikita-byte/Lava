using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;
    private Vector3 _startPos;
    private float _minMoveDelay = 5f;
    private float _maxMoveDelay = 7f;
    private float _moveRadius = 5;
    private Vector3 _curDistanation;
    private float _changePosTime;
    private bool _isDead;

    private void Start()
    {
        _isDead = false;
        _changePosTime = 1;
        _startPos = transform.position;
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!_isDead)
        {
            _changePosTime -= Time.deltaTime;

            if (_changePosTime <= 0)
            {
                _curDistanation = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up) * new Vector3(_moveRadius, 0, 0) + _startPos;
                _agent.SetDestination(_curDistanation);
                _changePosTime = Random.Range(_minMoveDelay, _maxMoveDelay);
            }
        }
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

    public void Die()
    {
        _animator.enabled = false;
        _agent.enabled = false;
        _isDead = true;
    }
}
