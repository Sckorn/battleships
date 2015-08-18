using UnityEngine;
using System.Collections;

public class Cell{
    private bool cellIsShot = false;
    private bool cellHasShipPart = false;
    private bool cellOccupied = false;
    private int cellX;
    private int cellY;

    public bool IsShot
    {
        get { return this.cellIsShot; }
        set { this.cellIsShot = value; }
    }

    public bool HasShip
    {
        get { return this.cellHasShipPart; }
        set { this.cellHasShipPart = value; }
    }

    public bool IsOccupied
    {
        get { return this.cellOccupied; }
        set { this.cellOccupied = value; }
    }

    public int X
    {
        get { return this.cellX; }
        set { this.cellX = value; }
    }

    public int Y
    {
        get { return this.cellY; }
        set { this.cellY = value; }
    }

    public Cell()
    { 
        
    }

    public Cell(int x, int y)
    {
        this.cellX = x;
        this.cellY = y;
    }

    public override string ToString()
    {
        string retStr = string.Empty;
        retStr += "Cell class | Occupied: " + this.IsOccupied.ToString() + "\n"
            + "Has ship: " + this.HasShip.ToString() + "\n"
            + "Is shot: " + this.IsShot.ToString() + "\n"
            + "Coords: X -> " + this.X.ToString() + "; Y -> " + this.Y.ToString() + "\n";
        return retStr;
    }
}
