using System;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace banking.Server
{
    public class ServerMain : BaseScript
    {

        BankChecks check = new BankChecks();
        public ServerMain()
        {
            EventHandlers["getIfPlayerHaveAccount"] += new Action<Player, string>(getIfPlayerHaveAccount);
        }

        private void getIfPlayerHaveAccount([FromSource] Player user, string token)
        {
            TriggerClientEvent(user, "changePlayerHaveAccount", check.account(token));
        }

    }
}