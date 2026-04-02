using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

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
    private Image hitImage;
    private float maxHealth;

    [SerializeField]
    private AudioClip hitClip;
    [SerializeField]
    private AudioClip deathClip;
    private AudioSource audioSource;

    private float flashTime = 0.5f;

    private void Awake()
    {
        maxHealth = data.Health;
        Health = data.Health;
        animator = GetComponent<Animator>();
        move = GetComponent<PlayerMovement>();
        shoot = GetComponent<PlayerShoot>();
        audioSource = GetComponent<AudioSource>();

        move.enabled = true;
        shoot.enabled = true;
        hitImage.color = new Color(1f, 0f, 0f, 0f);
    }

    public override void OnDamage(float damage, Vector3 hitPosition, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPosition, hitNormal);
        gameManager.UpdateHpBar(Health / maxHealth);
        audioSource.PlayOneShot(hitClip);
        if(Health / maxHealth > 0)
        {
            StartCoroutine(CoHitFlash());
        }
    }

    private IEnumerator CoHitFlash()
    {
        float time = 0f;

        while (time < flashTime)
        {
            float alpha = Mathf.Lerp(0.1f, 0f, time/flashTime);
            hitImage.color = new Color(1f, 0f, 0f, alpha);

            time += Time.deltaTime;
            yield return null;
        }
        hitImage.color = new Color(1f, 0f, 0f, 0f);
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
        audioSource.PlayOneShot(deathClip);
        move.enabled = false;
        shoot.enabled = false;

        gameManager.OpenGameoverUI(IsDeath);
    }
}
