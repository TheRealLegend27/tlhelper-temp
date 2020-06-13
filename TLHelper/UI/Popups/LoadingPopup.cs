using System.Windows.Forms;

namespace TLHelper.UI.Popups
{
    public partial class LoadingPopup : Form
    {
        public ProgressBar progress;
        public LoadingPopup()
        {
            InitializeComponent();
            progress = progressBar;
        }
    }
}
