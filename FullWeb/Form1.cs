using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace FullWeb
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {   
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.Location = new Point( -500, 100 );

            int posX = 0;
            int posY = 0;

            string URL = "";

            try
            {
                FileStream fs = new FileStream("config.ini", FileMode.Open);
                StreamReader sr = new StreamReader(fs);

                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("POSITION-X"))
                    {
                        string value = null;
                        value = sr.ReadLine();
                        posX = Int32.Parse(value);
                    }
                    if (line.Contains("POSITION-Y"))
                    {
                        string value = null;
                        value = sr.ReadLine();
                        posY = Int32.Parse(value);
                    }
                    if (line.Contains("URL"))
                    {
                        string value = null;
                        value = sr.ReadLine();
                        URL = value;
                    }
                }

            }
            catch (Exception ex)
            {
                //만들기..
                try
                {
                    FileStream fs = new FileStream("config.ini", FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine("[POSITION-X]");
                    sw.WriteLine("0");
                    sw.WriteLine("[POSITION-Y]");
                    sw.WriteLine("0");
                    sw.WriteLine("[URL]");
                    sw.WriteLine("");
                    sw.Close();
                    fs.Close();
                    MessageBox.Show("설정파일이 생성되었습니다. 설정값을 확인해주십시오.");
                    this.Close();
                }
                catch (Exception ex2)
                {
                    MessageBox.Show("설정파일을 생성하지 못하였습니다. \r\n[config.ini] 파일을 확인해 주십시오.");
                    this.Close();
                }
            }

            this.Location = new Point( posX, posY );
            
            
            webControl.Navigate( URL );
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {   
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {   
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void webControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
