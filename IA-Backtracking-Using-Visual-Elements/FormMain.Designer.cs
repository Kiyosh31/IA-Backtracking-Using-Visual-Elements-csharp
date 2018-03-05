﻿namespace IA_Backtracking_Using_Visual_Elements
{
    partial class FormMain
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelMap = new System.Windows.Forms.Panel();
            this.labelRoute = new System.Windows.Forms.Label();
            this.buttonExamine = new System.Windows.Forms.Button();
            this.GroundButton = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.buttonCharacter = new System.Windows.Forms.Button();
            this.labelCurrentXY = new System.Windows.Forms.Label();
            this.labelSelectedX = new System.Windows.Forms.Label();
            this.labelSelectedY = new System.Windows.Forms.Label();
            this.buttonFinalCoord = new System.Windows.Forms.Button();
            this.buttonInitialCord = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelMap
            // 
            this.panelMap.BackColor = System.Drawing.Color.White;
            this.panelMap.Location = new System.Drawing.Point(44, 73);
            this.panelMap.Name = "panelMap";
            this.panelMap.Size = new System.Drawing.Size(480, 480);
            this.panelMap.TabIndex = 0;
            this.panelMap.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMap_Paint);
            this.panelMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelMap_MouseClick);
            // 
            // labelRoute
            // 
            this.labelRoute.AutoSize = true;
            this.labelRoute.Location = new System.Drawing.Point(93, 17);
            this.labelRoute.Name = "labelRoute";
            this.labelRoute.Size = new System.Drawing.Size(56, 13);
            this.labelRoute.TabIndex = 0;
            this.labelRoute.Text = "LabelRuta";
            // 
            // buttonExamine
            // 
            this.buttonExamine.Location = new System.Drawing.Point(12, 12);
            this.buttonExamine.Name = "buttonExamine";
            this.buttonExamine.Size = new System.Drawing.Size(75, 23);
            this.buttonExamine.TabIndex = 1;
            this.buttonExamine.TabStop = false;
            this.buttonExamine.Text = "Examinar";
            this.buttonExamine.UseVisualStyleBackColor = true;
            this.buttonExamine.Click += new System.EventHandler(this.buttonExamine_Click);
            this.buttonExamine.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.FormMain_PreviewKeyDown);
            // 
            // GroundButton
            // 
            this.GroundButton.Location = new System.Drawing.Point(587, 73);
            this.GroundButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GroundButton.Name = "GroundButton";
            this.GroundButton.Size = new System.Drawing.Size(100, 34);
            this.GroundButton.TabIndex = 3;
            this.GroundButton.TabStop = false;
            this.GroundButton.Text = "Terrenos";
            this.GroundButton.UseVisualStyleBackColor = true;
            this.GroundButton.Click += new System.EventHandler(this.GroundButton_Click);
            this.GroundButton.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.FormMain_PreviewKeyDown);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Location = new System.Drawing.Point(587, 338);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(100, 34);
            this.buttonPlay.TabIndex = 4;
            this.buttonPlay.TabStop = false;
            this.buttonPlay.Text = "Play";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            this.buttonPlay.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.FormMain_PreviewKeyDown);
            // 
            // buttonCharacter
            // 
            this.buttonCharacter.Enabled = false;
            this.buttonCharacter.Location = new System.Drawing.Point(587, 112);
            this.buttonCharacter.Name = "buttonCharacter";
            this.buttonCharacter.Size = new System.Drawing.Size(100, 34);
            this.buttonCharacter.TabIndex = 5;
            this.buttonCharacter.TabStop = false;
            this.buttonCharacter.Text = "Character";
            this.buttonCharacter.UseVisualStyleBackColor = true;
            this.buttonCharacter.Click += new System.EventHandler(this.buttonCharacter_Click);
            this.buttonCharacter.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.FormMain_PreviewKeyDown);
            // 
            // labelCurrentXY
            // 
            this.labelCurrentXY.AutoSize = true;
            this.labelCurrentXY.Location = new System.Drawing.Point(575, 162);
            this.labelCurrentXY.Name = "labelCurrentXY";
            this.labelCurrentXY.Size = new System.Drawing.Size(87, 13);
            this.labelCurrentXY.TabIndex = 9;
            this.labelCurrentXY.Text = "Posición elegida:";
            // 
            // labelSelectedX
            // 
            this.labelSelectedX.AutoSize = true;
            this.labelSelectedX.Location = new System.Drawing.Point(668, 162);
            this.labelSelectedX.Name = "labelSelectedX";
            this.labelSelectedX.Size = new System.Drawing.Size(14, 13);
            this.labelSelectedX.TabIndex = 10;
            this.labelSelectedX.Text = "A";
            // 
            // labelSelectedY
            // 
            this.labelSelectedY.AutoSize = true;
            this.labelSelectedY.Location = new System.Drawing.Point(681, 162);
            this.labelSelectedY.Name = "labelSelectedY";
            this.labelSelectedY.Size = new System.Drawing.Size(13, 13);
            this.labelSelectedY.TabIndex = 11;
            this.labelSelectedY.Text = "0";
            // 
            // buttonFinalCoord
            // 
            this.buttonFinalCoord.Enabled = false;
            this.buttonFinalCoord.Location = new System.Drawing.Point(639, 193);
            this.buttonFinalCoord.Name = "buttonFinalCoord";
            this.buttonFinalCoord.Size = new System.Drawing.Size(75, 34);
            this.buttonFinalCoord.TabIndex = 12;
            this.buttonFinalCoord.TabStop = false;
            this.buttonFinalCoord.Text = "Coordenada final";
            this.buttonFinalCoord.UseVisualStyleBackColor = true;
            this.buttonFinalCoord.Click += new System.EventHandler(this.buttonFinalCoord_Click);
            // 
            // buttonInitialCord
            // 
            this.buttonInitialCord.Enabled = false;
            this.buttonInitialCord.Location = new System.Drawing.Point(562, 193);
            this.buttonInitialCord.Name = "buttonInitialCord";
            this.buttonInitialCord.Size = new System.Drawing.Size(75, 34);
            this.buttonInitialCord.TabIndex = 13;
            this.buttonInitialCord.TabStop = false;
            this.buttonInitialCord.Text = "Coordenada inicial";
            this.buttonInitialCord.UseVisualStyleBackColor = true;
            this.buttonInitialCord.Click += new System.EventHandler(this.buttonInitialCord_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 609);
            this.Controls.Add(this.buttonInitialCord);
            this.Controls.Add(this.buttonFinalCoord);
            this.Controls.Add(this.labelSelectedY);
            this.Controls.Add(this.labelSelectedX);
            this.Controls.Add(this.labelCurrentXY);
            this.Controls.Add(this.buttonCharacter);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.GroundButton);
            this.Controls.Add(this.buttonExamine);
            this.Controls.Add(this.labelRoute);
            this.Controls.Add(this.panelMap);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMain";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormMain_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.FormMain_PreviewKeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelMap;
        private System.Windows.Forms.Label labelRoute;
        private System.Windows.Forms.Button buttonExamine;
        private System.Windows.Forms.Button GroundButton;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Button buttonCharacter;
        private System.Windows.Forms.Label labelCurrentXY;
        private System.Windows.Forms.Label labelSelectedX;
        private System.Windows.Forms.Label labelSelectedY;
        private System.Windows.Forms.Button buttonFinalCoord;
        private System.Windows.Forms.Button buttonInitialCord;
    }
}

