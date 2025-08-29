using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    private Animator animator;
    private Rigidbody rb;

    private Vector3 movement;
    private Camera mainCam;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        mainCam = Camera.main;
    }

    void Update()
    {
        // 월드 좌표 기준
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        movement = new Vector3(h, 0, v).normalized;

        // 애니메이션 전환
        animator.SetBool("isMoving", movement.magnitude > 0);
        //마우스 위치값 받기
        RotateTowardsMouse();
    }

    void FixedUpdate()
    {
        // Rigidbody 이동
        if (movement.magnitude > 0)
        {
            rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);
        }
    }

    void RotateTowardsMouse()
    {
        // 마우스 → 화면상의 Ray
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // y=0 평면
        float rayLength;

        if (groundPlane.Raycast(ray, out rayLength))
        {
            Vector3 pointToLook = ray.GetPoint(rayLength);
            Vector3 direction = (pointToLook - transform.position).normalized;
            direction.y = 0; // 수평 회전만

            if (direction.magnitude > 0.1f)
            {
                transform.forward = direction;
            }
        }
    }
}
