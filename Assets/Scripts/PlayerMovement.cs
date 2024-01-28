using Bipolar;
using Bipolar.Input;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;
    public CharacterController CharacterController
    {
        get
        {
            if (_characterController == null)
                _characterController = GetComponent<CharacterController>();
            return _characterController;
        }
    }

    [Header("Direction Providers")]
    [SerializeField]
    private Transform forwardProvider;
    [SerializeField]
    private Transform rightProvider;
    [SerializeField]
    private Transform upProvider;

    [Header("Movement")]
    [SerializeField, RequireInterface(typeof(IMoveInputProvider))]
    private Object moveInputProvider;
    public IMoveInputProvider MoveInputProvider
    {
        get => moveInputProvider as IMoveInputProvider;
        set => moveInputProvider = (Object)value;
    }

    [SerializeField]
    private float moveSpeed;

    [Header("Jumping")]
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private KeyCode jumpKey = KeyCode.Space;

    [Header("States")]
    [SerializeField, ReadOnly]
    private bool isGrounded;
    public bool IsGrounded => isGrounded || (CharacterController.collisionFlags & CollisionFlags.Below) > 0;

    [SerializeField, ReadOnly]
    private Vector3 velocity;
    
    private void Update()
    {
        UpdateGrounded();
        float dt = Time.deltaTime;  
        var motionInput = MoveInputProvider.GetMotion();
        if (motionInput.sqrMagnitude > 1)
            motionInput.Normalize();

        float ySpeed = velocity.y;
        if (IsGrounded)
        {
            ySpeed = -upProvider.up.y;
        }
        else
        {
            ySpeed += upProvider.up.y * Physics.gravity.y * dt;
        }

        velocity = moveSpeed * (forwardProvider.forward * motionInput.y + rightProvider.right * motionInput.x);
        velocity.y = ySpeed;
        
        if (IsGrounded && Input.GetKeyDown(jumpKey))
            velocity += upProvider.up * jumpForce;

        CharacterController.Move(velocity * dt);
    }

    private void UpdateGrounded()
    {
        var groundedRay = new Ray(
            transform.position + upProvider.up * 0.4f,
            -upProvider.up);

        isGrounded = Physics.Raycast(groundedRay, 0.5f, PhysicsCollisionMatrixLayerMasks.MaskForLayer(gameObject.layer));
    }
}
 
public static class PhysicsCollisionMatrixLayerMasks
{
    private static Dictionary<int, int> _masksByLayer;
    private static Dictionary<int, int> MasksByLayer
    {
        get
        {
            if (_masksByLayer == null)
                Init();
            return _masksByLayer;
        } 
    }

    public static void Init()
    {
        _masksByLayer = new Dictionary<int, int>();
        for (int i = 0; i < 32; i++)
        {
            int mask = 0;
            for (int j = 0; j < 32; j++)
            {
                if (!Physics.GetIgnoreLayerCollision(i, j))
                {
                    mask |= 1 << j;
                }
            }
            _masksByLayer.Add(i, mask);
        }
    }

    public static int MaskForLayer(int layer) => MasksByLayer[layer];
}
