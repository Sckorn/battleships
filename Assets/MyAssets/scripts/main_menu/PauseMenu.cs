using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int thirdPartOfScreen = Screen.height / 3;
        int targetWidth = Screen.width;

        GameObject.Find("newGame").GetComponent<RectTransform>().sizeDelta = new Vector2(targetWidth, thirdPartOfScreen);
        GameObject.Find("resumeGame").GetComponent<RectTransform>().sizeDelta = new Vector2(targetWidth, thirdPartOfScreen);
        GameObject.Find("mainMenu").GetComponent<RectTransform>().sizeDelta = new Vector2(targetWidth, thirdPartOfScreen);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void HandleClick()
    {
        Debug.Log(gameObject.name);
        if (gameObject.name == "resumeGame")
            EventManager.FireGameResume();
        else
            if (gameObject.name == "newGame")
                Application.LoadLevel(1);
            else
                if (gameObject.name == "mainMenu")
                    Application.LoadLevel(0);
    }

    public void MouseHandlerOn()
    {
        gameObject.GetComponent<Text>().color = Color.red;
    }

    public void MouseHandlerExit()
    {
        gameObject.GetComponent<Text>().color = Color.white;
    }
}
