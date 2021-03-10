using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace MPFrameworkClient
{
    class MPF_Vehicle : BaseScript
    {
        public const int HEALTH_NONE = -1;
        public const int HANDLE_NONE = -1;

        public const int SEAT_NONE = -2;
        public const int SEAT_DRIVER = -1;
        public const int SEAT_PASSENGER = 0;
        public const int SEAT_BACK_LEFT_PASSENGER = 1;
        public const int SEAT_BACK_RIGHT_PASSENGER = 2;
        public const int SEAT_FURTHER_BACK_LEFT_PASSENGER = 3;
        public const int SEAT_FURTHER_BACK_RIGHT_PASSENGER = 4;

        public static int[] seats = new int[6] { SEAT_DRIVER, SEAT_PASSENGER, SEAT_BACK_LEFT_PASSENGER, SEAT_BACK_RIGHT_PASSENGER, SEAT_FURTHER_BACK_LEFT_PASSENGER, SEAT_FURTHER_BACK_RIGHT_PASSENGER };
        // Could be bigger seat index also for example bus.

        public static int GetMaxNumberOfSeats(int vehicleHandle)
        {
            return GetVehicleMaxNumberOfPassengers(vehicleHandle) + 1; // NOTE(Caupo 06.02.2021): +1 because the functions returns only passengers and does not include driver seat.
        }

        public static float GetSpeedInKmh(int vehicleHandle)
        {
            return (GetEntitySpeed(vehicleHandle) * 3.6f);
        }
        public static float GetSpeedInMph(int vehicleHandle)
        {
            return (GetEntitySpeed(vehicleHandle) * 2.236936f);
        }
        public static float GetSpeedInMeterPerSecond(int vehicleHandle)
        {
            return GetEntitySpeed(vehicleHandle);
        }
    }
}
