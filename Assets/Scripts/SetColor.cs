using UnityEngine;
using System.Collections;

public class SetColor : MonoBehaviour {

    public Color color1 = Color.white;
    public Color color2 = Color.red;
    public GameObject[] objects1;
    public GameObject[] objects2;

	void Start () 
    {
        foreach (GameObject obj in objects1)
            obj.renderer.material.color = color1;
        foreach (GameObject obj in objects2)
            obj.renderer.material.color = color2;
	}
	
}
