
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AD.DistribuicaoEPIs.JPA
{
    public class LoadingForm : Form
    {
        private ProgressBar progressBar;
        private Label lblMensagem;
        private Timer animationTimer;
        private int dotCount = 0;
        private string baseMessage;

        public LoadingForm(string mensagem = "A processar")
        {
            baseMessage = mensagem;
            InitializeComponents();
            InitializeAnimation();
        }

        private void InitializeComponents()
        {
            // Configurações do formulário
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(420, 160);
            this.BackColor = Color.White;
            this.ShowInTaskbar = false;
            this.TopMost = true; // Manter sempre visível

            // Painel com borda
            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.BackColor = Color.White;
            this.Controls.Add(panel);

            // Label de mensagem
            lblMensagem = new Label();
            lblMensagem.Text = baseMessage + "...";
            lblMensagem.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            lblMensagem.ForeColor = Color.FromArgb(64, 64, 64);
            lblMensagem.TextAlign = ContentAlignment.MiddleCenter;
            lblMensagem.AutoSize = false;
            lblMensagem.Size = new Size(400, 40);
            lblMensagem.Location = new Point(10, 30);
            panel.Controls.Add(lblMensagem);

            // ProgressBar moderno
            progressBar = new ProgressBar();
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.MarqueeAnimationSpeed = 30;
            progressBar.Size = new Size(380, 30);
            progressBar.Location = new Point(20, 85);
            progressBar.ForeColor = Color.FromArgb(0, 120, 215); // Azul moderno
            panel.Controls.Add(progressBar);

            // Ícone de loading (simulado com label)
            Label lblIcon = new Label();
            lblIcon.Text = "⏳";
            lblIcon.Font = new Font("Segoe UI", 20F);
            lblIcon.AutoSize = true;
            lblIcon.Location = new Point(175, 5);
            panel.Controls.Add(lblIcon);
        }

        private void InitializeAnimation()
        {
            animationTimer = new Timer();
            animationTimer.Interval = 500;
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            dotCount = (dotCount + 1) % 4;
            string dots = new string('.', dotCount == 0 ? 3 : dotCount);
            lblMensagem.Text = baseMessage + dots;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (animationTimer != null)
            {
                animationTimer.Stop();
                animationTimer.Dispose();
            }
            base.OnFormClosing(e);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= 0x20000; // CS_DROPSHADOW
                return cp;
            }
        }
    }
}
