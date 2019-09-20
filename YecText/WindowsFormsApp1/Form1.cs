using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // richTextBox1 属性设置
            //初始化panel与richTextBox1控件，使之形状长宽相等
            richTextBox1.Location = this.panel1.Location;
            richTextBox1.Width = this.panel1.Width;
            richTextBox1.Height = this.panel1.Height;

            richTextBox1.EnableAutoDragDrop = true;  //设置允许拖放
         
            timer2.Interval = 10;
            timer2.Enabled = true;

            jiShuHang = 0;

        }


        public static int shuDuJianGe ;   //时间间隔调节   
        private int zonghangshu;
        private int index;
        private int line;
        private int yiDongLine;
        private int jiShuHang ;
        private int col;
        public static bool flag_Daodi = false;
        public static bool flag_Qingling = false;
        public static bool flag_ChongQi = false;
        public static bool flag_QiTa = false;
        public int suoYin_jiShuHang;
        public int suoYin_zongShuHang;



        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {
          

        }

        public void ChongQi()
        {
            flag_ChongQi = true;
        }

        public void QiTa()
        {
            flag_QiTa = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

      
        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Dispose();
            flag_Qingling = true;
           
            // 计数行复位
            jiShuHang = 0;
          
            // 索引复位
            suoYin_jiShuHang = this.richTextBox1.GetFirstCharIndexFromLine(jiShuHang);

            // 光标 复位
            int Line = this.richTextBox1.GetFirstCharIndexFromLine(0);  //获取指定行号的第 1 行所在的第一个字符索引
            this. richTextBox1.SelectionStart = Line;   // 设置要移动到的点为获取指定行的第一个字符索引  
            this.richTextBox1.ScrollToCaret();   // 光标移动到设定点
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //字体设置
            fontDialog1.ShowDialog();   
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }



        private void button10_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            richTextBox1.BackColor = colorDialog1.Color;
        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {
            richTextBox1.Font = fontDialog1.Font;
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
           
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string FileExtension = System.IO.Path.GetExtension(openFileDialog1.FileName);
            if ( FileExtension == ".txt" )
            {
                StreamReader str = new StreamReader(openFileDialog1.FileName, Encoding.Default);
                richTextBox1.Text = str.ReadToEnd();
                str.Close();

            }
            else if (FileExtension == ".rtf")
            {
                richTextBox1.LoadFile(openFileDialog1.FileName);
            }


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (suoYin_jiShuHang < suoYin_zongShuHang)
            {
                jiShuHang++;  //计数行加1
                              //配置计数的行第一个字符的索引
                suoYin_jiShuHang = this.richTextBox1.GetFirstCharIndexFromLine(jiShuHang);   //获取指定行号的第  jiShuHang  行所在的第一个字符索引

                yiDongLine = suoYin_jiShuHang ;
                richTextBox1.SelectionStart = yiDongLine ;   // 设置要移动到的索引点为获取指定行的第一个字符索引  
                richTextBox1.ScrollToCaret();   // 光标移动到设定的索引点

            }
            else
            {
                timer1.Enabled = false;
                Form2 form2 = new Form2();
                form2.ShowDialog();
            }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer1.Interval = shuDuJianGe;

            if (flag_Qingling == true)
            {
                jiShuHang = 0;
                flag_Qingling = false;
            }

            if (Form3.traValueBian == true)
            {
                shuDuJianGe = 1001 - Form3.traValue;
                Form3.traValueBian = false;
            }

            if ( flag_ChongQi == true )
            {
                flag_ChongQi = false;
                timer1.Dispose();
                button3_Click(new object(), new EventArgs());
                timer1.Start();
              
            }

            if ( flag_QiTa == true )
            {
                flag_QiTa = false;
                timer1.Dispose();
                button5_Click_1(new object(), new EventArgs());
                button3_Click(new object(), new EventArgs());

            }

            index = richTextBox1.GetFirstCharIndexOfCurrentLine();   //光标行第一个字符索引
            line = richTextBox1.GetLineFromCharIndex(index) + 1;    //光标行的行号，从0开始，所以加1
            col = richTextBox1.SelectionStart - index + 1;    //光标列的列号，从0开始，所以加1
            zonghangshu = richTextBox1.GetLineFromCharIndex(richTextBox1.Text.Length) + 1;  //总行数

            suoYin_zongShuHang = richTextBox1.GetFirstCharIndexFromLine(zonghangshu - 1);   //获取指定行号的第  zonghangshu  行所在的第一个字符索引

            // 功能提示
            {
                //    System.IO.Path.GetExtension(openFileDialog1.FileName)  ;  获取文件的拓展名
                //    System.IO.Path.GetFileName(openFileDialog1.FileName)  ;  获取文件的文件名和拓展名
                //    System.IO.Path.GetFullPath(openFileDialog1.FileName)  ;  获取文件的绝对路径

                //  常用设计
                //  换行   Environment.NewLine 
                //  "  richTextBox1.Size " + richTextBox1.Size 获取控件的（宽，高）
                //  "  控件高： " + richTextBox1.Height  获取控件的 高 
                //  "  richTextBox1. " +    richTextBox1.TextLength  获取文本框内所有文档的总字数
                //  "  richTextBox1. =  " + richTextBox1.WordWrap  设置是否多行切换
                //  "  richTextBox1. =  " + richTextBox1.ZoomFactor  获取或设置当前控件的缩放级别
                //   richTextBox1.UseWaitCursor = true / false;  设置是否将  等待光标  用于当前控件，并实现等待功能
                //   richTextBox1.AcceptsTab = true / false;  设置是否将用  Tab选择键  用于当前控件,false 实现缩进功能，true 实现选择功能，默认为 false
                //   richTextBox1.AllowDrop = true / false;  设置是否允许拖放
                //   richTextBox1.AutoWordSelection = false;   设置是否使用 自动选词功能  
                //   richTextBox1.BackColor = Color.Blue;  设置当前空间的背景色 （如蓝色），可以实现背景光，护眼光 主题 等功能
                //   richTextBox1.BorderStyle = BorderStyle.Fixed3D;  设置或获取当前控件的 边框类型  
                //   richTextBox1.Bottom ; 获取当前控件的 距离容器底部的距离
                //   richTextBox1.Bounds   获取当前控件的 距离父容器相对位置 和 边沿的距离
                //   richTextBox1.Clear();  清空当前控件的  所有文件内容
                //   richTextBox1.ClearUndo();  清空当前控件的  撤销缓冲区，重新生成撤销列表 ，可以用于保存的后续操作
                //   this.AutoScroll  设置当前控件  是否自动添加滚动条
                //   richTextBox1.AutoScrollOffset = new Point(100, 200);  获取或设置当前控件 滚动条 自动滚动到的位置  结合 this.ScrollControlIntoView(子控件名); 用以移动子控件
                //   richTextBox1.Select()  设置空间文本选择范围 richTextBox1.Select (int start,int length);
                //   richTextBox1.SelectAll()  设置选择全部文本
                //   richTextBox1.ScrollToCaret () ; 设置将空间内容滚动到插入符号的位置
                //   richTextBox1.ScrollBars = RichTextBoxScrollBars.Both;  设置控件的  滚动条显示样式
                //   richTextBox1.SelectedText = "内容 "；  以保留原格式的形式  获取或设置  文本框内  多行中  选定的内容为 txt 格式内容,可进行多行 txt 文本获取 ， 可用以 收索替换,对齐格式设置 等功能
                //   richTextBox1.SelectionAlignment = HorizontalAlignment.Center;  设置或获取 控件 选定的文本的对齐方式 
                //   richTextBox1.SelectedRtf = "内容 "；  获取或设置  文本框内某行中选定的内容为 rtf 格式内容， 结合 richTextBox1.SelectAll() 可以进行格式转化
                //   richTextBox1.SelectionBackColor = Color.Red;   获取或设置  文本框内某行中选定的内容的背景色， 可用以 涂画式进行 高亮，标记 等功能
                //   richTextBox1.ClientRectangle ; 获取控件 工作区的矩形 位置及大小
                //   richTextBox1.ClientSize = new Size(Width, Height);  获取或设置  控件文本框矩形工作区 的 （宽度，高度）
                //   richTextBox1.ClientSize = new Size(200, 300);
                //   ContextMenu 控件用以 设置右键菜单
                //   richTextBox1.Copy();  在文本框中  复制  所选内容到剪贴板 ， 可实现复制功能 ，结合 richTextBox1.SelectedText 或  richTextBox1.SelectedRtf 使用
                //   richTextBox1.Cursor = Cursors.Cross;   获取或设置 当鼠标位于控件中时显示的 光标
                //   richTextBox1.Cut();   在文本框中  剪切  所选内容到剪贴板 ， 可实现剪切功能 ，结合 richTextBox1.SelectedText 或  richTextBox1.SelectedRtf 使用
                //   richTextBox1.DisplayRectangle ;  获取控件 显示区域的矩形 位置及大小
                //   richTextBox1.AllowDrop = true / false ;  设置或获取 控件是否 允许拖放   
                //   richTextBox1.EnableAutoDragDrop = true / false ; 设置或获取 控件是否 允许 文本，图片,或其他数据 等内容 拖放   
                //   richTextBox1.Enabled = true / false ; 设置或获取 控件是否 允许 用户交互
                //   richTextBox1.Find( string str);  设置在控件 文本内搜索 相应字符串
                //   richTextBox1.FindForm();  设置检索  控件  所在的窗体 
                //   richTextBox1.Focus();  设置  控件  输入焦点
                //   richTextBox1.Focused ;  获取  控件是否 有输入焦点, bool
                //   richTextBox1.Font ;  设置或获取 控件 显示文字的字体，结合 FontDialog控件 使用
                //   richTextBox1.ForeColor ; 设置或获取 控件 的前景色，可以设置字体的颜色
                //   richTextBox1.LoadFile(string path);  读取 rtf 或  Ascll 文本文件到 richTextBox
                //   richTextBox1.Location ;  设置或获取 控件 左上角坐标相对其容器 左上角的坐标
                //   richTextBox1.Multiline ; (bool) 设置或获取 控件 是否可以 支持多行
                //   richTextBox1.Paste();   在文本框中  粘贴  所选内容到剪贴板 ， 可实现粘贴功能 ，结合 richTextBox1.SelectedText 或  richTextBox1.SelectedRtf 使用
                //   richTextBox1.Redo();   重新应用上一步撤销的 操作
                //   richTextBox1.Refresh();  刷新 控件及子控件
                //   richTextBox1.SaveFile(string path);   保存 richTextBox中的  rtf 或  Ascll  文本文件
                //   richTextBox1.Scale() ;   设置 控件及子控件的缩放
                //   richTextBox1.SelectionColor ;   设置或获取 控件 选定文本的字体颜色
                //   richTextBox1.SelectionBullet = (bool);   设置或获取  是否将项目样式符号 用以当前插入点或所选内容
                //   richTextBox1.SelectionFont ;    设置或获取 控件 选定文本的字体
                //   richTextBox1.SelectionHangingIndent ;  设置或获取 控件 选定文本 与 后文段落 的 相对缩进
                //   richTextBox1.SelectionIndent ；  设置或获取 控件 选定文本开始位置 行 的 缩进
                //   richTextBox1.SelectionLength ；  设置或获取 控件 选定文本 字符数目
                //   richTextBox1.SelectionProtected = (bool);   设置或获取 控件 选定文本 是否被保护 
                //   richTextBox1.SelectionRightIndent ;  设置或获取 控件 插入点 选定文本 的 右边缩进
                //   richTextBox1.SelectionStart ;  设置或获取 控件 选定文本 的 开始点
                //   richTextBox1.SelectionType  ;  设置或获取 控件 选定内容 的 类型
                //   richTextBox1.Size ;  设置或获取 控件 的 宽度与高度
                //   richTextBox1.TabStop = (bool) ;  设置或获取 控件 是否支持 Tab 键焦点 选择停靠
                //   richTextBox1.TextLength  ;  获取 控件 文本框内文本的总长度，空格也包括在内
                //   richTextBox1.Undo  ;   撤消文本框的 上一个编辑操作
                //   richTextBox1.UndoActionName  ;  获取 撤消文本框的 上一个编辑操作 的操作名称
                //   richTextBox1.UseWaitCursor = (bool) ;  设置或获取 控件及子控件 是否使用 等待光标
                //   richTextBox1.Update() ;  刷新  重绘 控件无效区域
                //   richTextBox1.Visible  = (bool) ;  设置或获取 控件及子控件 是否 可见
                //   richTextBox1.Width  ;  设置或获取 控件 的 宽度
                //   richTextBox1.WordWrap = (bool) ;  设置或获取 多行控件 是否在必要时 才 换行
                //   richTextBox1.ZoomFactor  ;  设置或获取 控件 当前控件 的 缩放级别 (缩放因子)
                //   richTextBox1.GetFirstCharIndexFromLine( 行号 ) ;  检索 获取 给定行 的第一个字符的索引
                //   richTextBox1.GetCharIndexFromPosition( 位置点 ) ;  检索 获取 给定距离 位置点 最近的第一个字符的索引
                //   richTextBox1.GetFirstCharIndexOfCurrentLine();   检索 获取 当前行 的第一个字符的索引

            }

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            openFileDialog1.ShowDialog();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0); //退出程序
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            shuDuJianGe = 200;

            timerZ.Elapsed += new System.Timers.ElapsedEventHandler(ZongShi);
            timerZ.Enabled = true;   //是否触发Elapsed事件
            timerZ.AutoReset = true;   //每到指定时间Elapsed事件是触发一次（false），还是一直触发（true）
            timerZ.Interval = 100;  // 设置时间间隔为  0.1 秒
            timerZ.Enabled = true;
            timerZ.Start();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            timer1.Dispose();
            richTextBox1.Clear();
        }

        

        System.Timers.Timer timerZ = new System.Timers.Timer();
        
        public void ZongShi(object sender, System.Timers.ElapsedEventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;

            int jiShu = jiShuHang + 1;

            //  设置状态提示框
            int yiDongShuDu = 1000 - shuDuJianGe;

            //现实系统时间
            label2.Text = "时间： " + DateTime.Now.ToString();


            label1.Text = "行号: " + line
                         + "  列号：" + col
                         + "  文件长度：  " + richTextBox1.TextLength
                         + "    缩放比例：" + richTextBox1.ZoomFactor * 100 + " %"
                         + "  总行数: " + zonghangshu
                         + "  计数行：" + jiShu
                         + Environment.NewLine
                         + "移动速度：" + yiDongShuDu
                         + "  suoYin_jiShuHang：" + suoYin_jiShuHang.ToString()
                         + "  suoYin_zongShuHang：" + suoYin_zongShuHang.ToString()
                         + "  shuDuJianGe：" + shuDuJianGe
                         // + "   文件拓展名：" + System.IO.Path.GetExtension(openFileDialog1.FileName)
                         + "   文件名：" + System.IO.Path.GetFileName(openFileDialog1.FileName)
                         + "   index：" + index

                       ;

        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            System.Environment.Exit(0); //退出程序
        }
    }
}
