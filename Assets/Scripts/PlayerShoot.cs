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

    // Update is called once per frame
    void Update()
    {
        if(input.FireButton == true)
        {
            gun.Fire();
        }
    }
}
