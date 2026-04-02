using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class Gun : MonoBehaviour
{
    private LineRenderer bulletLine;
    [SerializeField]
    private Transform firePosition;
    [SerializeField]
    private ParticleSystem shootParticle;

    [SerializeField]
    private AudioClip shootClip;
    private AudioSource audioSource;
    private float maxDistance = 50f;

    [SerializeField]
    private LayerMask enemy;

    private Coroutine shoot;
    private float shootCoolTimeInterval = 0.12f;
    private float lastFireTime;

    private void Awake()
    {
        bulletLine = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();

        shootParticle.transform.position = firePosition.position;

        bulletLine.positionCount = 2;
        bulletLine.enabled = false;
        lastFireTime = 0f;
    }

    public void Fire()
    {
        if(Time.time > lastFireTime + shootCoolTimeInterval)
        {
            lastFireTime = Time.time;

            Shoot();
        }
    }

    public void Shoot()
    {
        var hitPosition = Vector3.zero;

        Ray ray = new Ray(firePosition.position, firePosition.forward);

        if(Physics.Raycast(ray, out RaycastHit hit, maxDistance, enemy))
        {
            hitPosition = hit.point;
            var target = hit.collider.GetComponent<IDamageable>();
            if(target != null)
            {
                target.OnDamage(1f, hitPosition, hit.normal);
            }
        }
        else
        {
            hitPosition = firePosition.position + maxDistance * firePosition.forward;
        }

        if(shoot != null)
        {
            return;
        }

        shoot = StartCoroutine(CoShoot(hitPosition));
    }

    private IEnumerator CoShoot(Vector3 hit)
    {
        shootParticle.Play();
        audioSource.PlayOneShot(shootClip);

        bulletLine.SetPosition(0, firePosition.position);
        bulletLine.SetPosition(1, hit);
        bulletLine.enabled = true;

        yield return new WaitForSeconds(0.03f);

        bulletLine.enabled = false ;
        shoot = null;
    }
}
