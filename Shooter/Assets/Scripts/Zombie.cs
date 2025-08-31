using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;

public class Zombie: LivingEntity
{
    public enum Status   // 좀비 상태
    {
        Idle,
        Trace,
        Attack,
        Die,
    }
    public enum ZombieType
    {
        Normal,
        Boss
    }
    public ZombieType zombieType = ZombieType.Normal;//좀비 종류

    private Status currentStatus;




    public Status CurrentStatus
    {
        get { return currentStatus; }
        set
        {
            var prevStatus = currentStatus;
            currentStatus = value;

            switch (currentStatus)
            {
                case Status.Idle:
                    animator.SetBool("HasTarget", false);
                    agent.isStopped = true;
                    break;
                case Status.Trace:
                    animator.SetBool("HasTarget", true);
                    agent.isStopped = false;
                    break;
                case Status.Attack:
                    animator.SetBool("HasTarget", false);
                    agent.isStopped = true; // 이동을 멈추는것
                    break;
                case Status.Die:
                    animator.SetTrigger("Die");
                    agent.isStopped = true;
                    break;
            }
        }
    }

    private Transform target;

    public float traceDistance;
    public float attackDistance;

    public float damage = 10f;
    public float lastAttackTime;
    public float attackInterval = 0.5f;


    private Animator animator;
    private NavMeshAgent agent;
    private AudioSource audioSource;
    private Collider zombieCollider;
    public Renderer zombieRenderer;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }
    //public void SetUp(ZombieData data)
    //{
    //    MaxHealth = data.maxHP;
    //    damage = data.damage;
    //    agent.speed = data.speed;

    //    zombieRenderer.material.color = data.skinColor;

    //}
    private void Update()
    {
        switch (currentStatus)
        {
            case Status.Idle:
                UpdateIdle();
                break;
            case Status.Trace:
                UpdateTrace();
                break;
            case Status.Attack:
                UpdateAttack();
                break;
            case Status.Die:
                UpdateDie();

                break;
        }
    }

    public void UpdateIdle()
    {
        if (target != null && Vector3.Distance(transform.position, target.position) < traceDistance)
        {
            CurrentStatus = Status.Trace;
        }

        target = FindTarget(traceDistance);
    }
    public void UpdateTrace()
    {
        // 공격
        if (target != null && Vector3.Distance(transform.position, target.position) < attackDistance)
        {
            CurrentStatus = Status.Attack;
            return;
        }
        // 아이들
        if (target == null || Vector3.Distance(transform.position, target.position) > traceDistance)
        {
            CurrentStatus = Status.Idle;
            return;
        }
        agent.SetDestination(target.position);  // 매 프레임 하지말고 1초 또는 0.5초에 한번 하는게 좋음
    }
    public void UpdateAttack()
    {
        // 죽어서 없어진 경우
        if (target == null || Vector3.Distance(transform.position, target.position) > attackDistance)
        {
            CurrentStatus = Status.Trace;
            return;
        }

        // 공격 범위 벗어난 경우
        if (target != null && Vector3.Distance(transform.position, target.position) > attackDistance)
        {
            CurrentStatus = Status.Trace;
            return;
        }

        // 캐릭터를 바라보게 하기 
        var lookAt = target.position; ;
        lookAt.y = transform.position.y;    // Y 기우는거 방지
        transform.LookAt(lookAt);

        // 공격
        if (lastAttackTime + attackInterval < Time.time)
        {
            lastAttackTime = Time.time;

            var damagable = target.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.OnDamage(damage, transform.position, -transform.forward);
            }
        }
    }

    public void UpdateDie()
    {

    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNoraml)
    {
        base.OnDamage(damage, hitPoint, hitNoraml);
       
    }

    protected override void Die()
    {
        base.Die();
        CurrentStatus = Status.Die;

        var colider = GetComponent<CapsuleCollider>();
        if (colider != null)
            colider.enabled = false;
        int socoreToAdd = (zombieType == ZombieType.Boss) ? 50 : 10;

        var uiManager = FindAnyObjectByType<UiManager>();
        if(uiManager!=null)
        {
            uiManager.AddScore(socoreToAdd);
        }

    }
    public LayerMask targetLayer;

    protected Transform FindTarget(float radius)
    {
        var colliders = Physics.OverlapSphere(transform.position, radius, targetLayer.value);
        if (colliders.Length == 0)
        {

            return null;
        }

        var target = colliders.OrderBy(
            x => Vector3.Distance(x.transform.position, transform.position)).First();
        return target.transform;
    }
}
