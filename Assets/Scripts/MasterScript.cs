using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterScript : MonoBehaviour
{
    public GameObject leftWall, rightWall; // Walls to stop the player moving off the screen
    public GameObject statsUpgradeUI, gameOverUI; // UI objects
    public EnemySpawner enemySpawner; // The enemy spawner

    void Start()
    {
        // Set the walls to the edges of the screen
        SetScreenWalls();

        // Start the first wave of enemies
        Reference.Instance.enemySpawner.StartSpawing();
    }

    void Update()
    {
        // If escape key was pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Quit the game
            Application.Quit();
        }

        // If the space key was pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // If the upgrades UI is showing
            if (statsUpgradeUI.activeInHierarchy)
            {
                // Hide the upgrades UI
                statsUpgradeUI.SetActive(false);

                // Start the next wave of enemies
                Reference.Instance.enemySpawner.NextWave();
            }

            // If the game over UI is showing
            if (gameOverUI.activeInHierarchy)
            {
                // Restart the game
                SceneManager.LoadScene("Game");
            }
        }
    }

    public void GameOver()
    {
        // Make sure the upgrades UI is hidden
        statsUpgradeUI.SetActive(false);

        // Show the game over UI
        gameOverUI.SetActive(true);
    }

    public void WaveComplete()
    {
        // Show the upgrades UI
        statsUpgradeUI.SetActive(true);

        // Give the player some coins for completing the wave
        Reference.Instance.player.lifeCoins.Value += 10;
    }

    private void SetScreenWalls()
    {
        // Calculate the distance from the center of the screen to the edge
        float screenWidthHalf = Reference.Instance.mainCamera.orthographicSize * ((float)Screen.width / (float)Screen.height);

        // Set the wall positions
        leftWall.transform.position = new Vector3(screenWidthHalf * -1, 0, 0);
        rightWall.transform.position = new Vector3(screenWidthHalf, 0, 0);
    }
}
