using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public int enemyHP = 1; // Сколько требуется попаданий
    public int strength = 1; // Наносимый урон
    
    public Collider[] enemyColliders;
    public bool activeAtStart = false;
    public float detectionRadius = 150f; // Радиус обнаружения игрока
    public GameObject blood;
    private Animator anim;
    private GameObject player;
    private float curDistance; // Текущее расстояние между врагом и игроком
    private NavMeshAgent searcher;
    private bool attack = false;
    private StaticVars sv;
    
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        if (activeAtStart)
			StartCoroutine(StandUpCoroutine());
        searcher = GetComponent<NavMeshAgent>();
        sv = GameObject.FindGameObjectWithTag("StaticVars").GetComponent<StaticVars>();
        
	}
	
	void Update () 
    {
        if (anim.GetBool("Alive"))
        {
            curDistance = Vector3.Magnitude
                (player.transform.position - transform.position);

            if (anim.GetBool("Active"))
            {
                transform.LookAt(player.transform);
                // Поиск игрока и бег
                searcher.SetDestination(player.transform.position);

                // Атака (если радиус меньше заданного в аниматоре)
                anim.SetFloat("Distance", curDistance);
                if (!attack && curDistance <= 4 && player.GetComponent<DamageController>() != null)
                    StartCoroutine(AttackCoroutine());

                else if (curDistance > 100)
                {
                    Debug.Log("Enemy destroyed");
                    Destroy(gameObject);
                    sv.curAmount--;
                }
            }

            else if (curDistance < detectionRadius && !anim.GetBool("Detected")) // Ищем игрока в радиусе обнаружения
            	StartCoroutine(StandUpCoroutine());            

            if (enemyHP <= 0)
                Death();
        }
	}

    IEnumerator StandUpCoroutine()
    {
		anim.SetBool("Detected", true);
        yield return new WaitForSeconds(3f);
        anim.SetBool("Active", true);
    }

    IEnumerator AttackCoroutine()
    {
        attack = true;
        yield return new WaitForSeconds(0.7f);
        if (anim.GetBool("Alive") && curDistance <= 4)
            player.GetComponent<DamageController>().GotDamage(strength);
        yield return new WaitForSeconds(1f);
        attack = false;
    }

    void Death()
    {
        sv.curAmount--;
        if (sv.needToKill > 0)
            sv.needToKill--;
        anim.SetBool("Alive", false);
        anim.SetBool("Active", false);
        Destroy(searcher);
        foreach (Collider coll in enemyColliders)
            coll.enabled = false;
        blood.gameObject.SetActive(true);
        GameObject.Destroy(gameObject, 10f);
    }

}
