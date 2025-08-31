using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.HID;


public class PlayerHealth : LivingEntity
{
    private HUD hud;
    public GameOverUI gameOverUI;
    public Image fillImage;
    public Slider healthSlider;
    public AudioClip DeathClip;
    public AudioClip HitClip;

    private AudioSource audioSource;
    private Animator animator;

    private Move movement;
    private PlayerShooter shooter;

    private static readonly int DieHash = Animator.StringToHash("Die");
    private void Awake()
    {
        if (healthSlider != null)
            healthSlider.value = 1f;  // 처음에 풀체력

        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        movement = GetComponent<Move>();
        shooter = GetComponent<PlayerShooter>();
        hud = GetComponent<HUD>();
        GetComponent<GameOverUI>();


    }
    protected override void OnEnable()
    {
        base.OnEnable();

        healthSlider.gameObject.SetActive(true);
        healthSlider.value = Health / MaxHealth;

        movement.enabled = true;
        //shooter.enabled = true;

    }

    private void Update()
    {
        
    }
    public override void OnDamage(float damage, Vector3 hitpoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitpoint, hitNormal);
        fillImage.fillAmount = Health / MaxHealth;
        if (IsDead)
        {
            return;
        }
        audioSource.PlayOneShot(HitClip);
    }

    protected override void Die()
    {
        base.Die();

        healthSlider.gameObject.SetActive(false);
        animator.SetTrigger(DieHash);
        audioSource.PlayOneShot(DeathClip);

        movement.enabled = false;
        //shooter.enabled = false;
        gameOverUI?.ShowGameOver();
        fillImage.fillAmount = 0f;
        // 게임 일시정지
        Time.timeScale = 0f;
    }
    public void Heal(int amount)
    {
        Health = Mathf.Min(Health + (float)amount, MaxHealth);
        //healthSlider.value = Health / MaxHealth;
    }
    public void RestartLevel()
    {
        // 현재 씬 다시 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
