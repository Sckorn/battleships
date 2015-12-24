using UnityEngine;
using System.Collections;

public class WorkInProgressMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject wnp = GameObject.Find("workInProgress");

        Rect wnpRect = wnp.GetComponent<GUIText>().GetScreenRect();

        double ratio = (wnp.GetComponent<GUIText>().fontSize) / wnpRect.width;

        double aimFont = Screen.width * ratio;

        wnp.GetComponent<GUIText>().fontSize = (int) aimFont;

        wnp.GetComponent<GUIText>().pixelOffset = new Vector2((Screen.width / 2), (Screen.height / 2));

        gameObject.GetComponent<GUIText>().pixelOffset = new Vector2((Screen.width / 2), 50);
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
        Application.LoadLevel(0);
    }
}
