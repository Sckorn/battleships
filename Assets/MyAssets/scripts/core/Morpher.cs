using UnityEngine;
using System.Collections;

public class Morpher : MonoBehaviour {
    public GameObject empty;
    public GameObject crosshair;
    public GameObject battleShipHor;
    public GameObject battleShipVert;
    public GameObject ShotCell;
    public GameObject MissCell;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void InstAField(string objectName, ref GameObject obj)
    {
        GameObject go = (GameObject)Instantiate(this.empty, new Vector3(0, 0, 0.3f), Quaternion.identity);
        go.name = objectName;
        obj = go;
    }

    public GameObject InstACross()
    {
        GameObject go = (GameObject)Instantiate(this.crosshair, new Vector3(0, 0, 0.1f), Quaternion.identity);
        go.name = "crosshair";
        return go;
    }

    public void InstAShip(int orientation)
    {
        GameObject blueBor = GameObject.Find("blueBorder");
        GameObject go = (GameObject) Instantiate((orientation > 0) ? this.battleShipHor : this.battleShipVert, new Vector3(0, 0, 0.5f), Quaternion.identity);
        go.GetComponent<GUITexture>().pixelInset = blueBor.GetComponent<GUITexture>().pixelInset;
    }

    public void InstShotCell(int x, int y, int field, char mode)
    {
        Jobster jb = GameObject.Find("jobster").GetComponent<Jobster>();
        y = 9 - y;
        float resultX = fieldResize.leftMargin + jb.startedGame.Human.currentField.singleCellSize * x;
        float resultY = fieldResize.topMargin + jb.startedGame.Human.currentField.singleCellSize * y;
        GameObject resultingPref = (mode == 's') ? this.ShotCell : this.MissCell ;
        GameObject go = new GameObject();
        if (field == 1) // player
        {
            go = (GameObject)Instantiate(resultingPref, new Vector3(0, 0, 1.5f), Quaternion.identity);
        }
        else // enemy
        {
            resultX = jb.startedGame.AI.currentField.realObjectReference.GetComponent<GUITexture>().pixelInset.xMin + x * jb.startedGame.Human.currentField.singleCellSize;
            go = (GameObject)Instantiate(resultingPref, new Vector3(0, 0, 0), Quaternion.identity);
        }

        Rect rt = new Rect();

        rt.yMin = resultY;
        rt.yMax = resultY + jb.startedGame.Human.currentField.singleCellSize;
        rt.xMax = resultX + jb.startedGame.Human.currentField.singleCellSize;
        rt.xMin = resultX;

        go.GetComponent<GUITexture>().pixelInset = rt;
    }
}
