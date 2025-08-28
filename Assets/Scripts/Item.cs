using Microsoft.Win32;
using UnityEngine;

public class Item : MonoBehaviour, IItem
{
    private UIManager uiManager;

    public enum Types
    {
        Coin,
        Ammo,
        Health,
    }
    public Types itemType;
    public int value = 10;

    public AudioClip pickupClip;

    private string tag;

    private AudioSource source;

    private float rotationSpeed = 60f;
    public float moveSpeed = 3f;
    private Vector3 moveDir = Vector3.up;
    public float moveTop = 0.5f;
    public float moveBottom = 0f;

    private void Awake()
    {
        tag = gameObject.tag;
        source = GameObject.FindWithTag(Tags.PlayerTag).GetComponent<AudioSource>();
        uiManager = GameObject.FindWithTag("GameController").GetComponent<UIManager>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);

        if(transform.position.y >= moveTop)
        {
            moveDir = Vector3.down;
        }
        if(transform.position.y <= moveBottom)
        {
            moveDir = Vector3.up;
        }

        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PlayerTag))
        {
            Use(other.gameObject);
            Destroy(gameObject);
        }
    }

    public void Use(GameObject go)
    {
        switch (itemType)
        {
            case Types.Ammo:
                {
                    var shooter = go.GetComponent<PlayerShooter>();
                    shooter?.gun?.AddAmmo(value);
                }
                break;
            case Types.Health:
                {
                    var playerHealth = go.GetComponent<PlayerHealth>();
                    playerHealth?.AddHealth(value);
                }
                break;
            case Types.Coin:
                uiManager.SetUpdateScore(value);
                break;
        }
        source.PlayOneShot(pickupClip);
    }
}
