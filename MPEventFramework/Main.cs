using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using API = CitizenFX.Core.Native.API; // Õppisin, et niiviisi saab namespacedele aliasi teha.

namespace MPEventFramework
{
    public class Main : BaseScript
    {
        // TODO luua fn
        // TODO uurida kuidas seda delegateda
        // TODO includeda selle projekti dll cnr projektis
        const bool debug = true;

        int playerHandle = -1;
        int playerNetworkId = -1;
        int pedHandle = -1;
        int pedNetworkId = -1;

        
        int state_lastVehicleHandle = Vehicle.HANDLE_NONE;
        bool state_inVehicle = false;
        int state_vehicleSeat = Vehicle.SEAT_INDEX_NONE;

        int previouseSecond = 0;
        int previouseMilliSecond = 0;

        public delegate int OnTryingToEnterVehicle(int handle);
        public delegate int OnPlayerEnteredVehicle(int vehicleHandle, int vehicleSeat);
        public delegate int OnPlayerLeaveVehicle(int vehicleHandle, int vehicleSeat);

        public Main()
        {
            InitPlayerIds();
            InitSystemVariables();
        }

        public void InitSystemVariables()
        {
            previouseSecond = DateTime.Now.Second;
            previouseMilliSecond = DateTime.Now.Millisecond;
        }

        public void InitPlayerIds()
        {
            playerHandle = API.PlayerId();
            playerNetworkId = API.NetworkGetNetworkIdFromEntity(playerHandle);
            pedHandle = API.GetPlayerPed(playerHandle);
            pedNetworkId = API.NetworkGetNetworkIdFromEntity(pedHandle);
        }

        [Tick]
        public void OnTickMain()
        {
            DateTime dt = DateTime.Now;
            if (previouseSecond != dt.Second)
            {
                previouseSecond = dt.Second;
                OnSecondPassed();
            }

            if(Math.Abs(previouseMilliSecond - dt.Millisecond) >= 100)
            {
                previouseMilliSecond = dt.Millisecond;
                OnHundredMilliSecondPassed();
            }
        }

        public void OnSecondPassed()
        {
            if(debug) Utils.Log("OnSecondPassed");

            bool isInVehicle = API.IsPedInAnyVehicle(pedHandle, false);

            if(isInVehicle)
            {
                int vHandle = API.GetVehiclePedIsIn(pedHandle, false);
                state_lastVehicleHandle = vHandle;
                int seat = API.GetSeatPedIsTryingToEnter(pedHandle);
                state_vehicleSeat = seat;
                // TODO seat tuleb kuidagi teistmoodi teha. Kaardistada ära mis sõidukis ollakse ja siis eraldi checkida.
                OnPlayerEnteredVehicle(vHandle, seat);
            }
            else if(state_inVehicle)
            {
                state_inVehicle = false;
                OnPlayerLeaveVehicle(state_lastVehicleHandle, state_vehicleSeat);
                state_vehicleSeat = Vehicle.SEAT_INDEX_NONE;
            }
        }

        public void OnHundredMilliSecondPassed()
        {
            if (debug) Utils.Log("OnHundredMilliSecondPassed");

            bool isTryingToEnter = API.IsPedInAnyVehicle(pedHandle, true);

            if(isTryingToEnter && !state_inVehicle)
            {
                int veh = API.GetVehiclePedIsEntering(pedHandle);
                int veh2 = API.GetVehiclePedIsTryingToEnter(pedHandle);

                // TODO testida autode peal ja testida ka helikopterite peal. Kas mõlemas tulevad valued välja? Kasutada ainult seda mis töötab kõige paremini.

                Utils.Log("VEH: [" + veh + "] VEH2: [" + veh2 + "]");
                TryingToEnterVehicle(veh2);
            }
        }

        public void TryingToEnterVehicle(int handle)
        {
            if (debug) Utils.Log("StartingToEnterVehicle");
        }

        public void PlayerEnteredVehicle(int vehicleHandle, int vehicleSeat)
        {
            if (debug) Utils.Log("OnPlayerEnteredVehicle");
            state_lastVehicleHandle = vehicleHandle;
            state_inVehicle = true;
        }

        public void PlayerLeaveVehicle(int vehicleHandle, int vehicleSeat)
        {
            if (debug) Utils.Log("OnPlayerLeaveVehicle");
            state_inVehicle = false;
        }
    }
}
