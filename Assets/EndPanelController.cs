using UnityEngine;

public class EndPanelController : MonoBehaviour
{
    [SerializeField]
    private EnemiesManager enemiesManager;

    [SerializeField]
    private GameObject endPanel;
    
    private bool hasFinished = false;

    private float timer;

    private void OnEnable()
    {
        enemiesManager.OnAllEnemiesKilled += EnemiesManager_OnAllEnemiesKilled;
    }

    private void EnemiesManager_OnAllEnemiesKilled()
    {
        hasFinished = true;
    }

    private void Update()
    {
        if (hasFinished)
        {
            endPanel.SetActive(true);
            timer += Time.unscaledDeltaTime;
            if (Input.anyKey && timer > 2)
                Application.Quit();
        }
    }

    private void OnDisable()
    {
        enemiesManager.OnAllEnemiesKilled -= EnemiesManager_OnAllEnemiesKilled;
    }
}
