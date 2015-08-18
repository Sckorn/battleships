using UnityEngine;
using System.Collections;
using System;

public class Jobster : MonoBehaviour {
    public Game startedGame;
    private float lastScreenWidth;
    private float lastScreenHeight;
    public int changeSizeFlag;
    private int currentHour;
    private bool EnenmyCoroutine = false;

    public float LastWidth
    {
        set { this.lastScreenWidth = value; }
        get { return this.lastScreenWidth; }
    }

    public float LastHeight
    {
        set { this.lastScreenHeight = value; }
        get { return this.lastScreenHeight; }
    }

	// Use this for initialization
	void Start () {
        this.startedGame.Human.PrepareAllocate();
        this.lastScreenWidth = Screen.width;
        this.lastScreenHeight = Screen.height;
	}
	
	// Update is called once per frame
	void Update () {

        if (!this.startedGame.GameIsOver)
        {
            if(!this.EnenmyCoroutine)
                this.handleKeyPress();

            this.changeSizeFlag = (int)((this.lastScreenHeight - Screen.height) + (this.lastScreenWidth - Screen.width));

            if (this.startedGame.Human.allocationInProcess)
            {
                this.handleAllocation();
            }
            else
            {
                this.GameProcessItself();
            }
        }
	}

    void Awake()
    {
        this.HandleBattlefiel();

        this.startedGame = new Game();
        this.startedGame.Human.currentField.ResizeAfterCreate();
        this.startedGame.AI.currentField.ResizeAfterCreate();
        this.startedGame.AI.PlaceEnemyShips();
    }

    private void HandleBattlefiel()
    {
        this.currentHour = DateTime.Now.Hour;

        if (this.currentHour <= 7 || this.currentHour >= 21)
        {
            GameObject.Find("Nighttime Simple Water").SetActive(true);
            GameObject.Find("Daylight Simple Water").SetActive(false);
        }
        else if (this.currentHour > 7 && this.currentHour <= 20)
        {
            GameObject.Find("Nighttime Simple Water").SetActive(false);
            GameObject.Find("Daylight Simple Water").SetActive(true);
        }
    }

    private void GameProcessItself()
    {
        if (this.startedGame.WhosTurn == 1 && !this.EnenmyCoroutine)
        {
            return;
        }
        else
        {
            //this.startedGame.AI.EnemyShot();
            
            /*if (!this.startedGame.AI.TurnInProcess)
            {
                Debug.LogError("repeating Shots");
                StartCoroutine("EnemyShotHandle");
            }*/

            if (!this.EnenmyCoroutine)
            {
                StartCoroutine("EnemyTurnHandler");
            }
        }
    }

    private IEnumerator EnemyTurnHandler()
    {
        this.EnenmyCoroutine = true;
        int j = 0;
        yield return new WaitForSeconds(2.0f);
        while (this.EnenmyCoroutine)
        {
            Debug.Log("1:Time:" + Time.time);
            Debug.Log("enemy coroutine in progress; turn for: " + this.startedGame.WhosTurn.ToString());
            yield return new WaitForSeconds(2.0f);
            if (!this.startedGame.AI.justHit)
            {
                switch (this.startedGame.AI.EnemyShot())
                {
                    case 0: Debug.LogError("Hit the bitch"); yield return new WaitForSeconds(2.0f); this.startedGame.AI.currentCycle = 0; continue;
                    case 1: Debug.LogError("Missed the bitch"); yield return new WaitForSeconds(2.0f); this.startedGame.WhosTurn = 1; this.EnenmyCoroutine = false; break;
                    case 2: Debug.LogError("Cell is shot already"); yield return new WaitForSeconds(2.0f); continue;
                    case 3: Debug.LogError("Some bad shit happend"); yield return new WaitForSeconds(2.0f); Application.Quit(); break;

                };
            }
            else
            {
                //this.startedGame.AI.justHit = false;
                Debug.Log("Was hit now false");
                Debug.Log("Current cycle" + this.startedGame.AI.currentCycle.ToString());
                if (this.startedGame.AI.currentCycle < 4)
                {
                    int dir = 0;
                    TmpDir tDir;
                    tDir.dirX = 0;
                    tDir.dirY = 0;

                    switch (this.startedGame.AI.currentCycle)
                    {
                        case 0:
                            dir = -1;
                            tDir.dirX = 0;
                            tDir.dirY = 1;
                            break;

                        case 1:
                            dir = 1;
                            tDir.dirX = 1;
                            tDir.dirY = 0;
                            break;

                        case 2:
                            dir = 1;
                            tDir.dirX = 0;
                            tDir.dirY = 1;
                            break;

                        case 3:
                            dir = -1;
                            tDir.dirX = 1;
                            tDir.dirY = 0;
                            break;
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        yield return new WaitForSeconds(2.0f);
                        int tmpX = this.startedGame.AI.hp.x + (dir * tDir.dirX * i);
                        int tmpY = this.startedGame.AI.hp.y + (dir * tDir.dirY * i);

                        if (tmpX < 0 || tmpY < 0)
                        {
                            yield return new WaitForSeconds(2.0f);
                            this.startedGame.AI.currentCycle++;
                            break;
                        } //stop all coroutines

                        if (this.startedGame.Human.currentField.cells[tmpX, tmpY].HasShip
                            && this.startedGame.Human.currentField.cells[tmpX, tmpY].IsShot)
                        {
                            yield return new WaitForSeconds(2.0f);
                            continue;
                        } // yield return break; 

                        if (!this.startedGame.Human.currentField.cells[tmpX, tmpY].HasShip)
                        {
                            yield return new WaitForSeconds(2.0f);
                            this.startedGame.AI.currentCycle++;
                            break;
                            //StopCoroutine("EnemyTurnHandler");
                        } // yield return break;

                        int shotResult = this.startedGame.AI.EnemyShot(this.startedGame.AI.hp.x + (dir * tDir.dirX * i), this.startedGame.AI.hp.y + (dir * tDir.dirY * i));
                        Debug.Log("Ship alive? - " + this.startedGame.Human.ships[this.startedGame.AI.hitShipIndex].IsAlive + "Hit ship index " + this.startedGame.AI.hitShipIndex.ToString());
                        switch (shotResult)
                        {
                            case 0:
                                if (!this.startedGame.Human.ships[this.startedGame.AI.hitShipIndex].IsAlive)
                                {
                                    Debug.LogError("The ship is dead");
                                    yield return new WaitForSeconds(2.0f);
                                    this.startedGame.AI.justHit = false;
                                    this.startedGame.AI.hp.x = -1;
                                    this.startedGame.AI.hp.y = -1;
                                    this.startedGame.AI.currentCycle = 0;
                                    break;
                                    //StopCoroutine("EnemyTurnHandler");
                                }
                                break;
                            case 1:
                                yield return new WaitForSeconds(2.0f); this.startedGame.WhosTurn = 1; this.EnenmyCoroutine = false;
                                break;
                            case 2:
                                yield return new WaitForSeconds(2.0f); continue;
                            case 3:

                                break;
                        }
                    }
                }
                else
                {
                    this.startedGame.AI.justHit = false;
                    this.startedGame.AI.hp.x = -1;
                    this.startedGame.AI.hp.y = -1;
                    this.startedGame.AI.currentCycle = 0;
                    this.startedGame.WhosTurn = 1; 
                    this.EnenmyCoroutine = false;
                }
            }

            yield return new WaitForSeconds(2.0f);
            Debug.Log("2:Time:" + Time.time);
            j++;
            if (j > 100) break;
        }
        Debug.Log("enemy turn is over");
        yield return new WaitForSeconds(1.0f);
    }

    private IEnumerator EnemyShotHandle()
    {
        this.startedGame.AI.TurnInProcess = true;
        yield return new WaitForSeconds(0.5f);
        this.startedGame.AI.EnemyShot();
    }

    private void handleAllocation()
    {
        HumanPlayer playa = this.startedGame.Human;
        if (playa.currentShip == 0)
        {
            if (playa.ships[playa.currentShip] == null)
            {
                playa.ships[playa.currentShip] = new QuadDeck();
                EventManager.OnBindShip += this.startedGame.Human.ships[this.startedGame.Human.currentShip].Bind;
                playa.ships[playa.currentShip].Allocate(-1, playa.ships[playa.currentShip].X, playa.ships[playa.currentShip].Y, playa.currentField.singleCellSize);
            }
        }
        else if (playa.currentShip > 0 && playa.currentShip < 3)
        {
            if (playa.ships[playa.currentShip] == null)
            {
                playa.ships[playa.currentShip] = new TriDeck();
                EventManager.OnBindShip += this.startedGame.Human.ships[this.startedGame.Human.currentShip].Bind;
                playa.ships[playa.currentShip].Allocate(-1, playa.ships[playa.currentShip].X, playa.ships[playa.currentShip].Y, playa.currentField.singleCellSize);
            }
        }
        else if (playa.currentShip > 2 && playa.currentShip < 6)
        {
            if (playa.ships[playa.currentShip] == null)
            {
                playa.ships[playa.currentShip] = new DoubleDeck();
                EventManager.OnBindShip += this.startedGame.Human.ships[this.startedGame.Human.currentShip].Bind;
                playa.ships[playa.currentShip].Allocate(-1, playa.ships[playa.currentShip].X, playa.ships[playa.currentShip].Y, playa.currentField.singleCellSize);
            }
        }
        else if(playa.currentShip < 10)
        {
            if (playa.ships[playa.currentShip] == null)
            {
                playa.ships[playa.currentShip] = new SingleDeck();
                EventManager.OnBindShip += this.startedGame.Human.ships[this.startedGame.Human.currentShip].Bind;
                playa.ships[playa.currentShip].Allocate(-1, playa.ships[playa.currentShip].X, playa.ships[playa.currentShip].Y, playa.currentField.singleCellSize);
            }
        }
        else if (playa.currentShip == 10)
        {
            this.startedGame.Human.allocationInProcess = false;
            GameObject bluBor = GameObject.Find("blueBorder");
            Destroy(bluBor);
        }

        if (playa.currentShip < 10)
            playa.ships[playa.currentShip].CheckBindSpace();
    }

    private void handleKeyPress()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            EventManager.FireChangeOrientation();
        }

        if (!this.startedGame.Human.allocationInProcess)
        {
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                this.moveCrossHorizontal(-1);
            }

            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                this.moveCrossHorizontal(1);
            }

            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                this.moveCrossVertical(-1);
            }

            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                this.moveCrossVertical(1);
            }

            if (Input.GetKeyUp(KeyCode.Return))
            {
                this.startedGame.Human.ShootTheBitch(this.startedGame.Human.currentField.crosshairCellX, this.startedGame.Human.currentField.crosshairCellY);
            }

            /*if (Input.GetKeyUp(KeyCode.Return))
            {
                Time.timeScale = 0;
                this.startedGame.isGamePaused = true;
            }*/
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                this.moveBordHorizontal(-1);
            }

            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                this.moveBordHorizontal(1);
            }

            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                this.moveBordVertical(-1);
            }

            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                this.moveBordVertical(1);
            }

            if (Input.GetKeyUp(KeyCode.Return))
            {
                EventManager.FireBindShip();
            }
        }
    }

    private void moveCrossHorizontal(int dir)
    {
        if (dir > 0)
        {
            if (this.startedGame.Human.currentField.crosshairCellX < 9)
                this.startedGame.Human.currentField.moveCrosshair(1, 0);
        }
        else
        {
            if (this.startedGame.Human.currentField.crosshairCellX > 0)
            {
                this.startedGame.Human.currentField.moveCrosshair(-1, 0);
            }
        }
    }

    private void moveCrossVertical(int dir)
    {
        if (dir > 0)
        {
            if (this.startedGame.Human.currentField.crosshairCellY < 9)
                this.startedGame.Human.currentField.moveCrosshair(0, 1);
        }
        else
        {
            if (this.startedGame.Human.currentField.crosshairCellY > 0)
            {
                this.startedGame.Human.currentField.moveCrosshair(0, -1);
            }
        }
    }

    private void moveBordHorizontal(int dir)
    {
        if (dir > 0)
        {
            if (this.startedGame.Human.ships[this.startedGame.Human.currentShip].X < 9)
                this.startedGame.Human.ships[this.startedGame.Human.currentShip].ReAllocate(
                    this.startedGame.Human.ships[this.startedGame.Human.currentShip].Orientation,
                    this.startedGame.Human.ships[this.startedGame.Human.currentShip].X + dir, 
                    this.startedGame.Human.ships[this.startedGame.Human.currentShip].Y,
                    this.startedGame.Human.currentField.singleCellSize
                    );
        }
        else
        {
            if (this.startedGame.Human.ships[this.startedGame.Human.currentShip].X > 0)
            {
                this.startedGame.Human.ships[this.startedGame.Human.currentShip].ReAllocate(
                    this.startedGame.Human.ships[this.startedGame.Human.currentShip].Orientation,
                    this.startedGame.Human.ships[this.startedGame.Human.currentShip].X + dir,
                    this.startedGame.Human.ships[this.startedGame.Human.currentShip].Y,
                    this.startedGame.Human.currentField.singleCellSize
                    );
            }
        }
    }

    private void moveBordVertical(int dir)
    {
        if (dir > 0)
        {
            if (this.startedGame.Human.ships[this.startedGame.Human.currentShip].Y < 9)
                this.startedGame.Human.ships[this.startedGame.Human.currentShip].ReAllocate(
                    this.startedGame.Human.ships[this.startedGame.Human.currentShip].Orientation,
                    this.startedGame.Human.ships[this.startedGame.Human.currentShip].X,
                    this.startedGame.Human.ships[this.startedGame.Human.currentShip].Y + dir,
                    this.startedGame.Human.currentField.singleCellSize
                    );
        }
        else
        {
            if (this.startedGame.Human.ships[this.startedGame.Human.currentShip].Y > 0)
            {
                this.startedGame.Human.ships[this.startedGame.Human.currentShip].ReAllocate(
                    this.startedGame.Human.ships[this.startedGame.Human.currentShip].Orientation,
                    this.startedGame.Human.ships[this.startedGame.Human.currentShip].X,
                    this.startedGame.Human.ships[this.startedGame.Human.currentShip].Y + dir,
                    this.startedGame.Human.currentField.singleCellSize
                    );
            }
        }
    }

    public Jobster()
    { 
        
    }

    public CustomError startJob(string action, string fromClass, string fromMethod)
    {
        CustomError ce = new CustomError(fromClass, fromMethod);
        
        ce.isSuccess = false;
        System.Type t = this.GetType();
        foreach (System.Reflection.MethodInfo mi in t.GetMethods())
        {
            if (mi.Name == action)
            {
                ce.isSuccess = true;
                mi.Invoke(this, null);
            }
        }
        if (!ce.isSuccess)
        {
            ce.description = "There is no such action!\n";
        }

        return ce;
    }

    public void TestJob()
    {
        Debug.Log("This is test job. Testing jobster.");
    }

    public void GetCrossTexture()
    {
        CustomError ce = new CustomError();
        GUITexture gt = GameObject.Find("jobster").GetComponent<GUITexture>();
        if (gt == null)
        {
            ce.description = "Texture component not found!\n";
           // ce.isSuccess = false;
        }
        else
        {
           // ce.isSuccess = true;
            this.startedGame.Human.currentField.crossHair = gt;
        }
    }

    public void ShipCorInit(int sz)
    {
        StartCoroutine("ShipCourout", sz);
    }

    public void ShipCourout(int sz)
    {
        bool allocated = false;

        int size = sz;
        int x = 0;
        int y = 0;
        int or = 0;
        
        bool continueFlag = false;
        
        while (!allocated)
        {
            continueFlag = false;
            x = UnityEngine.Random.Range(0, 10);
            y = UnityEngine.Random.Range(0, 10);
            or = UnityEngine.Random.Range(1, 3);

            if (or == 1)
            {
                if (x + size > 9) {continue; }
            }
            else if (or == 2)
            {
                if (y + size > 9) {continue; }
            }

            for (int k = 0; k < size; k++)
            {
                if (or == 1)
                {
                    if (this.startedGame.AI.currentField.cells[x + k, y].IsOccupied)
                        continueFlag = true;
                }
                else if (or == 2)
                {
                    if (this.startedGame.AI.currentField.cells[x, y + k].IsOccupied)
                        continueFlag = true;
                }
            }

            if (continueFlag) continue;

            if (or == 1)
            {
                int j = 0;
                int barrier = size;
                if (x + size < 10) barrier += 1;
                if (x > 0) j = -1;
                
                for (; j < barrier; j++)
                {
                    this.startedGame.AI.currentField.cells[x + j, y].IsOccupied = true;
                    if(j > -1 && j < size)
                        this.startedGame.AI.currentField.cells[x + j, y].HasShip = true;

                    if (y > 0)
                        this.startedGame.AI.currentField.cells[x + j, y - 1].IsOccupied = true;
                    if (y < 9)
                        this.startedGame.AI.currentField.cells[x + j, y + 1].IsOccupied = true;
                }

                allocated = true;
            }
            else
            {
                int j = 0;
                int barrier = size;
                if (y + size < 10) barrier += 1;
                if (y > 0) j = -1;
                
                for (; j < barrier; j++)
                {
                    this.startedGame.AI.currentField.cells[x, y + j].IsOccupied = true;
                    if (j > -1 && j < size)
                        this.startedGame.AI.currentField.cells[x, y + j].HasShip = true;

                    if (x > 0)
                        this.startedGame.AI.currentField.cells[x - 1, y + j].IsOccupied = true;
                    if (x < 9)
                        this.startedGame.AI.currentField.cells[x + 1, y + j].IsOccupied = true;
                }

                allocated = true;
            }
        }

        this.startedGame.AI.ships[this.startedGame.AI.currentShip].Orientation = (or == 1) ? 1 : -1 ;
       
        for (int k = 0; k < size; k++)
        {
            if (or == 1)
            {
                this.startedGame.AI.ships[this.startedGame.AI.currentShip].SetCell(k, x + k, y, true, false, true);
            }
            else
            {
                this.startedGame.AI.ships[this.startedGame.AI.currentShip].SetCell(k, x, y + k, true, false, true);
            }
        }

        this.startedGame.AI.NextShip();
        return;
    }

    public void PlayExplosion()
    {
        AudioSource[] aas = GameObject.Find("Main Camera").GetComponents<AudioSource>();
        foreach (AudioSource a in aas)
        {
            if (a.clip.name == "explosion")
            {
                if (!a.isPlaying)
                {
                    AudioSource.PlayClipAtPoint(a.clip, GameObject.Find("Main Camera").transform.position);
                }
            }
        }
    }

    public void PlayMiss()
    {
        AudioSource[] aas = GameObject.Find("Main Camera").GetComponents<AudioSource>();
        foreach (AudioSource a in aas)
        {
            if (!a.isPlaying)
            {
                if (a.clip.name == "miss")
                {
                    AudioSource.PlayClipAtPoint(a.clip, GameObject.Find("Main Camera").transform.position);
                }
            }
        }
    }

    public void DestroyCrosshair()
    {
        Destroy(GameObject.Find("crosshair"));
    }
}
