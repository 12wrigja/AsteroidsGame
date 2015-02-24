using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject[] possibleEnemies;

    public GameObject currentEnemy;

    public GameObject player;

    public float timeTillSpawn = 5f;
    public bool shouldSpawn = true;

    void Update()
    {
        if (currentEnemy == null)
        {
            shouldSpawn = true;
        }
        if (timeTillSpawn > 0 && shouldSpawn)
        {
            timeTillSpawn -= Time.deltaTime;
        } else if(shouldSpawn){
           Vector3 spawnLoc = Random.onUnitSphere.normalized * 50;
            spawnLoc.z = 0;
            currentEnemy = Instantiate(possibleEnemies[Random.Range(0,possibleEnemies.Length)],spawnLoc,Quaternion.identity) as GameObject;
            currentEnemy.GetComponent<EnemyScript>().target = player;
            currentEnemy.SetActive(true);
            timeTillSpawn = Random.Range(30, 60);
            shouldSpawn = false;
        }
    }
}
