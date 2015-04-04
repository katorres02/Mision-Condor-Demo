using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public PlayerHealth playerHealth;
	public GameObject enemy;
	public float spawnTime = 6f;
	public Transform[] spawnPoints;

	
	void Start ()
	{
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}
	
	
	void Spawn ()
	{
		if(playerHealth.currentHealth <= 0f)
		{
			return;
		}

		//int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		//Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		for (int i = 0; i < spawnPoints.Length; i++) 
		{
			Instantiate (enemy, spawnPoints[i].position, spawnPoints[i].rotation);
		}

	}
}
