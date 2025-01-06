using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCADANEO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ACTMULTILib.ActEasyIF plc = new ACTMULTILib.ActEasyIF();
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true; // Timer etkinleştiriliyor
            plc.ActLogicalStationNumber = 99; // PLC istasyon numarası ayarlanıyor
            plc.Open(); // PLC bağlantısı açılıyor

            int result = plc.SetDevice("M1541", 1); // PLC'de M1541 cihazı ayarlanıyor
            plc.SetDevice("M1541", 0); // M1541 cihazı sıfırlanıyor
            pictureBox19.BackColor = Color.Green;
            pictureBox20.Visible=false; 
            pictureBox19.Visible=true;
            label1.Visible = true;

            if (result == 0)
            {
                label2.ForeColor = Color.Red;
                pictureBox1.BackColor = Color.Red;
                label1.Text = "KÖNVEYÖR BAŞLADI";

            }
            else
            {
                label2.ForeColor = Color.Green;
                pictureBox1.BackColor = Color.Green;
                plc.Close(); // PLC bağlantısı kapatılıyor
                label1.Text = "KÖNVEYÖR BAŞLADI";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            plc.ActLogicalStationNumber = 99; // PLC istasyon numarası ayarlanıyor
            plc.Open(); // PLC bağlantısı açılıyor
            int veri1, veri1_oku, veri2, veri2_oku, veri3, veri3_oku, veri4, veri4_oku, veri5, veri5_oku, veri6, veri6_oku, veri7, veri7_oku, veri8, veri8_oku;
            veri1_oku = plc.ReadDeviceRandom("X16", 1, out (veri1)); // X16 cihazının değeri okunuyor (DC sensör)
            veri2_oku = plc.ReadDeviceRandom("Y13", 1, out (veri2)); // Y13 cihazının değeri okunuyor (kırmızı ışık)
            veri3_oku = plc.ReadDeviceRandom("Y11", 1, out (veri3)); // Y11 cihazının değeri okunuyor (yeşil ışık)
            veri4_oku = plc.ReadDeviceRandom("Y12", 1, out (veri4)); // Y12 cihazının değeri okunuyor (sarı ışık)
            veri5_oku = plc.ReadDeviceRandom("X4", 1, out (veri5)); // X4 cihazının değeri okunuyor (tutma)
            veri6_oku = plc.ReadDeviceRandom("X5", 1, out (veri6)); // X5 cihazının değeri okunuyor (vakum pistolu)
            veri7_oku = plc.ReadDeviceRandom("X6", 1, out (veri7)); // X6 cihazının değeri okunuyor (geri)
            veri8_oku = plc.ReadDeviceRandom("X7", 1, out (veri8)); // X7 cihazının değeri okunuyor (ileri)

            // Sensör kontrolü
            if (veri1 == 1)
            {
               pictureBox2.BackColor=Color.Red;
                pictureBox13.BackColor = Color.Green;
                pictureBox14.Visible=false;
            }
            else
            {
                pictureBox2.BackColor = Color.Red;
                pictureBox14.Visible = true;
                pictureBox13.Visible = false;
                pictureBox14.BackColor = Color.Red;
            }

            // Işıkların kontrolü
            if (veri2 == 1)
            {
                pictureBox3.BackColor=Color.Red;
                pictureBox4.Visible=false;
                pictureBox5.Visible=false;
                pictureBox12.BackColor=Color.Red;
                pictureBox12.Visible=true;
                pictureBox13.Visible=false;
                pictureBox14.Visible=false;
            }
            else if (veri4 == 1)
            {
                pictureBox4.BackColor = Color.Gold;
                pictureBox5.Visible=false;
                pictureBox4.Visible = true;
                pictureBox3.Visible = false;
                pictureBox11.BackColor=Color.Gold;
                pictureBox11.Visible=true;
                pictureBox12.Visible = false;
                pictureBox13.Visible=false;

            }
            else if (veri3 == 1)
            {
                pictureBox5.BackColor = Color.Green;
                pictureBox5.Visible = true;
                pictureBox4.Visible = false;
                pictureBox3.Visible = false;
                pictureBox10.BackColor=Color.Green;
                pictureBox10.Visible=true;
                pictureBox11.Visible=false;
               pictureBox12.Visible=false;

                // Pistollerin kontrolü
                if (veri5 == 1)
            {
                pictureBox6.BackColor = Color.Green;
                    pictureBox15.Visible = true;
                    pictureBox15.BackColor=Color.Green;
                
            }
            else
            {
                    pictureBox6.BackColor = Color.Red;
                    pictureBox15.BackColor=Color.Red;
                    pictureBox15.Visible=false;

            }

            // Vakum kontrolü
            if (veri6 == 1)
            {
                pictureBox7.BackColor = Color.Green;
                    pictureBox16.BackColor = Color.Green;
                    pictureBox16.Visible = true;
            }
            else
            {
                pictureBox7.BackColor = Color.Red;
                    pictureBox16.BackColor=Color.Red;
                    pictureBox16.Visible=false;
            }

            // İleri geri pistolu kontrolü
            if (veri7 == 1)
            {
                pictureBox8.BackColor = Color.Green;
                    pictureBox17.BackColor = Color.Green;
                    pictureBox17.Visible=true;
                    pictureBox18.Visible=false;
            }
            else
            {
              pictureBox8.BackColor = Color.Red;
                    pictureBox17.BackColor=Color.Red; pictureBox18.Visible=false;   
            }

            if (veri8 == 1)
            {
               pictureBox9.BackColor = Color.Green;
                    pictureBox18.BackColor=Color.Green;
                    pictureBox18.Visible=true;
                    pictureBox17.Visible = false;

            }
            else
            {

                    pictureBox9.BackColor = Color.Red;
                    pictureBox18.BackColor=Color.Red;
                    pictureBox18.Visible = true; pictureBox17.Visible=false;
                   
            }
        }
    }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true; // Timer etkinleştiriliyor
            plc.ActLogicalStationNumber = 99; // PLC istasyon numarası ayarlanıyor
            plc.Open(); // PLC bağlantısı açılıyor

            int result = plc.SetDevice("M1686", 1); // PLC'de M1686 cihazı ayarlanıyor
            plc.SetDevice("M1686", 0); // M1686 cihazı sıfırlanıyor
            label1.Visible=true;
            pictureBox20.BackColor = Color.Green;
            pictureBox20.Visible = true;
            pictureBox19.Visible = false;
            pictureBox1.BackColor = Color.Red; // Şeklin arka plan rengi kırmızı yapılıyor
            if (result == 0)
            {
                label1.Text = "KÖNVEYÖR DURDU"; // Label metni ayarlanıyor
                
            }

            plc.Close(); // PLC bağlantısı kapatılıyor
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Visible = false;
            label24.Visible = false;
        }
    }
    }

