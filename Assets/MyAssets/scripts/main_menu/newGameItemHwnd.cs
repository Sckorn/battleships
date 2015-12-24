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
        GetComponent<GUIText>().material.color = Color.red;
    }

    void OnMouseExit()
    {
        GetComponent<GUIText>().material.color = Color.white;
    }

    void OnMouseUp()
    {
        Debug.Log(GetComponent<GUIText>().name);
        if (GetComponent<GUIText>().name == "newGame")
            Application.LoadLevel(1);
        else
            if (GetComponent<GUIText>().name == "multiplayer")
                Application.LoadLevel(2);
            else
                if (GetComponent<GUIText>().name == "exitGame")
                    Application.Quit();
    }
}
