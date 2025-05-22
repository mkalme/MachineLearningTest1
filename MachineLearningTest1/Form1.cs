using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace MachineLearningTest1
{
    public partial class Form1 : Form
    {
        public static double distance = 1000; //KM
        public static double refuel = 125; //KM AT
        public static double refuelAt = 60; //125 KM REFUEL AT 60 KM/H
        public static double timeToRefuel = 1200; //Seconds
        public static double maxSpeed = 100; //KM/H

        public static int bestOf = 200;

        public static int numberOfPlayers = 1000;
        public static int generations = 2;

        public static double[] arraySpeed = new double[numberOfPlayers];
        public static double[] arrayResult = new double[numberOfPlayers];

        Random random = new Random();

        //SPEED MAX 100 KM / H

        //IF SPEED

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loops();
        }

        private void loops() {
            for (int i = 0; i < generations; i++) {
                if (i > 1) {
                    reArrange();
                }

                double[] sortResult = new double[numberOfPlayers];
                for (int b = 0; b < numberOfPlayers; b++)
                {
                    sortResult[b] = arrayResult[b];
                }

                create();
                Debug.Write("Best: " + arrayResult.Min() + " Sec, Sp.: " + arraySpeed[Array.IndexOf(arrayResult, arrayResult.Min())] + " KM/H || ");
                Debug.Write("Worst: " + arrayResult.Max() + " Sec, Sp.: " + arraySpeed[Array.IndexOf(arrayResult, arrayResult.Max())] + " KM/H || ");
                Debug.Write("Aver: " + Math.Floor(arrayResult.Sum() / Double.Parse(numberOfPlayers.ToString())) + " Sec, Sp: " + (arraySpeed.Sum() / Double.Parse(numberOfPlayers.ToString())) + " KM/H\n");
            }
        }

        private void reArrange() {
            double[] arrayTempResult = new double[bestOf];
            double[] arrayTempSpeed = new double[bestOf];

            double[] sortResult = new double[numberOfPlayers];
            for (int i = 0; i < numberOfPlayers; i++)
            {
                sortResult[i] = arrayResult[i];
            }

            Array.Sort<double>(sortResult);
            Array.Reverse(sortResult);

            for (int i = 0; i < bestOf; i++) {
                arrayTempResult[i] = arrayResult[Array.IndexOf(arrayResult, sortResult[i])];
                arrayTempSpeed[i] = arraySpeed[Array.IndexOf(arrayResult, sortResult[i])];
            }

            for (int i = bestOf; i < numberOfPlayers; i++) {
                arrayResult[i] = arrayTempResult[i % bestOf];
                arraySpeed[i] = arrayTempSpeed[i % bestOf];
            }
        }

        private void create() {
            for (int i = 0; i < arraySpeed.Length; i++)
            {
                int speed = getSpeed(i);

                Double refuelNum = (refuelAt / speed * 125);

                Double time = Math.Floor(((distance / speed) * 3600) + (((distance / refuelNum) - 1) * timeToRefuel));
                arraySpeed[i] = speed;
                arrayResult[i] = time;
            }
        }

        private int getSpeed(int index) {
            int speed = 0;
            if (arraySpeed[index] == 0) {
                speed = random.Next(Int32.Parse(maxSpeed.ToString())) + 1;
            }
            else {
                int temp = random.Next(2);
                speed += (temp == 1 ? 1 : -1);
            }

            return speed;
        }
    }
}
