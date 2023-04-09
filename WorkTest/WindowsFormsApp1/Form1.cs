using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TestForSitec;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        FileHandlerRKK fileHandlerRKK = new FileHandlerRKK();
        FileHandlerAppeals fileHandlerAppeals = new FileHandlerAppeals();
        TotalOfDocuments totalOfDocuments = new TotalOfDocuments();
        Stopwatch stopwatch = new Stopwatch();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            stopwatch.Start();
            var column1 = new DataGridViewColumn();
            column1.HeaderText = "Ответственный исполнитель";
            column1.Width = 100;
            column1.Name = "name";
            column1.Frozen = true;
            column1.CellTemplate = new DataGridViewTextBoxCell();

            var column2 = new DataGridViewColumn();
            column2.HeaderText = "Количество неисполненых входящих документов";
            column2.Name = "RKK";
            column2.CellTemplate = new DataGridViewTextBoxCell();

            var column3 = new DataGridViewColumn();
            column3.HeaderText = "Количество неисполненых письменых обращений граждан";
            column3.Name = "Appeals";
            column3.CellTemplate = new DataGridViewTextBoxCell();

            var column4 = new DataGridViewColumn();
            column4.HeaderText = "Общее Количество документов и обращений";
            column4.Name = "TotalNumber";
            column4.CellTemplate = new DataGridViewTextBoxCell();

            dataGridView1.Columns.Add(column1);
            dataGridView1.Columns.Add(column2);
            dataGridView1.Columns.Add(column3);
            dataGridView1.Columns.Add(column4);

            dataGridView1.AllowUserToAddRows = false;
            DateTime dateTime = DateTime.Now;
            stopwatch.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("File.txt", FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fs);

            try
            {
                streamWriter.WriteLine("                                                                    Справка о неисполненых документах и обращениях граждан\n");
                streamWriter.WriteLine("Не исполнено в срок " + totalOfDocuments.TotalOfNumber.Sum() + " документов, из них:\n");
                streamWriter.WriteLine("-количество неисполненных входящих документов: " + totalOfDocuments.RKK.Sum() + ";\n");
                streamWriter.WriteLine("-количество неисполненных письменых обращений граждан: " + totalOfDocuments.Appeals.Sum() + ".\n");
                for (int q = 0; q < dataGridView1.Columns.Count; q++)
                {
                    streamWriter.Write(dataGridView1.Columns[q].HeaderText + "    ");
                }
                streamWriter.WriteLine();
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    for (int i = 0; i < dataGridView1.Rows[j].Cells.Count; i++)
                    {
                        streamWriter.Write(dataGridView1.Rows[j].Cells[i].Value.ToString() + "                                          ");
                    }

                    streamWriter.WriteLine("\n");
                }

                streamWriter.WriteLine("Дата составления справки:" + textBox2.Text);

                streamWriter.Close();
                fs.Close();

                MessageBox.Show("Файл успешно сохранен в папку " + fs.Name);
            }
            catch
            {
                MessageBox.Show("Ошибка при сохранении файла!");
            }
        }

        private void textBox2_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            textBox2.Text = Convert.ToString(dateTime);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                string path = openFileDialog1.FileName;


                stopwatch.Start();

                fileHandlerAppeals.FileProcessing1(path);
                fileHandlerAppeals.SearchResposiblExecutor(fileHandlerAppeals.DirectorInAppeals, fileHandlerAppeals.ExecutorInAppeals);
                fileHandlerAppeals.CountingAppeals();



                totalOfDocuments.SearchTotalOfDocument(fileHandlerAppeals.Numbers, fileHandlerAppeals.ResponsibleExecutorAppeals, fileHandlerRKK.NumbersRKK, fileHandlerRKK.ResponsibleExecutorRKK);

                for (int i = 0; i < totalOfDocuments.TotalOfNumber.Count; ++i)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1["name", dataGridView1.Rows.Count - 1].Value = totalOfDocuments.ResponsibleExecutor[i];
                    dataGridView1["RKK", dataGridView1.Rows.Count - 1].Value = totalOfDocuments.RKK[i];
                    dataGridView1["Appeals", dataGridView1.Rows.Count - 1].Value = totalOfDocuments.Appeals[i];
                    dataGridView1["TotalNumber", dataGridView1.Rows.Count - 1].Value = totalOfDocuments.TotalOfNumber[i];
                }

                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; ++j)
                    {
                        object o = dataGridView1[j, i].Value;
                    }
                }

                dataGridView1.Sort(dataGridView1.Columns["name"], ListSortDirection.Ascending);

                stopwatch.Stop();

                MessageBox.Show("Файл c обращениями загружен");
            }
            catch
            {
                MessageBox.Show("Не удалось загрузить файл");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                string path = openFileDialog1.FileName;

                stopwatch.Start();

                fileHandlerRKK.FileProcessing1(path);
                fileHandlerRKK.SearchResposiblExecutor(fileHandlerRKK.DirectorInRKK, fileHandlerRKK.ExecutorInRKK);
                fileHandlerRKK.CountingAppeals();

                totalOfDocuments.SearchTotalOfDocument(fileHandlerAppeals.Numbers, fileHandlerAppeals.ResponsibleExecutorAppeals, fileHandlerRKK.NumbersRKK, fileHandlerRKK.ResponsibleExecutorRKK);

                for (int i = 0; i < totalOfDocuments.TotalOfNumber.Count; ++i)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1["name", dataGridView1.Rows.Count - 1].Value = totalOfDocuments.ResponsibleExecutor[i];
                    dataGridView1["RKK", dataGridView1.Rows.Count - 1].Value = totalOfDocuments.RKK[i];
                    dataGridView1["Appeals", dataGridView1.Rows.Count - 1].Value = totalOfDocuments.Appeals[i];
                    dataGridView1["TotalNumber", dataGridView1.Rows.Count - 1].Value = totalOfDocuments.TotalOfNumber[i];
                }

                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; ++j)
                    {
                        object o = dataGridView1[j, i].Value;
                    }
                }

                dataGridView1.Sort(dataGridView1.Columns["name"], ListSortDirection.Ascending);

                textBox1.Text = Convert.ToString(stopwatch.ElapsedMilliseconds);

                stopwatch.Stop();

                MessageBox.Show("Файл c RKK загружен");
            }
            catch
            {
                MessageBox.Show("Не удалось загрузить файл");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns["name"], ListSortDirection.Ascending);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns["RKK"], ListSortDirection.Descending);
            for (int i = 0; i <= dataGridView1.Rows.Count - 1; ++i)
            {
                for (int j = 0; j <= dataGridView1.Rows.Count - 1; ++j)
                {
                    if (Convert.ToInt32(dataGridView1["RKK", i].Value.ToString()) == Convert.ToInt32(dataGridView1["RKK", j].Value.ToString()) && i > j)
                    {
                        if (int.Parse(dataGridView1["Appeals", i].Value.ToString()) > int.Parse(dataGridView1["Appeals", j].Value.ToString()) && int.Parse(dataGridView1["Appeals", i].Value.ToString()) != int.Parse(dataGridView1["Appeals", j].Value.ToString()))
                        {
                            var point = dataGridView1.Rows[i];
                            var obmenpoint = dataGridView1.Rows[j];
                            dataGridView1.Rows.Remove(obmenpoint);
                            dataGridView1.Rows.Remove(point);
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows.Insert(j, point);
                            dataGridView1.Rows.Insert(i, obmenpoint);
                            dataGridView1.Rows.RemoveAt(31);
                            dataGridView1.Rows.RemoveAt(31);
                        }
                        if (Convert.ToInt32(dataGridView1["Appeals", i].Value.ToString()) == Convert.ToInt32(dataGridView1["Appeals", j].Value.ToString()))
                        {
                            if (int.Parse(dataGridView1["TotalNumber", i].Value.ToString()) > int.Parse(dataGridView1["TotalNumber", j].Value.ToString()) && int.Parse(dataGridView1["TotalNumber", i].Value.ToString()) != int.Parse(dataGridView1["TotalNumber", j].Value.ToString()))
                            {
                                var point = dataGridView1.Rows[i];
                                var obmenpoint = dataGridView1.Rows[j];
                                dataGridView1.Rows.Remove(obmenpoint);
                                dataGridView1.Rows.Remove(point);
                                dataGridView1.Rows.Add();
                                dataGridView1.Rows.Add();
                                dataGridView1.Rows.Insert(j, point);
                                dataGridView1.Rows.Insert(i, obmenpoint);
                                dataGridView1.Rows.RemoveAt(31);
                                dataGridView1.Rows.RemoveAt(31);
                            }
                            if (Convert.ToInt32(dataGridView1["TotalNumber", i].Value.ToString()) == Convert.ToInt32(dataGridView1["TotalNumber", j].Value.ToString()))
                            {
                                if (string.Compare(dataGridView1["name", i].Value.ToString(), dataGridView1["name", j].Value.ToString()) < 0 && dataGridView1["name", i].Value.ToString() != dataGridView1["name", j].Value.ToString())
                                {
                                    var point = dataGridView1.Rows[i];
                                    var obmenpoint = dataGridView1.Rows[j];
                                    dataGridView1.Rows.Remove(obmenpoint);
                                    dataGridView1.Rows.Remove(point);
                                    dataGridView1.Rows.Add();
                                    dataGridView1.Rows.Add();
                                    dataGridView1.Rows.Insert(j, point);
                                    dataGridView1.Rows.Insert(i, obmenpoint);
                                    dataGridView1.Rows.RemoveAt(31);
                                    dataGridView1.Rows.RemoveAt(31);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns["Appeals"], ListSortDirection.Descending);
            for (int i = 0; i <= dataGridView1.Rows.Count - 1; ++i)
            {
                for (int j = 0; j <= dataGridView1.Rows.Count - 1; ++j)
                {
                    if (Convert.ToInt32(dataGridView1["Appeals", i].Value.ToString()) == Convert.ToInt32(dataGridView1["Appeals", j].Value.ToString()) && i > j)
                    {
                        if (int.Parse(dataGridView1["RKK", i].Value.ToString()) > int.Parse(dataGridView1["RKK", j].Value.ToString()) && int.Parse(dataGridView1["RKK", i].Value.ToString()) != int.Parse(dataGridView1["RKK", j].Value.ToString()))
                        {
                            var point = dataGridView1.Rows[i];
                            var obmenpoint = dataGridView1.Rows[j];
                            dataGridView1.Rows.Remove(obmenpoint);
                            dataGridView1.Rows.Remove(point);
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows.Insert(j, point);
                            dataGridView1.Rows.Insert(i, obmenpoint);
                            dataGridView1.Rows.RemoveAt(31);
                            dataGridView1.Rows.RemoveAt(31);
                        }
                        if (Convert.ToInt32(dataGridView1["Appeals", i].Value.ToString()) == Convert.ToInt32(dataGridView1["Appeals", j].Value.ToString()))
                        {
                            if (int.Parse(dataGridView1["TotalNumber", i].Value.ToString()) > int.Parse(dataGridView1["TotalNumber", j].Value.ToString()) && int.Parse(dataGridView1["TotalNumber", i].Value.ToString()) != int.Parse(dataGridView1["TotalNumber", j].Value.ToString()))
                            {
                                var point = dataGridView1.Rows[i];
                                var obmenpoint = dataGridView1.Rows[j];
                                dataGridView1.Rows.Remove(obmenpoint);
                                dataGridView1.Rows.Remove(point);
                                dataGridView1.Rows.Add();
                                dataGridView1.Rows.Add();
                                dataGridView1.Rows.Insert(j, point);
                                dataGridView1.Rows.Insert(i, obmenpoint);
                                dataGridView1.Rows.RemoveAt(31);
                                dataGridView1.Rows.RemoveAt(31);
                            }
                            if (Convert.ToInt32(dataGridView1["TotalNumber", i].Value.ToString()) == Convert.ToInt32(dataGridView1["TotalNumber", j].Value.ToString()))
                            {
                                if (string.Compare(dataGridView1["name", i].Value.ToString(), dataGridView1["name", j].Value.ToString()) < 0 && dataGridView1["name", i].Value.ToString() != dataGridView1["name", j].Value.ToString())
                                {
                                    var point = dataGridView1.Rows[i];
                                    var obmenpoint = dataGridView1.Rows[j];
                                    dataGridView1.Rows.Remove(obmenpoint);
                                    dataGridView1.Rows.Remove(point);
                                    dataGridView1.Rows.Add();
                                    dataGridView1.Rows.Add();
                                    dataGridView1.Rows.Insert(j, point);
                                    dataGridView1.Rows.Insert(i, obmenpoint);
                                    dataGridView1.Rows.RemoveAt(31);
                                    dataGridView1.Rows.RemoveAt(31);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns["TotalNumber"], ListSortDirection.Descending);
            for (int i = 0; i <= dataGridView1.Rows.Count - 1; ++i)
            {
                for (int j = 0; j <= dataGridView1.Rows.Count - 1; ++j)
                {
                    if (Convert.ToInt32(dataGridView1["TotalNumber", i].Value.ToString()) == Convert.ToInt32(dataGridView1["TotalNumber", j].Value.ToString()) && i > j)
                    {
                        if (int.Parse(dataGridView1["RKK", i].Value.ToString()) > int.Parse(dataGridView1["RKK", j].Value.ToString()) && int.Parse(dataGridView1["RKK", i].Value.ToString()) != int.Parse(dataGridView1["RKK", j].Value.ToString()))
                        {
                            var point = dataGridView1.Rows[i];
                            var obmenpoint = dataGridView1.Rows[j];
                            dataGridView1.Rows.Remove(obmenpoint);
                            dataGridView1.Rows.Remove(point);
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows.Insert(j, point);
                            dataGridView1.Rows.Insert(i, obmenpoint);
                            dataGridView1.Rows.RemoveAt(31);
                            dataGridView1.Rows.RemoveAt(31);
                        }
                        if (Convert.ToInt32(dataGridView1["RKK", i].Value.ToString()) == Convert.ToInt32(dataGridView1["RKK", j].Value.ToString()))
                        {
                            if (int.Parse(dataGridView1["Appeals", i].Value.ToString()) > int.Parse(dataGridView1["Appeals", j].Value.ToString()) && int.Parse(dataGridView1["Appeals", i].Value.ToString()) != int.Parse(dataGridView1["Appeals", j].Value.ToString()))
                            {
                                var point = dataGridView1.Rows[i];
                                var obmenpoint = dataGridView1.Rows[j];
                                dataGridView1.Rows.Remove(obmenpoint);
                                dataGridView1.Rows.Remove(point);
                                dataGridView1.Rows.Add();
                                dataGridView1.Rows.Add();
                                dataGridView1.Rows.Insert(j, point);
                                dataGridView1.Rows.Insert(i, obmenpoint);
                                dataGridView1.Rows.RemoveAt(31);
                                dataGridView1.Rows.RemoveAt(31);
                            }
                            if (Convert.ToInt32(dataGridView1["Appeals", i].Value.ToString()) == Convert.ToInt32(dataGridView1["Appeals", j].Value.ToString()))
                            {
                                if (string.Compare(dataGridView1["name", i].Value.ToString(), dataGridView1["name", j].Value.ToString()) < 0 && dataGridView1["name", i].Value.ToString() != dataGridView1["name", j].Value.ToString())
                                {
                                    var point = dataGridView1.Rows[i];
                                    var obmenpoint = dataGridView1.Rows[j];
                                    dataGridView1.Rows.Remove(obmenpoint);
                                    dataGridView1.Rows.Remove(point);
                                    dataGridView1.Rows.Add();
                                    dataGridView1.Rows.Add();
                                    dataGridView1.Rows.Insert(j, point);
                                    dataGridView1.Rows.Insert(i, obmenpoint);
                                    dataGridView1.Rows.RemoveAt(31);
                                    dataGridView1.Rows.RemoveAt(31);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
