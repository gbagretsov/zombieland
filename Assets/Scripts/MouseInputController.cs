using UnityEngine;
using System.Collections;

public class MouseInputController : MonoBehaviour {

    private float x;
    private float y;

    public float MouseXInput
    {
        get { return x; }
    }

    public float MouseYInput
    {
        get { return y; }
    }

    public float mouseXSensitivity = 1;
    public bool invertYAxes = false;
    private int sign;

	void Start () 
    {
        sign = invertYAxes ? -1 : 1;
        Screen.showCursor = false;
	}
	
	void Update () 
    {
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y") * sign;
	}

}
