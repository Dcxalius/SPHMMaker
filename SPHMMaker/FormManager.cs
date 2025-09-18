using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPHMMaker
{
    internal static class FormManager
    {
        public static void AddPostLoad(Action a) => postLoads?.Add(a);
        static List<Action>? postLoads;
        public static int i;
        public static Form1 form;

        public static void Init()
        {
            ApplicationConfiguration.Initialize();
            postLoads = new List<Action>();
            form = new Form1();

            if (postLoads != null)
            {
                foreach (Action action in postLoads)
                {
                    action.Invoke();
                }
            }

            postLoads = null;
            Application.Run(form);

        }

    }
}
