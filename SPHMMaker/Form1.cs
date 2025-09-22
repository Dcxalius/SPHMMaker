using System.Runtime.InteropServices;
using System.Windows.Forms;
using SPHMMaker.Items;

namespace SPHMMaker
{
    public partial class Form1 : Form
    {
        public static Form1 Instance;
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        int editingItem = -1;


        public Form1()
        {
            Instance = this;
            InitializeComponent();
            AllocConsole();

            InitializeItems();
        }


        
    }
}
