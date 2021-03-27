using UnityEngine;
using DG.Tweening;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask _movementMask;

    private TypeOfShooting _typeOfShooting;
    private float _shotingPower = 30;
    private Player _player;
    private Camera _camera;
    private GameMode _gameMode;
    private float _raycastDistance = 100f;
    private Quaternion _startPos;
    private float _horizontalAngle;
    private float _verticalAngle;
    private float _mouseSens;
    private bool _isShotMode;
    private bool _isFire;
    private float _duration = 0.2f;
    private float _strength = 0.5f;
    private int _vibrato = 0;
    private float _randomness = 2;
    private bool _isColdown;
    private float _coldown;
    private float _coldownTime = 0;



    void Start()
    {
        _typeOfShooting = GamePreferences.Instance.TypeOfShooting;
        _mouseSens = GamePreferences.Instance.MouseSensetive;
        _coldown = GamePreferences.Instance.ShootingSpeed;
        _player = FindObjectOfType<Player>();
        _camera = Camera.main;
        _isShotMode = false;
        _isFire = false;
    }


    void Update()
    {
        if (_isFire)
        {
            _isColdown = true;
            _coldownTime = _coldown;
        }

        if (_isColdown)
        {
            _coldownTime -= Time.deltaTime;

            if (_coldownTime <= 0)
            {
                _isColdown = false;
            }
        }

        switch (_gameMode)
        {
            case GameMode.MoveMode:
            case GameMode.ShotZoneMode:
                _isShotMode = false;
                if (Input.GetMouseButtonDown(0))
                {
                    _player.StartMoving();
                    Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, _raycastDistance, _movementMask))
                    {
                        _player.MoveToPoint(hit.point);
                    }
                }
                break;

            case GameMode.ShotMode:
                _player.StopMoving();

                if (!_isFire)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        _startPos = _camera.transform.rotation;
                    }
                    else if (Input.GetMouseButton(0))
                    {
                        _isShotMode = true;
                        _horizontalAngle += Input.GetAxis("Mouse X") * _mouseSens;
                        _verticalAngle += Input.GetAxis("Mouse Y") * _mouseSens;
                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        _startPos = _camera.transform.rotation;
                        _horizontalAngle = 0;
                        _verticalAngle = 0;
                    }
                }
                else
                {
                    Tweener tweener = DOTween.Shake(() => _camera.transform.position, pos => _camera.transform.position = pos,
                        _duration, _strength, _vibrato, _randomness);

                    _isFire = false;
                }

                break;
        }
    }

    private void LateUpdate()
    {
        if (_isShotMode)
        {
            if (!_isFire)
            {
                _camera.transform.rotation = _startPos * Quaternion.AngleAxis(_horizontalAngle, Vector3.up) * Quaternion.AngleAxis(-_verticalAngle, Vector3.right);
            }
            else
            {
                _camera.transform.rotation *= Quaternion.AngleAxis(Random.Range(-1f, 1f), Vector3.up) * Quaternion.AngleAxis(Random.Range(0.1f, 1f), Vector3.right);
            }
        }
    }

    public void SetGameMode(GameMode gameMode)
    {
        _gameMode = gameMode;
    }

    public void Fire()
    {
        if (!_isColdown)
        {
            _isFire = true;

            switch (_typeOfShooting)
            {
                case TypeOfShooting.RaycastShot:
                    Ray ray = _camera.ScreenPointToRay(new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0));
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, _raycastDistance))
                    {
                        EnemyPart enemyPart;

                        if (hit.transform.gameObject.TryGetComponent<EnemyPart>(out enemyPart))
                        {
                            enemyPart.OffAnimator();
                            enemyPart.GetComponent<Rigidbody>().AddForce(ray.direction * 200, ForceMode.Impulse);
                            Debug.Log(hit.transform.gameObject.name);
                        }
                    }
                    break;

                case TypeOfShooting.PhisicsPower_x_1:
                    _player.Fire(_shotingPower);
                    break;
                case TypeOfShooting.PhisicsPower_x_2:
                    _player.Fire(_shotingPower * 2);
                    break;
                case TypeOfShooting.PhisicsPower_x_3:
                    _player.Fire(_shotingPower * 3);
                    break;
            }
        }
    }
}
