using UnityEngine;
using System.Collections;

public class WorkInProgressMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject wnp = GameObject.Find("workInProgress");

        Rect wnpRect = wnp.guiText.GetScreenRect();

        double ratio = (wnp.guiText.fontSize) / wnpRect.width;

        double aimFont = Screen.width * ratio;

        wnp.guiText.fontSize = (int) aimFont;

        wnp.guiText.pixelOffset = new Vector2((Screen.width / 2), (Screen.height / 2));

        gameObject.guiText.pixelOffset = new Vector2((Screen.width / 2), 50);
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
        Application.LoadLevel(0);
    }
}
