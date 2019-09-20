using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public static int traValue;
        public static bool  traValueBian = false;


        public Form3()
        {
            InitializeComponent();
           

        }
      
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // 初始化 
            traValue = trackBar1.Value;
            label4.Text = trackBar1.Value.ToString();         
                       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            traValueBian = true;
           
            this.Close();
          
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            trackBar1.Value = 1000 - Form1.shuDuJianGe;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }
    }
}
