using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace banking.Client
{
    public class AtmExitNui : BaseScript
    {
        public AtmExitNui()
        {
            RegisterNuiCallbackType("exit");
            EventHandlers["__cfx_nui:exit"] += new Action<IDictionary<string, object>, CallbackDelegate>(Exit);
        }

        private void Exit(IDictionary<string, object> data, CallbackDelegate cb)
        {
            AtmNui.AtmNuiIn(false);
            Atm.atmOp = false;
        }

    }
}