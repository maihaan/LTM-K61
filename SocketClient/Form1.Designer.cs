﻿namespace SocketClient
{
    partial class Form1
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
            this.tbIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbGuiNhan = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbNoiDung = new System.Windows.Forms.TextBox();
            this.btGui = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbCong = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Địa chỉ IP";
            // 
            // tbIP
            // 
            this.tbIP.Location = new System.Drawing.Point(16, 30);
            this.tbIP.Name = "tbIP";
            this.tbIP.Size = new System.Drawing.Size(211, 20);
            this.tbIP.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tin đã gửi nhận";
            // 
            // tbGuiNhan
            // 
            this.tbGuiNhan.Location = new System.Drawing.Point(16, 74);
            this.tbGuiNhan.Multiline = true;
            this.tbGuiNhan.Name = "tbGuiNhan";
            this.tbGuiNhan.ReadOnly = true;
            this.tbGuiNhan.Size = new System.Drawing.Size(318, 268);
            this.tbGuiNhan.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 349);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nội dung";
            // 
            // tbNoiDung
            // 
            this.tbNoiDung.Location = new System.Drawing.Point(16, 366);
            this.tbNoiDung.Multiline = true;
            this.tbNoiDung.Name = "tbNoiDung";
            this.tbNoiDung.Size = new System.Drawing.Size(249, 66);
            this.tbNoiDung.TabIndex = 5;
            // 
            // btGui
            // 
            this.btGui.Location = new System.Drawing.Point(272, 366);
            this.btGui.Name = "btGui";
            this.btGui.Size = new System.Drawing.Size(62, 66);
            this.btGui.TabIndex = 6;
            this.btGui.Text = "Gửi";
            this.btGui.UseVisualStyleBackColor = true;
            this.btGui.Click += new System.EventHandler(this.btGui_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(230, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Cổng";
            // 
            // tbCong
            // 
            this.tbCong.Location = new System.Drawing.Point(233, 30);
            this.tbCong.Name = "tbCong";
            this.tbCong.Size = new System.Drawing.Size(101, 20);
            this.tbCong.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 444);
            this.Controls.Add(this.tbCong);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btGui);
            this.Controls.Add(this.tbNoiDung);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbGuiNhan);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbIP);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Nhắn tin - Socket";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbGuiNhan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbNoiDung;
        private System.Windows.Forms.Button btGui;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbCong;
    }
}

