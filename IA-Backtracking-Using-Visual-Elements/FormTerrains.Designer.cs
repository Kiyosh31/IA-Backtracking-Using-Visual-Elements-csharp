﻿namespace IA_Backtracking_Using_Visual_Elements
{
    partial class FormTerrains
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.NextButton = new System.Windows.Forms.Button();
            this.TypeGroundLabel = new System.Windows.Forms.Label();
            this.GroundTypeComboBox = new System.Windows.Forms.ComboBox();
            this.GroundListBox = new System.Windows.Forms.ListBox();
            this.pictureBoxTerrain = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTerrain)).BeginInit();
            this.SuspendLayout();
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(224, 304);
            this.NextButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(62, 48);
            this.NextButton.TabIndex = 10;
            this.NextButton.Text = "Siguiente";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // TypeGroundLabel
            // 
            this.TypeGroundLabel.AutoSize = true;
            this.TypeGroundLabel.Location = new System.Drawing.Point(156, 44);
            this.TypeGroundLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TypeGroundLabel.Name = "TypeGroundLabel";
            this.TypeGroundLabel.Size = new System.Drawing.Size(108, 13);
            this.TypeGroundLabel.TabIndex = 9;
            this.TypeGroundLabel.Text = "Elija el tipo de terreno";
            // 
            // GroundTypeComboBox
            // 
            this.GroundTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GroundTypeComboBox.FormattingEnabled = true;
            this.GroundTypeComboBox.Items.AddRange(new object[] {
            "Tierra",
            "Agua",
            "Pradera",
            "Bosque",
            "Montaña",
            "Desierto",
            "Pantano",
            "Jungla",
            "Lava"});
            this.GroundTypeComboBox.Location = new System.Drawing.Point(156, 63);
            this.GroundTypeComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GroundTypeComboBox.Name = "GroundTypeComboBox";
            this.GroundTypeComboBox.Size = new System.Drawing.Size(110, 21);
            this.GroundTypeComboBox.TabIndex = 8;
            this.GroundTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.GroundTypeComboBox_SelectedIndexChanged);
            // 
            // GroundListBox
            // 
            this.GroundListBox.FormattingEnabled = true;
            this.GroundListBox.Location = new System.Drawing.Point(11, 11);
            this.GroundListBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GroundListBox.Name = "GroundListBox";
            this.GroundListBox.Size = new System.Drawing.Size(130, 342);
            this.GroundListBox.TabIndex = 7;
            // 
            // pictureBoxTerrain
            // 
            this.pictureBoxTerrain.Location = new System.Drawing.Point(158, 131);
            this.pictureBoxTerrain.Name = "pictureBoxTerrain";
            this.pictureBoxTerrain.Size = new System.Drawing.Size(128, 128);
            this.pictureBoxTerrain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxTerrain.TabIndex = 14;
            this.pictureBoxTerrain.TabStop = false;
            // 
            // FormTerrains
            // 
            this.AcceptButton = this.NextButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 365);
            this.Controls.Add(this.pictureBoxTerrain);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.TypeGroundLabel);
            this.Controls.Add(this.GroundTypeComboBox);
            this.Controls.Add(this.GroundListBox);
            this.Name = "FormTerrains";
            this.Text = "FormTerrains";
            this.Load += new System.EventHandler(this.FormTerrains_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTerrain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Label TypeGroundLabel;
        private System.Windows.Forms.ComboBox GroundTypeComboBox;
        private System.Windows.Forms.ListBox GroundListBox;
        private System.Windows.Forms.PictureBox pictureBoxTerrain;
    }
}