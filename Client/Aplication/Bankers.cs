using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace banking.Client
{
    public class Bankers : BaseScript
    {
        public Bankers()
        {
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
        }

        private void OnClientResourceStart(string resourceName)
        {
            addBlips();
            bankerInteraction();
        }

        private async void addBlips()
        {
            foreach (string atmName in Atm.bankerMetas.Keys)
            {
                await Delay(0);
                AddTextEntry(atmName, atmName);
                int current_blip = AddBlipForCoord(Atm.bankerMetas[atmName][0], Atm.bankerMetas[atmName][1], Atm.bankerMetas[atmName][2]);
                SetBlipSprite(current_blip, 408);
                SetBlipDisplay(current_blip, 5);
                SetBlipScale(current_blip, 0.75f);
                SetBlipColour(current_blip, 25);
                SetBlipAsShortRange(current_blip, true);
                BeginTextCommandSetBlipName(atmName);
                AddTextComponentSubstringPlayerName("me");
                EndTextCommandSetBlipName(current_blip);
            }
        }

        private async void bankerInteraction()
        {
            List<List<float>> currentCoords = new List<List<float>>();
            float radeo = 2.0f;

            while (true)
            {
                await Delay(0);
                foreach (string bankerName in Atm.bankerMetas.Keys)
                {
                    DrawMarker(
                    25,
                    // map positon
                    Atm.bankerMetas[bankerName][0],
                    Atm.bankerMetas[bankerName][1],
                    Atm.bankerMetas[bankerName][2],
                    // direction
                    0.0f,
                    0.0f,
                    0.0f,
                    // rotation
                    0.0f,
                    0.0f,
                    0.0f,
                    //scale
                    1.0f,
                    1.0f,
                    1.0f,
                    // rgb and alpha
                    0,
                    100,
                    0,
                    125,
                    // bobUpAndDown
                    false,
                    // faceCamera
                    true,
                    // p19
                    2,
                    // rotate
                    false,
                    // textureDict
                    null,
                    // textureName
                    null,
                    // drawOnEnts
                    false
                    );
                    List<float> tempCoords = new List<float>();

                    tempCoords.Add(Atm.bankerMetas[bankerName][0]);
                    tempCoords.Add(Atm.bankerMetas[bankerName][1]);
                    tempCoords.Add(Atm.bankerMetas[bankerName][2]);
                    currentCoords.Add(tempCoords);
                }

                Vector3 currect_coords = GetEntityCoords(PlayerPedId(), false);
                foreach (List<float> coords in currentCoords)
                {
                    if (MathUtils.CheckIfCoordsAreInRadeo(coords[0], coords[1], coords[2], currect_coords.X, currect_coords.Y, currect_coords.Z, radeo))
                    {
                        if (IsControlJustReleased(0, 38))
                        {
                            TriggerServerEvent("getIfPlayerHaveAccount", Exports["core-ztzbx"].playerToken());

                            if (!Atm.playerHaveAccount)
                            {
                                TriggerServerEvent("createBankAcccount", Exports["core-ztzbx"].playerToken());
                                Exports["core-ztzbx"].sendOnUserChat("^2^*[Banker]^r^0 You have created a bank account !");
                            }
                            else 
                            {
                                Exports["core-ztzbx"].sendOnUserChat("^2^*[Banker]^r^0 You already have a bank account!");
                            }
                            await Delay(3);
                        }
                    }

                }
            }
        }


    }
}