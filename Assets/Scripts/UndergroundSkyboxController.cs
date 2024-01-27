using UnityEngine;

public class UndergroundSkyboxController : MonoBehaviour
{
    [SerializeField]
    private UndergroundWorldChanger undergroundWorldChanger;

    [SerializeField]
    private Material overgroundSkybox;
    [SerializeField]
    private Material undergroundSkybox;
    
    [SerializeField]
    private GameObject undergroundSkyboxObject;

    private void OnEnable()
    {
        undergroundWorldChanger.OnTransitionStarted += UndergroundWorldChanger_OnTransitionStarted;    
        undergroundWorldChanger.OnTransitionEnded += UndergroundWorldChanger_OnTransitionEnded;
    }

    private void UndergroundWorldChanger_OnTransitionStarted(float target)
    {
        undergroundSkyboxObject.SetActive(true);
        if (target > 0)
            RenderSettings.skybox = overgroundSkybox;
    }

    private void UndergroundWorldChanger_OnTransitionEnded(float target)
    {
        if (target < 0)
            RenderSettings.skybox = undergroundSkybox;
        undergroundSkyboxObject.SetActive(false);
    }

    private void OnDisable()
    {
        undergroundWorldChanger.OnTransitionEnded -= UndergroundWorldChanger_OnTransitionEnded;
        undergroundWorldChanger.OnTransitionStarted -= UndergroundWorldChanger_OnTransitionStarted; 
    }
}
