using MCSkinn.Scripts.Languages;

namespace MCSkinn.Forms
{
    partial class DontAskAgain
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
            button1 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            languageProvider1 = new LanguageProvider();
            checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)languageProvider1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            button1.Location = new System.Drawing.Point(381, 225);
            button1.Margin = new System.Windows.Forms.Padding(6);
            button1.Name = "button1";
            languageProvider1.SetPropertyNames(button1, "Text");
            button1.Size = new System.Drawing.Size(138, 42);
            button1.TabIndex = 0;
            button1.Text = "C_NO";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            button2.Location = new System.Drawing.Point(233, 225);
            button2.Margin = new System.Windows.Forms.Padding(6);
            button2.Name = "button2";
            languageProvider1.SetPropertyNames(button2, "Text");
            button2.Size = new System.Drawing.Size(138, 42);
            button2.TabIndex = 1;
            button2.Text = "C_YES";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label1.Location = new System.Drawing.Point(22, 17);
            label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            label1.Name = "label1";
            languageProvider1.SetPropertyNames(label1, "Text");
            label1.Size = new System.Drawing.Size(710, 150);
            label1.TabIndex = 3;
            label1.Text = "M_IRREVERSIBLE";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // languageProvider1
            // 
            languageProvider1.BaseControl = this;
            // 
            // checkBox1
            // 
            checkBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            checkBox1.AutoSize = true;
            checkBox1.Location = new System.Drawing.Point(381, 172);
            checkBox1.Margin = new System.Windows.Forms.Padding(6);
            checkBox1.Name = "checkBox1";
            languageProvider1.SetPropertyNames(checkBox1, "Text");
            checkBox1.Size = new System.Drawing.Size(146, 28);
            checkBox1.TabIndex = 2;
            checkBox1.Text = "D_DONTASK";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // DontAskAgain
            // 
            AcceptButton = button2;
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = button1;
            ClientSize = new System.Drawing.Size(754, 279);
            Controls.Add(checkBox1);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin = new System.Windows.Forms.Padding(6);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DontAskAgain";
            ShowInTaskbar = false;
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            Load += DontAskAgain_Load;
            ((System.ComponentModel.ISupportInitialize)languageProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private LanguageProvider languageProvider1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}