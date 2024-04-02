using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;


namespace задачи
{
    public partial class Form1 : Form
    {

        private bool flag = false;
        public Form1()
        {
            InitializeComponent();
           // this.BackgroundImage = Image.FromFile(@"C:\Users\Alisa\Desktop\учёба\программирование\visual studio\задачи\23.jpg");
            listView1.CheckBoxes = true;
            listView1.ItemCheck += listView1_ItemCheck;
            out_file();
        }

        private void listView1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Запрещаем изменение флажка CheckBox
            if (e.CurrentValue == CheckState.Checked) e.NewValue = e.CurrentValue;

            if (e.NewValue == CheckState.Checked)
                foreach (ListViewItem item2 in listView1.Items)
                    if (listView1.Items[e.Index] == item2)
                    {
                        item2.SubItems[3].Text = "done";
                        item2.BackColor = Color.LightGreen;  
                    }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListViewItem item = new ListViewItem();
            string check = "", name = "", date = "";
            if (checkBox1.Checked) check = "true";
            else { check = "false"; flag = false; }

            name = textBox1.Text;
            if (name != "")
            {
                date = dateTimePicker1.Value.ToString();
                if (check == "true") { flag = true; item.Checked = true; }
                else if (flag == false) item.Checked = false;
                item.SubItems.Add(name);
                item.SubItems.Add(date);
                if (item.Checked == true) { item.SubItems.Add("done"); item.BackColor = Color.LightGreen; }
                else if (DateTime.Now > dateTimePicker1.Value) { item.SubItems.Add("overdue"); item.BackColor = Color.Red; }
                else if (DateTime.Now <= dateTimePicker1.Value) item.SubItems.Add("in process");

                listView1.Items.Add(item);
            }
            textBox1.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            checkBox1.Checked = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        public void in_file()
        {
            string file_path = "infile.txt";
            if (!File.Exists(file_path)) File.Create(file_path).Close();
            ListViewItem item = new ListViewItem();
            StreamWriter writer = new StreamWriter(file_path);
            foreach (ListViewItem item2 in listView1.Items)
            {
                string s = $"{item2.SubItems[1].Text},{item2.SubItems[2].Text},{item2.SubItems[3].Text}";
                writer.WriteLine(s);
            }
            writer.Close();
        }

        public void out_file()
        {
            string file_path = "infile.txt";
            if (File.Exists(file_path))
            {
                StreamReader reader = new StreamReader(file_path);
                string line = reader.ReadLine();
                while (line != null)
                {
                    string[] values = line.Split(',');
                    if (values.Length == 3)
                    {
                        ListViewItem item = new ListViewItem();
                        item.SubItems.Add(values[0]);
                        item.SubItems.Add(values[1]);
                        item.SubItems.Add(values[2]);
                        DateTime date = DateTime.Parse(item.SubItems[2].Text);
                        if (item.SubItems[3].Text == "done") { item.Checked = true; item.BackColor = Color.LightGreen; }
                        else if (DateTime.Now <= date) item.SubItems[3].Text = "in process";
                        else if (DateTime.Now > date) { item.SubItems[3].Text = "overdue"; item.BackColor = Color.Red; }

                        listView1.Items.Add(item);
                    }
                    line = reader.ReadLine();
                }
                reader.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item2 in listView1.Items)
            {
                if (item2.Selected) item2.Remove();
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            in_file();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}