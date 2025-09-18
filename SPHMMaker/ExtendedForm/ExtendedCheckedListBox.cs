using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPHMMaker.ExtendedForm
{
    public partial class ExtendedCheckedListBox : CheckedListBox
    {
        public int GetSingleCheckedIndex
        {
            get
            {
                if (CheckedIndices.Count == 0 || CheckedIndices.Count >= 2)
                {
                    throw new Exception();
                }
                return CheckedIndices[0];
            }
        }

        public ExtendedCheckedListBox()
        {
            InitializeComponent();

            ItemCheck += ExlusiveItemCheck;
            CheckOnClick = true;

            FormManager.AddPostLoad(PostLoad);
        }

        void PostLoad()
        {
            if (Items.Count == 0) return;
            SetItemChecked(0, true);
        }

        void ExlusiveItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (CheckedIndices.Count == 1 && e.NewValue == CheckState.Unchecked)
            {
                e.NewValue = CheckState.Checked;
                return;
            }

            foreach (int i in CheckedIndices)
            {
                if (i == e.Index) continue;

                ItemCheck -= ExlusiveItemCheck;
                SetItemCheckState(i, CheckState.Unchecked);
                ItemCheck += ExlusiveItemCheck;
            }
        }

    }
}
