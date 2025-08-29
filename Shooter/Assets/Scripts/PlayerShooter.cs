using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    //public UiManager uiManager;


    public static readonly int IdReload = Animator.StringToHash("Reload");
    public Gun gun;


    private Vector3 gunInitPosition;
    private Quaternion gunInitRotation;

    private Rigidbody gunRb;
    private Collider gunCollider;

    private PlayerInput input;
    private Animator animator;

    public Transform gunPivot;
    public Transform leftHandMount;
    public Transform rightHandMount;


    private void Awake()
    {

        input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        gunRb = gun.GetComponent<Rigidbody>();
        gunCollider = gun.GetComponent<Collider>();

        gunInitPosition = gun.transform.localPosition;
        gunInitRotation = gun.transform.localRotation;
    }
    private void OnEnable()
    {
        gunRb.isKinematic = true;
        gunCollider.enabled = false;
        gun.transform.localPosition = gunInitPosition;
        gun.transform.localRotation = gunInitRotation;

    }
    private void OnDisable()
    {
        if (gunRb != null)
        {

            gunRb.linearVelocity = Vector3.zero;
            gunRb.angularVelocity = Vector3.zero;
            gunRb.isKinematic = false;
        }
        gunCollider.enabled = true;
    }

    private void Update()
    {
        if (input.Fire)
        {
            gun.Fire();
        }
      
    }
}
