using System;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace banking.Server
{
    public class BankChecks : BaseScript
    {
        public BankChecks()
        {

        }

        public bool account(string token)
        {
            /*
            select money from economy as ec
            RIGHT join players as player
            on player.username = ec.username
            where player.token = '<TOKEN>'
            */
            string query = $"select money from economy as ec RIGHT join players as player on player.username = ec.username where player.token = '{token}'";
            dynamic result = Exports["fivem-mysql"].raw(query);

            if (result[0][0] == "") { return false; }
            return true;
        }

        public void createBankAccount(string token)
        {
            /*
            INSERT INTO economy (username)
            SELECT username from players 
            WHERE token = '<TOKEN>'
            */
            string query = $"insert into economy (username) select username from players WHERE token = '{token}'";
            Exports["fivem-mysql"].raw(query);
        }

    }
}