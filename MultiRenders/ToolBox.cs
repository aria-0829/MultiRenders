using System;
using System.Windows.Forms;

namespace MultiRenders
{
    public partial class ToolBox : Form
    {
        public bool isPosColor = true;
        public bool isDynamicLight = false;
        public bool isMoveCube = false;
        public bool isButtonClicked { get; set; }

        public ToolBox()
        {
            InitializeComponent();
        }

        private void rbtn_positionColor_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_positionColor.Checked)
            {
                isPosColor = true;
            }
            else
            {
                isPosColor = false;
            }
        }

        private void rbtn_dynamicLight_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_dynamicLight.Checked)
            {
                isDynamicLight = true;
            }
            else
            {
                isDynamicLight = false;
            }
        }

        private void btn_resetP_Click(object sender, EventArgs e)
        {
            isButtonClicked = true;
        }

        private void rbtn_moveCube_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_moveCube.Checked)
            {
                isMoveCube = true;
            }
            else
            {
                isMoveCube = false;
            }
        }
    }
}
