using UnityEngine;
using UnityEngine.InputSystem.HID;

public class PlayerHealth : LivingEntity
{
    //public Slider healthSlider;
    public AudioClip DeathClip;
    public AudioClip HitClip;
    public AudioClip itemPickupClip;

    private AudioSource audioSource;
    private Animator animator;

    private Move movement;
    private PlayerShooter shooter;

    private static readonly int DieHash = Animator.StringToHash("Die");
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        movement = GetComponent<Move>();
        shooter = GetComponent<PlayerShooter>();


    }
    protected override void OnEnable()
    {
        base.OnEnable();

        //healthSlider.gameObject.SetActive(true);
        //healthSlider.value = Health / MaxHealth;

        movement.enabled = true;
        shooter.enabled = true;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnDamage(10f, Vector3.zero, Vector3.zero);
        }
    }
    public override void OnDamage(float damage, Vector3 hitpoint, Vector3 hitNormal)
    {
        if (IsDead)
        {
            return;
        }
        base.OnDamage(damage, hitpoint, hitNormal);
        //healthSlider.value = Health / MaxHealth;
        audioSource.PlayOneShot(HitClip);
    }

    protected override void Die()
    {
        base.Die();

        //healthSlider.gameObject.SetActive(false);
        animator.SetTrigger(DieHash);
        audioSource.PlayOneShot(DeathClip);

        movement.enabled = false;
        shooter.enabled = false;
        //hud.ShowGameOver();

        // 게임 일시정지
        Time.timeScale = 0f;
    }
    public void Heal(int amount)
    {
        Health = Mathf.Min(Health + (float)amount, MaxHealth);
        //healthSlider.value = Health / MaxHealth;
    }

}
