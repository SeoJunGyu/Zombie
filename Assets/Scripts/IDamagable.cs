using UnityEngine;

public interface IDamagable
{
    //데미지 수치, 맞은 위치, 맞은 충돌지점의 수직 벡터
    void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal);
}
