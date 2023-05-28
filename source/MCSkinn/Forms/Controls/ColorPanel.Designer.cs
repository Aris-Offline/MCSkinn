using Inkore.Coreworks.Localization;
using MCSkinn.Scripts.Paril.Controls.Color;

namespace MCSkinn.Forms.Controls
{
    partial class ColorPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            colorTabControl = new System.Windows.Forms.TabControl();
            rgbTabPage = new System.Windows.Forms.TabPage();
            panel2 = new System.Windows.Forms.Panel();
            alphaColorSlider = new MB.Controls.ColorSlider();
            colorPreview1 = new ColorPreview();
            colorPreview2 = new ColorPreview();
            textBox1 = new System.Windows.Forms.TextBox();
            redColorSlider = new MB.Controls.ColorSlider();
            label9 = new System.Windows.Forms.Label();
            blueColorSlider = new MB.Controls.ColorSlider();
            redNumericUpDown = new System.Windows.Forms.NumericUpDown();
            greenColorSlider = new MB.Controls.ColorSlider();
            label2 = new System.Windows.Forms.Label();
            greenNumericUpDown = new System.Windows.Forms.NumericUpDown();
            alphaNumericUpDown = new System.Windows.Forms.NumericUpDown();
            label3 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            blueNumericUpDown = new System.Windows.Forms.NumericUpDown();
            label4 = new System.Windows.Forms.Label();
            colorPick1 = new Scripts.lemon42.Colors.ColorPick();
            hsvTabPage = new System.Windows.Forms.TabPage();
            panel3 = new System.Windows.Forms.Panel();
            hueNumericUpDown = new System.Windows.Forms.NumericUpDown();
            hueColorSlider = new MB.Controls.ColorSlider();
            label6 = new System.Windows.Forms.Label();
            valueColorSlider = new MB.Controls.ColorSlider();
            saturationNumericUpDown = new System.Windows.Forms.NumericUpDown();
            saturationColorSlider = new MB.Controls.ColorSlider();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            valueNumericUpDown = new System.Windows.Forms.NumericUpDown();
            swatchTabPage = new System.Windows.Forms.TabPage();
            panel1 = new System.Windows.Forms.Panel();
            loadingSwatchLabel = new System.Windows.Forms.Label();
            swatchContainer = new SwatchContainer();
            languageProvider1 = new LanguageProvider();
            colorTabControl.SuspendLayout();
            rgbTabPage.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)redNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)greenNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)alphaNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)blueNumericUpDown).BeginInit();
            hsvTabPage.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)hueNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)saturationNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)valueNumericUpDown).BeginInit();
            swatchTabPage.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)languageProvider1).BeginInit();
            SuspendLayout();
            // 
            // colorTabControl
            // 
            colorTabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            colorTabControl.Controls.Add(rgbTabPage);
            colorTabControl.Controls.Add(hsvTabPage);
            colorTabControl.Controls.Add(swatchTabPage);
            colorTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            colorTabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            colorTabControl.ItemSize = new System.Drawing.Size(0, 1);
            colorTabControl.Location = new System.Drawing.Point(0, 0);
            colorTabControl.Margin = new System.Windows.Forms.Padding(4);
            colorTabControl.Name = "colorTabControl";
            colorTabControl.SelectedIndex = 0;
            colorTabControl.Size = new System.Drawing.Size(300, 300);
            colorTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            colorTabControl.TabIndex = 2;
            colorTabControl.SelectedIndexChanged += colorTabControl_SelectedIndexChanged;
            // 
            // rgbTabPage
            // 
            rgbTabPage.Controls.Add(panel2);
            rgbTabPage.Location = new System.Drawing.Point(4, 5);
            rgbTabPage.Margin = new System.Windows.Forms.Padding(0);
            rgbTabPage.Name = "rgbTabPage";
            rgbTabPage.Size = new System.Drawing.Size(292, 291);
            rgbTabPage.TabIndex = 1;
            rgbTabPage.Text = "RGBA";
            rgbTabPage.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.BackColor = System.Drawing.SystemColors.Window;
            panel2.Controls.Add(alphaColorSlider);
            panel2.Controls.Add(colorPreview1);
            panel2.Controls.Add(colorPreview2);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(redColorSlider);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(blueColorSlider);
            panel2.Controls.Add(redNumericUpDown);
            panel2.Controls.Add(greenColorSlider);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(greenNumericUpDown);
            panel2.Controls.Add(alphaNumericUpDown);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(blueNumericUpDown);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(colorPick1);
            panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            panel2.Location = new System.Drawing.Point(0, 0);
            panel2.Margin = new System.Windows.Forms.Padding(0);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(292, 291);
            panel2.TabIndex = 1;
            // 
            // alphaColorSlider
            // 
            alphaColorSlider.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            alphaColorSlider.BackColor = System.Drawing.Color.Transparent;
            alphaColorSlider.LargeChange = 5U;
            alphaColorSlider.Location = new System.Drawing.Point(28, 256);
            alphaColorSlider.Margin = new System.Windows.Forms.Padding(4);
            alphaColorSlider.Maximum = 255;
            alphaColorSlider.Name = "alphaColorSlider";
            alphaColorSlider.Size = new System.Drawing.Size(202, 26);
            alphaColorSlider.SmallChange = 1U;
            alphaColorSlider.TabIndex = 22;
            alphaColorSlider.Text = "Alpha";
            alphaColorSlider.Scroll += alphaColorSlider_Scroll;
            // 
            // colorPreview1
            // 
            colorPreview1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            colorPreview1.Location = new System.Drawing.Point(214, 27);
            colorPreview1.Margin = new System.Windows.Forms.Padding(4);
            colorPreview1.Name = "colorPreview1";
            colorPreview1.Size = new System.Drawing.Size(40, 40);
            colorPreview1.TabIndex = 17;
            colorPreview1.Text = "colorPreview1";
            colorPreview1.Click += colorPreview1_Click;
            // 
            // colorPreview2
            // 
            colorPreview2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            colorPreview2.Location = new System.Drawing.Point(232, 46);
            colorPreview2.Margin = new System.Windows.Forms.Padding(4);
            colorPreview2.Name = "colorPreview2";
            colorPreview2.Size = new System.Drawing.Size(40, 40);
            colorPreview2.TabIndex = 20;
            colorPreview2.Text = "colorPreview2";
            colorPreview2.Click += colorPreview2_Click;
            // 
            // textBox1
            // 
            textBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            textBox1.Location = new System.Drawing.Point(216, 147);
            textBox1.Margin = new System.Windows.Forms.Padding(4);
            textBox1.MaxLength = 9;
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(67, 30);
            textBox1.TabIndex = 19;
            textBox1.Text = "FFFFFFFF";
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // redColorSlider
            // 
            redColorSlider.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            redColorSlider.BackColor = System.Drawing.Color.Transparent;
            redColorSlider.LargeChange = 5U;
            redColorSlider.Location = new System.Drawing.Point(28, 177);
            redColorSlider.Margin = new System.Windows.Forms.Padding(4);
            redColorSlider.Maximum = 255;
            redColorSlider.Name = "redColorSlider";
            redColorSlider.Size = new System.Drawing.Size(202, 26);
            redColorSlider.SmallChange = 1U;
            redColorSlider.TabIndex = 12;
            redColorSlider.Text = "colorSlider1";
            redColorSlider.Scroll += redColorSlider_Scroll;
            // 
            // label9
            // 
            label9.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(198, 151);
            label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(21, 24);
            label9.TabIndex = 18;
            label9.Text = "#";
            // 
            // blueColorSlider
            // 
            blueColorSlider.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            blueColorSlider.BackColor = System.Drawing.Color.Transparent;
            blueColorSlider.LargeChange = 5U;
            blueColorSlider.Location = new System.Drawing.Point(28, 230);
            blueColorSlider.Margin = new System.Windows.Forms.Padding(4);
            blueColorSlider.Maximum = 255;
            blueColorSlider.Name = "blueColorSlider";
            blueColorSlider.Size = new System.Drawing.Size(202, 26);
            blueColorSlider.SmallChange = 1U;
            blueColorSlider.TabIndex = 13;
            blueColorSlider.Text = "colorSlider2";
            blueColorSlider.Scroll += blueColorSlider_Scroll;
            // 
            // redNumericUpDown
            // 
            redNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            redNumericUpDown.Location = new System.Drawing.Point(237, 177);
            redNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            redNumericUpDown.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            redNumericUpDown.Name = "redNumericUpDown";
            redNumericUpDown.Size = new System.Drawing.Size(47, 30);
            redNumericUpDown.TabIndex = 1;
            redNumericUpDown.Value = new decimal(new int[] { 255, 0, 0, 0 });
            redNumericUpDown.ValueChanged += redNumericUpDown_ValueChanged;
            // 
            // greenColorSlider
            // 
            greenColorSlider.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            greenColorSlider.BackColor = System.Drawing.Color.Transparent;
            greenColorSlider.LargeChange = 5U;
            greenColorSlider.Location = new System.Drawing.Point(28, 204);
            greenColorSlider.Margin = new System.Windows.Forms.Padding(4);
            greenColorSlider.Maximum = 255;
            greenColorSlider.Name = "greenColorSlider";
            greenColorSlider.Size = new System.Drawing.Size(202, 26);
            greenColorSlider.SmallChange = 1U;
            greenColorSlider.TabIndex = 14;
            greenColorSlider.Text = "colorSlider3";
            greenColorSlider.Scroll += greenColorSlider_Scroll;
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(4, 180);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(22, 24);
            label2.TabIndex = 2;
            label2.Text = "R";
            // 
            // greenNumericUpDown
            // 
            greenNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            greenNumericUpDown.Location = new System.Drawing.Point(237, 204);
            greenNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            greenNumericUpDown.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            greenNumericUpDown.Name = "greenNumericUpDown";
            greenNumericUpDown.Size = new System.Drawing.Size(47, 30);
            greenNumericUpDown.TabIndex = 3;
            greenNumericUpDown.Value = new decimal(new int[] { 255, 0, 0, 0 });
            greenNumericUpDown.ValueChanged += greenNumericUpDown_ValueChanged;
            // 
            // alphaNumericUpDown
            // 
            alphaNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            alphaNumericUpDown.Location = new System.Drawing.Point(237, 256);
            alphaNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            alphaNumericUpDown.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            alphaNumericUpDown.Name = "alphaNumericUpDown";
            alphaNumericUpDown.Size = new System.Drawing.Size(47, 30);
            alphaNumericUpDown.TabIndex = 7;
            alphaNumericUpDown.Value = new decimal(new int[] { 255, 0, 0, 0 });
            alphaNumericUpDown.ValueChanged += alphaNumericUpDown_ValueChanged;
            // 
            // label3
            // 
            label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(4, 206);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(23, 24);
            label3.TabIndex = 4;
            label3.Text = "G";
            // 
            // label5
            // 
            label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(4, 259);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(23, 24);
            label5.TabIndex = 8;
            label5.Text = "A";
            // 
            // blueNumericUpDown
            // 
            blueNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            blueNumericUpDown.Location = new System.Drawing.Point(237, 230);
            blueNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            blueNumericUpDown.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            blueNumericUpDown.Name = "blueNumericUpDown";
            blueNumericUpDown.Size = new System.Drawing.Size(47, 30);
            blueNumericUpDown.TabIndex = 5;
            blueNumericUpDown.Value = new decimal(new int[] { 255, 0, 0, 0 });
            blueNumericUpDown.ValueChanged += blueNumericUpDown_ValueChanged;
            // 
            // label4
            // 
            label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(4, 232);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(21, 24);
            label4.TabIndex = 6;
            label4.Text = "B";
            // 
            // colorPick1
            // 
            colorPick1.Anchor = System.Windows.Forms.AnchorStyles.None;
            colorPick1.CurrentAlpha = 255;
            colorPick1.CurrentHue = 0;
            colorPick1.CurrentSat = 0;
            colorPick1.CurrentVal = 0;
            colorPick1.Location = new System.Drawing.Point(41, 27);
            colorPick1.Margin = new System.Windows.Forms.Padding(5);
            colorPick1.Name = "colorPick1";
            colorPick1.Size = new System.Drawing.Size(133, 142);
            colorPick1.TabIndex = 21;
            colorPick1.HSVChanged += colorPick1_HSVChanged;
            // 
            // hsvTabPage
            // 
            hsvTabPage.Controls.Add(panel3);
            hsvTabPage.Location = new System.Drawing.Point(4, 5);
            hsvTabPage.Margin = new System.Windows.Forms.Padding(0);
            hsvTabPage.Name = "hsvTabPage";
            hsvTabPage.Size = new System.Drawing.Size(292, 291);
            hsvTabPage.TabIndex = 2;
            hsvTabPage.Text = "HSVA";
            hsvTabPage.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            panel3.BackColor = System.Drawing.SystemColors.Window;
            panel3.Controls.Add(hueNumericUpDown);
            panel3.Controls.Add(hueColorSlider);
            panel3.Controls.Add(label6);
            panel3.Controls.Add(valueColorSlider);
            panel3.Controls.Add(saturationNumericUpDown);
            panel3.Controls.Add(saturationColorSlider);
            panel3.Controls.Add(label7);
            panel3.Controls.Add(label8);
            panel3.Controls.Add(valueNumericUpDown);
            panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            panel3.Location = new System.Drawing.Point(0, 0);
            panel3.Margin = new System.Windows.Forms.Padding(0);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(292, 291);
            panel3.TabIndex = 27;
            // 
            // hueNumericUpDown
            // 
            hueNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            hueNumericUpDown.Location = new System.Drawing.Point(237, 177);
            hueNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            hueNumericUpDown.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
            hueNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            hueNumericUpDown.Name = "hueNumericUpDown";
            hueNumericUpDown.Size = new System.Drawing.Size(47, 30);
            hueNumericUpDown.TabIndex = 16;
            hueNumericUpDown.ValueChanged += hueNumericUpDown_ValueChanged;
            // 
            // hueColorSlider
            // 
            hueColorSlider.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            hueColorSlider.BackColor = System.Drawing.Color.Transparent;
            hueColorSlider.LargeChange = 5U;
            hueColorSlider.Location = new System.Drawing.Point(28, 177);
            hueColorSlider.Margin = new System.Windows.Forms.Padding(4);
            hueColorSlider.Maximum = 360;
            hueColorSlider.Name = "hueColorSlider";
            hueColorSlider.Size = new System.Drawing.Size(202, 26);
            hueColorSlider.SmallChange = 1U;
            hueColorSlider.TabIndex = 24;
            hueColorSlider.Text = "colorSlider1";
            hueColorSlider.Scroll += hueColorSlider_Scroll;
            // 
            // label6
            // 
            label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(4, 180);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(24, 24);
            label6.TabIndex = 17;
            label6.Text = "H";
            // 
            // valueColorSlider
            // 
            valueColorSlider.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            valueColorSlider.BackColor = System.Drawing.Color.Transparent;
            valueColorSlider.LargeChange = 5U;
            valueColorSlider.Location = new System.Drawing.Point(28, 230);
            valueColorSlider.Margin = new System.Windows.Forms.Padding(4);
            valueColorSlider.Name = "valueColorSlider";
            valueColorSlider.Size = new System.Drawing.Size(202, 26);
            valueColorSlider.SmallChange = 1U;
            valueColorSlider.TabIndex = 25;
            valueColorSlider.Text = "colorSlider2";
            valueColorSlider.Scroll += valueColorSlider_Scroll;
            // 
            // saturationNumericUpDown
            // 
            saturationNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            saturationNumericUpDown.Location = new System.Drawing.Point(237, 204);
            saturationNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            saturationNumericUpDown.Name = "saturationNumericUpDown";
            saturationNumericUpDown.Size = new System.Drawing.Size(47, 30);
            saturationNumericUpDown.TabIndex = 18;
            saturationNumericUpDown.Value = new decimal(new int[] { 100, 0, 0, 0 });
            saturationNumericUpDown.ValueChanged += saturationNumericUpDown_ValueChanged;
            // 
            // saturationColorSlider
            // 
            saturationColorSlider.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            saturationColorSlider.BackColor = System.Drawing.Color.Transparent;
            saturationColorSlider.LargeChange = 5U;
            saturationColorSlider.Location = new System.Drawing.Point(28, 204);
            saturationColorSlider.Margin = new System.Windows.Forms.Padding(4);
            saturationColorSlider.Name = "saturationColorSlider";
            saturationColorSlider.Size = new System.Drawing.Size(202, 26);
            saturationColorSlider.SmallChange = 1U;
            saturationColorSlider.TabIndex = 26;
            saturationColorSlider.Text = "colorSlider3";
            saturationColorSlider.Scroll += saturationColorSlider_Scroll;
            // 
            // label7
            // 
            label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(4, 206);
            label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(20, 24);
            label7.TabIndex = 19;
            label7.Text = "S";
            // 
            // label8
            // 
            label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(4, 232);
            label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(22, 24);
            label8.TabIndex = 21;
            label8.Text = "V";
            // 
            // valueNumericUpDown
            // 
            valueNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            valueNumericUpDown.Location = new System.Drawing.Point(237, 230);
            valueNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            valueNumericUpDown.Name = "valueNumericUpDown";
            valueNumericUpDown.Size = new System.Drawing.Size(47, 30);
            valueNumericUpDown.TabIndex = 20;
            valueNumericUpDown.Value = new decimal(new int[] { 100, 0, 0, 0 });
            valueNumericUpDown.ValueChanged += valueNumericUpDown_ValueChanged;
            // 
            // swatchTabPage
            // 
            swatchTabPage.Controls.Add(panel1);
            swatchTabPage.Location = new System.Drawing.Point(4, 5);
            swatchTabPage.Margin = new System.Windows.Forms.Padding(0);
            swatchTabPage.Name = "swatchTabPage";
            languageProvider1.SetPropertyNames(swatchTabPage, "Text");
            swatchTabPage.Size = new System.Drawing.Size(292, 291);
            swatchTabPage.TabIndex = 0;
            swatchTabPage.Text = "T_SWATCHES";
            swatchTabPage.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.SystemColors.Window;
            panel1.Controls.Add(loadingSwatchLabel);
            panel1.Controls.Add(swatchContainer);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Margin = new System.Windows.Forms.Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(292, 291);
            panel1.TabIndex = 1;
            // 
            // loadingSwatchLabel
            // 
            loadingSwatchLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            loadingSwatchLabel.BackColor = System.Drawing.SystemColors.Control;
            loadingSwatchLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            loadingSwatchLabel.Location = new System.Drawing.Point(70, 100);
            loadingSwatchLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            loadingSwatchLabel.Name = "loadingSwatchLabel";
            languageProvider1.SetPropertyNames(loadingSwatchLabel, "Text");
            loadingSwatchLabel.Size = new System.Drawing.Size(161, 91);
            loadingSwatchLabel.TabIndex = 1;
            loadingSwatchLabel.Text = "M_LOADING";
            loadingSwatchLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // swatchContainer
            // 
            swatchContainer.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            swatchContainer.BackColor = System.Drawing.SystemColors.Window;
            swatchContainer.Location = new System.Drawing.Point(9, 7);
            swatchContainer.Margin = new System.Windows.Forms.Padding(0);
            swatchContainer.Name = "swatchContainer";
            swatchContainer.Size = new System.Drawing.Size(276, 266);
            swatchContainer.TabIndex = 0;
            swatchContainer.SwatchChanged += swatchContainer_SwatchChanged;
            // 
            // languageProvider1
            // 
            languageProvider1.BaseControl = this;
            // 
            // ColorPanel
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.Color.White;
            Controls.Add(colorTabControl);
            Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Margin = new System.Windows.Forms.Padding(4);
            Name = "ColorPanel";
            Size = new System.Drawing.Size(300, 300);
            colorTabControl.ResumeLayout(false);
            rgbTabPage.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)redNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)greenNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)alphaNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)blueNumericUpDown).EndInit();
            hsvTabPage.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)hueNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)saturationNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)valueNumericUpDown).EndInit();
            swatchTabPage.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)languageProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public LanguageProvider languageProvider1;
        private System.Windows.Forms.TabControl colorTabControl;
        private System.Windows.Forms.TabPage rgbTabPage;
        private System.Windows.Forms.Panel panel2;
        private MB.Controls.ColorSlider alphaColorSlider;
        private ColorPreview colorPreview1;
        private ColorPreview colorPreview2;
        private System.Windows.Forms.TextBox textBox1;
        private MB.Controls.ColorSlider redColorSlider;
        private System.Windows.Forms.Label label9;
        private MB.Controls.ColorSlider blueColorSlider;
        private System.Windows.Forms.NumericUpDown redNumericUpDown;
        private MB.Controls.ColorSlider greenColorSlider;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown greenNumericUpDown;
        private System.Windows.Forms.NumericUpDown alphaNumericUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown blueNumericUpDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage hsvTabPage;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.NumericUpDown hueNumericUpDown;
        private MB.Controls.ColorSlider hueColorSlider;
        private System.Windows.Forms.Label label6;
        private MB.Controls.ColorSlider valueColorSlider;
        private System.Windows.Forms.NumericUpDown saturationNumericUpDown;
        private MB.Controls.ColorSlider saturationColorSlider;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown valueNumericUpDown;
        private System.Windows.Forms.TabPage swatchTabPage;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label loadingSwatchLabel;
        private SwatchContainer swatchContainer;
        private Scripts.lemon42.Colors.ColorPick colorPick1;
    }
}
