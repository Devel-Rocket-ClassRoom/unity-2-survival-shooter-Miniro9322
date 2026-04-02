using System;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity
{
    private enum State
    {
        Idle,
        Trace,
        Attack,
        Death,
    }

    private static readonly int HasTarget = Animator.StringToHash("HasTarget");
    private static readonly int Death = Animator.StringToHash("Death");

    [SerializeField]
    private AudioClip hitClip;
    [SerializeField]
    private AudioClip deathClip;
    private AudioSource audioSource;
    private GameManager gameManager;
    private NavMeshAgent agent;
    [SerializeField]
    private float traceDistance = 10000f;
    [SerializeField]
    private LayerMask player;
    private State status;
    private Animator animator;
    private Transform target;
    [SerializeField]
    private ParticleSystem hitEffect;
    private float attackCoolTimeInterval = 1f;
    private float attackCoolTime;
    [SerializeField]
    private EntityData data;
    private float damage;
    private Collider enemyCollider;

    private State Status
    {
        get { return status; }
        set
        {
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
                case State.Attack:
                    break;
                case State.Death:
                    animator.SetTrigger(Death);
                    agent.isStopped = true;
                    enemyCollider.enabled = false;
                    gameManager.AddScore(10);
                    Destroy(gameObject, 3f);
                    break;
            }
        }
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider>();
        Status = State.Idle;
        enemyCollider.enabled = true;
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        agent.enabled = true;
        agent.isStopped = true;
        agent.ResetPath();
        agent.speed = data.Speed;
        damage = data.Damage;
        Health = data.Health;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isPaused == true)
        {
            animator.speed = 0f;
            agent.isStopped = true;
            return;
        }
        else
        {
            animator.speed = 1f;
            agent.isStopped = false;
        }

        switch (status)
        {
            case State.Idle:
                UpdateIdle();
                break;
            case State.Trace:
                UpdateTrace();
                break;
            case State.Attack:
                UpdateAttack();
                break;
            case State.Death:
                UpdateDeath();
                break;
        }

        if (IsDeath == true)
        {
            return;
        }
    }

    private void UpdateAttack()
    {
        if (Vector3.Distance(target.position, transform.position) > data.attackRange)
        {
            Status = State.Trace;
            return;
        }

        attackCoolTime += Time.deltaTime;

        var player = target.GetComponent<LivingEntity>();
        if (player != null)
        {
            if (attackCoolTime > attackCoolTimeInterval)
            {
                if(player.IsDeath == false)
                {
                    player.OnDamage(damage, transform.position, -transform.forward);
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
        if(IsDeath == true)
        {
            Status = State.Death;
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
        if (IsDeath == true)
        {
            Status = State.Death;
        }
        if (Vector3.Distance(target.position, transform.position) < data.attackRange)
        {
            Status = State.Attack;
        }

        agent.SetDestination(target.position);
    }

    private Transform FindTarget(float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, player);
        if(colliders.Length == 0)
        {
            return null;
        }

        var target = colliders.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).First();
        return target.transform;
    }

    private void UpdateDeath()
    {

    }

    public override void OnDamage(float damage, Vector3 hitPosition, Vector3 hitNormal)
    {
        hitEffect.transform.position = hitPosition;
        hitEffect.transform.forward = hitNormal;
        hitEffect.Play();
        audioSource.PlayOneShot(hitClip);
        base.OnDamage(damage, hitPosition, hitNormal);
    }

    private void StartSinking()
    {
        StartCoroutine(Sinking());
    }

    private IEnumerator Sinking()
    {
        while (true)
        {
            transform.Translate(Vector3.down * 1f);

            yield return null;
        }
    }

    public override void Die()
    {
        if(IsDeath == true)
        {
            return;
        }

        audioSource.PlayOneShot(deathClip);
        Status = State.Death;

        base.Die();
    }
}
