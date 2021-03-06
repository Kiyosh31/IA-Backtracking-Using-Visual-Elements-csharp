﻿using IA_Backtracking_Using_Visual_Elements.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IA_Backtracking_Using_Visual_Elements
{
    public partial class FormMain : Form
    {
        Map mapa;
        const int CELL_WIDTH = 32;

        SolidBrush brushRed;
        Pen pen;
        Font font;
        Graphics graphics;

        ToolTip toolTip;

        Graphics formGraphics;

        Character character;

        Point currentXY;
        Point initXY;
        Point finalXY;

        bool playing;
        bool isCreated;

        List<Cell> noRepeat;

        List<Move> moveList;

        List<int> expantionOrder;

        Brush alphaGreenBrush;
        Brush alphaOrangeBrush;

        List<Point> visitedBT;
        List<Point> routeBT;

        int slp = 500;

        public FormMain()
        {
            InitializeComponent();
            KeyPreview = true;
            labelRoute.Text = "";
            labelSteps.Text = "";
            panelMap.Width = 0;
            panelMap.Height = 0;

            ButtonTree.Enabled = false;
            checkBoxBacktracking.Enabled = false;
            checkBoxAStar.Enabled = false;
            checkBoxRepeat.Enabled = false;
            buttonPlay.Enabled = false;
            buttonUp.Enabled = false;
            buttonDown.Enabled = false;

            // Inicializa el mapa donde irán las celdas
            mapa = new Map();

            // Graphic elements
            brushRed = new SolidBrush(Color.Red);
            pen = new Pen(Color.Black);

            font = new Font("Arial", 8.0f, FontStyle.Regular);

            currentXY = new Point(-1,-1);
            initXY = new Point(-1, -1);

            playing = false;
            isCreated = false;

            alphaOrangeBrush = new SolidBrush(Color.FromArgb(80, Color.Orange));

            alphaGreenBrush = new SolidBrush(Color.FromArgb(80, Color.Green));

            moveList = new List<Move>();

            formGraphics = this.CreateGraphics();

            expantionOrder = new List<int>();
            expantionOrder.Add(0);
            expantionOrder.Add(1);
            expantionOrder.Add(2);
            expantionOrder.Add(3);
            updateExpantionOrder();

            listBoxExpantionOrder.SelectedIndex = 0;
        }

        private void buttonExamine_Click(object sender, EventArgs e)
        {
            // Inicializa el dialogo de buscar archivo
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = "//";
            openFile.Filter = "All files (*.*)|*.*|Txt files (*.txt)|*.txt";
            openFile.FilterIndex = 2;
            openFile.RestoreDirectory = true;

            // Verifica que se haya seleccionado un archivo
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                // El lector se inicializa con la ruta del archivo
                Reader lector = new Reader(openFile.FileName);
                labelRoute.ForeColor = Color.Black;
                labelRoute.Text = openFile.SafeFileName;

                mapa.Clear();

                // El lector invoca a su método para llenar la estructura mapa
                if (!lector.fillMap(ref mapa))
                {
                    // En caso de fallo, borra todo del mapa y muestra error en el label de ruta
                    mapa.Vaciar();
                    System.Diagnostics.Debug.WriteLine("Algo ha fallado...");
                    labelRoute.ForeColor = Color.Red;
                    labelRoute.Text = "Archivo Inválido!";
                    panelMap.Width = 0;
                    panelMap.Height = 0;
                    formGraphics.Clear(SystemColors.Control);
                }
                finalXY.X = -1;
                finalXY.Y = -1;
            }
            panelMap.Refresh();
        }

        private void panelMap_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                currentXY.X = e.X / CELL_WIDTH;
                currentXY.Y = e.Y / CELL_WIDTH;
                char a = 'A';
                a += (char) currentXY.X;
                labelSelectedX.Text = a.ToString();
                labelSelectedY.Text = (currentXY.Y+1).ToString();
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (toolTip != null)
                {
                    toolTip.Dispose();
                }
                char letter = 'A';
                letter += (char)((e.X / CELL_WIDTH));
                String text = letter + ((e.Y / CELL_WIDTH) + 1).ToString();
                if (mapa[0][0].TerrainName != null)
                {
                    toolTip = new ToolTip();
                    text += '\n' + mapa[e.Y / CELL_WIDTH][e.X / CELL_WIDTH].TerrainName;
                    text += '\n' +mapa[e.Y / CELL_WIDTH][e.X / CELL_WIDTH].listStepsString();
                }

                toolTip = new ToolTip();

                toolTip.Show(text, panelMap, e.X, e.Y - 20, 1000);
            }
        }

        private void GroundButton_Click(object sender, EventArgs e)
        {
            if (mapa.Count < 1)
            {
                MessageBox.Show("Debe cargar un mapa primero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                FormTerrains GroundWindow = new FormTerrains(ref mapa);
                GroundWindow.ShowDialog();
                panelMap.Refresh();
                if (GroundWindow.GetSuccess)
                {
                    noRepeat = GroundWindow.GetNoRepeat;
                }

                buttonCharacter.Enabled = true;
            }
        }

        private void FormMain_Paint(object sender, PaintEventArgs e)
        {
            if (mapa.Count > 0)
            {
                panelMap.Width = mapa[0].Count * CELL_WIDTH;
                panelMap.Height = mapa.Count * CELL_WIDTH;

                char a = 'A';

                formGraphics = this.CreateGraphics();

                // Clear Coordenates
                for (int y = 0; y < 15; y++)
                {
                    formGraphics.FillRectangle(new SolidBrush(SystemColors.Control), panelMap.Location.X - CELL_WIDTH - 1, panelMap.Location.Y + (CELL_WIDTH * y), CELL_WIDTH, CELL_WIDTH);
                }

                for (int x = 0; x < 15; x++)
                {
                    formGraphics.FillRectangle(new SolidBrush(SystemColors.Control), panelMap.Location.X + (x * CELL_WIDTH), panelMap.Location.Y - CELL_WIDTH - 1, CELL_WIDTH, CELL_WIDTH);
                }

                // Draw Coordenates
                for (int y = 0; y < mapa.Count; y++)
                {
                    string text = (y + 1).ToString();
                    formGraphics.DrawString(text, font, brushRed, panelMap.Location.X - CELL_WIDTH - 1, panelMap.Location.Y + (y * CELL_WIDTH) + (CELL_WIDTH / 2) - 7f);
                }

                for (int x = 0; x < mapa[0].Count; x++)
                {
                    string text = a.ToString();
                    formGraphics.DrawString(text, font, brushRed, panelMap.Location.X + (x * CELL_WIDTH), panelMap.Location.Y - (CELL_WIDTH / 2) - 7f);
                    a += (char)1;
                }
            }
        }

        private void panelMap_Paint(object sender, PaintEventArgs e)
        {
            graphics = panelMap.CreateGraphics();
            if (mapa.Count > 0 && mapa[0][0].texture != null)
            {
                for (int i = 0; i < mapa.Count; i++)
                {
                    for (int j = 0; j < mapa[0].Count; j++)
                    {
                        if (mapa[i][j].veiled == false || playing==false || (finalXY.X == j && finalXY.Y == i))
                        {
                            graphics.DrawImage(mapa[i][j].texture, j * CELL_WIDTH, i * CELL_WIDTH);

                            if (initXY.X == j && initXY.Y == i) graphics.FillRectangle(alphaGreenBrush, initXY.X * CELL_WIDTH, initXY.Y * CELL_WIDTH, CELL_WIDTH, CELL_WIDTH);
                            if (finalXY.X == j && finalXY.Y == i) graphics.FillRectangle(alphaOrangeBrush, finalXY.X * CELL_WIDTH, finalXY.Y * CELL_WIDTH, CELL_WIDTH, CELL_WIDTH);

                            graphics.DrawString(mapa[i][j].listStepsString(), font, brushRed, j * CELL_WIDTH, i * CELL_WIDTH);
                        }
                    }
                }
            }

            if (character != null)
            graphics.DrawImage(character.characterImg, character.coordinateX * CELL_WIDTH, character.coordinateY * CELL_WIDTH);
        }

        public void doBackTrack()
        {
            for (int i = 0; i < expantionOrder.Count; i++)
            {
                Thread.Sleep(slp);
                bool moved = false;
                listBoxExpantionOrder.SelectedIndex = i;
                switch (expantionOrder[i])
                {
                    case 0:
                        if (!visitedBT.Contains(new Point(character.coordinateX, character.coordinateY - 1)))
                        {
                            moved = MoveCharacterUp();
                            if (moved)
                            {
                                visitedBT.Add(new Point(character.coordinateX, character.coordinateY));
                                routeBT.Add(new Point(character.coordinateX, character.coordinateY));
                            }
                            if (moved && playing)
                            {
                                doBackTrack();
                            }
                            if (moved && playing)
                            {
                                MoveCharacterDown();
                                routeBT.Remove(routeBT[routeBT.Count-1]);
                                Thread.Sleep(slp);
                            }
                        }
                        break;
                    case 1:
                        if (!visitedBT.Contains(new Point(character.coordinateX, character.coordinateY + 1)))
                        {
                            moved = MoveCharacterDown();
                            if (moved)
                            {
                                visitedBT.Add(new Point(character.coordinateX, character.coordinateY));
                                routeBT.Add(new Point(character.coordinateX, character.coordinateY));
                            }
                            if (moved && playing)
                            {
                                doBackTrack();
                            }
                            if (moved && playing)
                            {
                                MoveCharacterUp();
                                routeBT.Remove(routeBT[routeBT.Count - 1]);
                                Thread.Sleep(slp);
                            }
                        }
                        break;
                    case 2:
                        if (!visitedBT.Contains(new Point(character.coordinateX - 1, character.coordinateY)))
                        {
                            moved = MoveCharacterLeft();
                            if (moved)
                            {
                                visitedBT.Add(new Point(character.coordinateX, character.coordinateY));
                                routeBT.Add(new Point(character.coordinateX, character.coordinateY));
                            }
                            if (moved && playing)
                            {
                                doBackTrack();
                            }
                            if (moved && playing)
                            {
                                MoveCharacterRight();
                                routeBT.Remove(routeBT[routeBT.Count - 1]);
                                Thread.Sleep(slp);
                            }
                        }
                        break;
                    case 3:
                        if (!visitedBT.Contains(new Point(character.coordinateX + 1, character.coordinateY)))
                        {
                            moved = MoveCharacterRight();
                            if (moved)
                            {
                                visitedBT.Add(new Point(character.coordinateX, character.coordinateY));
                                routeBT.Add(new Point(character.coordinateX, character.coordinateY));
                            }
                            if (moved && playing)
                            {
                                doBackTrack();
                            }
                            if (moved && playing)
                            {
                                MoveCharacterLeft();
                                routeBT.Remove(routeBT[routeBT.Count - 1]);
                                Thread.Sleep(slp);
                            }
                        }
                        break;
                }
                if (!playing) break;
            }
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            if (!playing && character != null && finalXY.X>-1)
            {   //Empezar
                moveList.Clear();
                resetMapNumbers();
                buttonExamine.Enabled = false;
                buttonInitialCord.Enabled = false;
                buttonPlay.Text = "STOP";
                GroundButton.Enabled = false;
                buttonCharacter.Enabled = false;
                buttonFinalCoord.Enabled = false;
                buttonUp.Enabled = false;
                buttonDown.Enabled = false;

                ButtonTree.Enabled = true;
                playing = !playing;

                character.currentStep = 1;
                character.coordinateX = initXY.X;
                character.coordinateY = initXY.Y;
                mapa[character.coordinateY][character.coordinateX].listSteps.Add(character.currentStep);
                unveilKnown();
                panelMap.Refresh();

                if(checkBoxBacktracking.Checked == true)
                {
                    visitedBT = new List<Point>();
                    routeBT = new List<Point>();
                    //Backtracking
                    visitedBT.Add(new Point(character.coordinateX, character.coordinateY));
                    routeBT.Add(new Point(character.coordinateX, character.coordinateY));
                    doBackTrack();
                    string result = "";
                    for(int i = 0; i < routeBT.Count; i++)
                    {
                        char c = 'A';
                        c += (char)routeBT[i].X;
                        result += c.ToString() + "," + (routeBT[i].Y + 1);
                        if (i < routeBT.Count - 1) result += " -> ";
                    }
                    labelSteps.Text = result;
                    ButtonTree.PerformClick();
                }
                else if(checkBoxAStar.Checked == true)
                {
                    if (checkBoxRepeat.Checked == true)
                    {
                        //AStar Repeat
                    }
                    else
                    {
                        //AStar no repeat
                        while (playing) {
                            int minimum = -1;
                            double minFn = -1;
                            for (int i =0;i<expantionOrder.Count;i++)
                            {
                                switch (expantionOrder[i])
                                {
                                    //UP
                                    case 0:
                                        for (int j = 0; j < character.idCostList.Count; j++)
                                        {
                                            //Si encuentra el id
                                            if ((character.coordinateY - 1) > -1 && character.idCostList[j].id == mapa[character.coordinateY - 1][character.coordinateX].TerrainId)
                                            {
                                                // y este no es N/A
                                                if (character.idCostList[j].cost > -1)
                                                {
                                                    double manhattan = Math.Sqrt(Math.Pow(finalXY.X - character.coordinateX, 2) + Math.Pow(finalXY.Y - (character.coordinateY-1), 2));
                                                    int child = 0;
                                                    child += checkChild(-1, 1);
                                                    child += checkChild(-1, -1);
                                                    child += checkChild(0, 0);
                                                    child += checkChild(-2, 0);
                                                    moveList.Add(new Move(character.coordinateX, character.coordinateY - 1,child));
                                                    if (minimum == -1)
                                                    {
                                                        minimum = expantionOrder[i];
                                                        minFn = manhattan;
                                                    }
                                                    else if (manhattan < minFn)
                                                    {
                                                        minimum = expantionOrder[i];
                                                        minFn = manhattan;
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                        break;
                                    //DOWN
                                    case 1:
                                        for (int j = 0; j < character.idCostList.Count; j++)
                                        {
                                            //Si encuentra el id
                                            if ((character.coordinateY + 1) < mapa.Count && character.idCostList[j].id == mapa[character.coordinateY + 1][character.coordinateX].TerrainId)
                                            {
                                                // y este no es N/A
                                                if (character.idCostList[j].cost > -1)
                                                {
                                                    double manhattan = Math.Sqrt(Math.Pow(finalXY.X - character.coordinateX, 2) + Math.Pow(finalXY.Y - (character.coordinateY + 1), 2));
                                                    if (minimum == -1)
                                                    {
                                                        minimum = expantionOrder[i];
                                                        minFn = manhattan;
                                                    }
                                                    else if (manhattan < minFn)
                                                    {
                                                        minimum = expantionOrder[i];
                                                        minFn = manhattan;
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                        break;
                                    //LEFT
                                    case 2:
                                        for (int j = 0; j < character.idCostList.Count; j++)
                                        {
                                            //Si encuentra el id
                                            if ((character.coordinateX - 1) > -1 && character.idCostList[j].id == mapa[character.coordinateY][character.coordinateX - 1].TerrainId)
                                            {
                                                // y este no es N/A
                                                if (character.idCostList[j].cost > -1)
                                                {
                                                    double manhattan = Math.Sqrt(Math.Pow(finalXY.X - (character.coordinateX - 1), 2) + Math.Pow(finalXY.Y - character.coordinateY, 2));
                                                    if (minimum == -1)
                                                    {
                                                        minimum = expantionOrder[i];
                                                        minFn = manhattan;
                                                    }
                                                    else if (manhattan < minFn)
                                                    {
                                                        minimum = expantionOrder[i];
                                                        minFn = manhattan;
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                        break;
                                    //RIGHT
                                    case 3:
                                        for (int j = 0; j < character.idCostList.Count; j++)
                                        {
                                            //Si encuentra el id
                                            if ((character.coordinateX + 1) < mapa[0].Count && character.idCostList[j].id == mapa[character.coordinateY][character.coordinateX + 1].TerrainId)
                                            {
                                                // y este no es N/A
                                                if (character.idCostList[j].cost > -1)
                                                {
                                                        double manhattan = Math.Sqrt(Math.Pow(finalXY.X - (character.coordinateX + 1), 2) + Math.Pow(finalXY.Y - character.coordinateY, 2));
                                                        if (minimum == -1)
                                                        {
                                                            minimum = expantionOrder[i];
                                                            minFn = manhattan;
                                                        }
                                                        else if (manhattan < minFn)
                                                        {
                                                            minimum = expantionOrder[i];
                                                            minFn = manhattan;
                                                        }
                                                }
                                                break;
                                            }
                                        }
                                        break;
                                }
                            }
                            switch (minimum)
                            {
                                case 0:
                                    MoveCharacterUp();
                                    break;
                                case 1:
                                    MoveCharacterDown();
                                    break;
                                case 2:
                                    MoveCharacterLeft();
                                    break;
                                case 3:
                                    MoveCharacterRight();
                                    break;
                            }
                            Thread.Sleep(slp);
                        }
                    }
                }

            }
            else if(playing && character != null)
            {   //Dejar de jugar
                buttonExamine.Enabled = true;
                buttonInitialCord.Enabled = true;
                buttonPlay.Text = "Play";
                GroundButton.Enabled = true;
                buttonCharacter.Enabled = true;
                buttonFinalCoord.Enabled = true;
                buttonUp.Enabled = true;
                buttonDown.Enabled = true;
                playing = !playing;

                FormTreeView TreeWindow = new FormTreeView(ref moveList);
                TreeWindow.ShowDialog();
            }
            if(character == null)
            {
                MessageBox.Show("Debe haber un personaje para jugar", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if(finalXY.X == -1)
            {
                MessageBox.Show("Debe haber un estado final", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            panelMap.Refresh();
        }

        private void FormMain_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (playing) {
                switch (e.KeyCode)
                {
                    case Keys.Down:
                    case Keys.Up:
                    case Keys.Left:
                    case Keys.Right:
                        e.IsInputKey = true;
                        break;
                }
            }
        }

        public void CheckFinish()
        {
            double manhattan = Math.Sqrt(Math.Pow(finalXY.X - character.coordinateX,2)+Math.Pow(finalXY.Y - character.coordinateY, 2));

            System.Diagnostics.Debug.WriteLine("Manhattan: "+manhattan);

            if (character.coordinateX == finalXY.X && character.coordinateY == finalXY.Y && playing)
            {
                MessageBox.Show("Llegaste al punto final", "Finished!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                buttonPlay.PerformClick();
            }
        }

        private bool MoveCharacterLeft()
        {
            bool moved = false;
            //Revisa la lista del personaje
            for (int i = 0; i < character.idCostList.Count; i++)
            {
                //Si encuentra el id
                if ((character.coordinateX - 1) > -1 && character.idCostList[i].id == mapa[character.coordinateY][character.coordinateX - 1].TerrainId)
                {
                    // y este no es N/A
                    if (character.idCostList[i].cost > -1)
                    {
                        character.coordinateX -= 1;
                        moved = true;
                    }
                    break;
                }
            }

            if (moved)
            {
                character.currentStep++;
                mapa[character.coordinateY][character.coordinateX].listSteps.Add(character.currentStep);
                unveilKnown();
                panelMap.Refresh();
            }

            CheckFinish();

            return moved;
        }
        private bool MoveCharacterRight()
        {
            bool moved = false;
            //Revisa la lista del personaje
            for (int i = 0; i < character.idCostList.Count; i++)
            {
                //Si encuentra el id
                if ((character.coordinateX + 1) < mapa[0].Count && character.idCostList[i].id == mapa[character.coordinateY][character.coordinateX + 1].TerrainId)
                {
                    // y este no es N/A
                    if (character.idCostList[i].cost > -1)
                    {
                        character.coordinateX += 1;
                        moved = true;
                    }
                    break;
                }
            }

            if (moved)
            {
                character.currentStep++;
                mapa[character.coordinateY][character.coordinateX].listSteps.Add(character.currentStep);
                unveilKnown();
                panelMap.Refresh();
            }

            CheckFinish();

            return moved;
        }
        private bool MoveCharacterUp()
        {
            bool moved = false;
            //Revisa la lista del personaje
            for (int i = 0; i < character.idCostList.Count; i++)
            {
                //Si encuentra el id
                if ((character.coordinateY - 1) > -1 && character.idCostList[i].id == mapa[character.coordinateY - 1][character.coordinateX].TerrainId)
                {
                    // y este no es N/A
                    if (character.idCostList[i].cost > -1)
                    {
                        character.coordinateY -= 1;
                        moved = true;
                    }
                    break;
                }
            }

            if (moved)
            {
                character.currentStep++;
                mapa[character.coordinateY][character.coordinateX].listSteps.Add(character.currentStep);
                unveilKnown();
                panelMap.Refresh();
            }

            CheckFinish();

            return moved;
        }
        private bool MoveCharacterDown()
        {
            bool moved = false;
            //Revisa la lista del personaje
            for (int i = 0; i < character.idCostList.Count; i++)
            {
                //Si encuentra el id
                if ((character.coordinateY + 1) < mapa.Count && character.idCostList[i].id == mapa[character.coordinateY + 1][character.coordinateX].TerrainId)
                {
                    // y este no es N/A
                    if (character.idCostList[i].cost > -1)
                    {
                        character.coordinateY += 1;
                        moved = true;
                    }
                    break;
                }
            }

            if (moved)
            {
                character.currentStep++;
                mapa[character.coordinateY][character.coordinateX].listSteps.Add(character.currentStep);
                unveilKnown();
                panelMap.Refresh();
            }

            CheckFinish();

            return moved;
        }


        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (character != null)
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        MoveCharacterLeft();
                        break;
                    case Keys.Right:
                        MoveCharacterRight();
                        break;
                    case Keys.Up:
                        MoveCharacterUp();
                        break;
                    case Keys.Down:
                        MoveCharacterDown();
                        break;
                }
            }
        }

        private void buttonCharacter_Click(object sender, EventArgs e)
        {
            if (mapa[0][0].texture != null)
            {
                FormNewCharacter NewCharacterWindow = new FormNewCharacter(ref noRepeat);
                NewCharacterWindow.ShowDialog();

                isCreated = NewCharacterWindow.GetisCreated;
                if(isCreated)
                {
                    character = NewCharacterWindow.GetCharacter;

                    buttonCharacter.Enabled = true;
                    buttonFinalCoord.Enabled = true;
                    buttonInitialCord.Enabled = true;

                    buttonUp.Enabled = true;
                    buttonDown.Enabled = true;
                    
                    panelMap.Refresh();
                }
                else
                {
                    MessageBox.Show("Personaje no creado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Debe elegir los tipos de terreno", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonFinalCoord_Click(object sender, EventArgs e)
        {
            bool selectable = false;

            if (mapa[0][0].texture != null)
            {
                for (int i = 0; i < character.idCostList.Count; i++)
                {
                    if (mapa[currentXY.Y][currentXY.X].TerrainId == character.idCostList[i].id && character.idCostList[i].cost > -1)
                    {
                        finalXY.X = currentXY.X;
                        finalXY.Y = currentXY.Y;

                        selectable = true;
                        break;
                    }
                }
                if (!selectable)
                {
                    MessageBox.Show("Coordenada invalida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    panelMap.Refresh();
                    MessageBox.Show("Posicion final: " + labelSelectedX.Text + "," + labelSelectedY.Text, "Posicion final seleccionada!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    checkBoxBacktracking.Enabled = true;
                    checkBoxAStar.Enabled = true;
                    checkBoxRepeat.Enabled = true;
                }
            }
        }

        private void buttonInitialCord_Click(object sender, EventArgs e)
        {
            bool selectable = false;

            if (mapa[0][0].texture != null)
            {
                for (int i = 0; i < character.idCostList.Count; i++)
                {
                    if (mapa[currentXY.Y][currentXY.X].TerrainId == character.idCostList[i].id && character.idCostList[i].cost > -1)
                    {
                        initXY.X = currentXY.X;
                        initXY.Y = currentXY.Y;

                        character.coordinateX = currentXY.X;
                        character.coordinateY = currentXY.Y;
                        selectable = true;
                        break;
                    }
                }

                if (!selectable)
                {
                    MessageBox.Show("Coordenada invalida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    panelMap.Refresh();
                    MessageBox.Show("Posicion inicial: " + labelSelectedX.Text + "," + labelSelectedY.Text, "Posicion inicial seleccionada!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        public void resetMapNumbers()
        {
            for(int i = 0; i < mapa.Count; i++)
            {
                for (int j = 0; j< mapa[0].Count; j++)
                {
                    mapa[i][j].listSteps = new List<int>();
                    mapa[i][j].veiled = true;
                }
            }
        }

        public void unveilKnown()
        {
            int child = 0;
            //Down
            if (character.coordinateY + 1 < mapa.Count)
            {
                mapa[character.coordinateY + 1][character.coordinateX].veiled = false;
                //validate if it can walk
                child += checkChild(0, 1);
            }
            //Up
            if (character.coordinateY - 1 > -1)
            {
                mapa[character.coordinateY - 1][character.coordinateX].veiled = false;
                child += checkChild(0, -1);
            }
            //Right
            if (character.coordinateX + 1 < mapa[0].Count)
            {
                mapa[character.coordinateY][character.coordinateX + 1].veiled = false;
                child += checkChild(1, 0);
            }
            //Left
            if (character.coordinateX - 1 > -1)
            {
                mapa[character.coordinateY][character.coordinateX - 1].veiled = false;
                child += checkChild(-1, 0);
            }
            //Center
            mapa[character.coordinateY][character.coordinateX].veiled = false;

            moveList.Add(new Move(character.coordinateX, character.coordinateY,child,character.currentStep));
        }

        private void ButtonTree_Click(object sender, EventArgs e)
        {
            FormTreeView TreeWIndow = new FormTreeView(ref moveList);
            TreeWIndow.ShowDialog();
            buttonPlay.Focus();
        }

        public int checkChild(int x,int y)
        {
            int child = 0;
            for (int i = 0; i < character.idCostList.Count; i++)
            {
                //Si encuentra el id
                if (character.idCostList[i].id == mapa[character.coordinateY + y][character.coordinateX + x].TerrainId)
                {
                    if (character.idCostList[i].cost > -1)
                    {
                        child = 1;
                    }
                    break;
                }
            }
            return child;
        }

        private void checkBoxBacktracking_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBacktracking.Checked)
            {
                checkBoxAStar.Checked = false;
                buttonPlay.Enabled = true;
            }
            else
            {
                buttonPlay.Enabled = false;
            }
        }

        private void checkBoxAStar_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAStar.Checked)
            {
                checkBoxBacktracking.Checked = false;
                buttonPlay.Enabled = true;
            }
            else
            {
                buttonPlay.Enabled = false;
            }
        }

        // ALGORITMO DE BACKTRACKING OMFG YEAH!!! :3 <3

        public void AStar()
        {

        }

        public void backtracking()
        {
            
        }

        private void buttonUp_Click(object sender, EventArgs e)
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
                listBoxExpantionOrder.SelectedIndex = index - 1;
            }
        }

        private void buttonDown_Click(object sender, EventArgs e)
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
    }
}
