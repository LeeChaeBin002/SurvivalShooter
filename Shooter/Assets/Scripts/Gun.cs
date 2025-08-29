using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

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
            switch (currentState)
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

    private LineRenderer lineRenderer;
    private AudioSource audioSource;
    public int ammoRemain;
    public int magAmmo;

    public ParticleSystem gunParticles;

    public Transform firePosition;

    private float lastFireTime;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;

    }
    // Update is called once per frame
    void Update()
    {
     
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
}

    private IEnumerator CoShotEffet(Vector3 hitPosition)
    {
        gunParticles.Play();
        //audioSource.PlayOneShot(gunData.shootClip);

        //빔
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePosition.position);
        lineRenderer.SetPosition(1, hitPosition);
        yield return new WaitForSeconds(1f);
        lineRenderer.enabled = false;

    }
    public void Fire()
    {

        if (magAmmo <= 0)
        {
            CurrentState = State.Empty;
            return;
        }
        if (currentState == State.Ready && Time.time > lastFireTime + 0.05f)//발사간격 0.1f
        {
            lastFireTime = Time.time;
            Shoot();
        }
    }
    public void Shoot()
    {
        // 총구 위치
        Vector3 startPos = firePosition.position;

        // 총구가 바라보는 방향 (마우스 회전으로 transform.forward 가 바뀌고 있음)
        Vector3 dir = firePosition.forward;

        // 원하는 사거리
        float distance = 20f;

        Vector3 hitPosition = startPos + dir * distance;
        RaycastHit hit;
        if (Physics.Raycast(firePosition.position, firePosition.forward, out hit, gunData.fireDistance))
        {
            hitPosition = hit.point;

            var target = hit.collider.GetComponent<IDamagable>();

            if (target != null)
            {
                target.OnDamage(gunData.damage, hit.point, hit.normal);
            }
        }
        else
        {
            hitPosition = firePosition.position + firePosition.forward * gunData.fireDistance;
        }

        StartCoroutine(CoShotEffet(hitPosition));

       
       
    }
    
}
