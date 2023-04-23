using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace banking.Client
{
    public class AtmNui : BaseScript
    {
        private int markerTypeDollarSign = 29;

        public AtmNui()
        {
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
        }

        private void OnClientResourceStart(string resourceName)
        {
            Atm.atmOp = false;
            OpenNuiEvent();
            addBlips();
        }

        private async void addBlips()
        {
            foreach (string atmName in Atm.atmMetas.Keys)
            {
                AddTextEntry(atmName, atmName);
                await Delay(0);
                int current_blip = AddBlipForCoord(Atm.atmMetas[atmName][0], Atm.atmMetas[atmName][1], Atm.atmMetas[atmName][2]);
                SetBlipSprite(current_blip, 272);
                SetBlipDisplay(current_blip, 4);
                SetBlipScale(current_blip, 1.0f);
                SetBlipColour(current_blip, 60);
                SetBlipAsShortRange(current_blip, true);
                BeginTextCommandSetBlipName(atmName);
                AddTextComponentSubstringPlayerName("me");
                EndTextCommandSetBlipName(current_blip);
            }
        }

        private async void OpenNuiEvent()
        {
            List<List<float>> currentCoords = new List<List<float>>();

            float radeo = 3.0f;

            while (true)
            {
                await Delay(0);
                foreach (string atmName in Atm.atmMetas.Keys)
                {
                    DrawMarker(
                    markerTypeDollarSign,
                    // map positon
                    Atm.atmMetas[atmName][0],
                    Atm.atmMetas[atmName][1],
                    Atm.atmMetas[atmName][2],
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
                    255,
                    184,
                    28,
                    125,
                    // bobUpAndDown
                    true,
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

                    tempCoords.Add(Atm.atmMetas[atmName][0]);
                    tempCoords.Add(Atm.atmMetas[atmName][1]);
                    tempCoords.Add(Atm.atmMetas[atmName][2]);

                    currentCoords.Add(tempCoords);


                }

                Vector3 currect_coords = GetEntityCoords(PlayerPedId(), false);

                foreach (List<float> coords in currentCoords)
                {
                    if (MathUtils.CheckIfCoordsAreInRadeo(coords[0], coords[1], coords[2], currect_coords.X, currect_coords.Y, currect_coords.Z, radeo))
                    {
                        if (IsControlJustReleased(0, 38))
                        {
                            if (!Atm.atmOp)
                            {
                                TriggerServerEvent("getIfPlayerHaveAccount", Exports["core-ztzbx"].playerToken());
                                
                                if (Atm.playerHaveAccount)
                                {
                                    AtmNuiIn(true);
                                    Atm.atmOp = true;
                                }
                                else 
                                {
                                    Exports["core-ztzbx"].sendOnUserChat("^2^*[City Bank]^r^0 First you need to create a bank account");
                                }
                                await Delay(3);

                            }
                        }

                    }

                }
            }
        }

        static public void AtmNuiIn(bool state)
        {
            string jsonString = "";
            if (state) { jsonString = "{\"showAtm\": true }"; SetNuiFocus(true, true); SetNuiFocus(true, true); }
            if (!state) { jsonString = "{\"showAtm\": false }"; SetNuiFocus(false, false); SetNuiFocus(false, false); }

            SendNuiMessage(jsonString);
        }


    }
}