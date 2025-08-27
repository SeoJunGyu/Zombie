using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamagable
{
    public float MaxHealth = 100f;

    public float Health { get; protected set; }
    public bool IsDead { get; protected set; }

    public event Action OnDeath; //델리게이트

    public UIManager canvas;

    private void Awake()
    {
        canvas = GameObject.FindWithTag("GameController").GetComponent<UIManager>();
    }

    //Enable 함수 가상화
    protected virtual void OnEnable()
    {
        IsDead = false;
        Health = MaxHealth;
    }

    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        Health -= damage;

        if(Health <= 0 && !IsDead)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        //if(OnDeath != null)과 같은 의미다.
        OnDeath?.Invoke(); //연결된 함수 있으면 호출
        IsDead = true;
    }
}
