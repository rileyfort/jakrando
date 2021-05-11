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
using Memory;
// Randomizer created by Riley Fort and Taylor Berukoff
namespace Ruhndomizer_v2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Mem m = new Mem();

        // Array for Address 207CB984
        string[] spawn = { "0x04 0x73", "0x04 0x53", "0xF4 0x0A", "0x74 0x54", "0x94 0xF2", "0x6C 0x54", "0xAC 0x22", "0x9C 0xF2", "0xF4 0xE2", "0x84 0x62", "0x64 0x54", "0xC4 0x92", "0x5C 0x92", "0x84 0x2A", "0x84 0x52", "0xBC 0x52", "0x04 0x53", "0xEC 0xF2", "0xBC 0x52" , "0xBC 0x52", "0xBC 0x52", "0xBC 0x52", "0x6C 0x54", "0x9C 0xF2", "0x9C 0xF2", "0x9C 0xF2", "0x9C 0xF2", "0x9C 0xF2", "0xF4 0xE2", "0xF4 0xE2", "0xF4 0xE2", "0xF4 0xE2", "0xF4 0xE2", "0xF4 0xE2", "0x84 0x62", "0x84 0x62", "0x64 0x54", "0xC4 0x92", "0xEC 0x22", "0xEC 0x22", "0xC4 0x92", "0x74 0x92", "0x84 0x2A", "0x84 0x2A", "0x84 0x2A", "0x84 0x2A", "0x84 0x2A", "0x84 0x2A", "0x84 0x2A", "0x84 0x2A", "0x5C 0x92", "0x5C 0x92", "0x5C 0x92"};
        
        // Array for Address 20642D6C
        string[] checkpoint = { "0xA4 0xB8", "0x04 0xA8", "0xB4 0xC4", "0x34 0xBF", "0x14 0xB7", "0x04 0xA6", "0x44 0x92", "0x44 0xA3", "0x44 0x9C", "0xB4 0x90", "0xF4 0x8D", "0x44 0x84", "0xE4 0x7D", "0x14 0x8B", "0xF4 0x79", "0x34 0xB2", "0x64 0xA7", "0x24 0xB5", "0xF4 0xAD" , "0x14 0xB0", "0xB4 0xA9", "0xD4 0xAB", "0xC4 0xA4", "0xB4 0xA2", "0x24 0xA2", "0x84 0xA1", "0xF4 0x9F", "0x54 0x9F", "0xE4 0x9A", "0x84 0x99", "0x24 0x98", "0xC4 0x96", "0x64 0x95", "0x04 0x94", "0x14 0x90", "0x74 0x8F", "0xB4 0x8C", "0x04 0x83", "0xE4 0x7F", "0x44 0x7F", "0xA4 0x83", "0x64 0x81", "0x94 0x88", "0xD4 0x89", "0x14 0x86", "0xF4 0x87", "0xB4 0x86", "0x54 0x87", "0x74 0x8A", "0x34 0x89", "0x44 0x7D", "0xA4 0x7C", "0x04 0x7C"};

        string[] eco = {"1", "2", "3", "4" }; // Integers need to be set to string.

        string[] health = { "1" }; // Integers need to be set to string.

        float cellCount = 0; // Sets cell count to 0.

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                int ID = m.getProcIDFromName("pcsx2");

                bool openProc = false;

                if (ID > 0)
                {
                    openProc = m.OpenProcess(ID);
                }
                
                if (openProc)
                {
                    float newCellCount = m.readFloat("0x20642D60"); // Reads current amount of collected cells.
                    int newEcoType = m.readByte("0x20182848"); // Reads eco type.
                    float oneHitKill = m.readFloat("0x20182860"); // Reads current big health.

                    if (checkBox3.Checked == true) // If One Hit Kill checkbox is checked.
                    {
                        m.writeMemory("0x20182860", "float", health[0]); // Write value 1 to big health address.
                        System.Threading.Thread.Sleep(2000); // Wait 2 seconds before writing value 1 to big health address again.
                    }

                    if (checkBox2.Checked == true && newEcoType > 0) // If Eco checkbox is checked and eco type address is 0.
                    {
                        Random ran = new Random();
                        int num = ran.Next(4); // Generates random number up to 4.
                        m.writeMemory("0x20182848", "byte", eco[num]); // Writes random value to the eco type address.
                        System.Threading.Thread.Sleep(1000); // Waits 1 second to change eco type.
                    }

                    if (checkBox.Checked == true && checkBox1.Checked == true) // If both Cell randomizer checkboxes are checked...
                    {
                        ; // Do nothing.
                    }

                    else
                    {
                        if (checkBox.Checked == true && checkBox1.Checked == false && newCellCount > cellCount) // Checks if the cell count went up.
                        {
                            Random ran = new Random();
                            List<int> indexList = new List<int>();
                            int index;
                            for(int i = 0; i < 52; i++) // Creates a list of random integers from 0-51
                            {
                                do
                                {
                                    index = ran.Next(52); // Generates random integer below 52
                                } while (indexList.Contains(index)); // Loops until integer is not in the list
                                indexList.Add(index); // Adds random integer to the list
                            }
                            string[] tempSpawn = new string[52];
                            string[] tempCheckpoint = new string[52];
                            for (int i = 0; i < 52; i++) // Shuffles the spawn and checkpoint arrays by the random index list
                            {
                                tempSpawn[i] = spawn[indexList[i]];
                                tempCheckpoint[i] = checkpoint[indexList[i]];
                            }
                            spawn = tempSpawn;
                            checkpoint = tempCheckpoint;
                            System.Threading.Thread.Sleep(8000); // Waits for cell cutscene to finish.
                            Random ranNum = new Random(); //Random object for num
                            num = ranNum.Next(52);
                            //m.writeMemory("0x2017A9F8", "float", "1328623"); // Changes Z coordinate to out of bounds.
                            m.writeMemory("0x2017A9F4", "float", "-2127581"); // Changes Y coordinate to out of bounds.
                            m.writeMemory("0x207CB984", "bytes", spawn[num]); // Uses randomly genertated number to determine spawn value.
                            m.writeMemory("0x20642D6C", "bytes", checkpoint[num]); // Uses randomly genertated number to determine checkpoint value.
                            cellCount = newCellCount; // Updates static cell count to number of cells the player has.
                        }
                    

                        if (checkBox1.Checked == true && checkBox.Checked == false && newCellCount > cellCount + 2) // Checks if the cell count went up by 3.
                        {
                            Random ranIndex = new Random(); //Random object for index
                            List<int> indexList = new List<int>();
                            int index;
                            for(int i = 0; i < 52; i++) // Creates a list of random integers from 0-51
                            {
                                do
                                {
                                    index = ran.Next(52); // Generates random integer below 52
                                } while (indexList.Contains(index)); // Loops until integer is not in the list
                                indexList.Add(index); // Adds random integer to the list
                            }
                            string[] tempSpawn = new string[52];
                            string[] tempCheckpoint = new string[52];
                            for (int i = 0; i < 52; i++) // Shuffles the spawn and checkpoint arrays by the random index list
                            {
                                tempSpawn[i] = spawn[indexList[i]];
                                tempCheckpoint[i] = checkpoint[indexList[i]];
                            }
                            spawn = tempSpawn;
                            checkpoint = tempCheckpoint;
                            Random ranNum = new Random(); //Random object for num
                            num = ranNum.Next(52);
                            System.Threading.Thread.Sleep(8000); // Waits for cell cutscene to finish.
                            // m.writeMemory("0x2017A9F8", "float", "1328623"); // Changes Z coordinate to out of bounds.
                            m.writeMemory("0x2017A9F4", "float", "-2127581"); // Changes Y coordinate to out of bounds.
                            m.writeMemory("0x207CB984", "bytes", spawn[num]); // Uses randomly genertated number to determine spawn value.
                            m.writeMemory("0x20642D6C", "bytes", checkpoint[num]); // Uses randomly genertated number to determine checkpoint value.
                            cellCount = newCellCount; // Updates static cell count to number of cells the player has.
                        }
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.twitter.com/ruhphorte");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://discord.gg/HUzXuNn");
        }
    }
}
