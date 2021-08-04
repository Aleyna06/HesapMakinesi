using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        ArrayList sayilar = new ArrayList();//işlemlerde kullanılan sayıları tutmak için bir liste oluşturuyoruz.
        ArrayList islemler = new ArrayList();//işlemleri tutmak için bir liste oluşturuyoruz.
        double sonuc=0;//işlem sonuçlarını tutacağımız değişkenimiz
        byte gecmis = 0;//işlem geçmişi için sayaç

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Visible = false;//burada ilk açılışta işlem geçmişini kapatıyoruz.
            label1.Text = "";
        }
        //Matematiksel fonksiyonlar
        void topla(object sayi)
        {
            sonuc = sonuc + Convert.ToDouble(sayi);
        }
        void cikar(object sayi)
        {
            sonuc = sonuc - Convert.ToDouble(sayi);
        }
        void carp(object sayi)
        {
            sonuc = sonuc * Convert.ToDouble(sayi);
        }
        void faktoriyel(object sayi)
        {
            for (int i = 1; i <= Convert.ToInt32(sayi); i++)
            {
                sonuc = i * sonuc;//faktoriyel hesaplama
            } 
        }
        void kare(object sayi)
        {
            sonuc += Math.Pow(Convert.ToDouble(sayi), 2);//kare alma işlemi üs alma fonksiyonu ile yapılıyor
        }
        void bol(object sayi)
        {
            sonuc = sonuc / Convert.ToDouble(sayi);
        }
        void mod(object sayi)
        {
            sonuc = sonuc % Convert.ToDouble(sayi);
        }
        void kuvvet(object sayi)
        {
            sonuc = Math.Pow(sonuc, Convert.ToDouble(sayi));//Üs alma işlemini kuvvet fonksiyonu ile yapıyoruz
        }
        void kok(object sayi)
        {
            sonuc = Math.Sqrt(Convert.ToDouble(sayi));
        }
        void hesapla()
        {
            sonuc = Convert.ToDouble(sayilar[0]);

            for (var sayac = 1; sayac < sayilar.Count; sayac++)
            {
                switch (islemler[sayac - 1].ToString())
                {
                    case "+":
                        topla(sayilar[sayac]);
                        break;
                    case "-":
                        cikar(sayilar[sayac]);
                        break;
                    case "/":
                        bol(sayilar[sayac]);
                        break;
                    case "%":
                        mod(sayilar[sayac]);
                        break;
                    case "^":
                        kuvvet(sayilar[sayac]);
                        break;
                    case "*":
                        carp(sayilar[sayac]);
                        break;
                    case "kok":
                        kok(sonuc);
                        break;
                    case "kare":
                        kare(sonuc);
                        break;
                }
            }
            textBox1.Text = sonuc.ToString();
        }
        //-------------------------------------------------------------------
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);//sadece sayı girdirme işlemi
            if(textBox1.Text.Length !=0)
            {
                if (e.KeyChar == (char)Keys.Escape)
                {
                    
                    textBox1.Text = "";//esc tuşuna basılırsa sayı girişini silecek
                }
                if (e.KeyChar == (char)Keys.Enter)
                {
                    gecmis++;
                    if(gecmis == 5)
                    {
                        gecmis = 0;
                        label1.Text = "";
                    }
                    else
                    {
                        label1.Text += textBox1.Text+Environment.NewLine;
                        sayilar.Add(Convert.ToDouble(textBox1.Text));
                        hesapla();//enter tuşuna basılırsa hesaplama işlemini yapacak
                        //işlem yapıldıktan sonra matematiksel değişkenlerimizi sıfırlıyoruz
                        sonuc = 0;
                        islemler.Clear();
                        sayilar.Clear();
                    }
                }
                //Ondalığa çevirme
              
                if (e.KeyChar == ',' || e.KeyChar == '.')
                {
                    bool noktaBulunmaDurumu = false;
                   for(var i =0; i<textBox1.Text.Length;i++)
                    {
                        if (textBox1.Text[i]==',')
                        {
                            noktaBulunmaDurumu = true;
                        }
                    }
                    if (!noktaBulunmaDurumu)
                    {
                        if(textBox1.Text.Length>0)
                        {
                            textBox1.Text += ',';
                        }
                        else
                        {
                            textBox1.Text = "0,";
                        }
                    }      
                }

                //-------------------------------------------------
                //İşlem yakalama ile ilgili kodlar
                if (textBox1.Text[textBox1.Text.Length - 1] != ',')//burada son karakterin . olarak bırakılmamasını sağlıyoruz
                {
                    if (e.KeyChar == '+')
                    {
                        sayilar.Add(Convert.ToDouble(textBox1.Text));//sayı girişindeki sayıyı, sayılar dizisine atıyoruz.
                        islemler.Add("+");//seçilen işlemi işlemler dizisine atıyoruz.
                        label1.Text += " " + textBox1.Text + " " + e.KeyChar;
                        textBox1.Text = "";
                    }
                    if (e.KeyChar == '-')
                    {
                        sayilar.Add(Convert.ToDouble(textBox1.Text));//sayı girişindeki sayıyı, sayılar dizisine atıyoruz.
                        islemler.Add("-");//seçilen işlemi işlemler dizisine atıyoruz.
                        label1.Text += " " + textBox1.Text + " " + e.KeyChar;
                        textBox1.Text = "";
                    }
                    if (e.KeyChar == '*')
                    {
                        sayilar.Add(Convert.ToDouble(textBox1.Text));//sayı girişindeki sayıyı, sayılar dizisine atıyoruz.
                        islemler.Add("*");//seçilen işlemi işlemler dizisine atıyoruz.
                        label1.Text += " " + textBox1.Text + " " + e.KeyChar;
                        textBox1.Text = "";
                    }
                    if (e.KeyChar == '/')
                    {
                        sayilar.Add(Convert.ToDouble(textBox1.Text));//sayı girişindeki sayıyı, sayılar dizisine atıyoruz.
                        islemler.Add("/");//seçilen işlemi işlemler dizisine atıyoruz.
                        label1.Text += " " + textBox1.Text + " " + e.KeyChar;
                        textBox1.Text = "";
                    }
                    if (e.KeyChar == '%')
                    {
                        sayilar.Add(Convert.ToDouble(textBox1.Text));//sayı girişindeki sayıyı, sayılar dizisine atıyoruz.
                        islemler.Add("%");//seçilen işlemi işlemler dizisine atıyoruz.
                        label1.Text += " " + textBox1.Text + " " + e.KeyChar;
                        textBox1.Text = "";
                    }
                    if (e.KeyChar == '^')
                    {
                        sayilar.Add(Convert.ToDouble(textBox1.Text));//sayı girişindeki sayıyı, sayılar dizisine atıyoruz.
                        islemler.Add('^');//seçilen işlemi işlemler dizisine atıyoruz.
                        label1.Text += " " + textBox1.Text + " " + e.KeyChar;
                        textBox1.Text = "";

                    }
                    if (e.KeyChar == '!')
                    {
                        if (textBox1.Text.Length > 0 && textBox1.Text[0] != '-')//faktöriyel için null olmaması ve 0 dan büyük olması gerek  
                        {
                            label1.Text += " " + textBox1.Text + " !";
                            faktoriyel(sonuc);
                            textBox1.Text = sonuc.ToString();
                            label1.Visible = true;
                        }

                    }
                }
                label1.Visible = true;
            }                   
        }
        //----------------------------------------------------------------
 
       //Butonlarla işlemler
        //Butonlarla sayı yazma
        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text += '1';
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text += '2';
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text += '3';
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += '4';
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text += '5';
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text += '6';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text += '7';
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text += '8';
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Text += '9';
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text += '0';
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //burada ondalıklı sayıya çeviriyoruz
            if(textBox1.Text =="")
            {
                textBox1.Text = "0.";
            }
            else
            {
                bool noktaBulunmaDurumu = false;
                for (var i = 0; i < textBox1.Text.Length; i++)
                {
                    if (textBox1.Text[i] == ',')
                    {
                        noktaBulunmaDurumu = true;
                    }
                }
                if (!noktaBulunmaDurumu)
                {
                    textBox1.Text += ',';
                }
            }
        }

        //----------------------------------------------------
        //Butonlarla işlem seçme 
        //İşlemler klavye vuruşu algılamalarıyla tamamen aynı. O yüzden kodları da aynı. Tek fark butona basılması.
        private void button4_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length>0)//textbox içinin null olmamasını sağlıyoruz
            {
                //sayı negatifse pozitif,pozitifse negatif yapma
                if (textBox1.Text[0] == '-')//burada ilk indekse erişiyoruz
                {
                    ArrayList metin = new ArrayList();
                    foreach(char karakter in textBox1.Text)
                    {
                        metin.Add(karakter);
                    }
                    for(byte i = 0;i<metin.Count;i++)
                    {
                        if(i>0)
                        {
                            textBox1.Text = "";
                            textBox1.Text += metin[i];
                        }
                    }
                }
                else
                {
                    textBox1.Text="-"+textBox1.Text;
                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            //eşittir butonu
            if(textBox1.Text.Length>0)
            {
                gecmis++;
                if (gecmis == 5)
                {
                    gecmis = 0;
                    label1.Text = "";
                }
                else
                {
                    label1.Text += textBox1.Text + Environment.NewLine;
                    sayilar.Add(Convert.ToDouble(textBox1.Text));
                    hesapla();//enter tuşuna basılırsa hesaplama işlemini yapacak
                              //işlem yapıldıktan sonra matematiksel değişkenlerimizi sıfırlıyoruz
                    sonuc = 0;
                    islemler.Clear();
                    sayilar.Clear();
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length>0)
            {
                sayilar.Add(Convert.ToDouble(textBox1.Text));//sayı girişindeki sayıyı, sayılar dizisine atıyoruz.
                islemler.Add("+");//seçilen işlemi işlemler dizisine atıyoruz.
                label1.Text += " " + textBox1.Text + " +";
                textBox1.Text = "";
                label1.Visible = true;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length>0)
            {
                sayilar.Add(Convert.ToDouble(textBox1.Text));//sayı girişindeki sayıyı, sayılar dizisine atıyoruz.
                islemler.Add("-");//seçilen işlemi işlemler dizisine atıyoruz.
                label1.Text += " " + textBox1.Text + " -";
                textBox1.Text = "";
                label1.Visible = true;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length>0)
            {
                sayilar.Add(Convert.ToDouble(textBox1.Text));//sayı girişindeki sayıyı, sayılar dizisine atıyoruz.
                islemler.Add("*");//seçilen işlemi işlemler dizisine atıyoruz.
                label1.Text += " " + textBox1.Text + " *";
                textBox1.Text = "";
                label1.Visible = true;
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length>0)
            {
                sayilar.Add(Convert.ToDouble(textBox1.Text));//sayı girişindeki sayıyı, sayılar dizisine atıyoruz.
                islemler.Add("/");//seçilen işlemi işlemler dizisine atıyoruz.
                label1.Text += " " + textBox1.Text + " /";
                textBox1.Text = "";
                label1.Visible = true;
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length>0)
            {
                sayilar.Add(Convert.ToDouble(textBox1.Text));//sayı girişindeki sayıyı, sayılar dizisine atıyoruz.
                islemler.Add("^");//seçilen işlemi işlemler dizisine atıyoruz.
                label1.Text += " " + textBox1.Text + " ^";
                textBox1.Text = "";
                label1.Visible = true;

            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length>0)
            {
                sayilar.Add(Convert.ToDouble(textBox1.Text));//sayı girişindeki sayıyı, sayılar dizisine atıyoruz.
                islemler.Add("%");//seçilen işlemi işlemler dizisine atıyoruz.
                label1.Text += " " + textBox1.Text + " %";
                textBox1.Text = "";
                label1.Visible = true;
            }
            
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                if (textBox1.Text[0] == '-')
                {
                    MessageBox.Show("Kök içi eksi (-) değer alamaz");
                }
                else
                {
                    //bu buton farklı sayılar alamayacağı için direk olarak hesapla fonksiyonunu çağırıyor
                    //kök içinin eksi durumda olması bizi ileri düzey mat konusuna yönelteceği için kök içinin eksi olmamasını sağlıyoruz
                    sayilar.Add(Convert.ToDouble(textBox1.Text));//sayı girişindeki sayıyı, sayılar dizisine atıyoruz.
                    islemler.Add("kok");//seçilen işlemi işlemler dizisine atıyoruz.
                    label1.Text += "Kök" + textBox1.Text;
                    hesapla();
                    label1.Visible = true;
                }

            }

        }

        private void button24_Click(object sender, EventArgs e)
        {
            sayilar.Add(Convert.ToDouble(textBox1.Text));//sayı girişindeki sayıyı, sayılar dizisine atıyoruz.
            islemler.Add("kare");//seçilen işlemi işlemler dizisine atıyoruz.
            label1.Text += "" + textBox1.Text+"";
            hesapla();
            label1.Visible = true;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length>0 && textBox1.Text[0]!='-')//faktöriyel için null olmaması ve 0 dan büyük olması gerek  
            {
                label1.Text += " " + textBox1.Text + " !" ;
                faktoriyel(sonuc);
                textBox1.Text = sonuc.ToString();
                label1.Visible = true;
            }
        }
    }
}
