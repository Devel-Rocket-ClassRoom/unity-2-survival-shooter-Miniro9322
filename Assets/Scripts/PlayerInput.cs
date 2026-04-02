using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private static readonly string Vertical = "Vertical";
    private static readonly string Horizontal = "Horizontal";

    public float MoveVertical {  get; private set; }
    public float MoveHorizontal { get; private set; }

    // Update is called once per frame
    void Update()
    {
        MoveVertical = Input.GetAxis(Vertical);
        MoveHorizontal = Input.GetAxis(Horizontal);
    }
}
