using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int MoveHash = Animator.StringToHash("Move");

    private Rigidbody playerRigidBody;
    private PlayerInput input;
    private float moveSpeed = 5f;
    private Animator animator;
    [SerializeField]
    private GameManager gameManager;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Update()
    {
        animator.SetFloat(MoveHash, Math.Abs(input.MoveVertical) + Math.Abs(input.MoveHorizontal));

        gameManager.UpdatePauseUI(input.PauseButton);

    }

    private void FixedUpdate()
    {
        var move = new Vector3(input.MoveHorizontal, 0f, input.MoveVertical).normalized;
        playerRigidBody.MovePosition(playerRigidBody.position + move * moveSpeed * Time.fixedDeltaTime);

        playerRigidBody.transform.rotation = Quaternion.LookRotation(input.MousePosition);
    }
}
