using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsLab6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
        private int[] GenerateArray(int size, int min, int max)
        {
            Random random = new Random();
            return Enumerable.Range(0, size).Select(i => random.Next(min, max + 1)).ToArray();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int size = int.Parse(textBox1.Text);
                int min = int.Parse(textBox2.Text);
                int max = int.Parse(textBox3.Text);
                if (min > max)
                {
                    MessageBox.Show("Минимальное значение должно быть меньше максимального.");
                    return;
                }

                int[] array = GenerateArray(size, min, max);
                textBox4.Text = string.Join(" ", array);
            }
            catch (FormatException)
            {
                MessageBox.Show("Проверьте введенные данные. Они должны быть числовыми.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string[] lines = System.IO.File.ReadAllLines(openFileDialog.FileName);
                    int[] array = lines.SelectMany(line => line.Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)).ToArray();
                    textBox4.Text = string.Join(" ", array);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при чтении файла: {ex.Message}");
                }
            }
        }
        private int SumElements(int[] array) => array.Sum();

        private double AverageElements(int[] array) => array.Average();

        private (int value, int index) MinElement(int[] array)
        {
            int minIndex = Array.IndexOf(array, array.Min());
            return (array[minIndex], minIndex);
        }

        private (int value, int index) MaxElement(int[] array)
        {
            int maxIndex = Array.IndexOf(array, array.Max());
            return (array[maxIndex], maxIndex);
        }

        private int[] EvenElements(int[] array) => array.Where(x => x % 2 == 0).ToArray();

        private int[] OddElements(int[] array) => array.Where(x => x % 2 != 0).ToArray();

        private int[] SortAscending(int[] array) => array.OrderBy(x => x).ToArray();

        private int[] SortDescending(int[] array) => array.OrderByDescending(x => x).ToArray();

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int[] array = textBox4.Text.Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                string result = "";
                if (radioButton1.Checked)
                {
                    result = SumElements(array).ToString();
                }
                else if (radioButton2.Checked)
                {
                    result = AverageElements(array).ToString();
                }
                else if (radioButton3.Checked)
                {
                    var (value, index) = MinElement(array);
                    result = $"Значение: {value}, Индекс: {index}";
                }
                else if (radioButton4.Checked)
                {
                    var (value, index) = MaxElement(array);
                    result = $"Значение: {value}, Индекс: {index}";
                }
                else if (radioButton5.Checked)
                {
                    result = string.Join(" ", SortDescending(array));
                }
                else if (radioButton6.Checked)
                {
                    result = string.Join(" ", SortAscending(array));
                }
                else if (radioButton7.Checked)
                {
                    result = string.Join(" ", OddElements(array));
                }
                else if (radioButton8.Checked)
                {
                    result = string.Join(" ", EvenElements(array));
                }

                textBox5.Text = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string dataToSave = $"Исходный массив: {textBox4.Text}\r\nРезультат операции: {textBox5.Text}\r\n";
                    System.IO.File.AppendAllText(saveFileDialog.FileName, dataToSave);
                    MessageBox.Show("Данные сохранены.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}");
                }
            }
        }
    }
}
