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
            InitField();
        }

        private void InitField() // initialization buttons of field
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    myButtons[i, j] = new Button();
                    myButtons[i, j].Location = new Point(i * objectSize + objectSize, j * objectSize + objectSize);
                    myButtons[i, j].Size = new Size(objectSize, objectSize);
                    myButtons[i, j].Image = FieldObject.images[(int)myMap.field[i, j]];
                    this.parent.Controls.Add(myButtons[i, j]);
                    enemyButtons[i, j] = new Button();
                    enemyButtons[i, j].Location = new Point
                        (i * objectSize + parent.Width / 2 + objectSize,
                        j * objectSize + objectSize);
                    enemyButtons[i, j].Size = new Size(objectSize, objectSize);
                    enemyButtons[i, j].Image = FieldObject.images[(int)enemyMap.field[i, j]];
                    this.parent.Controls.Add(enemyButtons[i, j]);
                }
            }
        }
	}
}
