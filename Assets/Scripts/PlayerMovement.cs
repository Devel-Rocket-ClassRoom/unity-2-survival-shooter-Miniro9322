using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int MoveHash = Animator.StringToHash("Move");

    private Rigidbody playerRigidBody;
    private PlayerInput input;
    private float moveSpeed = 5f;
    private Animator animator;

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
    }

    private void FixedUpdate()
    {
        var verticlaDelta = input.MoveVertical * transform.forward * moveSpeed * Time.deltaTime;
        playerRigidBody.MovePosition(playerRigidBody.position + verticlaDelta);

        var horizontalDelta = input.MoveHorizontal * transform.right * moveSpeed * Time.deltaTime;
        playerRigidBody.MovePosition(playerRigidBody.position + horizontalDelta);
    }
}
