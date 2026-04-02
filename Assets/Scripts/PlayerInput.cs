using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private static readonly string Vertical = "Vertical";
    private static readonly string Horizontal = "Horizontal";
    private static readonly string Fire = "Fire1";
    private static readonly string ESC = "Cancel";

    [SerializeField]
    private LayerMask floor;

    public float MoveVertical {  get; private set; }
    public float MoveHorizontal { get; private set; }
    public Vector3 MousePosition { get; private set; }

    public bool FireButton { get; private set; }
    public bool PauseButton { get; private set; }

    // Update is called once per frame
    void Update()
    {
        MoveVertical = Input.GetAxis(Vertical);
        MoveHorizontal = Input.GetAxis(Horizontal);
        FireButton = Input.GetButton(Fire);
        
        if(Input.GetButton(ESC) == true)
        {
            if(PauseButton == true)
            {
                PauseButton = false;
            }
            else
            {
                PauseButton = true;
            }
        }
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit);
        if(hit.point == null)
        {
            return;
        }
        MousePosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
    }
}
