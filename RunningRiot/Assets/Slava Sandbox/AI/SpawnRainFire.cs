using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRainFire : MonoBehaviour {
    public GameObject fireball;
    public Vector3 center;
    public Vector3 size;
    public float heightOfInstantiating = 20f;
    public float timeOfInstantiating = 2f;
    public float timeToSwitchToPhase = 30f;
	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnFireBalls());
        StartCoroutine(SwitchToPhaseThree());
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    IEnumerator SpawnFireBalls()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x/2,size.x/2),center.y+ heightOfInstantiating, 0);
        Instantiate(fireball,pos,Quaternion.identity);
        yield return new WaitForSeconds(timeOfInstantiating);
        StartCoroutine(SpawnFireBalls());
    }
    IEnumerator SwitchToPhaseThree()
    {
        yield return new WaitForSeconds(timeToSwitchToPhase);
        GetComponent<Boss>().currPhase = Boss.Phase.Third;
        StopAllCoroutines();
        enabled = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(255,164,0,0.5f);
        Gizmos.DrawCube(center,size);
    }
}
