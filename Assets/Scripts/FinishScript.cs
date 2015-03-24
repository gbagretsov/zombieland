using UnityEngine;
using System.Collections;

public class FinishScript : MonoBehaviour {
    public GUIText winneryText;

    void OnTriggerStay(Collider other)
    {
        if (other.transform.root.gameObject.tag == "Player")
        {            
            StaticVars sv = GameObject.FindGameObjectWithTag("StaticVars").GetComponent<StaticVars>();  
            if ((sv.needToKill == 0 || sv.needToKill == -1) && (sv.seconds > 0 || sv.seconds == -1))
            {                
                GameObject.FindGameObjectWithTag("Respawn").SetActive(false);
                foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
                    Destroy(g);
                GameObject.FindGameObjectWithTag("StaticVars").SetActive(false);
                winneryText.text = "YOU WIN!";
                this.gameObject.SetActive(false);
            }
        }
    }

}
