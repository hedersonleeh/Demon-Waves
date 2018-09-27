using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject enemyPrefab;

	// Use this for 

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Invoke("SpawnEnemy", 2f);
	}
    void SpawnEnemy()
    {
        Instantiate(enemyPrefab);
        return;
       // enemyPrefab.transform.position = enemySpawn[Mathf.Clamp(Mathf.RoundToInt(Random.value * 4), 0, 3)].transform.position;
    }
}
