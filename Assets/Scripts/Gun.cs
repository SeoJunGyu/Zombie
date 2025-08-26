using System;
using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum State
    {
        Ready,
        Empty,
        Reloading,
    }

    private State currentState = State.Ready;
    public State CurrentState
    {
        get { return currentState; }
        private set
        {
            currentState = value;
            switch(currentState)
            {
                case State.Ready:
                    break;
                case State.Empty:
                    break;
                case State.Reloading:
                    break;
            }
        }
    }

    public GunData gunData;

    public ParticleSystem muzzleEffect;
    public ParticleSystem shellEffect;

    private LineRenderer lineRenderer;
    private AudioSource audioSource;

    public Transform firePosition;

    public int ammoRemain;
    public int magAmmo; //탄창에 남은 탄환

    private float lastFireTime;


    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();

        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
    }

    private void OnEnable()
    {
        ammoRemain = gunData.startAmmoRemain;
        magAmmo = gunData.magCapacity;

        lastFireTime = 0f;

        CurrentState = State.Ready;
    }

    private void Update()
    {
        switch(currentState)
        {
            case State.Ready:
                UpdateReady();
                break;
            case State.Empty:
                UpdateEmpty();
                break;
            case State.Reloading:
                UpdateReloading();
                break;
        }
    }

    private void UpdateReady()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Fire();
        }
    }

    private void UpdateEmpty()
    {
        
    }

    private void UpdateReloading()
    {
        
    } 

    private IEnumerator CoShotEffect(Vector3 hitPosition)//hitPosition : 끝점
    {

        audioSource.PlayOneShot(gunData.shootClip);

        muzzleEffect.Play();
        shellEffect.Play();

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePosition.position);
        lineRenderer.SetPosition(1, hitPosition);

        yield return new WaitForSeconds(0.2f);

        //Debug.Log("End");
        lineRenderer.enabled = false;
    }

    private IEnumerator CoReloadEffect()
    {
        audioSource.PlayOneShot(gunData.reloadClip);
        CurrentState = State.Reloading;

        yield return new WaitForSeconds(gunData.reloadTime);

        if (gunData.magCapacity - magAmmo > ammoRemain)
        {
            magAmmo += ammoRemain;
            ammoRemain = 0;
        }
        else
        {
            ammoRemain -= gunData.magCapacity - magAmmo;
            magAmmo += gunData.magCapacity - magAmmo;
        }

        CurrentState = State.Ready;
    }

    public void Fire()
    {
        //lastFireTime + gunData.timeBetFire : 발사할 수 있는 시간이 됬는지 확인
        if (currentState == State.Ready && Time.time > (lastFireTime + gunData.timeBetFire))
        {
            lastFireTime = Time.time;
            Shoot();
        }
    }

    private void Shoot()
    {
        if(CurrentState == State.Empty)
        {
            return;
        }
        Vector3 hitPosition = Vector3.zero;

        RaycastHit hit; //레이캐스트 결과가 담기는 클래스
        //충돌했으면 true, 아니면 false를 반환하게 된다.
        if(Physics.Raycast(firePosition.position, firePosition.forward, out hit, gunData.fireDistance))
        {
            //hit.point : 충돌한 지점 반환 / hit.collider : 맞은 콜라이더 반환
            hitPosition = hit.point;
            //hit.collider.CompareTag("") //태그를 이용한 충돌 검사
            var target = hit.collider.GetComponent<IDamagable>(); //인터페이스를 이용한 특정 대상만 검출해서 충돌 검사
            if(target != null)
            {
                target.OnDamage(gunData.damage, hit.point, hit.normal);
            }
        }
        else
        {
            hitPosition = firePosition.position + firePosition.forward * gunData.fireDistance;
        }

        StartCoroutine(CoShotEffect(hitPosition));

        --magAmmo;
        if(magAmmo == 0)
        {
            CurrentState = State.Empty;
        }

    }

    public bool Reload()
    {
        if(ammoRemain <= 0 || CurrentState == State.Reloading || magAmmo == gunData.magCapacity)
        {
            return false;
        }

        StartCoroutine(CoReloadEffect());

        return true;
    }

    public void AddAmmo(int amount)
    {
        ammoRemain = Mathf.Min(ammoRemain + amount, gunData.startAmmoRemain);
    }

}
