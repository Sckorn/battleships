using UnityEngine;
using System.Collections;

public class waiter : MonoBehaviour {
    public static int tmpX;
    public static int tmpY;
    public static int dir;
    public static TmpDir tDir;
    public static int enemyShotX;
    public static int enemyShotY;
    private Jobster jbRef;
	// Use this for initialization
	void Start () {
        this.jbRef = GameObject.Find("jobster").GetComponent<Jobster>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Wait()
    {
        StartCoroutine(Example());
    }

    public IEnumerator Example()
    {
        Debug.Log("wait start: " + Time.realtimeSinceStartup.ToString());
        yield return new WaitForSeconds(5);
        Debug.Log("wait start: " + Time.realtimeSinceStartup.ToString());
    }

    public void WaitForIteration(int iteration)
    {
        StartCoroutine("IterationCorout", iteration);
    }

    public void WaitForIteration()
    {
        StartCoroutine("IterationCorout");
    }

    public IEnumerator IterationCorout()
    {
        //Debug.Log("Coroutine " + iter.ToString() + " Started at: " + Time.time);
        /*yield return new WaitForSeconds(2.5f);
        Debug.Log("Coroutine " + iter.ToString() + " Code xcution: " + Time.time);
        if (!this.jbRef.startedGame.Human.currentField.cells[tmpX, tmpY].HasShip) StopAllCoroutines();

        this.jbRef.startedGame.AI.EnemyShot(waiter.enemyShotX, waiter.enemyShotY);

        if (!this.jbRef.startedGame.Human.ships[this.jbRef.startedGame.AI.hitShipIndex].IsAlive) 
        { 
            this.jbRef.startedGame.AI.justHit = false; 
            this.jbRef.startedGame.AI.hp.x = -1; 
            this.jbRef.startedGame.AI.hp.y = -1; 
            this.jbRef.startedGame.AI.currentCycle = 0; 
            StopAllCoroutines(); 
        }*/ // yield return break
        //Debug.Log("Cycle coroutine started at " + Time.time.ToString());
        for (int i = 0; i < this.jbRef.startedGame.AI.hitShipSize; i++)
        {
            this.jbRef.startedGame.AI.TurnInProcess = true;
            yield return new WaitForSeconds(2.5f);

            tmpX = this.jbRef.startedGame.AI.hp.x + (dir * tDir.dirX * i);
            tmpY = this.jbRef.startedGame.AI.hp.y + (dir * tDir.dirY * i);

            //GameObject.Find("waiter").GetComponent<waiter>().WaitForIteration(i);

            if (tmpX < 0 || tmpY < 0) { Debug.Log("Shooting cycle broken on iteration " + i.ToString() + "Cell(" + tmpX.ToString() + "," + tmpY.ToString() + ")"); this.jbRef.startedGame.AI.currentCycle++; this.jbRef.startedGame.AI.TurnInProcess = false; break; } //stop all coroutines

            if (this.jbRef.startedGame.Human.currentField.cells[tmpX, tmpY].HasShip
                && this.jbRef.startedGame.Human.currentField.cells[tmpX, tmpY].IsShot) { Debug.Log("Shooting cycle continue on iteration " + i.ToString() + "Cell(" + tmpX.ToString() + "," + tmpY.ToString() + ")"); continue; } // yield return break; 

            if (!this.jbRef.startedGame.Human.currentField.cells[tmpX, tmpY].HasShip) { Debug.Log("Shooting cycle stopped on iteration " + i.ToString() + "Cell(" + tmpX.ToString() + "," + tmpY.ToString() + ")"); this.jbRef.startedGame.AI.currentCycle++; this.jbRef.startedGame.AI.justHit = true; this.jbRef.startedGame.WhosTurn = 1; StopAllCoroutines(); } // yield return break;


            this.jbRef.startedGame.AI.EnemyShot(this.jbRef.startedGame.AI.hp.x + (dir * tDir.dirX * i), this.jbRef.startedGame.AI.hp.y + (dir * tDir.dirY * i));

            if (this.jbRef.startedGame.AI.hitShipIndex > -2 && !this.jbRef.startedGame.Human.ships[this.jbRef.startedGame.AI.hitShipIndex].IsAlive) { Debug.Log("Shooting cycle stopped on iteration " + i.ToString() + "Cell(" + tmpX.ToString() + "," + tmpY.ToString() + ")"); this.jbRef.startedGame.AI.justHit = false; this.jbRef.startedGame.AI.hp.x = -1; this.jbRef.startedGame.AI.hp.y = -1; this.jbRef.startedGame.AI.currentCycle = 0; this.jbRef.startedGame.AI.TurnInProcess = false; this.jbRef.startedGame.AI.hitShipIndex = -2; StopAllCoroutines(); } // yield return break
        }
        /*this.jbRef.startedGame.AI.TurnInProcess = false;*/
        yield return new WaitForSeconds(2.5f);
    }
}
