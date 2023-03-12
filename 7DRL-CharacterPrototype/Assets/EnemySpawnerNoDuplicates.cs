using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerNoDuplicates : MonoBehaviour
{
    public GameObject enemy;
    public float frequency = 10f;
    public float chance = .5f;

    private bool isPrevEnemyDead = true;

    void Start()
    {
        Debug.Log("spawn");
        StartCoroutine(spawnEnemy(frequency, enemy, chance));
    }
    private IEnumerator spawnEnemy(float frequency, GameObject enemy, float chance)
    {
        yield return new WaitForSeconds(frequency);
        float rdm = Random.value;
        Debug.Log("rdm " + rdm);
        if (isPrevEnemyDead && chance >= rdm)
        {
            Debug.Log("REAL");
            GameObject newEnemy = Instantiate(enemy, this.transform.position, Quaternion.identity);
        }
        StartCoroutine(spawnEnemy(frequency, enemy, chance));
    }
}
