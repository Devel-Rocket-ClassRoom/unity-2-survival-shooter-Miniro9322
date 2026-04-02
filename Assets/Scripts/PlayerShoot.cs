using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private PlayerInput input;
    [SerializeField]
    private Gun gun;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(input.FireButton == true)
        {
            gun.Fire();
        }
    }
}
