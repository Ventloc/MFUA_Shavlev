using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathQuiz
{
    public partial class Form1 : Form
    {
        Random randomizer = new Random();

        int addend1; // Переменные для сложения
        int addend2;

        int minuend; // Переменные для вычитания
        int subtrahend;

        int multiplicand; // Переменные для умножения
        int multiplier;

        int dividend; // Переменные для деления
        int divisor;

        int timeLeft; // Переменная для отсчета таймера
        public void StartTheQuiz()
        {
            addend1 = randomizer.Next(51); // Рандомизация значения для переменных сложения
            addend2 = randomizer.Next(51);

            plusLeftLabel.Text = addend1.ToString(); // ToString используется для передачи текста в label
            plusRightLabel.Text = addend2.ToString();
            sum.Value = 0;

            minuend = randomizer.Next(1, 101); // Рандомизация значения для переменных вычитания
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            multiplicand = randomizer.Next(2, 11); // Рандомизация значения для переменных умножения
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            divisor = randomizer.Next(2, 11); // Рандомизация значения для переменных деления
            int temporaryQuotient = randomizer.Next(divisor+1, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            timeLeft = 30;
            timeLabel.Text = "30 cекунд"; // обозначения для игрока, сколько времени у него осталось
            timer1.Start();
        }

        private bool CheckTheAnswer() // функция для проверки ответов, возвращается значение для таймера
        {
            if ((addend1 + addend2 == sum.Value)
                && (minuend - subtrahend == difference.Value)
                && (multiplicand * multiplier == product.Value)
                && (dividend / divisor == quotient.Value))
                return true;
            else
                return false;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {

                timer1.Stop();
                MessageBox.Show("Всё верно!",
                                "Поздравляем!");
                startButton.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " секунд";
                if ((timeLeft == 21) ^ (timeLeft == 1))
                {
                    timeLabel.Text = timeLeft + " секунда"; // Моё личное добавление, корректно выводит слово под кол-во секунд
                }
            }
            else
            {
                timer1.Stop();
                timeLabel.Text = "Время вышло!";
                MessageBox.Show("Вы не успели.", "Увы!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
                timeLabel.BackColor = Color.Red; // при окончании отсчета ответов правильных не поступило, таймер становится красным
            }
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }
    }
}
