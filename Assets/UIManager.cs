using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text ammoText;
    public Text scoreText;
    public Text waveText;
    public GameObject gameOver;

    private GameObject player;
    private PlayerShooter playerGun;

    int score = 0;

    private Zombie zombie;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        gameOver.SetActive(false);
        score = 0;

        playerGun = player.GetComponent<PlayerShooter>();
        
    }

    public void OnEnable()
    {
        //UpdateAmmo(0, 0);
        SetUpdateScore(0);
        SetWaveInfo(0, 0);
        SetActiveGameOverUi(false);
    }

    public void UpdateAmmo(int magAmmo, int remainAmmo)
    {
        ammoText.text = $"{magAmmo}/{remainAmmo}";
    }

    public void SetUpdateScore(int score)
    {
        scoreText.text = $"Score: {this.score += score}";
    }

    public void SetWaveInfo(int wave, int count)
    {
        waveText.text = $"Wave:{wave}\nEnemy Left: {count}";
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetActiveGameOverUi(bool active)
    {
        gameOver.SetActive(active);
    }
}
