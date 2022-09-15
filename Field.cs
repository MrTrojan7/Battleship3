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
                    myButtons[i, j].Image = FieldObject.images[(int)playerField.objectType[i, j]];
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
        public void MovePlayer(Button button, short y, short x)
        {
            if (IsPossibleMove(enemyMap, y, x))
            {
                if (enemyMap.IsShip(y, x))
                {
                    enemyMap.ToDead(y, x);
                    enemyMap.count_of_ships--;
                    button.Image = FieldObject.images[(int)FieldObject.ObjectType.DEAD];
                }
                if (enemyMap.IsWave(y, x))
                {
                    enemyMap.ToFailure(y, x);
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
        public void MoveEnemy(Button button, short y, short x)
        {
            if (IsPossibleMove(playerField, y, x))
            {
                if (playerField.IsShip(y, x))
                {
                    playerField.ToDead(y, x);
                    playerField.count_of_ships--;
                    button.Image = FieldObject.images[(int)FieldObject.ObjectType.DEAD];
                }
                if (playerField.IsWave(y, x))
                {
                    playerField.ToFailure(y, x);
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
        public bool IsPossibleMove(FieldObject fieldObject, short y, short x)
        {
            if (fieldObject.IsShip(y, x) || fieldObject.IsWave(y, x))
            { 
                return true; 
            }
            return false;
        }

        // end of class
    }
}
