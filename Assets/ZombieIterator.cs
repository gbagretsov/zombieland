using UnityEngine;
using System.Collections;

public class ZombieIterator : MonoBehaviour {

    public GameObject[] zombies;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.F1))
            ChangeZombie(1);
        if (Input.GetKeyUp(KeyCode.F2))
            ChangeZombie(2);
        if (Input.GetKeyUp(KeyCode.F3))
            ChangeZombie(3);
        if (Input.GetKeyUp(KeyCode.F4))
            ChangeZombie(4);
        if (Input.GetKeyUp(KeyCode.F5))
            ChangeZombie(5);
        if (Input.GetKeyUp(KeyCode.F6))
            ChangeZombie(6);
        if (Input.GetKeyUp(KeyCode.F7))
            ChangeZombie(7);
        if (Input.GetKeyUp(KeyCode.F8))
            ChangeZombie(8);
        if (Input.GetKeyUp(KeyCode.F9))
            ChangeZombie(0);	
	}

    void ChangeZombie(int i)
    {
        foreach (GameObject g in zombies)
            g.transform.position = new Vector3(0, 0, 0);
        zombies[i].transform.position = new Vector3(-395.5f, 0, 100);
    }
}
