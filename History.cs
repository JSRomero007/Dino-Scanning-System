using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace ScaningSystem
{
    public partial class History : Form
    {
        Point lastPoint = Point.Empty;//Point.Empty represents null for a Point object

        bool isMouseDown = new Boolean();//this is used to evaluate whether our mousebutton is down or not

        public History()
        {
            InitializeComponent();
        }

        private void History_Load(object sender, EventArgs e)
        {
            groupBox3.Visible = false;
            label28.Visible = false;
            label20.Visible = false;
            flowLayoutPanel1.Visible = false;
            pictureBox7.Visible = false;
            label20.Visible = false;
            label6.Text = Global.Global_V.V;
            label12.Text = string.Empty;
            label14.Text = string.Empty;
            label16.Text = string.Empty;
            label18.Text = string.Empty;
            dataGridView1.Visible = false;
            groupBox3.Visible = false;
            label27.Text = string.Empty;
            groupBox2.Visible = false;
            Global.DBConect dbObject = new Global.DBConect();
            label8.Visible = false;
            label2.Text = Global.Global_V.ZcodeFilter;

            WindowState = FormWindowState.Maximized;
            Global.Global_V.CountToReset = Global.Global_V.CountToReset + 1;//Counter to Reset
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Global.Global_V.ZcodeFilter = string.Empty;



            Application.Restart();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Global.Global_V.ZcodeFilter = textBox1.Text;
            label2.Text = Global.Global_V.ZcodeFilter;
            Global.DBConect dbObject = new Global.DBConect();


            if (textBox1.Text != "")
            {
                dataGridView1.Visible = true;
            
                label8.Visible = false;
                string query = "SELECT * FROM ZcodeINFO WHERE ZCode = '" + Global.Global_V.ZcodeFilter + "'";
                SQLiteCommand mycomand = new SQLiteCommand(query, dbObject.myConnection);
                dbObject.OpenConnection();
                var reader = mycomand.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                dataGridView1.DataSource = table;
                mycomand.ExecuteReader();
                dbObject.CloseConnection();
                textBox1.Text = string.Empty;
                groupBox2.Visible = true;



            }
            else { label8.Visible = true; }



        }



        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox5_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = e.Location;
            isMouseDown = true;
        }

        private void pictureBox5_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;

            lastPoint = Point.Empty;


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                groupBox3.Visible = true;
                label28.Visible = true;
                pictureBox7.Visible = true;
                flowLayoutPanel1.Visible = true;
                label28.Visible = true;
                Global.Global_V.SqlZcode = dataGridView1.SelectedCells[1].Value.ToString();
                Global.Global_V.SqlDate = dataGridView1.SelectedCells[2].Value.ToString();
                Global.Global_V.SqlTime = dataGridView1.SelectedCells[3].Value.ToString() + " " + dataGridView1.SelectedCells[4].Value.ToString();
                Global.Global_V.SqlOperator = dataGridView1.SelectedCells[6].Value.ToString();
                Global.Global_V.SqlShift = dataGridView1.SelectedCells[7].Value.ToString();
                label12.Text = Global.Global_V.SqlZcode;
                label14.Text = Global.Global_V.SqlDate;
                label16.Text = Global.Global_V.SqlTime;
                label18.Text = Global.Global_V.SqlOperator;

                byte[] img = (byte[])dataGridView1.SelectedCells[5].Value;
                MemoryStream ms = new MemoryStream(img);
                pictureBox5.Image = Image.FromStream(ms);
                label27.Text = "";
            }
            catch
            {
                groupBox3.Visible = false;  
                label28.Visible=false;
                label20.Visible=false;
               flowLayoutPanel1.Visible = false;
                pictureBox7.Visible = false;
                label27.Text = "Es necesario seleccionar una fila válida...";
            }




        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string date = dateTimePicker1.Value.ToString("MM/dd/yyyy");
            string turn = comboBox1.SelectedItem.ToString();
            try
            {
                Global.DBConect dbObject = new Global.DBConect();
                dataGridView1.Visible = true;
                groupBox3.Visible = true;
                label8.Visible = false;
                string query = "SELECT * FROM ZcodeINFO WHERE Date = '" + date + "' AND Shift = '" + turn + "'" + " AND ZCode = '" + Global.Global_V.ZcodeFilter + "'";
                SQLiteCommand mycomand = new SQLiteCommand(query, dbObject.myConnection);
                dbObject.OpenConnection();
                var reader = mycomand.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                dataGridView1.DataSource = table;
                mycomand.ExecuteReader();
                dbObject.CloseConnection();
                textBox1.Text = string.Empty;
                groupBox2.Visible = true;
            }
            catch { Console.WriteLine("No data"); }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog Guardar = new SaveFileDialog();
                Guardar.Filter = "JPEG(*.JPG)|*.JPG|BMP(*.BMP)|*.BMP";
                Image Imagen = pictureBox5.Image;
                Guardar.ShowDialog();
                try
                {label20.Visible= true;
                    Imagen.Save(Guardar.FileName);
                    label20.Text = "Información descargada"; label20.ForeColor = Color.Green;
                    string fullP = @"" + Guardar.FileName + "_Info.txt";
                    TextWriter Txt = new StreamWriter(fullP);
                    Global.Global_V.UB = fullP;
                    //Creat file in C:\Users\Public\Pictures\Config\
                    Txt.WriteLine("┌────────────────────────────────────────────────────┐");
                    Txt.WriteLine("│     Información capturada en la base de datos      │");
                    Txt.WriteLine("│────────────────────────────────────────────────────│");
                    Txt.WriteLine("│                                                    │");
                    Txt.WriteLine("│                                                    │");
                    Txt.WriteLine("│▪ Código z capturado : " + Global.Global_V.SqlZcode);
                    Txt.WriteLine("│▪ Fecha de del registro : " + Global.Global_V.SqlDate);
                    Txt.WriteLine("│▪ Hora del registro : " + Global.Global_V.SqlTime);
                    Txt.WriteLine("│▪ Turno: " + Global.Global_V.SqlShift);
                    Txt.WriteLine("│▪ Operador que realizo el registro : " + Global.Global_V.SqlOperator);
                    Txt.WriteLine("└────────────────────────────────────────────────────┘");
                    Txt.Close();

                }
                catch { label20.Text = "Error de descarga"; label20.ForeColor = Color.Red; }
            }
            catch
            { }

        }

        private void pictureBox5_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                if (lastPoint != null)
                {
                    if (pictureBox5.Image == null)
                    {
                        Bitmap bmp = new Bitmap(pictureBox5.Width, pictureBox5.Height);
                        pictureBox5.Image = bmp; 
                    }
                    using (Graphics g = Graphics.FromImage(pictureBox5.Image))
                    {
                        g.DrawLine(new Pen(Color.Red, 8), lastPoint, e.Location);
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                    }
                    pictureBox5.Invalidate();
                    lastPoint = e.Location;
                }

            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if (pictureBox5.Image != null)
            {
                pictureBox5.Image = null;
                Invalidate();
            }
        }
    }
}
