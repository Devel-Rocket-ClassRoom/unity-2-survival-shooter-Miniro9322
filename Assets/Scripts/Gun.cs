using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private LineRenderer bulletLine;
    [SerializeField]
    private Transform firePosition;
    [SerializeField]
    private ParticleSystem shootParticle;

    private float maxDistance = 50f;

    [SerializeField]
    private LayerMask enemy;

    private Coroutine shoot;

    private void Awake()
    {
        bulletLine = GetComponent<LineRenderer>();

        shootParticle.transform.position = firePosition.position;

        bulletLine.positionCount = 2;
        bulletLine.enabled = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Fire()
    {
        var hitPosition = Vector3.zero;

        Ray ray = new Ray(firePosition.position, firePosition.forward);

        if(Physics.Raycast(ray, out RaycastHit hit, maxDistance, enemy))
        {
            hitPosition = hit.point;
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
        
        bulletLine.SetPosition(0, firePosition.position);
        bulletLine.SetPosition(1, hit);
        bulletLine.enabled = true;

        yield return new WaitForSeconds(0.03f);

        bulletLine .enabled = false ;
        shoot = null;
    }
}
