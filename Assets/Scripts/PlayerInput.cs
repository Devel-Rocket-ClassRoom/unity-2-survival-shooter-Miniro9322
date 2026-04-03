using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private static readonly string Vertical = "Vertical";
    private static readonly string Horizontal = "Horizontal";
    private static readonly string Fire = "Fire1";
    private static readonly string ESC = "Cancel";

    [SerializeField]
    private LayerMask floor;
    [SerializeField]
    private GameManager gameManager;

    public float MoveVertical {  get; private set; }
    public float MoveHorizontal { get; private set; }
    public Vector3 MousePosition { get; private set; }

    public bool FireButton { get; private set; }
    public bool PauseButton { get; private set; }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(ESC) == true)
        {
            if (gameManager.isPaused == true)
            {
                gameManager.UpdatePauseUI(false);
            }
            else
            {
                gameManager.UpdatePauseUI(true);
            }
        }

        if (gameManager.isPaused == true)
        {
            return;
        }

        MoveVertical = Input.GetAxis(Vertical);
        MoveHorizontal = Input.GetAxis(Horizontal);
        FireButton = Input.GetButton(Fire);
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(ray, out RaycastHit hit) == false)
        {   
            return;
        }
        Vector3 target = hit.point;
        target.y = transform.position.y;

        Vector3 direction = target - transform.position;

        MousePosition = direction;
    }
}
