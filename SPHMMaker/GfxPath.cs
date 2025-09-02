﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPHMMaker
{


    enum GfxType
    {
        Debug,
        Object,
        Corpse,
        Tile,
        Item,
        UI,
        Effect,
        SpellImage,
        Count
    }
    [DebuggerStepThrough]
    internal class GfxPath
    {
        public static GfxPath NullPath { get => new GfxPath(GfxType.Debug, null); }

        public GfxType Type
        {
            get { return type; }
        }

        public string Name
        {
            get { return name; }
        }


        GfxType type;
        string name;
        public GfxPath(GfxType aType, string aName)
        {
            type = aType;
            name = aName;
        }

        public override string ToString()
        {
            string s = type.ToString() + "\\" + name;
            return s;
        }
    }
}
