using UnityEngine;
using System.Collections;

public class HumanPlayer : Player {

    public HumanPlayer()
    {
        this.jobsterReference = GameObject.Find("jobster").GetComponent<Jobster>();

        this.currentField = new Field("player_field", 1);
        EventManager.OnBindShip += NextShip;
    }

    public override void ShootTheBitch(int x, int y)
    {
        Jobster jb = GameObject.Find("jobster").GetComponent<Jobster>();
        Morpher mph = GameObject.Find("Morpher").GetComponent<Morpher>();

        if (!jb.startedGame.AI.currentField.cells[x, y].IsShot)
        {
            jb.startedGame.AI.currentField.cells[x, y].IsShot = true;
            if (jb.startedGame.AI.currentField.cells[x, y].HasShip)
            {
                jb.startedGame.WhosTurn = 1;
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < jb.startedGame.AI.ships[i].Size; j++)
                    {
                        Cell c = jb.startedGame.AI.ships[i].GetCell(j);
                        if (x == c.X && y == c.Y)
                        {
                            jb.startedGame.AI.ships[i].ShotIncrease();
                            mph.InstShotCell(x, y, 2, 's');
                            jb.PlayExplosion();
                        }
                    }
                }
            }
            else
            {
                jb.startedGame.WhosTurn = 2;
                mph.InstShotCell(x, y, 2, 'm');
                jb.PlayMiss();
            }
        }
        else
        {
            jb.startedGame.WhosTurn = 1;
        }
    }

    public override void PrepareAllocate()
    {
        GameObject bord = GameObject.Find("blueBorder");

        if (this.currentField.realObjectReference != null)
        {
            this.Allocating();
        }
    }

    public override void Allocating()
    {
        this.allocationInProcess = true;
    }
}
