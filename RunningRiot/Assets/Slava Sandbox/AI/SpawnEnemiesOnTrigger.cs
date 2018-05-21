using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemiesOnTrigger : MonoBehaviour {
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private int howManyEnemies;
    [SerializeField]
    private GameObject spawnPoint;
    private bool once;
    private void OnTriggerEnter(Collider other)
    {
        if (once) return;
        
        if (other.tag == "Player")
        {
            once = true;
            for (int i = 0; i < howManyEnemies; i++)
            {
                GameObject newEnemy = Instantiate(enemy, spawnPoint.transform.position, Quaternion.identity);
                newEnemy.GetComponent<MeleeEnemy>().aggroDistance = 1000f;
                newEnemy.GetComponent<MeleeEnemy>().speed = 6;
            }
        }
    }
}
