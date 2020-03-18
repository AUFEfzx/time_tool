using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace small_tool
{
    public partial class Form2 : Form
    {
        int x = 0, y = 0, flag = 0;
        int pos_x, pos_y;
        Label l_form1 = new Label();
        Label l_time = new Label();
        public Form2(Label l1,Label l2,int x_pos,int y_pos)
        {
            l_form1 = l1;
            l_time = l2;
            pos_x = x_pos - 420;
            pos_y = y_pos + 240;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Opacity = 0.5;
            this.Left = pos_x;
            this.Top = pos_y;
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            flag = 0;
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (flag == 1)
            {
                this.Left += e.X - x;
                this.Top += e.Y - y;
                this.Refresh();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            l_time.Text = textBox2.Text;
            l_form1.Text = textBox1.Text;
            this.Hide();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            flag = 1;
            x = e.X;
            y = e.Y;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
