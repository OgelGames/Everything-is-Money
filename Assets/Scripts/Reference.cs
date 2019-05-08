using UnityEngine;

// A basic reference class to eliminate FindObjectOfType() calls
public class Reference : MonoBehaviour
{
    #region Singleton
    public static Reference Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public Player player;
    public MasterScript masterScript;
    public EnemySpawner enemySpawner;
    public CameraShaker cameraShaker;
    public Camera mainCamera;
}
