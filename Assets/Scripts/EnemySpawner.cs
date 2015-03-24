using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject[] zombies;
    public float spawnDelay = 5;
    private float curDelay;
    public float spawnMaxRadius = 70;
    public float spawnMinRadius = 30;
    public int maxEnemiesAmount = 100;
    private StaticVars sv;
    public int spawnAmount = 2;
    private int enemyTypesAmount;
    private GameObject player;
    private System.Random rnd;

	void Start() 
    {
        curDelay = spawnDelay;
        sv = GameObject.FindGameObjectWithTag("StaticVars").GetComponent<StaticVars>();
        enemyTypesAmount = zombies.Length;
        player = GameObject.FindGameObjectWithTag("Player"); 
        rnd = new System.Random();
	}
	
	void Update()
    {
        if (curDelay > 0)
            curDelay -= Time.deltaTime;

        else
        {
            Spawn();
            curDelay = spawnDelay;
        }
	}

    public void Spawn()
    {        
        // Генерация на точке спауна
        if (Vector3.Magnitude(player.transform.position - transform.position) < spawnMaxRadius &&
            Vector3.Magnitude(player.transform.position - transform.position) > spawnMinRadius)
            for (int i = 0; i < spawnAmount; i++)
            {
                if (sv.curAmount < maxEnemiesAmount)
                {
                    Instantiate(zombies[rnd.Next(enemyTypesAmount)], transform.position, Quaternion.identity);
                    sv.curAmount++;
                }
            }
         
    }
}
