using UnityEngine;
using UnityEngine.AI;

public class ZombieInput : MonoBehaviour
{
    private bool isSinking;
    public float sinkSpeed = 2.5f;

    private Rigidbody rb;
    private Collider col;
    private NavMeshAgent agent;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime,Space.World);
        }
    }

    // Death 애니메이션 이벤트에서 호출됨
    public void StartSinking()
    {
        if (rb != null) rb.isKinematic = true;
        if (col != null) col.isTrigger = true;      // 다른 오브젝트와 충돌 무시
        if (agent != null) agent.enabled = false;   // 네비게이션 꺼줌
        isSinking = true;
        Destroy(gameObject, 1f); //제거
    }
}
