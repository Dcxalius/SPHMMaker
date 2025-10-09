using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPHMMaker
{
    internal static class FormManager
    {
        public static void AddPostLoad(Action a) => postLoads?.Add(a);
        static List<Action>? postLoads;
        public static int i;
        public static MainForm Form { get; private set; } = null!;

        public static void Init()
        {
            ApplicationConfiguration.Initialize();
            postLoads = new List<Action>();
            Form = new MainForm();

            if (postLoads != null)
            {
                foreach (Action action in postLoads)
                {
                    action.Invoke();
                }
            }

            postLoads = null;
            Application.Run(Form);

        }

    }
}
