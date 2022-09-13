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
        public Battleship()
        {
            InitializeComponent();
            Field field = new Field(this);
        }
    }
}
