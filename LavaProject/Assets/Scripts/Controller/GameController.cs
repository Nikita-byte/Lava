using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    private ShotZone _shotZone;
    private Vector3 _firstCameraPosition;
    private Quaternion _firstCameraRotation;
    private Player _player;
    private Camera _camera;

    private void Start()
    {
        ObjectPool.Instance.Initialize();
        _camera = Camera.main;
        _player = FindObjectOfType<Player>();
        _firstCameraPosition = _camera.transform.position;
        _firstCameraRotation = _camera.transform.rotation;

        ScreenInterface.GetInstance().GetFireModeButton().onClick.AddListener(delegate {
            ActivateShotMode();
        });

        ScreenInterface.GetInstance().GetMoveModeButton().onClick.AddListener(delegate {
            ActivateMoveMode();
        });

        ScreenInterface.GetInstance().GetRestartButton().onClick.AddListener(delegate {
            RestartLVL();
        });

        EventTrigger eventTrigger = ScreenInterface.GetInstance().GetFireButton().gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerDown
        };

        entry.callback.AddListener(delegate {
            Fire();
        });

        eventTrigger.triggers.Add(entry);

        ScreenInterface.GetInstance().Execute(GameMode.MoveMode);
        _playerController.SetGameMode(GameMode.MoveMode);

        _shotZone = FindObjectOfType<ShotZone>();
        _shotZone.InZone += PlayerInZone;
        _shotZone.OutZone += PlayerOutZone;
    }

    public void PlayerInZone()
    {
        ScreenInterface.GetInstance().Execute(GameMode.ShotZoneMode);
        _playerController.SetGameMode(GameMode.ShotZoneMode);
    }

    public void PlayerOutZone()
    {
        ScreenInterface.GetInstance().Execute(GameMode.MoveMode);
        _playerController.SetGameMode(GameMode.MoveMode);
    }

    private void ActivateShotMode()
    {
        _shotZone.gameObject.SetActive(false);
        ScreenInterface.GetInstance().Execute(GameMode.ShotMode);
        _playerController.SetGameMode(GameMode.ShotMode);
        _camera.transform.position = _player.GetCameraPosition().position;
        _camera.transform.rotation = _player.GetCameraPosition().rotation;
        _camera.fieldOfView = _camera.fieldOfView / 2;
    }

    private void ActivateMoveMode()
    {
        _shotZone.gameObject.SetActive(true);
        ScreenInterface.GetInstance().Execute(GameMode.ShotZoneMode);
        _playerController.SetGameMode(GameMode.ShotZoneMode);
        _camera.transform.position = _firstCameraPosition;
        _camera.transform.rotation = _firstCameraRotation;
        _camera.fieldOfView = _camera.fieldOfView * 2;
    }

    private void Fire()
    {
        _playerController.Fire();
    }

    private void RestartLVL()
    {
        SceneManager.LoadScene("SampleScene");
    }
}