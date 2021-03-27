using UnityEngine;

[CreateAssetMenu(fileName = "GamePreferences", menuName = "Preferences")]
public class GamePreferences : ScriptableObject
{
    [SerializeField] private TypeOfShooting _typeOfShooting;
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _shootingSpeed;
    [SerializeField] private float _mouseSensetive;

    private static GamePreferences _instance; 

    public static GamePreferences Instance 
    { 
        get 
        {
            if (_instance == null)
            {
                _instance = Resources.Load<GamePreferences>(AssetsPath.Path[GameObjectType.Preferences]);
            }
            return _instance;
        } 
    }

    public TypeOfShooting TypeOfShooting { get { return _typeOfShooting; } }
    public float PlayerSpeed { get { return _playerSpeed; } }
    public float ShootingSpeed { get { return _shootingSpeed; } }
    public float MouseSensetive { get { return _mouseSensetive; } }
}