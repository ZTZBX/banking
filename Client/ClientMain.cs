using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace banking.Client
{
    public class ClientMain : BaseScript
    {
        public ClientMain()
        {
            EventHandlers["changePlayerHaveAccount"] += new Action<bool>(getIfPlayerHaveAccount);
        }


        private void getIfPlayerHaveAccount(bool have)
        {
            Atm.playerHaveAccount = have;
        }
    }
}