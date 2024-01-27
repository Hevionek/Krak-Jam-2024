using Bipolar.Humanoid3D.Player;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private UndergroundWorldChanger undergroundWorldChanger;
    [SerializeField]
    private PlayerMovement movement;
    [SerializeField]
    private FPPViewRotation viewRotation;

    [SerializeField]
    private float horizontalRotationSpeed = 1.8f;

    [SerializeField]
    private KeyCode switchKey = KeyCode.LeftShift;

    private void Update()
    {
        movement.enabled = !undergroundWorldChanger.IsInTransition;
        if (Input.GetKeyUp(switchKey))
        {
            if (undergroundWorldChanger.UnderGroundValue > 0)
                undergroundWorldChanger.GoUnderground();    
            else
                undergroundWorldChanger.GoOverground();
        }
    }
}
