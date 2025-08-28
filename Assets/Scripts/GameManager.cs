using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager;

    public ZombieSpawner zombieSpawner;

    private int score;

    public bool IsGameOver { get; set; }

    private void Start()
    {
        var findGo = GameObject.FindWithTag("Player");
        var playerHealth = findGo.GetComponent<PlayerHealth>();
        if(playerHealth != null)
        {
            playerHealth.OnDeath += EndGame;
        }
    }

    public void AddScore(int add)
    {
        score += add;
        uiManager.SetUpdateScore(score);
    }

    public void EndGame()
    {
        IsGameOver = true;
        uiManager.SetActiveGameOverUi(true);
        zombieSpawner.enabled = false;
    }
}
