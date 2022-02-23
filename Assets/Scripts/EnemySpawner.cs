using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    private class EnemyOptions
    {
        public ChasingScript prefab;
        public float spawningTime;
    }

    [SerializeField]
    private EnemyOptions[] enemyPrefabs = new EnemyOptions[0];
    float enemyTimePassed = 0;
    int enemySelected;

    private void Start()
    {
        enemySelected = Random.Range(0, enemyPrefabs.Length);
    }


    void Update()
    {
        enemyTimePassed += Time.deltaTime;

        if (enemyTimePassed >= enemyPrefabs[enemySelected].spawningTime)
        {
            Spawn();

            enemyTimePassed = 0;       
        }
    }

    void Spawn()
    {
        if (enemyPrefabs[enemySelected].prefab.particle != null)
            Instantiate(enemyPrefabs[enemySelected].prefab.particle, this.transform.position, this.transform.rotation);

        Instantiate(enemyPrefabs[enemySelected].prefab.transform, this.transform.position, this.transform.rotation);

        enemySelected = Random.Range(0, enemyPrefabs.Length);
    }
}
