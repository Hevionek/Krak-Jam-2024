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
        viewRotation.enabled = movement.enabled = !undergroundWorldChanger.IsInTransition;
        viewRotation.sensitivity.x = horizontalRotationSpeed * undergroundWorldChanger.UnderroundValue;


        if (Input.GetKeyUp(switchKey))
        {
            if (undergroundWorldChanger.UnderroundValue > 0)
                undergroundWorldChanger.GoUnderground();    
            else
                undergroundWorldChanger.GoOverground();
        }
    }
}
