using UnityEngine;
using System.Collections;

public class StaticVars : MonoBehaviour {

    public int curAmount = 0;
    public int needToKill = -1;
    public float seconds = -1;
    public GameObject finish;
    public GUIText time;
    public GUIText kill;

    public void Start()
    {
        GameObject go = GameObject.Find("Menu");
        if (go.GetComponent<MenuScript>().freeRide)
            GameObject.FindGameObjectWithTag("Finish").SetActive(false);
        else
        {
            if (go.GetComponent<MenuScript>().timeLimit)
                // Запуск таймера
                seconds = go.GetComponent<MenuScript>().minKills ? 300 : 59;
            if (go.GetComponent<MenuScript>().minKills)
                // Запуск счетчика
                needToKill = 666;
        }

    }

    public void Update()
    {
        if (needToKill != -1)
            kill.text = "Need to kill: " + needToKill.ToString();
        if (seconds != -1)
        {
            if (seconds > 0)
            {
                seconds -= Time.deltaTime;
                if (seconds < 30)
                    time.color = Color.red;
                time.text = "Time Left: " + Mathf.CeilToInt(seconds).ToString();
            }
            else
                time.text = "No Time Left!";
        }
    }

}
