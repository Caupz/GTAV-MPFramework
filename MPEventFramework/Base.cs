using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using API = CitizenFX.Core.Native.API; // Õppisin, et niiviisi saab namespacedele aliasi teha.

namespace MPEventFramework
{
    public class Base : BaseScript
    {
        const bool debug = true;

        int playerHandle = -1;
        int playerNetworkId = -1;
        int pedHandle = -1;
        int pedNetworkId = -1;

        int state_lastVehicleHandle = Vehicle.HANDLE_NONE;
        bool state_inVehicle = false;
        int state_vehicleSeat = Vehicle.SEAT_NONE;
        bool state_tryingToEnterVehicle = false;

        bool getNewPed = true;
        int previouseSecond = 0;
        int previouseMilliSecond = 0;


        public event PlayerTryingToEnterVehicle OnPlayerTryingToEnterVehicle;
        public event PlayerEnteredVehicle OnPlayerEnteredVehicle;
        public event PlayerLeaveVehicle OnPlayerLeaveVehicle;
        public event PlayerSeatChange OnPlayerSeatChange;
        public event PlayerSpawnIntoVehicle OnPlayerSpawnIntoVehicle;

        public delegate void PlayerTryingToEnterVehicle(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerEnteredVehicle(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerLeaveVehicle(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerSeatChange(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerSpawnIntoVehicle(int vehicleHandle);

        public Base()
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

            Utils.Log("InitPlayerIds pedHandle ["+ pedHandle+ "] pedNetworkId ["+ pedNetworkId+"]");
        }

        public void Process()
        {
            API.Wait(0);
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

        public void CheckSeat()
        {
            int seats = Vehicle.GetMaxNumberOfSeats(state_lastVehicleHandle);
            //Utils.Log("CheckSeat seats " + seats);

            for (int i = 0; i < Vehicle.seats.Length; i++)
            {
                int seatIdx = Vehicle.seats[i];
                int ped = API.GetPedInVehicleSeat(state_lastVehicleHandle, seatIdx);
                //Utils.Log("SEAT CHECKED " + i);

                if (ped == pedHandle)
                {
                    if (seatIdx != state_vehicleSeat)
                    {
                        state_vehicleSeat = seatIdx;
                        if (debug) Utils.Log("BASE: OnPlayerSeatChange " + state_vehicleSeat);
                        OnPlayerSeatChange?.Invoke(state_lastVehicleHandle, state_vehicleSeat);
                    }
                    //Utils.Log("SEAT BREAK " + i);
                    break;
                }
            }

            if (seats >= Vehicle.seats.Length) // NOTE(Caupo 06.02.2021): For cases where vehicle has more than 6 pre-defined seats for example bus.
            {
                int lastSeatIdx = Vehicle.seats.Last() + 1;
                int seatsToCheckMore = seats - lastSeatIdx;
                int lastSeat = seatsToCheckMore + lastSeatIdx;

                for (int seat = lastSeatIdx; seat < lastSeat; seat++)
                {
                    //Utils.Log("SEAT CHECKED-2: " + seat);

                    int ped = API.GetPedInVehicleSeat(state_lastVehicleHandle, seat);

                    if (ped == pedHandle)
                    {
                        if(seat != state_vehicleSeat)
                        {
                            state_vehicleSeat = seat;
                            if (debug) Utils.Log("BASE: OnPlayerSeatChange " + state_vehicleSeat);
                            OnPlayerSeatChange?.Invoke(state_lastVehicleHandle, state_vehicleSeat);
                        }
                        //Utils.Log("SEAT BREAK-2: " + seat);
                        break;
                    }
                }
            }
        }

        public void OnSecondPassed()
        {
            if(getNewPed)
            {
                int pHandle = API.GetPlayerPed(playerHandle);
                int pNetId = API.NetworkGetNetworkIdFromEntity(pHandle);

                if(pHandle != pedHandle)
                {
                    pedHandle = pHandle;
                    pedNetworkId = pNetId;
                    getNewPed = false;
                }
            }

            //if (debug) Utils.Log("OnSecondPassed");

            bool isInVehicle = API.IsPedInAnyVehicle(pedHandle, false);

            if (isInVehicle && !state_inVehicle)
            {
                int vHandle = API.GetVehiclePedIsIn(pedHandle, false);
                state_lastVehicleHandle = vHandle;
                state_inVehicle = true;
                state_tryingToEnterVehicle = false;
                CheckSeat();

                if (debug) Utils.Log("BASE: OnPlayerEnteredVehicle " + vHandle);

                state_tryingToEnterVehicle = false;
                OnPlayerEnteredVehicle?.Invoke(vHandle, state_vehicleSeat);
            }
            else if(!isInVehicle && state_inVehicle)
            {
                state_inVehicle = false;
                if (debug) Utils.Log("BASE: OnPlayerLeaveVehicle");
                OnPlayerLeaveVehicle?.Invoke(state_lastVehicleHandle, state_vehicleSeat);
                state_vehicleSeat = Vehicle.SEAT_NONE;
            }
        }

        public void OnHundredMilliSecondPassed()
        {
            bool isTryingToEnter = API.IsPedInAnyVehicle(pedHandle, true);

            if(isTryingToEnter && !state_tryingToEnterVehicle && !state_inVehicle)
            {
                state_tryingToEnterVehicle = isTryingToEnter;
                int veh = API.GetVehiclePedIsEntering(pedHandle);
                // int veh2 = API.GetVehiclePedIsTryingToEnter(pedHandle); // NOTE(Caupo 06.02.2021): This is giving value 0 when almost in vehicle, so it is not so reliable.
                int seatTryingtoEnter = API.GetSeatPedIsTryingToEnter(pedHandle);

                if (seatTryingtoEnter != state_vehicleSeat)
                {
                    state_vehicleSeat = seatTryingtoEnter;
                    OnPlayerSeatChange?.Invoke(veh, state_vehicleSeat);
                }

                if(veh == 0)
                {
                    veh = API.GetVehiclePedIsIn(pedHandle, false);
                    Utils.Log("BASE OnPlayerSpawnIntoVehicle]VEH: [" + veh + "] SEAT: [" + seatTryingtoEnter + "] vHandle: " + veh);
                    OnPlayerSpawnIntoVehicle?.Invoke(veh);
                }
                else
                {
                    Utils.Log("BASE OnTryingToEnterVehicle]VEH: [" + veh + "] SEAT: [" + seatTryingtoEnter + "]");
                    OnPlayerTryingToEnterVehicle?.Invoke(veh, state_vehicleSeat);
                }
            }
        }
    }
}
