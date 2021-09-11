using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace alvinBreadShopApplication
{
    public partial class Form1 : Form
    {
        alvinBreads myBread;

        List<alvinBreads> listBread = new List<alvinBreads>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //non aktifkan group box produksi dan penjualan
            groupBoxMade.Enabled = false;
            groupBoxSell.Enabled = false;
        }

        //method menampilkan data roti
        private void TampilData()
        {
            foreach (alvinBreads bread in listBread)
            {
                //menampilkan data roti
                listBoxData.Items.Add("Variant Name : " + bread.Name);
                listBoxData.Items.Add("Price per pcs : " + bread.Price);
                listBoxData.Items.Add("Stock : " + bread.Stock);

                //menambah 1 baris kosong 
                listBoxData.Items.Add("");
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //membuat roti baru dengan parameterized constructor
                myBread = new alvinBreads(textBoxName.Text, int.Parse(textBoxPrice.Text));

                /*//mengisi data roti
                myBread.Name = textBoxName.Text;
                myBread.Price = int.Parse(textBoxPrice.Text);*/

                //simpan atau tambahkan objek roti baru ke dalam list
                listBread.Add(myBread);

                //kosongi dulu listBox
                listBoxData.Items.Clear();

                TampilData();

                //kosongi inputan user
                textBoxName.Text = "";
                textBoxPrice.Text = "";
                textBoxName.Focus();

                //groupbox produksi dan penjualan diaktifkan
                groupBoxMade.Enabled = true;
                groupBoxSell.Enabled = true;

                //tambahkan variant name roti baru ke combobox yg ada
                comboBoxVariantMade.Items.Add(myBread.Name);
                comboBoxVariantSell.Items.Add(myBread.Name);

                //atur agar user tidak bisa mengetik di combobox
                comboBoxVariantMade.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBoxVariantSell.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonClearData_Click(object sender, EventArgs e)
        {
            //menghapus data yang muncul di listbox
            listBoxData.Items.Clear();
        }

        private void buttonDisplayData_Click(object sender, EventArgs e)
        {
            //menampilkan data roti
            TampilData();
        }

        private void buttonMade_Click(object sender, EventArgs e)
        {
            try
            {
                //membuat roti yang dipilih user dari combobox
                //cari di dalam listBread
                foreach (alvinBreads bread in listBread)
                {
                    //cek apakah nama roti sama dengan yang dipilih user di combobox
                    if (bread.Name == comboBoxVariantMade.Text)
                    {
                        //membuat roti
                        //memanggil method production dengan parameter
                        bread.Production(int.Parse(textBoxQuantityMade.Text));

                        //tampilkan quantity yang diproduksi
                        listBoxData.Items.Add("Bread Name : " + bread.Name);
                        listBoxData.Items.Add("Quantity to Made : " + textBoxQuantityMade.Text);

                        //tampilkan data
                        TampilData();
                    }
                }
                //mengosongi textbox quantity made
                textBoxQuantityMade.Text = "";
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void buttonSell_Click(object sender, EventArgs e)
        {
            try
            {
                //cari di dalam listBread
                foreach (alvinBreads bread in listBread)
                {
                    if (bread.Name == comboBoxVariantSell.Text)
                    {
                        //panggil method sell
                        int totalPayment = bread.Sell(int.Parse(textBoxQuantitySell.Text), comboBoxPayment.Text);

                        //mengisi di listbox
                        listBoxData.Items.Add("SELL THE BREADS");
                        listBoxData.Items.Add("Bread Name : " + bread.Name);
                        listBoxData.Items.Add("Quantity to sell : " + textBoxQuantitySell.Text);
                        listBoxData.Items.Add("Payment Method : " + comboBoxPayment.Text);
                        listBoxData.Items.Add("Total Payment : " + totalPayment);
                        listBoxData.Items.Add("===============================");

                        //menampilkan data
                        TampilData();
                    }
                }
                //mengosongi textbox quantity sell
                textBoxQuantitySell.Text = "";
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }
    }
}
