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

        public Button[,] myButtons = new Button[mapSize, mapSize];
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
                    myButtons[i, j] = new Button();
                    myButtons[i, j].Location = new Point(i * objectSize + objectSize, j * objectSize + objectSize);
                    myButtons[i, j].Size = new Size(objectSize, objectSize);
                    if (playerField.objectType[i, j] == FieldObject.ObjectType.SHIP)
                    {
                        myButtons[i, j].Image = FieldObject.images[(int)FieldObject.ObjectType.SHIP];
                        // for drawing player ships
                    }
                    else
                    {
                        myButtons[i, j].Image = FieldObject.images[(int)playerField.objectType[i, j]];
                    }
                    this.parent.Controls.Add(myButtons[i, j]);
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
        public void MovePlayer(Button button, short x, short y)
        {
            if (enemyField.IsPossibleMove(x, y))
            {
                if (enemyField.IsShip(x, y))
                {
                    enemyField.SetDead(x, y);
                    enemyField.count_of_ships--;
                    if (enemyField.IsDeadShip(x, y))
                    {
                        MessageBox.Show("DEAD SHIP");
                    }
                    button.Image = FieldObject.images[(int)FieldObject.ObjectType.DEAD];
                }
                if (enemyField.IsWave(x, y))
                {
                    enemyField.SetFailure(x, y);
                    button.Image = FieldObject.images[(int)FieldObject.ObjectType.FAILURE];
                }
            }
            else
            {
                MessageBox.Show("Incorrected click...\nPLAYER fault");
            }
        }
        /// <summary>
        /// Move Enemy
        /// </summary>
        /// <param name="button"></param>
        /// <param name="y">coord Y</param>
        /// <param name="x">coord X</param>
        public void MoveEnemy(Button button, short x, short y)
        {
            if (playerField.IsPossibleMove(x, y))
            {
                if (playerField.IsShip(x, y))
                {
                    playerField.SetDead(x, y);
                    playerField.count_of_ships--;
                    if (playerField.IsDeadShip(x, y))
                    {
                        MessageBox.Show("DEAD SHIP");
                    }
                    button.Image = FieldObject.images[(int)FieldObject.ObjectType.DEAD];
                }
                if (playerField.IsWave(x, y))
                {
                    playerField.SetFailure(x, y);
                    button.Image = FieldObject.images[(int)FieldObject.ObjectType.FAILURE];
                }
            }
            else
            {
                MessageBox.Show("Incorrected click...\nENEMY fault");
            }
        }

        /// <summary>
        /// Is possible move
        /// </summary>
        /// <param name="fieldObject"></param>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <returns></returns>

        // end of class
    }
}
