using UnityEngine;
using System.Collections;

public class newGameItemHwnd : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseOver()
    {
        guiText.material.color = Color.red;
    }

    void OnMouseExit()
    {
        guiText.material.color = Color.white;
    }

    void OnMouseUp()
    {
        Debug.Log(guiText.name);
        if (guiText.name == "newGame")
            Application.LoadLevel(1);
        else
            if (guiText.name == "multiplayer")
                Application.LoadLevel(2);
            else
                if (guiText.name == "exitGame")
                    Application.Quit();
    }
}
