using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace banking.Client
{
    public class GetPlayerMoney : BaseScript
    {
        public GetPlayerMoney()
        {
            RegisterNuiCallbackType("getMoney");
            EventHandlers["__cfx_nui:getMoney"] += new Action<IDictionary<string, object>, CallbackDelegate>(GetMoney);
            EventHandlers["updateMoney"] += new Action<int, int>(UpdateMoney);
        }

        private void GetMoney(string token)
        {
            // TODO: Generar export cliente para poder obtener el dinero en tiempo real del usuario a nivel de backend
        }

        private void GetVp(string token)
        {
            // TODO: Generar export cliente para poder obtener los vp en tiempo real del usuario a nivel de backend
        }

        private void UpdateMoney(int money, int vp)
        {
            Player.money = money;
            Player.vp = vp;
        }

        private void GetMoney(IDictionary<string, object> data, CallbackDelegate cb)
        {

            TriggerServerEvent("updateMoneyInLocalBank", Exports["core-ztzbx"].playerToken());

            cb(new
            {
                data = "{\"money\":"+Player.money.ToString()+", \"vp\": "+Player.vp.ToString()+", \"currency\": \""+Player.currency+"\"}",
            });
            return;
        }

    }
}