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
            checkIfLogin();
            addBlips();
            bankerInteraction();
            bankerPeds();
        }

        private async void checkIfLogin()
        {
            while (true)
            {
                await Delay(500);
                if (Exports["core-ztzbx"].playerToken() != null)
                {
                    if (!Atm.playerHaveAccount)
                    {
                        TriggerServerEvent("getIfPlayerHaveAccount", Exports["core-ztzbx"].playerToken());        
                    }

                    TriggerServerEvent("updateMoneyInLocalBank", Exports["core-ztzbx"].playerToken());
                    break;

                }

            }

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


        private async void bankerPeds()
        {
            if (!Atm.pedsAreLoaded)
            {
                uint banker = (uint)GetHashKey("cs_barry");
                RequestModel(banker);
                while (HasModelLoaded(banker) == false)
                {
                    RequestModel(banker);
                    await Delay(100);
                }

                uint chairM = (uint)GetHashKey("v_ret_gc_chair03");
                RequestModel(chairM);

                while (HasModelLoaded(chairM) == false)
                {
                    RequestModel(chairM);
                    await Delay(100);
                }

                RequestAnimDict("amb@prop_human_seat_chair_mp@male@generic@base");

                while (HasAnimDictLoaded("amb@prop_human_seat_chair_mp@male@generic@base") == false)
                {
                    RequestAnimDict("amb@prop_human_seat_chair_mp@male@generic@base");
                    await Delay(100);
                }

                /*
                The ped is created in local memory of the player not in network for performace !
                */

                foreach (string playerPed in Atm.bankerPeds.Keys)
                {
                    await Delay(100);

                    int id_ob = CreateObject(
                        (int)chairM,
                        Atm.bankerPeds[playerPed][0],
                        Atm.bankerPeds[playerPed][1],
                        Atm.bankerPeds[playerPed][2] + 0.5f,
                        false,
                        false,
                        false
                    );

                    FreezeEntityPosition(id_ob, true);


                    int id = CreatePed(
                        4,
                        banker,
                        Atm.bankerPeds[playerPed][0],
                        Atm.bankerPeds[playerPed][1],
                        Atm.bankerPeds[playerPed][2],
                        150.0f,
                        false,
                        true
                      );
                    if (id != 0)
                    {
                        TaskPlayAnim(
                            // ped
                            id,
                            // TaskPlayAnim
                            "amb@prop_human_seat_chair_mp@male@generic@base",
                            // animationName
                            "base",
                            // blendInSpeed
                            8.0f,
                            // blendInSpeed
                            8.0f,
                            // duration
                            -1,
                            // flag
                            1,
                            // playbackRate
                            0.0f,
                            true,
                            true,
                            true
                            );
                        FreezeEntityPosition(id, true);
                        SetEntityInvincible(id, true);
                        SetBlockingOfNonTemporaryEvents(id, true);

                    }
                }

            }

            Atm.pedsAreLoaded = true;
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
                    if (!currentCoords.Contains(tempCoords))
                    {
                        currentCoords.Add(tempCoords);
                    }
                    
                }

                Vector3 currect_coords = GetEntityCoords(PlayerPedId(), false);
                foreach (List<float> coords in currentCoords)
                {
                    if (MathUtils.CheckIfCoordsAreInRadeo(coords[0], coords[1], coords[2], currect_coords.X, currect_coords.Y, currect_coords.Z, radeo))
                    {
                        if (IsControlJustReleased(0, 38))
                        {
                            if (!Atm.playerHaveAccount)
                            {
                                TriggerServerEvent("createBankAcccount", Exports["core-ztzbx"].playerToken());
                                TriggerServerEvent("getIfPlayerHaveAccount", Exports["core-ztzbx"].playerToken());
                                Exports["notification"].send("Congratulations " + Exports["core-ztzbx"].playerUsername(), "Banker - Central Bank", "You have created a bank account!");
                            }
                            else
                            {
                                Exports["notification"].send("Dear " + Exports["core-ztzbx"].playerUsername(), "Banker - Central Bank", "If you already have an account, you can't create another one.");
                            }
                            await Delay(100);
                        }
                    }

                }
            }
        }


    }
}