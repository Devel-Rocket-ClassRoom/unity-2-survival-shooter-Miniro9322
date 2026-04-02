using UnityEngine;

public class PlayerHealth : LivingEntity
{
    private static readonly int Death = Animator.StringToHash("Death");

    [SerializeField]
    private EntityData data;
    private Animator animator;
    private PlayerMovement move;
    private PlayerShoot shoot;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private GameObject HitUi;
    private float maxHealth;

    private void Awake()
    {
        maxHealth = data.Health;
        Health = data.Health;
        animator = GetComponent<Animator>();
        move = GetComponent<PlayerMovement>();
        shoot = GetComponent<PlayerShoot>();

        move.enabled = true;
        shoot.enabled = true;
        HitUi.SetActive(false);
    }

    public override void OnDamage(float damage, Vector3 hitPosition, Vector3 hitNormal)
    {
        HitUi.SetActive(true);

        base.OnDamage(damage, hitPosition, hitNormal);
        gameManager.UpdateHpBar(Health / maxHealth);
        
        HitUi.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Die()
    {
        if(IsDeath == true)
        {
            return;
        }

        base.Die();

        animator.SetTrigger(Death);
        move.enabled = false;
        shoot.enabled = false;

        gameManager.OpenGameoverUI(IsDeath);
    }
}
