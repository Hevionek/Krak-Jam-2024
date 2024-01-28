using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField]
    private Vector2 size;

    [SerializeField]
    private int spawnedEnemiesCount = 100;

    [SerializeField]
    private GameObject[] enemies;

    private void Start()
    {
        for (int i = 0; i < spawnedEnemiesCount; i++)
        {
            int randomIndex = Random.Range(0, enemies.Length);
            float halfWidth = size.x / 2;
            float halfHeight = size.y / 2;

            Vector3 position = new Vector3(
                Random.Range(transform.position.x - halfWidth, transform.position.x + halfWidth),
                0,
                Random.Range(transform.position.y - halfHeight, transform.position.y + halfHeight));

            Instantiate(enemies[randomIndex], position, Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up), transform);
        }    
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(size.x, 1, size.y));
    }
}
