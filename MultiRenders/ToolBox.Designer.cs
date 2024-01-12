namespace MultiRenders
{
    partial class ToolBox
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
            rbtn_positionColor = new System.Windows.Forms.RadioButton();
            rbtn_dynamicLight = new System.Windows.Forms.RadioButton();
            rbtn_moveCube = new System.Windows.Forms.RadioButton();
            btn_resetP = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // rbtn_positionColor
            // 
            rbtn_positionColor.AutoSize = true;
            rbtn_positionColor.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            rbtn_positionColor.Location = new System.Drawing.Point(53, 45);
            rbtn_positionColor.Name = "rbtn_positionColor";
            rbtn_positionColor.Size = new System.Drawing.Size(131, 23);
            rbtn_positionColor.TabIndex = 0;
            rbtn_positionColor.TabStop = true;
            rbtn_positionColor.Text = "Color By Position";
            rbtn_positionColor.UseVisualStyleBackColor = true;
            rbtn_positionColor.CheckedChanged += rbtn_positionColor_CheckedChanged;
            // 
            // rbtn_dynamicLight
            // 
            rbtn_dynamicLight.AutoSize = true;
            rbtn_dynamicLight.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            rbtn_dynamicLight.Location = new System.Drawing.Point(53, 94);
            rbtn_dynamicLight.Name = "rbtn_dynamicLight";
            rbtn_dynamicLight.Size = new System.Drawing.Size(170, 23);
            rbtn_dynamicLight.TabIndex = 1;
            rbtn_dynamicLight.TabStop = true;
            rbtn_dynamicLight.Text = "Dynamic Light Specular";
            rbtn_dynamicLight.UseVisualStyleBackColor = true;
            rbtn_dynamicLight.CheckedChanged += rbtn_dynamicLight_CheckedChanged;
            // 
            // rbtn_moveCube
            // 
            rbtn_moveCube.AutoSize = true;
            rbtn_moveCube.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            rbtn_moveCube.Location = new System.Drawing.Point(53, 184);
            rbtn_moveCube.Name = "rbtn_moveCube";
            rbtn_moveCube.Size = new System.Drawing.Size(162, 23);
            rbtn_moveCube.TabIndex = 2;
            rbtn_moveCube.TabStop = true;
            rbtn_moveCube.Text = "Move Cube To Sphere";
            rbtn_moveCube.UseVisualStyleBackColor = true;
            rbtn_moveCube.CheckedChanged += rbtn_moveCube_CheckedChanged;
            // 
            // btn_resetP
            // 
            btn_resetP.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_resetP.Location = new System.Drawing.Point(73, 123);
            btn_resetP.Name = "btn_resetP";
            btn_resetP.Size = new System.Drawing.Size(150, 35);
            btn_resetP.TabIndex = 3;
            btn_resetP.Text = "Reset Light Position";
            btn_resetP.UseVisualStyleBackColor = true;
            btn_resetP.Click += btn_resetP_Click;
            // 
            // ToolBox
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(290, 278);
            Controls.Add(btn_resetP);
            Controls.Add(rbtn_moveCube);
            Controls.Add(rbtn_dynamicLight);
            Controls.Add(rbtn_positionColor);
            Name = "ToolBox";
            Text = "Tool Box";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.RadioButton rbtn_positionColor;
        private System.Windows.Forms.RadioButton rbtn_dynamicLight;
        private System.Windows.Forms.RadioButton rbtn_moveCube;
        private System.Windows.Forms.Button btn_resetP;
    }
}