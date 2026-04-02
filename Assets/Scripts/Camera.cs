using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement playerPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(playerPosition.transform.position.x, playerPosition.transform.position.y + 5f, playerPosition.transform.position.z - 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerPosition.transform.position.x, playerPosition.transform.position.y + 5f, playerPosition.transform.position.z - 3f);
    }
}
