using UnityEngine;
using System.Collections;

public class Player{
    protected string pName = "Player1";
    public Field currentField;
    public Ship[] ships = new Ship[10];
    public int currentShip = 0;
    public bool allocationInProcess = false;
    protected float enemyallocationTime;
    protected Jobster jobsterReference;
    protected int shipCount = 10;
    public string playerName
    {
        get { return this.pName; }
        set { this.pName = value; }
    }

    public void NextShip()
    {
        this.currentShip++;
    }

    public void DecShips()
    {
        this.shipCount--;
        if (this.shipCount == 0)
        {
            EventManager.FireGameOver();
        }
    }

    public Player()
    {

    }

    //human interface

    public virtual void ShootTheBitch(int x, int y)
    { 
        
    }

    public virtual void PrepareAllocate()
    {
        
    }

    public virtual void Allocating()
    {
        
    }

    //human interface

    //ai interface

    public virtual int EnemyShot()
    {
        return 0;
    }

    public virtual void PlaceEnemyShips()
    {
        
    }

    //ai interface
}