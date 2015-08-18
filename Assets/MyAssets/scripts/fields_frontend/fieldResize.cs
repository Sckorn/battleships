using UnityEngine;
using System.Collections;

public class fieldResize : MonoBehaviour {
    public static float leftMargin = 20.0f;
    public static float topMargin = 20.0f;
	// Use this for initialization
	void Start () {
        
	}

    public void Resize()
    {
        Rect fieldSizes = gameObject.guiTexture.GetScreenRect();

        float halfOfScreen = Screen.width / 2;

        float aimWidth = halfOfScreen - fieldResize.leftMargin * 2;
        float aimHeight = Screen.height - fieldResize.topMargin * 2;
        int fixDimension = 0;
        float fixValue = 0.0f;
        if (aimHeight != aimWidth)
        {
            if (aimHeight > aimWidth)
            {
                fixDimension = 1;//fix height;
                fixValue = aimHeight - aimWidth;
            }
            else
                if (aimHeight < aimWidth)
                {
                    fixDimension = 2;//fix width
                    fixValue = aimWidth - aimHeight;
                }
        }

        float dimension = Mathf.Min(aimHeight, aimWidth);
        
        if (gameObject.name == "player_field")
            gameObject.guiTexture.pixelInset = new Rect(fieldResize.leftMargin, fieldResize.topMargin, dimension, dimension);
        else
            gameObject.guiTexture.pixelInset = new Rect(halfOfScreen + fieldResize.leftMargin, fieldResize.topMargin, dimension, dimension);
        Jobster jb = GameObject.Find("jobster").GetComponent<Jobster>();
        float cellSize = Mathf.Ceil(jb.startedGame.Human.currentField.realObjectReference.guiTexture.pixelInset.width / 10);
        jb.startedGame.Human.currentField.singleCellSize = cellSize;
        GameObject crosshair = GameObject.Find("crosshair");
        if (fixDimension == 1)
            crosshair.guiTexture.pixelInset = new Rect(halfOfScreen + fieldResize.leftMargin, Mathf.Floor(Screen.height - fieldResize.topMargin - cellSize - fixValue), cellSize, cellSize);
        else if (fixDimension == 2)
            crosshair.guiTexture.pixelInset = new Rect(halfOfScreen + fieldResize.leftMargin, Mathf.Floor(Screen.height - fieldResize.topMargin - cellSize), cellSize, cellSize);

        GameObject blueBor = GameObject.Find("blueBorder");
        if (blueBor != null)
        {
            if (fixDimension == 1)
                blueBor.guiTexture.pixelInset = new Rect(fieldResize.leftMargin, Mathf.Floor(Screen.height - fieldResize.topMargin - fixValue), cellSize, cellSize);
            else if (fixDimension == 2)
                blueBor.guiTexture.pixelInset = new Rect(fieldResize.leftMargin, Mathf.Floor(Screen.height - fieldResize.topMargin), cellSize, cellSize);

            blueBor.transform.position = new Vector3(0, 0, 0.4f);
        }

        jb.LastHeight = Screen.height;
        jb.LastWidth = Screen.width;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
