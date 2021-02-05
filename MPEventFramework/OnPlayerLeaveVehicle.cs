using System;

namespace MPEventFramework
{
    public class OnPlayerLeaveVehicle : EventArgs
    {
        public OnPlayerLeaveVehicle(int vehicleHandle, int vehicleSeat)
        {
            VehicleHandle = vehicleHandle;
            VehicleSeat = vehicleSeat;
        }

        public int VehicleHandle { get; }
        public int VehicleSeat { get; }
    }
}