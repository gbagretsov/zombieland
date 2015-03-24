using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

    public GUIStyle titleLabel;

	private bool gameSettings = false;
    private bool inMenu = true;

    public bool timeLimit = true;
    public bool minKills = true;
    public bool freeRide = false;

    public void Awake()
    {
        DontDestroyOnLoad(this);
        Screen.showCursor = true;
    }


    public void OnGUI()
    {
        if (inMenu)
        {
            GUI.Label(new Rect(Screen.width / 2, 0, 50, 20), "ZombieLAND", titleLabel);
            if (gameSettings)
            {
                timeLimit = GUI.Toggle(new Rect(Screen.width / 2 - 85, 190, 170, 40), timeLimit, "Ограничение по времени");
                minKills = GUI.Toggle(new Rect(Screen.width / 2 - 112, 240, 225, 40), minKills, "Минимальное количество убийств");
                if (GUI.Button(new Rect(Screen.width / 2 - 75, 290, 150, 40), "Начать"))
                {
                    inMenu = false;
                    freeRide = false;
                    Application.LoadLevel("CityLevel");
                }
                if (GUI.Button(new Rect(Screen.width / 2 - 75, 340, 150, 40), "Назад"))
                    gameSettings = false;
            }
            else
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 75, 190, 150, 40), "Новая игра"))
                    gameSettings = true;
                if (GUI.Button(new Rect(Screen.width / 2 - 75, 240, 150, 40), "Свободная езда"))
                {
                    freeRide = true;
                    inMenu = false;
                    Application.LoadLevel("CityLevel");
                }
                if (GUI.Button(new Rect(Screen.width / 2 - 75, 290, 150, 40), "Выход"))
                    Application.Quit();
            }
        }

    }

}
