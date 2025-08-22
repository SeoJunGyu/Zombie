using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public ParticleSystem muzzleEffect;
    public ParticleSystem shellEffect;

    private LineRenderer lineRenderer;
    private AudioSource audioSource;

    public Transform firePosition;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();

        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(CoShotEffect());
        }
    }

    private IEnumerator CoShotEffect()
    {
        //Debug.Log("Start");
        muzzleEffect.Play();
        shellEffect.Play();

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePosition.position);
        Vector3 endPos = firePosition.position + firePosition.forward * 10f;
        lineRenderer.SetPosition(1, endPos);

        yield return new WaitForSeconds(0.2f);

        //Debug.Log("End");
        lineRenderer.enabled = false;
    }

}
