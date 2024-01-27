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
    private Transform rightProvider;

    [SerializeField]
    private float moveSpeed;

    private void Update()
    {
        var motionInput = MoveInputProvider.GetMotion();
        if (motionInput.sqrMagnitude > 1)
            motionInput.Normalize();

        var direction = forwardProvider.forward * motionInput.y + rightProvider.right * motionInput.x;
        CharacterController.Move(moveSpeed * Time.deltaTime * direction);
    }
}
