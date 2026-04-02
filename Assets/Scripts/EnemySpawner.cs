using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemys;

    [SerializeField]
    private float spawnCoolTimeInterval = 5f;
    private float spawnCoolTime = 0f;
    [SerializeField]
    private float maxRange = 1000f;
    [SerializeField]
    private GameManager gameManager;

    private void Update()
    {
        if(gameManager.isGameEnd == true)
        {
            return;
        }

        spawnCoolTime += Time.deltaTime;

        if(spawnCoolTime >= spawnCoolTimeInterval)
        {
            var position = Random.insideUnitSphere * maxRange;
            if(NavMesh.SamplePosition(position, out NavMeshHit hit, maxRange, NavMesh.AllAreas) == false)
            {
                return;
            }

            position = hit.position;
            position.y += 1f;

            Instantiate(enemys[Random.Range(0, enemys.Length)], position, Quaternion.identity);

            spawnCoolTime = 0f;
        }
    }
}
