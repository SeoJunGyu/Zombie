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

    public bool IsGameOver { get; private set; }

    private Zombie zombie;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        gameOver.SetActive(false);
        IsGameOver = false;
        score = 0;

        playerGun = player.GetComponent<PlayerShooter>();
        player.GetComponent<LivingEntity>().OnDeath += OnPlayerDead;
        
    }

    private void Update()
    {
        UpdateAmmo();
    }

    public void UpdateAmmo()
    {
        string ammo = $"{playerGun.gun.magAmmo}/{playerGun.gun.ammoRemain}";
        ammoText.text = ammo;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnPlayerDead()
    {
        IsGameOver = true;
        gameOver.SetActive(true);
    }
}
