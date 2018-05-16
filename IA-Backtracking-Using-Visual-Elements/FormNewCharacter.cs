﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IA_Backtracking_Using_Visual_Elements
{
    public partial class FormNewCharacter : Form
    {
        IdCost idCost;
        Character character;

        List<Cell> noRepeat;
        List<IdCost> idCostList;

        int last;
        bool isCreated;

        List<int> expantionOrder;

        public FormNewCharacter(ref List<Cell> noRepeat)
        {
            InitializeComponent();
            this.noRepeat = noRepeat;
            character = new Character();

            idCostList = new List<IdCost>();

            ListBoxTerrain.Enabled = false;
            isCreated = false;

            ComboBoxImage.SelectedIndex = 0;

            expantionOrder = new List<int>();
            expantionOrder.Add(0);
            expantionOrder.Add(1);
            expantionOrder.Add(2);
            expantionOrder.Add(3);
        }

        //solo recibir numeros en el textbox
        private void textBoxCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
            else if(e.KeyChar != (char)Keys.Back)
            {
                string checkInput = textBoxCost.Text;
                checkInput += (char)e.KeyChar;
                if (!System.Text.RegularExpressions.Regex.IsMatch(checkInput, "^[0-9]*\\.?[0-9]{0,2}$"))
                {
                    e.Handled = true;
                }
            }
        }

        private void FormNewCharacter_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < noRepeat.Count; i++)
            {
                ListBoxTerrain.Items.Add(noRepeat[i].TerrainName);
            }

            //set index in listBox for comfort
            ListBoxTerrain.SetSelected(0, true);
            last = ListBoxTerrain.Items.Count - 1;

            updateExpantionOrder();

            listBoxExpantionOrder.SelectedIndex = 0;
        }

        private void ButtonNext_Click(object sender, EventArgs e)
        {
            if(textBoxCost.Text != "" || CheckBoxNA.Checked)
            {
                if (CheckBoxNA.Checked)
                {
                    idCost = new IdCost(Int32.Parse(noRepeat[ListBoxTerrain.SelectedIndex].TerrainId.ToString()), -1);
                    textBoxCost.Text = "";
                }
                else
                {
                    idCost = new IdCost(Int32.Parse(noRepeat[ListBoxTerrain.SelectedIndex].TerrainId.ToString()), float.Parse(textBoxCost.Text));
                    textBoxCost.Text = "";
                }

                idCostList.Add(idCost);


                if (ListBoxTerrain.SelectedIndex == last)
                {
                    character.characterImg = pictureBoxCharacter.Image;
                    character.idCostList = idCostList;

                    isCreated = true;

                    this.Close();
                }
                else
                {
                    ListBoxTerrain.SelectedIndex++;
                }
            }
            else
            {
                MessageBox.Show("Debes ingresar un valor para continuar", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            CheckBoxNA.Checked = false;
        }

        private void ComboBoxImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ComboBoxImage.SelectedIndex)
            {
                // Humano
                case 0:
                    pictureBoxCharacter.Image = Properties.Resources.man;
                    break;

                // Tejon
                case 1:
                    pictureBoxCharacter.Image = Properties.Resources.badger;
                    break;

                // Pez
                case 2:
                    pictureBoxCharacter.Image = Properties.Resources.fish;
                    break;

                // Lobo
                case 3:
                    pictureBoxCharacter.Image = Properties.Resources.wolf;
                    break;
            }
        }

        private void CheckBoxNA_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxNA.Checked)
            {
                textBoxCost.Text = "";
                textBoxCost.Enabled = false;
            }
            else
            {
                textBoxCost.Enabled = true;
            }
        }

        public void updateExpantionOrder()
        {
            listBoxExpantionOrder.Items.Clear();
            string text = "";

            for (int i = 0; i < 4; i++)
            {
                switch (expantionOrder[i])
                {
                    case 0:
                        text = "Arriba";
                        break;
                    case 1:
                        text = "Abajo";
                        break;
                    case 2:
                        text = "Izquierda";
                        break;
                    case 3:
                        text = "Derecha";
                        break;
                }
                listBoxExpantionOrder.Items.Add(text);
            }
        }
        public Character GetCharacter { get { return character; } }
        public bool GetisCreated { get { return isCreated; } }
        public List<int> GetExpantionOrder { get { return expantionOrder; } }

        private void buttonSubir_Click(object sender, EventArgs e)
        {
            if (listBoxExpantionOrder.SelectedIndex != 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (expantionOrder[i] == expantionOrder[listBoxExpantionOrder.SelectedIndex])
                    {
                        int tmp = expantionOrder[i - 1];
                        expantionOrder[i - 1] = expantionOrder[i];
                        expantionOrder[i] = tmp;
                        break;
                    }
                }
                int index = listBoxExpantionOrder.SelectedIndex;
                updateExpantionOrder();
                listBoxExpantionOrder.SelectedIndex = index -1;
            }
        }

        private void buttonBajar_Click(object sender, EventArgs e)
        {
            if (listBoxExpantionOrder.SelectedIndex != 3)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (expantionOrder[i] == expantionOrder[listBoxExpantionOrder.SelectedIndex])
                    {
                        int tmp = expantionOrder[i + 1];
                        expantionOrder[i + 1] = expantionOrder[i];
                        expantionOrder[i] = tmp;
                        break;
                    }
                }
                int index = listBoxExpantionOrder.SelectedIndex;
                updateExpantionOrder();
                listBoxExpantionOrder.SelectedIndex = index + 1;
            }
        }
    }
}
