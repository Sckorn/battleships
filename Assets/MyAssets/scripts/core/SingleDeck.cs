using UnityEngine;
using System.Collections;

public class SingleDeck : Ship
{
    public SingleDeck() : base(1)
    {
        this.size = 1;
        this.alive = true;
        this.allocated = false;
        this.currentX = 0;
        this.currentY = 0;
        //EventManager.OnBindShip += Bind;
    }

    public SingleDeck(int sz) : base(sz)
    {

    }

    public override void Allocate(int orientation, int x, int y, float cellSize)
    {
        GameObject playaField = GameObject.Find("player_field");
        GameObject blueBor = GameObject.Find("blueBorder");
        Rect pIns = blueBor.GetComponent<GUITexture>().pixelInset;
        Rect tmp = new Rect();
        tmp.yMin = fieldResize.topMargin + playaField.GetComponent<GUITexture>().pixelInset.height - this.Y * cellSize;
        tmp.xMin = fieldResize.leftMargin;

        if (orientation > 0)
        {
            tmp.yMax = tmp.yMin - cellSize;
            tmp.xMax = tmp.xMin + cellSize * this.size;
            blueBor.GetComponent<GUITexture>().pixelInset = tmp;
        }
        else
        {
            tmp.yMax = tmp.yMin - this.size * cellSize;
            tmp.xMax = tmp.xMin + cellSize;
            blueBor.GetComponent<GUITexture>().pixelInset = tmp;
        }
    }

    public override void ReAllocate(int orientation, int x, int y, float cellSize)
    {
        bool allowRealloc = false;
        if (orientation > 0)
        {
            if (x + this.size < 11)
                allowRealloc = true;
        }
        else
        {
            if (y + this.size < 11)
                allowRealloc = true;
        }
        if (allowRealloc)
        {
            GameObject blueBor = GameObject.Find("blueBorder");
            Rect pIns = blueBor.GetComponent<GUITexture>().pixelInset;
            int diffX = x - this.currentX;
            int diffY = y - this.currentY;

            if (orientation > 0)
            {
                blueBor.GetComponent<GUITexture>().pixelInset = new Rect(pIns.xMin + (diffX * cellSize), pIns.yMin - (diffY * cellSize), pIns.width, pIns.height);
            }
            else
            {
                blueBor.GetComponent<GUITexture>().pixelInset = new Rect(pIns.xMin + (diffX * cellSize), pIns.yMin - (diffY * cellSize), pIns.width, pIns.height);
            }

            this.currentX = x;
            this.currentY = y;
        }
    }

    public override void PreparationsForChange(int orientation, int x, int y, float cellSize)
    {
        bool allowChange = false;
        GameObject blueBor = GameObject.Find("blueBorder");
        Rect pIns = blueBor.GetComponent<GUITexture>().pixelInset;
        GameObject playaField = GameObject.Find("player_field");
        Rect rt = new Rect();
        rt.xMin = fieldResize.leftMargin + this.X * cellSize;
        rt.yMax = fieldResize.topMargin + playaField.GetComponent<GUITexture>().pixelInset.height - (this.Y) * cellSize;

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

        if (allowChange)
            blueBor.GetComponent<GUITexture>().pixelInset = rt;
    }

    public override void Bind()
    {
        this.CheckBindSpace();
        Morpher mph = GameObject.Find("Morpher").GetComponent<Morpher>();
        Jobster jb = GameObject.Find("jobster").GetComponent<Jobster>();
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
        Jobster jb = GameObject.Find("jobster").GetComponent<Jobster>();

        int plusX = (this.Orientation > 0) ? 1 : 0;
        int plusY = (this.Orientation < 0) ? 1 : 0;

        for (int i = 0; i < this.size; i++)
        {
            if (jb.startedGame.Human.currentField.cells[this.X + (plusX * i), this.Y + (plusY * i)].IsOccupied)
            {
                this.AllowedToBind = false;
                return;
            }
        }

        this.AllowedToBind = true;
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
        return this.cells[index];
    }
}
