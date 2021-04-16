
namespace KelimeOyunu
{
    partial class Oyun
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
            this.components = new System.ComponentModel.Container();
            this.lbOyuncu1 = new System.Windows.Forms.Label();
            this.lbOyuncu2 = new System.Windows.Forms.Label();
            this.lbOyuncu3 = new System.Windows.Forms.Label();
            this.lbSkor1 = new System.Windows.Forms.Label();
            this.lbSkor2 = new System.Windows.Forms.Label();
            this.lbSkor3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cevapText = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnHarf = new System.Windows.Forms.Button();
            this.lbSure = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cevapBox = new System.Windows.Forms.TextBox();
            this.cevapKontrol = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.kalanSure = new System.Windows.Forms.Timer(this.components);
            this.tahminSure = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.tahminTime = new System.Windows.Forms.Label();
            this.btnTahmin = new System.Windows.Forms.Button();
            this.lbPuan = new System.Windows.Forms.Label();
            this.lbOyuncu = new System.Windows.Forms.Label();
            this.mainBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbOyuncu1
            // 
            this.lbOyuncu1.AutoSize = true;
            this.lbOyuncu1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbOyuncu1.Location = new System.Drawing.Point(28, 51);
            this.lbOyuncu1.Name = "lbOyuncu1";
            this.lbOyuncu1.Size = new System.Drawing.Size(100, 24);
            this.lbOyuncu1.TabIndex = 0;
            this.lbOyuncu1.Text = "Oyuncu 1";
            // 
            // lbOyuncu2
            // 
            this.lbOyuncu2.AutoSize = true;
            this.lbOyuncu2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbOyuncu2.Location = new System.Drawing.Point(307, 51);
            this.lbOyuncu2.Name = "lbOyuncu2";
            this.lbOyuncu2.Size = new System.Drawing.Size(100, 24);
            this.lbOyuncu2.TabIndex = 1;
            this.lbOyuncu2.Text = "Oyuncu 2";
            // 
            // lbOyuncu3
            // 
            this.lbOyuncu3.AutoSize = true;
            this.lbOyuncu3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbOyuncu3.Location = new System.Drawing.Point(564, 51);
            this.lbOyuncu3.Name = "lbOyuncu3";
            this.lbOyuncu3.Size = new System.Drawing.Size(100, 24);
            this.lbOyuncu3.TabIndex = 2;
            this.lbOyuncu3.Text = "Oyuncu 3";
            // 
            // lbSkor1
            // 
            this.lbSkor1.AutoSize = true;
            this.lbSkor1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbSkor1.Location = new System.Drawing.Point(134, 51);
            this.lbSkor1.Name = "lbSkor1";
            this.lbSkor1.Size = new System.Drawing.Size(32, 24);
            this.lbSkor1.TabIndex = 3;
            this.lbSkor1.Text = "00";
            // 
            // lbSkor2
            // 
            this.lbSkor2.AutoSize = true;
            this.lbSkor2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbSkor2.Location = new System.Drawing.Point(413, 51);
            this.lbSkor2.Name = "lbSkor2";
            this.lbSkor2.Size = new System.Drawing.Size(32, 24);
            this.lbSkor2.TabIndex = 4;
            this.lbSkor2.Text = "00";
            // 
            // lbSkor3
            // 
            this.lbSkor3.AutoSize = true;
            this.lbSkor3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbSkor3.Location = new System.Drawing.Point(670, 51);
            this.lbSkor3.Name = "lbSkor3";
            this.lbSkor3.Size = new System.Drawing.Size(32, 24);
            this.lbSkor3.TabIndex = 5;
            this.lbSkor3.Text = "00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(28, 230);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 24);
            this.label1.TabIndex = 6;
            this.label1.Text = "Cevap:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(43, 319);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 24);
            this.label2.TabIndex = 7;
            this.label2.Text = "Soru:";
            // 
            // cevapText
            // 
            this.cevapText.AutoSize = true;
            this.cevapText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cevapText.Location = new System.Drawing.Point(109, 230);
            this.cevapText.Name = "cevapText";
            this.cevapText.Size = new System.Drawing.Size(75, 24);
            this.cevapText.TabIndex = 8;
            this.cevapText.Text = "- - - - - ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(122, 319);
            this.label7.MaximumSize = new System.Drawing.Size(650, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 24);
            this.label7.TabIndex = 9;
            this.label7.Text = "- - - - - ";
            // 
            // btnHarf
            // 
            this.btnHarf.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnHarf.Location = new System.Drawing.Point(32, 402);
            this.btnHarf.Name = "btnHarf";
            this.btnHarf.Size = new System.Drawing.Size(75, 39);
            this.btnHarf.TabIndex = 10;
            this.btnHarf.Text = "Harf Al";
            this.btnHarf.UseVisualStyleBackColor = true;
            this.btnHarf.Click += new System.EventHandler(this.btnHarf_Click);
            // 
            // lbSure
            // 
            this.lbSure.AutoSize = true;
            this.lbSure.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbSure.Location = new System.Drawing.Point(352, 145);
            this.lbSure.Name = "lbSure";
            this.lbSure.Size = new System.Drawing.Size(55, 37);
            this.lbSure.TabIndex = 11;
            this.lbSure.Text = "00";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label9.Location = new System.Drawing.Point(326, 121);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 24);
            this.label9.TabIndex = 12;
            this.label9.Text = "KalanSure";
            // 
            // cevapBox
            // 
            this.cevapBox.Enabled = false;
            this.cevapBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cevapBox.Location = new System.Drawing.Point(298, 436);
            this.cevapBox.Name = "cevapBox";
            this.cevapBox.Size = new System.Drawing.Size(134, 29);
            this.cevapBox.TabIndex = 13;
            this.cevapBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cevapBox_KeyDown);
            // 
            // cevapKontrol
            // 
            this.cevapKontrol.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cevapKontrol.Location = new System.Drawing.Point(332, 480);
            this.cevapKontrol.Name = "cevapKontrol";
            this.cevapKontrol.Size = new System.Drawing.Size(75, 33);
            this.cevapKontrol.TabIndex = 14;
            this.cevapKontrol.Text = "Cevapla";
            this.cevapKontrol.UseVisualStyleBackColor = true;
            this.cevapKontrol.Visible = false;
            this.cevapKontrol.Click += new System.EventHandler(this.cevapKontrol_Click);
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnNext.Location = new System.Drawing.Point(619, 436);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(134, 38);
            this.btnNext.TabIndex = 15;
            this.btnNext.Text = "Sonraki Soru";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // kalanSure
            // 
            this.kalanSure.Enabled = true;
            this.kalanSure.Interval = 1000;
            this.kalanSure.Tick += new System.EventHandler(this.kalanSure_Tick);
            // 
            // tahminSure
            // 
            this.tahminSure.Interval = 1000;
            this.tahminSure.Tick += new System.EventHandler(this.tahminSure_Tick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(418, 393);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 24);
            this.label3.TabIndex = 17;
            this.label3.Text = "Tahmin Süre";
            // 
            // tahminTime
            // 
            this.tahminTime.AutoSize = true;
            this.tahminTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tahminTime.Location = new System.Drawing.Point(468, 428);
            this.tahminTime.Name = "tahminTime";
            this.tahminTime.Size = new System.Drawing.Size(55, 37);
            this.tahminTime.TabIndex = 16;
            this.tahminTime.Text = "00";
            // 
            // btnTahmin
            // 
            this.btnTahmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnTahmin.Location = new System.Drawing.Point(180, 432);
            this.btnTahmin.Name = "btnTahmin";
            this.btnTahmin.Size = new System.Drawing.Size(104, 37);
            this.btnTahmin.TabIndex = 18;
            this.btnTahmin.Text = "Tahmin Et";
            this.btnTahmin.UseVisualStyleBackColor = true;
            this.btnTahmin.Click += new System.EventHandler(this.btnTahmin_Click);
            // 
            // lbPuan
            // 
            this.lbPuan.AutoSize = true;
            this.lbPuan.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbPuan.Location = new System.Drawing.Point(715, 229);
            this.lbPuan.Name = "lbPuan";
            this.lbPuan.Size = new System.Drawing.Size(38, 25);
            this.lbPuan.TabIndex = 19;
            this.lbPuan.Text = "00";
            // 
            // lbOyuncu
            // 
            this.lbOyuncu.AutoSize = true;
            this.lbOyuncu.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbOyuncu.Location = new System.Drawing.Point(539, 230);
            this.lbOyuncu.Name = "lbOyuncu";
            this.lbOyuncu.Size = new System.Drawing.Size(107, 24);
            this.lbOyuncu.TabIndex = 20;
            this.lbOyuncu.Text = "oyuncuadi";
            // 
            // mainBtn
            // 
            this.mainBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.mainBtn.Location = new System.Drawing.Point(599, 135);
            this.mainBtn.Name = "mainBtn";
            this.mainBtn.Size = new System.Drawing.Size(142, 28);
            this.mainBtn.TabIndex = 21;
            this.mainBtn.Text = "Ana Menü";
            this.mainBtn.UseVisualStyleBackColor = true;
            this.mainBtn.Click += new System.EventHandler(this.mainBtn_Click);
            // 
            // Oyun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.BackgroundImage = global::KelimeOyunu.Properties.Resources.arkaplan;
            this.ClientSize = new System.Drawing.Size(847, 538);
            this.Controls.Add(this.mainBtn);
            this.Controls.Add(this.lbOyuncu);
            this.Controls.Add(this.lbPuan);
            this.Controls.Add(this.btnTahmin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tahminTime);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.cevapKontrol);
            this.Controls.Add(this.cevapBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lbSure);
            this.Controls.Add(this.btnHarf);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cevapText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbSkor3);
            this.Controls.Add(this.lbSkor2);
            this.Controls.Add(this.lbSkor1);
            this.Controls.Add(this.lbOyuncu3);
            this.Controls.Add(this.lbOyuncu2);
            this.Controls.Add(this.lbOyuncu1);
            this.Name = "Oyun";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Oyun";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Oyun_FormClosed);
            this.Load += new System.EventHandler(this.Oyun_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbOyuncu1;
        private System.Windows.Forms.Label lbOyuncu2;
        private System.Windows.Forms.Label lbOyuncu3;
        private System.Windows.Forms.Label lbSkor1;
        private System.Windows.Forms.Label lbSkor2;
        private System.Windows.Forms.Label lbSkor3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label cevapText;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnHarf;
        private System.Windows.Forms.Label lbSure;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox cevapBox;
        private System.Windows.Forms.Button cevapKontrol;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Timer kalanSure;
        private System.Windows.Forms.Timer tahminSure;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label tahminTime;
        private System.Windows.Forms.Button btnTahmin;
        private System.Windows.Forms.Label lbPuan;
        private System.Windows.Forms.Label lbOyuncu;
        private System.Windows.Forms.Button mainBtn;
    }
}