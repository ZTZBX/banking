using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using System.Collections.Generic;

namespace banking.Server
{
    public class ServerMain : BaseScript
    {

        BankChecks bank = new BankChecks();
        public ServerMain()
        {
            EventHandlers["getIfPlayerHaveAccount"] += new Action<Player, string>(getIfPlayerHaveAccount);
            EventHandlers["createBankAcccount"] += new Action<Player, string>(CreateBankAcccount);
            EventHandlers["updateMoneyInLocalBank"] += new Action<Player, string>(UpdateMoneyInLocalBank);

        }

        private void getIfPlayerHaveAccount([FromSource] Player user, string token)
        {
            TriggerClientEvent(user, "changePlayerHaveAccount", bank.account(token));
        }

        private void UpdateMoneyInLocalBank([FromSource] Player user, string token)
        {
            Dictionary<string, int> r = bank.getMoneyAndVp(token);
            TriggerClientEvent(user, "updateMoney", r["money"], r["vp"]);
        }

        private void CreateBankAcccount([FromSource] Player user, string token)
        {
            bank.createBankAccount(token);
            TriggerClientEvent(user, "changePlayerHaveAccount", bank.account(token));
        }

    }
}