using UnityEngine;

public class PlayerHealth : LivingEntity
{
    public override void OnDamage(float damage, Vector3 hitPosition, Vector3 hitNormal)
    {
        Debug.Log(damage);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
