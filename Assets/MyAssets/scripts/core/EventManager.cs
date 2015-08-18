using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {
    public delegate void ChangeOrientationAction();
    public static event ChangeOrientationAction OnChangeOrientation;

    public delegate void Resize();
    public static event Resize OnResize;

    public delegate void BindShip();
    public static event BindShip OnBindShip;

    public delegate void ShipDestruction();
    public static event ShipDestruction OnShipDestruction;
    
    public delegate void GameOver();
    public static event GameOver OnGameOver;

    public delegate void GamePause();
    public static event GamePause OnGamePause;

    public delegate void GameResume();
    public static event GameResume OnGameResume; 


	// Use this for initialization
	void Start () {
  
	}
	
	// Update is called once per frame
	void Update () {
        Jobster job = GameObject.Find("jobster").GetComponent<Jobster>();

        if (job.changeSizeFlag != 0)
        {
            EventManager.FireResize();
        }

        if (Input.GetKeyUp(KeyCode.Escape))
            EventManager.FireGamePause();
	}

    private static void FireGamePause()
    {
        if (EventManager.OnGamePause != null)
        {
            EventManager.OnGamePause();
        }
    }

    public static void FireChangeOrientation()
    {
        if (EventManager.OnChangeOrientation != null)
        {
            EventManager.OnChangeOrientation();
        }
    }

    private static void FireResize()
    {
        if(EventManager.OnResize != null)
        {
            EventManager.OnResize();
        }
    }

    public static void FireBindShip()
    {
        Jobster jb = GameObject.Find("jobster").GetComponent<Jobster>();

        if (jb.startedGame.Human.ships[jb.startedGame.Human.currentShip].AllowedToBind)
        {
            if (EventManager.OnBindShip != null)
            {
                EventManager.OnBindShip();
            }
        }        
    }

    public static void FireShipDestruction()
    {
        if (EventManager.OnShipDestruction != null)
        {
            EventManager.OnShipDestruction();
        }
    }

    public static void FireGameOver()
    {
        if (EventManager.OnGameOver != null)
        {
            EventManager.OnGameOver();
        }
    }

    public static void FireGameResume()
    {
        if (EventManager.OnGameResume != null)
        {
            EventManager.OnGameResume();
        }
    }
}
