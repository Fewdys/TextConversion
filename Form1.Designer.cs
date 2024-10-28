namespace TextConversion
{
    partial class Unicode
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        /// 

        public PictureBox Picture;
        private Button ConvertButton;
        private Button AddModifier;
        private ComboBox Selector;
        private ComboBox Modifier;
        private TextBox Input;
        private TextBox Output;
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Unicode));
            Picture = new PictureBox();
            ConvertButton = new Button();
            Selector = new ComboBox();
            Input = new TextBox();
            Output = new TextBox();
            AddModifier = new Button();
            Modifier = new ComboBox();
            InputLabel = new Label();
            OutputLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)Picture).BeginInit();
            SuspendLayout();
            // 
            // Picture
            // 
            Picture.Dock = DockStyle.Fill;
            Picture.Image = (Image)resources.GetObject("Picture.Image");
            Picture.Location = new Point(0, 0);
            Picture.Name = "Picture";
            Picture.Size = new Size(906, 426);
            Picture.SizeMode = PictureBoxSizeMode.StretchImage;
            Picture.TabIndex = 0;
            Picture.TabStop = false;
            // 
            // Selector
            // 
            Selector.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Selector.FormattingEnabled = true;
            Selector.Items.AddRange(new object[] { "Default", "Cursive", "Bold", "Italic", "Greek" });
            Selector.Location = new Point(346, 93);
            Selector.Name = "Selector";
            Selector.Size = new Size(200, 23);
            Selector.TabIndex = 1;
            // 
            // Input
            // 
            Input.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Input.Location = new Point(346, 174);
            Input.Name = "Input";
            Input.Size = new Size(200, 23);
            Input.TabIndex = 2;
            // 
            // Output
            // 
            Output.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Output.Location = new Point(346, 220);
            Output.Name = "Output";
            Output.Size = new Size(200, 23);
            Output.TabIndex = 3;
            // 
            // AddModifier
            // 
            AddModifier.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AddModifier.Location = new Point(346, 249);
            AddModifier.Name = "AddModifier";
            AddModifier.Size = new Size(200, 23);
            AddModifier.TabIndex = 4;
            AddModifier.Text = "AddModifier";
            AddModifier.UseVisualStyleBackColor = true;
            // 
            // ConvertButton
            // 
            ConvertButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ConvertButton.Location = new Point(346, 278);
            ConvertButton.Name = "ConvertButton";
            ConvertButton.Size = new Size(200, 23);
            ConvertButton.TabIndex = 5;
            ConvertButton.Text = "Convert";
            ConvertButton.UseVisualStyleBackColor = true;
            // 
            // Modifier
            // 
            Modifier.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Modifier.FormattingEnabled = true;
            Modifier.Location = new Point(346, 122);
            Modifier.Name = "Modifier";
            Modifier.Size = new Size(200, 23);
            Modifier.TabIndex = 6;
            // 
            // InputLabel
            // 
            InputLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            InputLabel.BackColor = Color.White;
            InputLabel.BorderStyle = BorderStyle.FixedSingle;
            InputLabel.ForeColor = Color.Black;
            InputLabel.Location = new Point(346, 151);
            InputLabel.Margin = new Padding(3);
            InputLabel.Name = "InputLabel";
            InputLabel.Size = new Size(200, 17);
            InputLabel.TabIndex = 7;
            InputLabel.Text = "Input";
            InputLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // OutputLabel
            // 
            OutputLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            OutputLabel.BackColor = Color.White;
            OutputLabel.BorderStyle = BorderStyle.FixedSingle;
            OutputLabel.ForeColor = Color.Black;
            OutputLabel.Location = new Point(346, 200);
            OutputLabel.Name = "OutputLabel";
            OutputLabel.Size = new Size(200, 17);
            OutputLabel.TabIndex = 8;
            OutputLabel.Text = "Output";
            OutputLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Unicode
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(906, 426);
            Controls.Add(OutputLabel);
            Controls.Add(InputLabel);
            Controls.Add(Modifier);
            Controls.Add(AddModifier);
            Controls.Add(Output);
            Controls.Add(Input);
            Controls.Add(Selector);
            Controls.Add(ConvertButton);
            Controls.Add(Picture);
            Name = "Unicode";
            Text = "Unicode";
            ((System.ComponentModel.ISupportInitialize)Picture).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private Label InputLabel;
        private Label OutputLabel;
    }
}
