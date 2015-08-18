using UnityEngine;
using System.Collections;

public class Ship {
    protected bool allocated = false;
    protected int size;
    protected Cell[] cells;
    protected bool alive = true;
    protected int currentX;
    protected int currentY;
    protected int currentOrient = -1;
    protected bool allowBind = false;
    private int shotCellNum = 0;

    public Ship()
    { 
        
    }

    public Ship(int sz)
    {
        this.size = sz;
        this.cells = new Cell[this.size];
        for (int i = 0; i < this.size; i++)
        {
            this.cells[i] = new Cell();
        }
    }

    public int ShotCells
    {
        get { return this.shotCellNum; }
        set { this.shotCellNum = value; }
    }

    public int X
    {
        get { return this.currentX; }
        set { this.currentX = value; }
    }

    public int Y
    {
        get { return this.currentY; }
        set { this.currentY = value; }
    }

    public int Orientation
    {
        get { return this.currentOrient; }
        set { this.currentOrient = value; }
    }

    public bool AllowedToBind
    {
        get { return this.allowBind; }
        set { this.allowBind = value; }
    }

    public int Size
    {
        get { return this.size; }
        set { this.size = value; }
    }

    public bool IsAlive
    {
        get { return this.alive; }
        set { this.alive = value; }
    }

    public void ShotIncrease()
    {
        this.ShotCells += 1;
        if (this.ShotCells == this.size)
        {
            this.alive = false;
            EventManager.OnShipDestruction += this.ShipDestroyed;
            EventManager.FireShipDestruction();
            EventManager.OnShipDestruction -= this.ShipDestroyed;
        }
    }

    public virtual Cell GetCell(int index)
    {
        return this.cells[index];
    }

    public void SetCell(int index, Cell c)
    {
        this.cells[index].X = c.X;
        this.cells[index].Y = c.Y;

        this.cells[index].HasShip = c.HasShip;
        this.cells[index].IsOccupied = c.IsOccupied;
        this.cells[index].IsShot = c.IsShot;
    }

    public virtual void SetCell(int index, int _x, int _y, bool _hs, bool _is, bool _io)
    {
        
    }

    private void ShipDestroyed()
    {
        Jobster jb = GameObject.Find("jobster").GetComponent<Jobster>();
        Morpher mph = GameObject.Find("Morpher").GetComponent<Morpher>();
        if (this.shotCellNum == this.size)
        {
            //int i = 0;
            int barrier = this.size;

            int x = this.cells[0].X;
            int y = this.cells[0].Y;
            int field = (jb.startedGame.WhosTurn == 1) ? 2 : 1;
            Field fieldReference = (jb.startedGame.WhosTurn == 1) ? jb.startedGame.AI.currentField : jb.startedGame.Human.currentField;

            if (this.Orientation > 0) //width
            {
                if (x > 0)
                {
                    if (y > 0)
                    {
                        if (!fieldReference.cells[x - 1, y - 1].IsShot)
                        {
                            fieldReference.cells[x - 1, y - 1].IsShot = true;
                            mph.InstShotCell(x - 1, y - 1, field, 'm');
                        }
                    }

                    if (!fieldReference.cells[x - 1, y].IsShot)
                    {
                        fieldReference.cells[x - 1, y].IsShot = true;
                        mph.InstShotCell(x - 1, y, field, 'm');
                    }

                    if (y < 9)
                    {
                        if (!fieldReference.cells[x - 1, y + 1].IsShot)
                        {
                            fieldReference.cells[x - 1, y + 1].IsShot = true;
                            mph.InstShotCell(x - 1, y + 1, field, 'm');
                        }                        
                    }
                }

                for (int i = 0; i < this.size; i++)
                {
                    if (y > 0)
                    {
                        if (!fieldReference.cells[x + i, y - 1].IsShot)
                        {
                            mph.InstShotCell(x + i, y - 1, field, 'm');
                        }                        
                    }

                    if (y < 9)
                    {
                        if (!fieldReference.cells[x + i, y + 1].IsShot)
                        {
                            mph.InstShotCell(x + i, y + 1, field, 'm');
                        }
                    }
                }

                if (x < 9)
                {
                    x = this.cells[this.cells.Length - 1].X;
                    y = this.cells[this.cells.Length - 1].Y;

                    if (y > 0)
                    {
                        if (!fieldReference.cells[x + 1, y - 1].IsShot)
                        {
                            fieldReference.cells[x + 1, y - 1].IsShot = true;
                            mph.InstShotCell(x + 1, y - 1, field, 'm');
                        }
                    }

                    if (!fieldReference.cells[x + 1, y].IsShot)
                    {
                        fieldReference.cells[x + 1, y].IsShot = true;
                        mph.InstShotCell(x + 1, y, field, 'm');
                    }

                    if (y < 9)
                    {
                        if (!fieldReference.cells[x + 1, y + 1].IsShot)
                        {
                            fieldReference.cells[x + 1, y + 1].IsShot = true;
                            mph.InstShotCell(x + 1, y + 1, field, 'm');
                        }
                    }
                }
            }
            else // height
            {
                if (y > 0)
                {
                    if (x > 0)
                    {
                        if (!fieldReference.cells[x - 1, y - 1].IsShot)
                        {
                            fieldReference.cells[x - 1, y - 1].IsShot = true;
                            mph.InstShotCell(x - 1, y - 1, field, 'm');
                        }
                    }

                    if (!fieldReference.cells[x, y - 1].IsShot)
                    {
                        fieldReference.cells[x, y - 1].IsShot = true;
                        mph.InstShotCell(x, y - 1, field, 'm');
                    }

                    if (x < 9)
                    {
                        if (!fieldReference.cells[x + 1, y - 1].IsShot)
                        {
                            fieldReference.cells[x + 1, y - 1].IsShot = true;
                            mph.InstShotCell(x + 1, y - 1, field, 'm');
                        }
                    }
                }

                for (int i = 0; i < this.size; i++)
                {
                    if (x > 0)
                    {
                        if (!fieldReference.cells[x - 1, y + i].IsShot)
                            mph.InstShotCell(x - 1, y + i, field, 'm');
                    }

                    if (x < 9)
                    {
                        if (!fieldReference.cells[x + 1, y + i].IsShot)
                            mph.InstShotCell(x + 1, y + i, field, 'm');
                    }
                }

                if (y < 9)
                {
                    x = this.cells[this.cells.Length - 1].X;
                    y = this.cells[this.cells.Length - 1].Y;

                    if (x > 0)
                    {
                        if (!fieldReference.cells[x - 1, y + 1].IsShot)
                        {
                            fieldReference.cells[x - 1, y + 1].IsShot = true;
                            mph.InstShotCell(x - 1, y + 1, field, 'm');
                        }
                    }

                    if (!fieldReference.cells[x, y + 1].IsShot)
                    {
                        fieldReference.cells[x, y + 1].IsShot = true;
                        mph.InstShotCell(x, y + 1, field, 'm');
                    }

                    if (x < 9)
                    {
                        if (!fieldReference.cells[x + 1, y + 1].IsShot)
                        {
                            fieldReference.cells[x + 1, y + 1].IsShot = true;
                            mph.InstShotCell(x + 1, y + 1, field, 'm');
                        }
                    }
                }
            }

            if (jb.startedGame.WhosTurn == 1)
            {
                jb.startedGame.AI.DecShips();
            }
            else
            {
                jb.startedGame.Human.DecShips();
            }
        }
    }

    public virtual void Allocate(int orientation, int x, int y, float cellSize)
    {
        
    }

    public virtual void ReAllocate(int orientation, int x, int y, float cellSize)
    { 
        
    }

    public virtual void PreparationsForChange(int orientation, int x, int y, float cellSize)
    {
        
    }

    public virtual void Bind()
    { 
        
    }

    public virtual void CheckBindSpace()
    { 
        
    }

    public override string ToString()
    {
        string ret = string.Empty;
        ret += "Ship size: " + this.size.ToString() + ";\n" +
            "Class: " + this.GetType().ToString() + ";\n" +
            "Orientation: " + this.Orientation.ToString() + ";\n" +
            "Starting cell: " + this.cells[0].X.ToString() + ", " + this.cells[0].Y.ToString() + ";\n" +
            "Starting cell: " + this.cells[this.size - 1].X.ToString() + ", " + this.cells[this.size - 1].Y.ToString() + ";\n";
        return ret;
    }
}
