using UnityEngine;
using System.Collections;

public struct HitPoint
{
    public int x;
    public int y;

    public override string ToString()
    {
        string ret = string.Empty;
        ret += "\tHitPoint X: " + x.ToString() + "; Y: " + y.ToString();
        return ret;
    }
};

public struct Directions
{
    public int dir;
    public int sign;
};

public struct TmpDir
{
    public int dirX;
    public int dirY;

    public void swap()
    { 
        int tmp = this.dirX;
        this.dirX = this.dirY;
        this.dirY = tmp;
    }

    public override string ToString()
    {
        string ret = string.Empty;

        ret += "\tDir X: " + this.dirX.ToString() + "; Dir Y : " + this.dirY.ToString();

        return ret;
    }
};

public struct LastSuccessfulShot
{ 

};

public class AIPlayer : Player{
    public bool justHit = false;
    public HitPoint hp;
    private Jobster jbRef;
    public int hitShipSize;
    private TmpDir lastAxis;
    public int currentCycle = 0;
    public int hitShipIndex = -1;
    private bool turnInProcess = false;

    public bool TurnInProcess
    {
        get { return this.turnInProcess; }
        set { this.turnInProcess = value; }
    }
    
    public AIPlayer()
    {
        this.jobsterReference = GameObject.Find("jobster").GetComponent<Jobster>();
        this.currentField = new Field("enemy_field", 2);
        this.jbRef = GameObject.Find("jobster").GetComponent<Jobster>();
    }

    public override int EnemyShot()
    {
        /*if (this.justHit)
        {
            Debug.Log("Shooting after hit");
            this.ShootAfterHit();
            this.turnInProcess = false;
            return;
        }

        if (this.jbRef.startedGame.WhosTurn == 1)
            return;*/

        int x = Random.Range(0, 10);
        int y = Random.Range(0, 10);
        Jobster jb = GameObject.Find("jobster").GetComponent<Jobster>();
        Morpher mph = GameObject.Find("Morpher").GetComponent<Morpher>();
        //Debug.Log("enemy shot void");
        if (!jb.startedGame.Human.currentField.cells[x, y].IsShot)
        {
            jb.startedGame.Human.currentField.cells[x, y].IsShot = true;
            if (jb.startedGame.Human.currentField.cells[x, y].HasShip)
            {
                jb.startedGame.WhosTurn = 2;
                for (int i = 0; i < jb.startedGame.Human.ships.Length; i++)
                {
                    for (int j = 0; j < jb.startedGame.Human.ships[i].Size; j++)
                    {
                        Cell c = jb.startedGame.Human.ships[i].GetCell(j);
                        if (x == c.X && y == c.Y)
                        {
                            this.hp.x = x;
                            this.hp.y = y;
                            this.justHit = true;
                            jb.startedGame.Human.ships[i].ShotIncrease();
                            mph.InstShotCell(x, y, 1, 's');
                            this.hitShipSize = jb.startedGame.Human.ships[i].Size;
                            this.hitShipIndex = i;
                            jb.PlayExplosion();
                            this.turnInProcess = false;
                            //this.ShootAfterHit();
                            return 0;
                        }
                    }
                }
            }
            else
            {
                //jb.startedGame.WhosTurn = 1;
                mph.InstShotCell(x, y, 1, 'm');
                jb.PlayMiss();
                this.turnInProcess = false;
                return 1;
            }
        }
        else
        {
            //jb.startedGame.WhosTurn = 2;
            this.turnInProcess = false;
            return 2;
        }

        return 3;
    }

    public int EnemyShot(int x, int y)
    {
        x = Mathf.Abs(x);
        y = Mathf.Abs(y);
        Jobster jb = GameObject.Find("jobster").GetComponent<Jobster>();
        Morpher mph = GameObject.Find("Morpher").GetComponent<Morpher>();
        Debug.Log("Enemy shot int, int");
        if (!jb.startedGame.Human.currentField.cells[x, y].IsShot)
        {
            jb.startedGame.Human.currentField.cells[x, y].IsShot = true;
            if (jb.startedGame.Human.currentField.cells[x, y].HasShip)
            {
                jb.startedGame.WhosTurn = 2;
                for (int i = 0; i < jb.startedGame.Human.ships.Length; i++)
                {
                    for (int j = 0; j < jb.startedGame.Human.ships[i].Size; j++)
                    {
                        Cell c = jb.startedGame.Human.ships[i].GetCell(j);
                        if (x == c.X && y == c.Y)
                        {
                            jb.startedGame.Human.ships[i].ShotIncrease();
                            mph.InstShotCell(x, y, 1, 's');
                            jb.PlayExplosion();
                            return 0;
                        }
                    }
                }
            }
            else
            {
                //jb.startedGame.WhosTurn = 1;
                mph.InstShotCell(x, y, 1, 'm');
                jb.PlayMiss();
                this.turnInProcess = false;
                /*if (this.currentCycle >= 3)
                {
                    this.justHit = false;
                }*/
                return 1;
            }
        }
        else
        {
            //jb.startedGame.WhosTurn = 2;
            /*if (this.currentCycle >= 3)
            {
                this.justHit = false;
            }*/
            this.turnInProcess = false;
            return 2;
        }

        return 3;
    }

    private void ShootingCycle()
    {
        TmpDir tDir;
        tDir.dirX = 0;
        tDir.dirY = 0;
        int dir = 0;

        int tmpX = 0;
        int tmpY = 0;

        switch (this.currentCycle)
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

        waiter.dir = dir;
        waiter.tDir = tDir;
        GameObject.Find("waiter").GetComponent<waiter>().WaitForIteration();
        /*for (int i = 0; i < this.hitShipSize; i++)
        {
            tmpX = this.hp.x + (dir * tDir.dirX * i);
            tmpY = this.hp.y + (dir * tDir.dirY * i);

            waiter.tmpX = tmpX;
            waiter.tmpY = tmpY;

            //GameObject.Find("waiter").GetComponent<waiter>().WaitForIteration(i);

            if (tmpX < 0 || tmpY < 0) { this.currentCycle++; break; } //stop all coroutines

            if (this.jbRef.startedGame.Human.currentField.cells[tmpX, tmpY].HasShip 
                && this.jbRef.startedGame.Human.currentField.cells[tmpX, tmpY].IsShot) { continue; } // yield return break; 

            if (!this.jbRef.startedGame.Human.currentField.cells[tmpX, tmpY].HasShip) { this.currentCycle++; this.justHit = true; return; } // yield return break;

            waiter.enemyShotX = this.hp.x + (dir * tDir.dirX * i);
            waiter.enemyShotY = this.hp.y + (dir * tDir.dirY * i);

            GameObject.Find("waiter").GetComponent<waiter>().WaitForIteration(i);

            //this.EnemyShot(this.hp.x + (dir * tDir.dirX * i), this.hp.y + (dir * tDir.dirY * i));

            //if (!this.jbRef.startedGame.Human.ships[this.hitShipIndex].IsAlive) { this.justHit = false; this.hp.x = -1; this.hp.y = -1; this.currentCycle = 0; return; } // yield return break
            
        }*/
    }

    private void ShootAfterHit()
    {
        if (this.currentCycle < 4)
        {
            this.ShootingCycle();
        }
    }

    public override void PlaceEnemyShips()
    {
        Jobster jb = GameObject.Find("jobster").GetComponent<Jobster>();
        
        while (this.currentShip < 10)
        {
            if (this.currentShip == 0)
            {
                this.ships[this.currentShip] = new QuadDeck();
                jb.ShipCorInit(4);
            }
            else if (this.currentShip > 0 && this.currentShip < 3)
            {
                this.ships[this.currentShip] = new TriDeck();
                jb.ShipCorInit(3);
            }
            else if (this.currentShip > 2 && this.currentShip < 6)
            {
                this.ships[this.currentShip] = new DoubleDeck();
                jb.ShipCorInit(2);
            }
            else if (this.currentShip > 5 && this.currentShip < 10)
            {
                this.ships[this.currentShip] = new SingleDeck();
                jb.ShipCorInit(1);
            }
        }
    }
}
