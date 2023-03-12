using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerNoDuplicates : MonoBehaviour
{
    public GameObject enemy;
    public float frequency = 10f;
    public float chance = .5f;

    private bool isPrevEnemyDead = true;
    private GameObject curEnemy;

    void Start()
    {
        StartCoroutine(spawnEnemy(frequency, enemy, chance));
    }

    void Update()
    {
        float curHealth = curEnemy.GetComponent<MoreMountains.TopDownEngine.Health>().CurrentHealth;
        
        if (curHealth <= 0)
        {
            isPrevEnemyDead = true;
        }
    }

    private IEnumerator spawnEnemy(float frequency, GameObject enemy, float chance)
    {
        yield return new WaitForSeconds(frequency);
        float rdm = Random.value;

        if (isPrevEnemyDead && chance >= rdm)
        {
            GameObject newEnemy = Instantiate(enemy, this.transform.position, Quaternion.identity);
            curEnemy = newEnemy;
            isPrevEnemyDead = false;
        }
        StartCoroutine(spawnEnemy(frequency, enemy, chance));
    }
}
