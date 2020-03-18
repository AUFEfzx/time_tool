using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace small_tool
{
    public partial class Form1 : Form
    {
        int x = 0, y = 0, flag = 0;
        int finish = 0, atelic = 100;
        int my_time=0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Left = 1250;
            this.Top = 5;
            this.Opacity = 0.5;
            this.Refresh();
            chart1.Series[0].Points.AddY(finish);
            chart1.Series[0].Points.AddY(atelic - finish);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            flag = 0;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            finish++;
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddY(finish);
            chart1.Series[0].Points.AddY(atelic - finish);
            if (finish==atelic)
            {
                timer1.Enabled = false;
                MessageBox.Show("恭喜你完成任务！！！");
                insert_plan(label2.Text, finish/60);
                finish = 0;
                chart1.Series[0].Points.Clear();
                chart1.Series[0].Points.AddY(finish);
                chart1.Series[0].Points.AddY(atelic - finish);
                button3.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (label2.Width <= this.Width)
                {
                    label2.Left = (this.Width - label2.Width) / 2;
                }
                else
                    label2.Left = 0;
                my_time = Convert.ToInt32(label1.Text);
                timer1.Enabled = true;
                atelic = my_time * 60;
                button3.Hide();
            }
            catch
            {
                MessageBox.Show("添加的时间不符合规范，请重新添加！");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled==true)
            {
                MessageBox.Show("当前任务未完成！");
                return;
            }
            Form2 f2 = new Form2(this.label2,this.label1,this.Left,this.Top);
            f2.Show();
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
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            flag = 1;
            x = e.X;
            y = e.Y;
        }
        public void insert_plan(string plan,int times)
        {
            DateTime dt = DateTime.Now;
            string day = dt.ToShortDateString().ToString();
            day =day.Replace('/','-');
            string conn= "server=36.26.66.176;database=myplan;Character Set=utf8;Uid=myplan;password=myplan";
            string sql = String.Format("insert into fzx_plan(plan, times,day) values('{0}',{1},'{2}')", plan, times, day);
            MySqlConnection myconn = new MySqlConnection(conn);
            try
            {
                myconn.Open();
                MySqlCommand myCmd = new MySqlCommand(sql, myconn);
                if (myCmd.ExecuteNonQuery() > 0)
                {
                    //MessageBox.Show("数据插入成功！");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
