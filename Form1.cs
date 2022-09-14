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
        public bool isWin = false;
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
                    ((Button)item).Click += PlayerClick; //приводим к типу и устанавливаем обработчик события  
                }
            }
        }

        private void PlayerClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            short x = (short)((button.Location.X - 704) / 64 - 1);
            short y = (short)(button.Location.Y / 64 - 1);
            field.MovePlayer(button, x, y);
            //if(!isShoot)

        }


    }
}
