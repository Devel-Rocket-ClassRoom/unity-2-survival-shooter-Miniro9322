using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private static readonly string Vertical = "Vertical";
    private static readonly string Horizontal = "Horizontal";
    private static readonly string Fire = "Fire1";
    [SerializeField]
    private LayerMask floor;

    public float MoveVertical {  get; private set; }
    public float MoveHorizontal { get; private set; }
    public Vector3 MoustPosition { get; private set; }

    public bool FireButton { get; private set; }

    // Update is called once per frame
    void Update()
    {
        MoveVertical = Input.GetAxis(Vertical);
        MoveHorizontal = Input.GetAxis(Horizontal);
        FireButton = Input.GetButton(Fire);
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit);
        if(hit.point == null)
        {
            return;
        }
        MoustPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
    }
}
