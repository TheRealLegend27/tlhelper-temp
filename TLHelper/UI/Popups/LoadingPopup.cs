using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
