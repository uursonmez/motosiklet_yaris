using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace motosiklet_yaris
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        //Motosikletins sağa, sola ve ortadaki pozisyonunu belirlemek için kullanılmaktadır.
        private void aracYerine()
        {
            if (seritSayisi==1)
            {
                redMot.Location = new Point(282,458);
            }

            else if (seritSayisi == 0)
            {
                redMot.Location = new Point(76,458);
            }

            else if (seritSayisi == 2)
            {
                redMot.Location = new Point(501, 458);
            }
        }
        int seritSayisi = 1, yol = 0, hiz = 50;
        Random rnd = new Random();

        class randomMot
        {
            public bool mot = false;
            public PictureBox sahteMot;
            public bool vakit = false;
        }

        randomMot[] rndMot = new randomMot[2];

        //Rastgele motosiklet getirmek için kullanılmaktadır.

        void randomMotGetir(PictureBox pb)
        {
            int rnd2 = rnd.Next(0, 5);

            switch (rnd2)
            {
                case 0:
                    pb.Image = Properties.Resources.mot0;
                    break;

                case 1:
                    pb.Image = Properties.Resources.mot1;
                    break;

                case 2:
                    pb.Image = Properties.Resources.mot2;
                    break;

                case 3:
                    pb.Image = Properties.Resources.mot3;
                    break;

                case 4:
                    pb.Image = Properties.Resources.mot4;
                    break;

                
            }
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        //Motosikletin sağa ve sola gitmesi için oluşturulan değişken.
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Right || e.KeyCode == Keys.D)
            {
                if (seritSayisi<2)
                {
                    seritSayisi++;
                }
            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                if (seritSayisi > 0)
                {
                    seritSayisi--;
                }
            }

            aracYerine();
        }
        //Oyun başladığında rastgele müzik atamak için kullanılmakatdır.
        private void randomMuzikEkle()
        {
            int muzikDeger=rnd.Next(1,4);

            axWindowsMediaPlayer1.URL = "muzik/ses" + muzikDeger.ToString() + ".mp3";
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        //Oyun başladığında müziğin başlamasını ve motosikletin pozisyonuda başlaması için kullanılmaktadır.
        private void Form1_Load(object sender, EventArgs e)
        {

            for (var i = 0; i < rndMot.Length; i++)
            {
                rndMot[i] = new randomMot();
               
            }
            rndMot[0].vakit = true;

            aracYerine();
            randomMuzikEkle();

            labelYuksekSkor.Text = Settings1.Default.yüksekSkor.ToString();
        }

        bool sesKontrol = true;
        //Çalan müziği sessize alıp tekrar çalması için kullanılmaktadır.
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (sesKontrol==true)
            {
                sesKontrol = false;
                axWindowsMediaPlayer1.Ctlcontrols.pause();
                pictureBox1.Image = Properties.Resources.seskapali;
            }

            else if (sesKontrol==false)
            {
                sesKontrol = true;
                axWindowsMediaPlayer1.Ctlcontrols.play();
                pictureBox1.Image = Properties.Resources.sesacik;
            }
        }

        //Random motosikletleri şeritlere atmak için kullanılmaktadır.
        private void timerRandomMot_Tick(object sender, EventArgs e)
        {
          

            for (int i = 0; i < rndMot.Length; i++)
            {
                if (!rndMot[i].mot && rndMot[i].vakit)
                {
                    rndMot[i].sahteMot = new PictureBox();
                    randomMotGetir(rndMot[i].sahteMot);
                    rndMot[i].sahteMot.Size = new Size(116, 185);
                    rndMot[i].sahteMot.Top = -rndMot[i].sahteMot.Height;

                    int sol_Yerles = rnd.Next(0, 3);

                    if (sol_Yerles == 0)
                    {

                        rndMot[i].sahteMot.Left = 76;

                    }
                    else if (sol_Yerles == 1)
                    {
                        rndMot[i].sahteMot.Left = 282;
                    }

                    else if (sol_Yerles == 2)
                    {
                        rndMot[i].sahteMot.Left = 501;
                    }


                    this.Controls.Add(rndMot[i].sahteMot);
                    rndMot[i].mot = true;

                }

                else
                {
                    if (rndMot[i].vakit)
                    {
                        rndMot[i].sahteMot.Top += 20;
                        if (rndMot[i].sahteMot.Top >= 154)
                        {
                            for (int j = 0; j < rndMot.Length; j++)
                            {
                                if (!rndMot[j].vakit)
                                {
                                    rndMot[j].vakit = true;
                                    break;
                                }
                            }
                        }
                        if (rndMot[i].sahteMot.Top >= this.Height - 20)
                        {
                            rndMot[i].sahteMot.Dispose();
                            rndMot[i].mot = false;
                            rndMot[i].vakit = false;
                        }
                    }

                }

                //Kaza durumu
                if (rndMot[i].vakit)
                {
                    float mutlakX = Math.Abs((redMot.Left + (redMot.Width/2)) -(rndMot[i].sahteMot.Left +(rndMot[i].sahteMot.Width/2)));
                    float mutlakY = Math.Abs((redMot.Top + (redMot.Height/2)) - (rndMot[i].sahteMot.Top+(rndMot[i].sahteMot.Height/2)));

                    float farkGenislik = (redMot.Width / 2) + (rndMot[i].sahteMot.Width / 2);
                    float farkYükseklik = (redMot.Height/2) + (rndMot[i].sahteMot.Height / 2);

                    if ((farkGenislik>mutlakX) && (farkYükseklik>mutlakY))
                    {
                        timerRandomMot.Enabled = false;
                        timerSerit.Enabled = false;
                        axWindowsMediaPlayer1.Ctlcontrols.pause();
                        axWindowsMediaPlayer1.URL = "muzik/kaza.mp3";
                        axWindowsMediaPlayer1.Ctlcontrols.play();

                        if (yol>Settings1.Default.yüksekSkor)
                        {
                            MessageBox.Show("Yeni Yüksek Skor !!! " + yol.ToString() + "m", "", MessageBoxButtons.OK, MessageBoxIcon.Information);                        
                            Settings1.Default.yüksekSkor = yol;
                            Settings1.Default.Save();
                        }

                        DialogResult dr = MessageBox.Show ("Oyun Bitti! Tekrar Deneyin!","Warning",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                        if (dr == DialogResult.Yes)
                        {
                            aracYerine();

                            for (int j = 0; j < rndMot.Length; j++)
                            {
                                rndMot[j].sahteMot.Dispose();
                                rndMot[j].mot = false;
                                rndMot[j].vakit = false;
                            }

                            yol = 0;
                            hiz = 70;
                            rndMot[0].vakit = true;
                            timerRandomMot.Enabled = true;
                            timerRandomMot.Interval = 200;

                            timerSerit.Enabled = true;
                            timerSerit.Interval = 200;

                            randomMuzikEkle();
                            axWindowsMediaPlayer1.Ctlcontrols.play();

                            labelYuksekSkor.Text = Settings1.Default.yüksekSkor.ToString();
                        }
                        else
                        {
                            this.Close();
                        }
                    }


                }



            }

        }

        bool seritHareket = false;

       

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        //Motosiklet yol kat ettikçe seviye artması için kullanılmaktadır.
        void hizLevel()
        {
            //2.Seviye
            if (yol>150 && yol<300)
            {
                hiz = 70;
                timerSerit.Interval = 125;
                timerRandomMot.Interval = 100;
            }

            //3.Seviye
            else if (yol > 300 && yol < 450)
            {
                hiz = 100;
                timerSerit.Interval = 100;
                timerRandomMot.Interval = 90;
            }

            //4.Seviye

            else if (yol > 450 && yol < 700)
            {
                hiz = 150;
                timerSerit.Interval = 80;
                timerRandomMot.Interval = 70;
            }

            //5.Seviye
            else if (yol > 700)
            {
                hiz = 200;
                timerSerit.Interval = 60;
                timerRandomMot.Interval = 40;
            }

        }

        //Şeritleri haraket ettirerek yolu ilertlekmek için kullanılmaktadır.
        private void timerSerit_Tick(object sender, EventArgs e)
        {

            yol++;
            hizLevel();
            if (seritHareket==false)
            {
                for (int i = 1; i < 6; i++)
                {
                    this.Controls.Find("labelSolSerit" + i.ToString(), true)[0].Top -= 25;
                    this.Controls.Find("labelSagSerit" + i.ToString(), true)[0].Top -= 25;
                    seritHareket = true;
                }
            }

            else
            {
                for (int i = 1; i < 6; i++)
                {
                    this.Controls.Find("labelSolSerit" + i.ToString(), true)[0].Top += 25;
                    this.Controls.Find("labelSagSerit" + i.ToString(), true)[0].Top += 25;
                    seritHareket = false;
                }
            }

            labelYol.Text = yol.ToString()+"m";
            labelHiz.Text = hiz.ToString() + "km/h";
        }
    }
}
