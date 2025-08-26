using Microsoft.Win32;
using UnityEngine;

public class Item : MonoBehaviour, IItem
{
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

    private void Awake()
    {
        tag = gameObject.tag;
        source = GameObject.FindWithTag(Tags.PlayerTag).GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
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
                Debug.Log("Coin");
                break;
        }
        source.PlayOneShot(pickupClip);
    }
}
