using UnityEngine;
using System.Collections;

public class QuitToMenuScript : MonoBehaviour {

	void Update () 
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Destroy(GameObject.Find("Menu"));
            Application.LoadLevel("MenuScene");
        }
	}
}
