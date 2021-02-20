using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class TimeDownForm1 : Form
    {
        int time = 60 * 25;
        bool pbar = true;
        bool SleepFlag = true; //Перменная отслеживающая уход в сон (без нее вечный цикл сна)
        double MaxTime;
        public TimeDownForm1()
        {
            InitializeComponent();
            label1.Text = ("Осталось     " + time / 60 + ":" + (time % 60));
        }

        private void button1_Click(object sender, EventArgs e) //Кнопка запуска
        {  
            //Делаем таймер доступным
            Down.Enabled = true;
            //Запускаем таймер
            Down.Start();
            button1.Visible = false;
            button2.Visible = true;
            SleepFlag = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Down.Stop();
            time = Int32.Parse(textHour.Text) * 3600 + Int32.Parse(textMinut.Text) * 60;
            pbar = true;
            button1.Visible = true;
            button2.Visible = false;
        }

     private void timer1_Tick(object sender, EventArgs e)
        {
            if (pbar) {
                MaxTime = time/100;
                pbar = false;
            }
            time--;
            label1.Text = ("Осталось     " + time/60 + ":"+ (time % 60));
            if (time ==0)
            {
                if (this.Shutdown.Checked==true)
                {
                    Process.Start("shutdown", "-s -f -t 0");
                }
                if (this.Sleep.Checked == true && SleepFlag==true)
                {
                    SleepFlag = false;
                    Down.Stop();
                    System.Diagnostics.Process.Start("rundll32.exe", "powrprof.dll, SetSuspendState 0,1,0");//Отправка компьютера в режим сна
                }

            }
        }
        private void textHour_TextChanged(object sender, EventArgs e)
        {
            try
            {
                time = Int32.Parse(textHour.Text) * 360 + Int32.Parse(textMinut.Text) * 60;
            }
            catch
            {
                time =Int32.Parse(textMinut.Text) * 60;
                textHour.Text = "0";
            }
        }


        private void textMinut_TextChanged(object sender, EventArgs e)
        {
            try
            {
                time = Int32.Parse(textHour.Text) * 3600 + Int32.Parse(textMinut.Text) * 60;
            }
            catch
            {
                time = Int32.Parse(textHour.Text) * 3600 + 0;
                textMinut.Text ="0";
            }
            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textMinut.Text = (trackBar1.Value).ToString();
            time = Int32.Parse(textHour.Text)*3600 + trackBar1.Value*60;
            label1.Text = ("Осталось     " + time / 60 + ":" + (time % 60));
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            textHour.Text = (trackBar2.Value).ToString();
            time = trackBar2.Value*3600 + Int32.Parse(textMinut.Text)*60;
            label1.Text = ("Осталось     " + time / 60 + ":" + (time % 60));
        }

        private void Loop_Click(object sender, EventArgs e)
        {
            Process.Start("Magnify.exe");
        }

    }
}
