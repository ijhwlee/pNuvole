namespace desktopClient
{
  partial class Form1
  {
    /// <summary>
    /// 필수 디자이너 변수입니다.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// 사용 중인 모든 리소스를 정리합니다.
    /// </summary>
    /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form 디자이너에서 생성한 코드

    /// <summary>
    /// 디자이너 지원에 필요한 메서드입니다. 
    /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
    /// </summary>
    private void InitializeComponent()
    {
      this.button1 = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.textBoxMsg = new System.Windows.Forms.TextBox();
      this.buttonSend = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(352, 367);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(112, 42);
      this.button1.TabIndex = 0;
      this.button1.Text = "Exit";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(13, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(95, 18);
      this.label1.TabIndex = 1;
      this.label1.Text = "Message: ";
      // 
      // textBoxMsg
      // 
      this.textBoxMsg.Location = new System.Drawing.Point(105, 13);
      this.textBoxMsg.Multiline = true;
      this.textBoxMsg.Name = "textBoxMsg";
      this.textBoxMsg.Size = new System.Drawing.Size(658, 122);
      this.textBoxMsg.TabIndex = 2;
      // 
      // buttonSend
      // 
      this.buttonSend.Location = new System.Drawing.Point(197, 367);
      this.buttonSend.Name = "buttonSend";
      this.buttonSend.Size = new System.Drawing.Size(133, 42);
      this.buttonSend.TabIndex = 3;
      this.buttonSend.Text = "Send";
      this.buttonSend.UseVisualStyleBackColor = true;
      this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.buttonSend);
      this.Controls.Add(this.textBoxMsg);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.button1);
      this.Name = "Form1";
      this.Text = "pNuvoleClient";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBoxMsg;
    private System.Windows.Forms.Button buttonSend;
  }
}

