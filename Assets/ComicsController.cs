using NaughtyAttributes;
using UnityEngine;

public class ComicsController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] panelsInOrder;

    [SerializeField]
    private GameObject player;

    [SerializeField, ReadOnly]
    private int currentIndex = -1;

    [SerializeField]
    private float delay;

    private float timer;

    private void Awake()
    {
        player.SetActive(false);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (Input.anyKey && timer < 0)
        {
            currentIndex++;
            if (currentIndex < panelsInOrder.Length)
            {
                panelsInOrder[currentIndex].SetActive(true);
                timer = delay;
            }
            else
            {
                gameObject.SetActive(false);
                player.SetActive(true);
            }
        }
    }
}
