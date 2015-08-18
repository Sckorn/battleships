using UnityEngine;
using System.Collections;

public class QuadDeck : Ship {

    public QuadDeck() : base(4)
    {
        this.size = 4;
        this.alive = true;
        this.allocated = false;
        this.currentX = 0;
        this.currentY = 0;
        this.allowBind = true;
        EventManager.OnChangeOrientation += ChangeOrientation;
        //EventManager.OnBindShip += Bind;
    }

    public QuadDeck(int sz) : base(sz)
    {
        
    }

    public void ChangeOrientation()
    {
        this.Orientation *= -1;
        Jobster jb = GameObject.Find("jobster").GetComponent<Jobster>();
        this.PreparationsForChange(this.Orientation, this.X, this.Y, jb.startedGame.Human.currentField.singleCellSize);
    }

    public override void Allocate(int orientation, int x, int y, float cellSize)
    {
        GameObject blueBor = GameObject.Find("blueBorder");
        Rect pIns = blueBor.guiTexture.pixelInset;

        if (orientation > 0)
        {
            blueBor.guiTexture.pixelInset = new Rect(pIns.xMin + (x * cellSize) - cellSize, pIns.yMin - (y * cellSize), pIns.width * this.size, -(this.Orientation * cellSize));
        }
        else
        {
            blueBor.guiTexture.pixelInset = new Rect(pIns.xMin + (x * cellSize), pIns.yMin - (y * cellSize), cellSize, Mathf.Abs(pIns.height) * this.size * this.Orientation);
        }
        //blueBor.guiTexture.pixelInset = new Rect(pIns.left + (x * cellSize), pIns.top - (((this.size - 1) * cellSize) + (y * cellSize) ), (orientation == 1) ? pIns.width * this.size : pIns.width, (orientation == -1) ? pIns.height * this.size : pIns.height);
    }

    public override void ReAllocate(int orientation, int x, int y, float cellSize)
    {
        bool allowRealloc = false;
        if (orientation > 0)
        {
            if(x + this.size < 11)
                allowRealloc = true;
        }
        else
        { 
            if(y + this.size < 11)
                allowRealloc = true;
        }
        if (allowRealloc)
        {
            GameObject blueBor = GameObject.Find("blueBorder");
            Rect pIns = blueBor.guiTexture.pixelInset;
            int diffX = x - this.currentX;
            int diffY = y - this.currentY;

            if (orientation > 0)
            {
                blueBor.guiTexture.pixelInset = new Rect(pIns.xMin + (diffX * cellSize), pIns.yMin - (diffY * cellSize), pIns.width, pIns.height);
            }
            else
            {
                blueBor.guiTexture.pixelInset = new Rect(pIns.xMin + (diffX * cellSize), pIns.yMin - (diffY * cellSize), pIns.width, pIns.height);
            }

            this.currentX = x;
            this.currentY = y;
        }
    }

    public override void PreparationsForChange(int orientation, int x, int y, float cellSize)
    {
        bool allowChange = false;
        GameObject blueBor = GameObject.Find("blueBorder");
        Rect pIns = blueBor.guiTexture.pixelInset;
        GameObject playaField = GameObject.Find("player_field");
        Rect rt = new Rect();
        rt.xMin = fieldResize.leftMargin + this.X * cellSize;
        rt.yMax = fieldResize.topMargin + playaField.guiTexture.pixelInset.height - (this.Y) * cellSize;

        if (orientation > 0)
        { 
            rt.xMax = rt.xMin + this.size * cellSize;
            rt.yMin = rt.yMax - cellSize;
            if (this.X + this.size < 11)
                allowChange = true;
        }
        else
        {
            rt.xMax = rt.xMin + cellSize;
            rt.yMin = rt.yMax - cellSize * this.size;
            if (this.Y + this.size < 11)
                allowChange = true;
        }

        if(allowChange)
            blueBor.guiTexture.pixelInset = rt;
    }

    public override void Bind()
    {
        Jobster jb = GameObject.Find("jobster").GetComponent<Jobster>();
        Morpher mph = GameObject.Find("Morpher").GetComponent<Morpher>();
        int i = 0;
        int barrier = this.size;

        if (this.Orientation > 0)
        {
            if (this.X > 0) i = -1;
            else i = 0;

            if (this.X + this.size < 10) barrier += 1;
        }
        else
        {
            if (this.Y > 0) i = -1;
            else i = 0;

            if (this.Y + this.size < 10) barrier += 1;
        }

        for (; i < barrier; i++)
        {
            if (this.Orientation > 0)
            {
                jb.startedGame.Human.currentField.cells[this.X + i, this.Y].IsOccupied = true;
                if (i > -1 && i < size + 1)
                    jb.startedGame.Human.currentField.cells[this.X + i, this.Y].HasShip = true;

                if (this.Y > 0)
                    jb.startedGame.Human.currentField.cells[this.X + i, this.Y - 1].IsOccupied = true;
                if (this.Y < 9)
                    jb.startedGame.Human.currentField.cells[this.X + i, this.Y + 1].IsOccupied = true;
            }
            else
            {
                jb.startedGame.Human.currentField.cells[this.X, this.Y + i].IsOccupied = true;
                if (i > -1 && i < size + 1)
                    jb.startedGame.Human.currentField.cells[this.X, this.Y + i].HasShip = true;

                if (this.X > 0)
                    jb.startedGame.Human.currentField.cells[this.X - 1, this.Y + i].IsOccupied = true;
                if (this.X < 9)
                    jb.startedGame.Human.currentField.cells[this.X + 1, this.Y + i].IsOccupied = true;
            }
        }

        for (int k = 0; k < this.size; k++)
        {
            if (this.Orientation > 0)
            {
                this.cells[k] = jb.startedGame.Human.currentField.cells[this.X + k, this.Y];
            }
            else
            {
                this.cells[k] = jb.startedGame.Human.currentField.cells[this.X, this.Y + k];
            }
        }

        mph.InstAShip(this.Orientation);
        EventManager.OnBindShip -= this.Bind;
    }

    public override void CheckBindSpace()
    {
        this.AllowedToBind = true;
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

    public override void SetCell(int index, int _x, int _y, bool _hs, bool _is, bool _io)
    {
        this.cells[index].X = _x;
        this.cells[index].Y = _y;

        this.cells[index].HasShip = _hs;
        this.cells[index].IsOccupied = _io;
        this.cells[index].IsShot = _is;
    }

    public override Cell GetCell(int index)
    {
        /*Debug.Log("Getting the cell");
        Debug.Log("From ship");
        Debug.Log(base.ToString());
        Debug.Log("The cell: ");
        Debug.Log(base.cells[index].ToString());*/
        return this.cells[index];
    }
}
