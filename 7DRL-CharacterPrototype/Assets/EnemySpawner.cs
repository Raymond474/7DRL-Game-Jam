using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float frequency = 10f;
    public float chance = .5f;

    void Start()
    {
        StartCoroutine(spawnEnemy(frequency, enemy, chance));
    }
    
    private IEnumerator spawnEnemy(float frequency, GameObject enemy, float chance)
    {
        yield return new WaitForSeconds(frequency);
        float rdm = Random.value;

        if (chance >= rdm)
        {
            GameObject newEnemy = Instantiate(enemy, this.transform.position, Quaternion.identity);
        }
        StartCoroutine(spawnEnemy(frequency, enemy, chance));
    }
}
