using System;

namespace MPEventFramework
{
    public class OnPlayerEnteredVehicle : EventArgs
    {
        public OnPlayerEnteredVehicle(int vehicleHandle, int vehicleSeat)
        {
            VehicleHandle = vehicleHandle;
            VehicleSeat = vehicleSeat;
        }

        public int VehicleHandle { get; }
        public int VehicleSeat { get; }
    }
}