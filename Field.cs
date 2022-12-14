using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship_3._0
{
    public class Field
    {
        Form parent;

        public const int mapSize = 10;
        public int objectSize = 64;

        public Button[,] playerButtons = new Button[mapSize, mapSize];
        public Button[,] enemyButtons = new Button[mapSize, mapSize];

        public FieldObject playerField = new FieldObject();
        public FieldObject enemyField = new FieldObject();

        public Field(Form parent)
        {
            this.parent = parent;
            DrawField();
        }

        private void DrawField()
        {
            DrawPlayerField();
            DrawEnemyField();
        }

        private void DrawPlayerField()
        {
            for (short i = 0; i < mapSize; i++)
            {
                for (short j = 0; j < mapSize; j++)
                {
                    playerButtons[i, j] = new Button();
                    playerButtons[i, j].Location = new Point(i * objectSize + objectSize, j * objectSize + objectSize);
                    playerButtons[i, j].Size = new Size(objectSize, objectSize);
                    if (playerField.objectType[i, j] == FieldObject.ObjectType.SHIP)
                    {
                        playerButtons[i, j].Image = FieldObject.images[(int)FieldObject.ObjectType.SHIP];
                        // for drawing player ships
                    }
                    else
                    {
                        playerButtons[i, j].Image = FieldObject.images[(int)playerField.objectType[i, j]];
                    }
                    this.parent.Controls.Add(playerButtons[i, j]);
                }
            }
        }
        private void DrawEnemyField()
        {
            for (short i = 0; i < mapSize; i++)
            {
                for (short j = 0; j < mapSize; j++)
                {
                    enemyButtons[i, j] = new Button();
                    enemyButtons[i, j].Location = new Point
                        (i * objectSize + 704 + objectSize,
                        j * objectSize + objectSize);
                    enemyButtons[i, j].Size = new Size(objectSize, objectSize);
                    if (enemyField.objectType[i, j] == FieldObject.ObjectType.SHIP)
                    {
                        enemyButtons[i, j].Image = FieldObject.images[(int)FieldObject.ObjectType.SHIP];
                        // for drawing enemy ships
                    }
                    else 
                    {
                        enemyButtons[i, j].Image = FieldObject.images[(int)enemyField.objectType[i, j]];
                    }
                    this.parent.Controls.Add(enemyButtons[i, j]);
                }
            }
        }
        /// <summary>
        /// Move Player
        /// </summary>
        /// <param name="button">button of move</param>
        /// <param name="y">coord Y</param>
        /// <param name="x">coord X</param>
        public void Move(ref Button[,] buttons, ref FieldObject fieldObject, short x, short y)
        {
            if (fieldObject.IsPossibleMove(x, y))
            {
                if (fieldObject.IsShip(x, y))
                {
                    fieldObject.SetDead(x, y);
                    fieldObject.count_of_ships--;
                    if (fieldObject.IsDeadShip(x, y))
                    {
                        fieldObject.SetDeadWholeShip(ref buttons, x, y);
                        MessageBox.Show("SHIP IS DEAD");
                    }
                    buttons[x, y].Image = FieldObject.images[(int)FieldObject.ObjectType.DEAD];
                }
                if (fieldObject.IsWave(x, y))
                {
                    fieldObject.SetFailure(x, y);
                    buttons[x, y].Image = FieldObject.images[(int)FieldObject.ObjectType.FAILURE];
                }
            }
            else
            {
                MessageBox.Show("Incorrected click...\nThis field is already used");
            }
        }


        // end of class
    }
}
