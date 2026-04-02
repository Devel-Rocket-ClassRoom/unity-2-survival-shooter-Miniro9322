using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float Health;

    public bool IsDeath { get; private set; }

    private void Awake()
    {
        Health = 5f;
        IsDeath = false;
    }

    public virtual void OnDamage(float damage, Vector3 hitPosition, Vector3 hitNormal)
    {
        Health -= damage;
        if(Health < 0f)
        {
            Health = 0f;

            IsDeath = true;
        }
    }
}
