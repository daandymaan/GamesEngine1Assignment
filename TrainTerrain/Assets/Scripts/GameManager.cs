using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public GameManager()
    {
        Instance = this;
    }

    private static StringBuilder message = new StringBuilder();

    public void OnGUI()
    {
        GUI.color = Color.black;
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "" + message);
        if (Event.current.type == EventType.Repaint)
        {
            message.Length = 0;
        }

        bool quitGame = GUI.Button(new Rect(Screen.width - 100, Screen.height - 100, 100, 20), "Quit Game");
      
        if(quitGame) {
      
        Application.Quit(); 
      
        }
    }

    public static void Log(string text)
    {
        message.Append(text + "\n");
    }
}
