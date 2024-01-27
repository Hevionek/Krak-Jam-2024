using Bipolar;
using Bipolar.Input;
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

    [SerializeField, RequireInterface(typeof(IMoveInputProvider))]
    private Object moveInputProvider;
    public IMoveInputProvider MoveInputProvider
    {
        get => moveInputProvider as IMoveInputProvider;
        set => moveInputProvider = (Object)value;
    }

    [SerializeField]
    private Transform forwardProvider;

    [SerializeField]
    private float moveSpeed;

    private void Update()
    {
        var motion = MoveInputProvider.GetMotion();
        if (motion.sqrMagnitude > 1)
            motion.Normalize();

        CharacterController.Move(moveSpeed * Time.deltaTime * forwardProvider.TransformDirection(motion.x, 0, motion.y));
    }
}
