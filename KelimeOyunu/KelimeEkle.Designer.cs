
namespace KelimeOyunu
{
    partial class KelimeEkle
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnEkle = new System.Windows.Forms.Button();
            this.cevapText = new System.Windows.Forms.TextBox();
            this.soruText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(184, 226);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cevap:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnEkle
            // 
            this.btnEkle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEkle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnEkle.Location = new System.Drawing.Point(305, 267);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(152, 29);
            this.btnEkle.TabIndex = 1;
            this.btnEkle.Text = "Kelime Ekle";
            this.btnEkle.UseVisualStyleBackColor = true;
            this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);
            // 
            // cevapText
            // 
            this.cevapText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cevapText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cevapText.Location = new System.Drawing.Point(260, 220);
            this.cevapText.Name = "cevapText";
            this.cevapText.Size = new System.Drawing.Size(286, 26);
            this.cevapText.TabIndex = 2;
            // 
            // soruText
            // 
            this.soruText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.soruText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.soruText.Location = new System.Drawing.Point(260, 164);
            this.soruText.Name = "soruText";
            this.soruText.Size = new System.Drawing.Size(286, 26);
            this.soruText.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(196, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Soru:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // KelimeEkle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BackgroundImage = global::KelimeOyunu.Properties.Resources._1920x1080_px_abstract_backgrounds_hexagons_honeycomb_patterns_804899_wallhere_com__1_;
            this.ClientSize = new System.Drawing.Size(826, 450);
            this.Controls.Add(this.soruText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cevapText);
            this.Controls.Add(this.btnEkle);
            this.Controls.Add(this.label1);
            this.Name = "KelimeEkle";
            this.Text = "KelimeEkle";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEkle;
        private System.Windows.Forms.TextBox cevapText;
        private System.Windows.Forms.TextBox soruText;
        private System.Windows.Forms.Label label2;
    }
}