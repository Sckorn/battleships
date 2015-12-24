using UnityEngine;
using System.Collections;

public class Field {
    public Cell[,] cells;
    private int byX = 10;
    private int byY = 10;
    public GUITexture crossHair;
    private int magX;
    private int magY;
    public GameObject realObjectReference;
    private float cellSize;
    public int crosshairCellX = 0;
    public int crosshairCellY = 0;

    private bool allocatingShips = false;

    public float singleCellSize
    {
        set { this.cellSize = value; }
        get { return this.cellSize; }
    }

    public bool ShipsAllocated
    {
        set { this.allocatingShips = value; }
        get { return this.allocatingShips; }
    }

    public Field()
    {
        CustomError ce = new CustomError("Field", "constructor");
        this.cells = new Cell[this.byX, this.byY];
        GUITexture gt = new GUITexture();
        Jobster jb = GameObject.Find("jobster").GetComponent<Jobster>();
    }

    public Field(string fieldName)
    {
        CustomError ce = new CustomError("Field", "constructor");
        this.cells = new Cell[this.byX, this.byY];
        Morpher mphr = GameObject.Find("Morpher").GetComponent<Morpher>();
        mphr.InstAField(fieldName, ref this.realObjectReference);
        this.crossHair = mphr.InstACross().GetComponent<GUITexture>();
    }

    public Field(string fieldName, int player)
    {
        CustomError ce = new CustomError("Field", "constructor");
        this.cells = new Cell[this.byX, this.byY];

        for (int i = 0; i < this.byX; i++)
        {
            for (int j = 0; j < this.byY; j++)
            {
                this.cells[i, j] = new Cell(i, j);
            }
        }

        Morpher mphr = GameObject.Find("Morpher").GetComponent<Morpher>();
        mphr.InstAField(fieldName, ref this.realObjectReference);
        if (player == 1)
        {
            this.crossHair = mphr.InstACross().GetComponent<GUITexture>();
            this.allocatingShips = true;
        }
        else
        {
            this.allocatingShips = false;
        }

        EventManager.OnResize += this.ResizeAfterCreate;
    }

    public void ResizeAfterCreate()
    {
        this.realObjectReference.GetComponent<fieldResize>().Resize();
    }

    public void moveCrosshair(int dirX, int dirY)
    {
        Rect pIns = this.crossHair.GetComponent<GUITexture>().pixelInset;
        this.crossHair.GetComponent<GUITexture>().pixelInset = new Rect(pIns.xMin + (this.cellSize * dirX), pIns.yMin - (this.cellSize * dirY), pIns.width, pIns.height);
        this.crosshairCellX = this.crosshairCellX + dirX;
        this.crosshairCellY = this.crosshairCellY + dirY;
    }

    public override string ToString()
    {
        string ret = string.Empty;

        for (int i = 0; i < this.byX; i++)
        {
            ret += "|";
            for (int j = 0; j < this.byY; j++)
            {
                if (this.cells[i, j].HasShip)
                {
                    ret += "X|";
                }
                else
                {
                    ret += " |";
                }
            }
            ret += "\n";
        }

        ret += "\n";

        for (int i = 0; i < this.byX; i++)
        {
            for (int j = 0; j < this.byY; j++)
            {
                ret += "I: " + i.ToString() + "; J: " + j.ToString() + "\n"
                    + this.cells[i, j].ToString() + "\n";
            }
        }

        return ret;
    }
}
