using UnityEngine.UI;
using UnityEngine;


public sealed class CanvasUI : MonoBehaviour
{
    [SerializeField] private Button _aimButton;
    [SerializeField] private Button _moveModeButton;
    [SerializeField] private Button _fireButton;
    [SerializeField] private Image _aim;
    [SerializeField] private Button _restart;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetMode(GameMode gameMode)
    {
        switch (gameMode)
        {
            case GameMode.MoveMode:
                _aimButton.gameObject.SetActive(false);
                _moveModeButton.gameObject.SetActive(false);
                _fireButton.gameObject.SetActive(false);
                _aim.gameObject.SetActive(false);
                break;
            case GameMode.ShotZoneMode:
                _aimButton.gameObject.SetActive(true);
                _moveModeButton.gameObject.SetActive(false);
                _fireButton.gameObject.SetActive(false);
                _aim.gameObject.SetActive(false);
                break;
            case GameMode.ShotMode:
                _aimButton.gameObject.SetActive(false);
                _moveModeButton.gameObject.SetActive(true);
                _fireButton.gameObject.SetActive(true);
                _aim.gameObject.SetActive(true);
                break;
        }
    }

    public Button GetRestartButton()
    {
        return _restart;
    }

    public Button GetAimButton()
    {
        return _aimButton;
    }

    public Button GetFireButton()
    {
        return _fireButton;
    }

    public Button GetMoveButton()
    {
        return _moveModeButton;
    }
}
