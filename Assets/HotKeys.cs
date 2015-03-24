using UnityEngine;
using System.Collections;

public class HotKeys : MonoBehaviour {

    public GameObject g1;
    public GameObject g2;
    public GameObject g3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(KeyCode.F1))
            g1.SetActive(true);
        if (Input.GetKeyUp(KeyCode.F2))
            g1.SetActive(false);
        if (Input.GetKeyUp(KeyCode.F3))
            g2.SetActive(true);
        if (Input.GetKeyUp(KeyCode.F4))
            g2.SetActive(false);
        if (Input.GetKeyUp(KeyCode.F5))
            g3.SetActive(true);
        if (Input.GetKeyUp(KeyCode.F6))
            g3.SetActive(false);
	}
}
