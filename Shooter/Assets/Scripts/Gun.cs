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

    private LineRenderer lineRenderer;
    private AudioSource audioSource;
    public int ammoRemain;
    public int magAmmo;

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
        switch (currentState)
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
}
    private void UpdateReady()
    {

    }
    private void UpdateEmpty()
    {

    }
    private void UpdateReloading()
    {

    }
    private IEnumerator CoShotEffet(Vector3 hitPosition)
    {
        //audioSource.PlayOneShot(gunData.shootClip);

        //muzzleEffet.Play();
        //shellEffet.Play();
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePosition.position);
        // Vector3 endPos = firePosition.position + firePosition.forward * 10f;
        // lineRenderer.SetPosition(1, endPos);
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
        //if (currentState == State.Ready && Time.time > lastFireTime + gunData.timeBetFire)
        //{
        //    lastFireTime = Time.time;
        //    Shoot();
        //}
    }
    public void Shoot()
    {
        //Vector3 hitPosition = Vector3.zero;
        //RaycastHit hit;
        //if (Physics.Raycast(firePosition.position, firePosition.forward, out hit, gunData.fireDistance))
        //{
        //    hitPosition = hit.point;

        //    var target = hit.collider.GetComponent<IDamagable>();

        //    if (target != null)
        //    {
        //        target.OnDamage(gunData.damage, hit.point, hit.normal);
        //    }
        //}
        //else
        //{
        //    hitPosition = firePosition.position + firePosition.forward * gunData.fireDistance;
        //}

        //StartCoroutine(CoShotEffet(hitPosition));

        //--magAmmo;
        //uiManager.SetAmmoText(magAmmo, ammoRemain);
        ////hud.UpdateAmmo(magAmmo, ammoRemain);
        //if (magAmmo == 0)
        //{
        //    currentState = State.Empty;
        //}
    }
    //public bool Reload()
    //{
    //    if (currentState == State.Reloading ||
    //        ammoRemain == 0 ||
    //        magAmmo == gunData.magCapacity)
    //        return false;
    //    currentState = State.Reloading;

    //    StartCoroutine(CoReload());
    //    return true;
    //}

    //private IEnumerator CoReload()
    //{


    //    audioSource.PlayOneShot(gunData.reloadClip);

    //    yield return new WaitForSeconds(gunData.reloadTime);


    //    // 예비탄에서 탄창으로 옮기기
    //    int need = gunData.magCapacity - magAmmo;
    //    int take = Mathf.Min(need, ammoRemain);

    //    magAmmo += take;
    //    ammoRemain -= take;
    //    //uiManager.SetAmmoText(magAmmo, ammoRemain);
    //    //hud.UpdateAmmo(magAmmo, ammoRemain);
    //    // 다시 Ready로
    //    currentState = State.Ready;

    //}
    public void AddAmmo(int amount)
    {
        //ammoRemain = Mathf.Min(ammoRemain + amount, gunData.startAmmoRemain);
        //uiManager.SetAmmoText(magAmmo, ammoRemain);
        //hud.UpdateAmmo(magAmmo, ammoRemain);
    }
}
