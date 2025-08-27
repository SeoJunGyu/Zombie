using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamagable
{
    public float MaxHealth = 100f;

    public float Health { get; protected set; }
    public bool IsDead { get; protected set; }

    public event Action OnDeath; //��������Ʈ

    public UIManager canvas;

    private void Awake()
    {
        canvas = GameObject.FindWithTag("GameController").GetComponent<UIManager>();
    }

    //Enable �Լ� ����ȭ
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
        //if(OnDeath != null)�� ���� �ǹ̴�.
        OnDeath?.Invoke(); //����� �Լ� ������ ȣ��
        IsDead = true;
    }
}
