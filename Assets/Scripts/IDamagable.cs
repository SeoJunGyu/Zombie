using UnityEngine;

public interface IDamagable
{
    //������ ��ġ, ���� ��ġ, ���� �浹������ ���� ����
    void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal);
}
