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
        int width = 840;
        int hight = 1280;
        Form parent;
        private short posX = 0;
        private short posY = 0;

        public const int mapSize = 10;
        public int objectSize = 64;
        public string alphabet = "АБВГДЕЖЗИК";

        public Button[,] myButtons = new Button[mapSize, mapSize];
        public Button[,] enemyButtons = new Button[mapSize, mapSize];

        public FieldObject myMap = new FieldObject();
        public FieldObject enemyMap = new FieldObject();

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
                    myButtons[i, j].Image = FieldObject.images[(int)myMap.objectType[i, j]];
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
                    if (enemyMap.objectType[i, j] == FieldObject.ObjectType.SHIP)
                    {
                        enemyButtons[i, j].Image = FieldObject.images[(int)FieldObject.ObjectType.SHIP];
                    }
                    else 
                    {
                        enemyButtons[i, j].Image = FieldObject.images[(int)enemyMap.objectType[i, j]];
                    }
                    this.parent.Controls.Add(enemyButtons[i, j]);
                }
            }
        }
        
        public void MovePlayer(Button button, short y, short x)
        {
            if (IsPossibleMove(enemyMap, y, x))
            {
                if (enemyMap.IsShip(y, x))
                {
                    enemyMap.ToDead(y, x);
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
                MessageBox.Show("Incorrected click...\nYour fault");
            }
        }
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
