using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemies;

    [SerializeField]
    private GameObject boss;
    private int maxEnemyCount = 10;
    private int enemyCount = 0;
    [SerializeField]
    private float spawnInterval = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartEnemyRoutine();
    }
    void StartEnemyRoutine()
    {
        StartCoroutine("EnemyRoutine");
    }

    public void StopEnemyRoutine()
    {
        StopCoroutine("EnemyRoutine");
    }
    
     IEnumerator EnemyRoutine()
    {
        yield return new WaitForSeconds(3f);

        float moveSpeed = 5f;
        while(true)
        {
            if (enemyCount < maxEnemyCount)
            {
                SpawnEnemy(moveSpeed);
                enemyCount++;
            }


            yield return new WaitForSeconds(spawnInterval);
        }

    }

    void SpawnEnemy(float moveSpeed)
    {
        Vector3 spawnPos = new Vector3(Random.Range(-11, 7), Random.Range(5, 22), transform.position.z);

        GameObject enemyObject = Instantiate(enemies, spawnPos, Quaternion.identity);
        Enemy enemy = enemyObject.GetComponent<Enemy>();
    }

    void SpawnBoss()
    {
        Instantiate(boss, transform.position, Quaternion.identity);
    }
    
}
