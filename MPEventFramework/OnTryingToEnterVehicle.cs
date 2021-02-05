using System;

namespace MPEventFramework
{
    public class OnTryingToEnterVehicle : EventArgs
    {
        public OnTryingToEnterVehicle(int vehicleHandle)
        {
            VehicleHandle = vehicleHandle;
        }

        public int VehicleHandle { get; }
    }
}