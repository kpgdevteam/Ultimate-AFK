﻿using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateAFK
{
    /// <summary>
    /// Main class where all the handlers are loaded.
    /// </summary>
    public class UltimateAFK : Plugin<Config>
    {
        //  This is my programming style and if you don't like it, good for you.

        #region Variables
        public static UltimateAFK Instance;

        public List<API.Base.Handler> Handlers;
        #endregion

        public override string Author => "SrLicht";

        public override string Name => "Ultimate-AFK";

        public override string Prefix => "Ultimate_Afk";

        public override Version RequiredExiledVersion => new Version(5,0,0);

        public override Version Version => new Version(1,0,0);

        public override void OnEnabled()
        {
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            base.OnDisabled();
        }

        
    }
}
