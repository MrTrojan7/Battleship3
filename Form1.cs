using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship_3._0
{
    public partial class Battleship : Form
    {
        Field field;
        public bool isGame = true;
        public Battleship()
        {
            InitializeComponent();
            field = new Field(this);
            
        }

        private void Battleship_Load(object sender, EventArgs e)
        {
            foreach (var item in this.Controls) //обходим все элементы формы
            {
                if (item is Button) // проверяем, что это кнопка
                {
                    ((Button)item).Click += Click; //приводим к типу и устанавливаем обработчик события  
                }
            }
        }

        public void Click(object sender, EventArgs e)
        {
            if (isGame)
            {
                Button button = (Button)sender;
                short x;
                short y = (short)(button.Location.Y / 64 - 1);
                if (button.Location.X < 764) // Enemy move
                {
                    x = (short)(button.Location.X / 64 - 1);
                    field.MoveEnemy(button, x, y);
                }
                else // player move
                {
                    x = (short)((button.Location.X - 704) / 64 - 1);
                    field.MovePlayer(button, x, y);
                }
            }
        }

        public bool IsPlayerWin()
        {
            return field.enemyField.count_of_ships > 0;
        }
        public bool IsEnemyWin()
        {
            return field.playerField.count_of_ships > 0;
        }


    }
}
