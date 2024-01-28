using Bipolar.Humanoid3D.Player;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private LayersSettings layersSettings;

    [SerializeField]
    private UndergroundWorldChanger undergroundWorldChanger;
    [SerializeField]
    private PlayerMovement movement;
    [SerializeField]
    private FPPViewRotation viewRotation;
    [SerializeField]
    private ShootingController shootingController;

    [SerializeField]
    private float horizontalRotationSpeed = 1.8f;
    [SerializeField]
    private KeyCode switchKey = KeyCode.LeftControl;

    private void Update()
    {
        var undergroundValue = undergroundWorldChanger.UndergroundValue;
        shootingController.enabled = viewRotation.enabled = movement.enabled = !undergroundWorldChanger.IsInTransition;
        viewRotation.sensitivity.x = horizontalRotationSpeed * undergroundValue;
        var center = movement.CharacterController.center;
        center.y = undergroundValue;
        movement.CharacterController.center = center;

        movement.CharacterController.gameObject.layer = undergroundValue switch
        {
            >= 0.9f => layersSettings.overgroundLayer,
            <= -0.9f => layersSettings.undergroundLayer,
            _ => layersSettings.noCollisionLayer,
        };

        if (Input.GetKeyUp(switchKey) && CanRotate())
        {
            if (undergroundValue > 0)
                undergroundWorldChanger.GoUnderground();    
            else
                undergroundWorldChanger.GoOverground();
        }
    }

    private bool CanRotate()
    {
        return movement.IsGrounded && Mathf.Abs(movement.transform.position.y) < 0.15f;
    }
}
