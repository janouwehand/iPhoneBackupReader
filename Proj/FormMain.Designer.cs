
namespace iPhoneBackupReader
{
  partial class FormMain
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
      this.buttonDoStuff = new System.Windows.Forms.Button();
      this.edt = new System.Windows.Forms.RichTextBox();
      this.SuspendLayout();
      // 
      // buttonDoStuff
      // 
      this.buttonDoStuff.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.buttonDoStuff.Location = new System.Drawing.Point(0, 253);
      this.buttonDoStuff.Name = "buttonDoStuff";
      this.buttonDoStuff.Size = new System.Drawing.Size(742, 56);
      this.buttonDoStuff.TabIndex = 0;
      this.buttonDoStuff.Text = "Action";
      this.buttonDoStuff.UseVisualStyleBackColor = true;
      this.buttonDoStuff.Click += new System.EventHandler(this.button1_Click);
      // 
      // edt
      // 
      this.edt.Dock = System.Windows.Forms.DockStyle.Fill;
      this.edt.Location = new System.Drawing.Point(0, 0);
      this.edt.Name = "edt";
      this.edt.Size = new System.Drawing.Size(742, 253);
      this.edt.TabIndex = 1;
      this.edt.Text = "";
      // 
      // FormMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(742, 309);
      this.Controls.Add(this.edt);
      this.Controls.Add(this.buttonDoStuff);
      this.Name = "FormMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "iPhone Backup reader";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button buttonDoStuff;
    private System.Windows.Forms.RichTextBox edt;
  }
}

