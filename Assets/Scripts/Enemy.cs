using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    private enum State
    {
        Idle,
        Trace,
        Death,
    }

    private static readonly int HasTarget = Animator.StringToHash("HasTarget");

    private NavMeshAgent agent;
    [SerializeField]
    private float traceDistance = 10f;
    [SerializeField]
    private LayerMask player;
    private State status;
    private Animator animator;
    private Transform target;
    [SerializeField]
    private ParticleSystem hitEffect;
    private float attackCoolTimeInterval = 1f;
    private float attackCoolTime;

    private State Status
    {
        get { return status; }
        set
        {
            Debug.Log(status);

            var prevStatus = status;
            status = value;
            switch (status)
            {
                case State.Idle:
                    animator.SetBool(HasTarget, false);
                    agent.isStopped = true;
                    break;
                case State.Trace:
                    animator.SetBool(HasTarget, true);
                    agent.isStopped = false;
                    break;
                case State.Death:
                    break;
            }
        }
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        Status = State.Idle;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (status)
        {
            case State.Idle:
                UpdateIdle();
                break;
            case State.Trace:
                UpdateTrace();
                break;
            case State.Death:
                break;
        }

        if(Vector3.Distance(target.position, transform.position) < 1f)
        {
            attackCoolTime += Time.deltaTime;

            var player = target.GetComponent<IDamageable>();
            if(player != null)
            {
                if(attackCoolTime > attackCoolTimeInterval)
                {
                    player.OnDamage(1f, transform.position, -transform.forward);
                    attackCoolTime = 0f;
                }
            }
        }
    }

    private void UpdateIdle()
    {
        if(target != null && Vector3.Distance(target.position, transform.position) < traceDistance)
        {
            Status = State.Trace;
            return;
        }

        target = FindTarget(traceDistance);
    }

    private void UpdateTrace()
    {
        if (target == null || Vector3.Distance(target.position, transform.position) > traceDistance)
        {
            target = null;
            Status = State.Idle;
            return;
        }

        agent.SetDestination(target.position);
    }

    private Transform FindTarget(float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, player);
        if(colliders == null)
        {
            return null;
        }

        var target = colliders.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).FirstOrDefault();
        return target.transform;
    }

    public void OnDamage(float damage, Vector3 hitPosition, Vector3 hitNormal)
    {
        
    }
}
