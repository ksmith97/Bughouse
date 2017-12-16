namespace Bughouse.ChessGame
{
    partial class Chessboard : System.Windows.Forms.Panel
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
            this.SuspendLayout();
            // 
            // Chessboard
            // 
            this.Name = "Chessboard";
            this.Size = new System.Drawing.Size(640, 640);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Chessboard_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Chessboard_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Chessboard_MouseMove);
            this.ResumeLayout(false);
            this.DoubleBuffered = true;


        }

        #endregion
    }
}
