using System;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace banking.Server
{
    public class ServerMain : BaseScript
    {

        BankChecks bank = new BankChecks();
        public ServerMain()
        {
            EventHandlers["getIfPlayerHaveAccount"] += new Action<Player, string>(getIfPlayerHaveAccount);
            EventHandlers["createBankAcccount"] += new Action<Player, string>(CreateBankAcccount);
            
        }

        private void getIfPlayerHaveAccount([FromSource] Player user, string token)
        {
            TriggerClientEvent(user, "changePlayerHaveAccount", bank.account(token));
        }

        private void CreateBankAcccount([FromSource] Player user, string token)
        {
            bank.createBankAccount(token);
            TriggerClientEvent(user, "changePlayerHaveAccount", bank.account(token));
        }

    }
}