using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EndGameMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int halfOfScreen = Screen.height / 3;
        int targetWidth = Screen.width;

        GameObject.Find("endMainMenu").GetComponent<RectTransform>().sizeDelta = new Vector2(targetWidth, halfOfScreen);
        GameObject.Find("gameWinner").GetComponent<RectTransform>().sizeDelta = new Vector2(targetWidth, halfOfScreen);
        GameObject.Find("endNewGame").GetComponent<RectTransform>().sizeDelta = new Vector2(targetWidth, halfOfScreen);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void HandleClick()
    {
        if (gameObject.name == "endMainMenu")
            Application.LoadLevel(0);
        else
            if (gameObject.name == "endNewGame")
                Application.LoadLevel(1);
    }

    public void MouseHandlerOver()
    {
        gameObject.GetComponent<Text>().color = Color.red;
    }

    public void MouseHandlerOut()
    {
        gameObject.GetComponent<Text>().color = Color.white;
    }
}
