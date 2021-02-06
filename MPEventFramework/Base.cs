using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using API = CitizenFX.Core.Native.API; // Õppisin, et niiviisi saab namespacedele aliasi teha.

namespace MPEventFramework
{
    public class Base : BaseScript
    {
        const bool debug = true;

        // PLAYER IDS
        int playerHandle = -1;
        int playerNetworkId = -1;
        int pedHandle = -1;
        int pedNetworkId = -1;
        // PLAYER IDS END

        // STATES
        int state_lastVehicleHandle = MEF_Vehicle.HANDLE_NONE;
        bool state_inVehicle = false;
        int state_vehicleSeat = MEF_Vehicle.SEAT_NONE;
        bool state_tryingToEnterVehicle = false;

        bool state_running = false;
        bool state_sprinting = false;
        bool state_walking = false;
        bool state_jumping = false;
        bool state_cuffed = false;
        bool state_gettingUp = false;
        bool state_aimingFromCover = false;
        bool state_jacked = false;
        bool state_beingStealthKilled = false;
        bool state_stunned = false;
        bool state_climbing = false;
        bool state_dead = false;
        bool state_diving = false;
        bool state_driveBying = false;
        bool state_ducking = false;
        bool state_falling = false;
        bool state_inBoat = false;
        // STATES END

        bool getNewPed = true;
        int previouseSecond = 0;
        int previouseMilliSecond = 0;

        // PLAYER RELATED EVENTS
        public event PlayerSpawned OnPlayerSpawned; // TEST
        public event PlayerStartedWalking OnPlayerStartedWalking; // TEST
        public event PlayerStoppedWalking OnPlayerStoppedWalking; // TEST
        public event PlayerStartedRunning OnPlayerStartedRunning; // TEST
        public event PlayerStoppedRunning OnPlayerStoppedRunning; // TEST
        public event PlayerStartedSprinting OnPlayerStartedSprinting; // TEST
        public event PlayerStoppedSprinting OnPlayerStoppedSprinting; // TEST
        public event PlayerStartedJumping OnPlayerStartedJumping; // TEST
        public event PlayerStoppedJumping OnPlayerStoppedJumping; // TEST
        public event PlayerCuffed OnPlayerCuffed; // TEST
        public event PlayerUnCuffed OnPlayerUnCuffed; // TEST
        public event PlayerStartedToGetUp OnPlayerStartedToGetUp; // TEST
        public event PlayerStoppedToGetUp OnPlayerStoppedToGetUp; // TEST
        public event PlayerStartedToAimFromCover OnPlayerStartedToAimFromCover; // TEST
        public event PlayerStoppedToAimFromCover OnPlayerStoppedToAimFromCover; // TEST
        public event PlayerStartedGettingJacked OnPlayerStartedGettingJacked; // TEST
        public event PlayerStoppedGettingJacked OnPlayerStoppedGettingJacked; // TEST
        public event PlayerStartedGettingStunned OnPlayerStartedGettingStunned; // TEST
        public event PlayerStoppedGettingStunned OnPlayerStoppedGettingStunned; // TEST
        public event PlayerStartedClimbing OnPlayerStartedClimbing; // TEST
        public event PlayerStoppedClimbing OnPlayerStoppedClimbing; // TEST
        public event PlayerDied OnPlayerDied; // TEST
        public event PlayerRevived OnPlayerRevived; // TEST
        public event PlayerStartedDiving OnPlayerStartedDiving; // TEST
        public event PlayerStoppedDiving OnPlayerStoppedDiving; // TEST
        public event PlayerStartedDriveBy OnPlayerStartedDriveBy; // TEST
        public event PlayerStoppedDriveBy OnPlayerStoppedDriveBy; // TEST
        public event PlayerStartedDucking OnPlayerStartedDucking; // TEST
        public event PlayerStoppedDucking OnPlayerStoppedDucking; // TEST
        public event PlayerStartedFalling OnPlayerStartedFalling; // TEST
        public event PlayerStoppedFalling OnPlayerStoppedFalling; // TEST

        // VEHICLE RELATED EVENTS
        public event PlayerTryingToEnterVehicle OnPlayerTryingToEnterVehicle;
        public event PlayerEnteredVehicle OnPlayerEnteredVehicle;
        public event PlayerLeaveVehicle OnPlayerLeaveVehicle;
        public event PlayerSeatChange OnPlayerSeatChange;
        public event PlayerSpawnIntoVehicle OnPlayerSpawnIntoVehicle;
        public event PlayerEnteredBoat OnPlayerEnteredBoat; // TEST
        public event PlayerLeftBoat OnPlayerLeftBoat; // TEST

        // VEHICLE RELATED DELEGATES
        public delegate void PlayerTryingToEnterVehicle(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerEnteredVehicle(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerLeaveVehicle(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerSeatChange(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerSpawnIntoVehicle(int vehicleHandle);
        public delegate void PlayerEnteredBoat();
        public delegate void PlayerLeftBoat();

        // PLAYER RELATED DELEGATES
        public delegate void PlayerSpawned();
        public delegate void PlayerStartedWalking();
        public delegate void PlayerStoppedWalking();
        public delegate void PlayerStartedRunning();
        public delegate void PlayerStoppedRunning();
        public delegate void PlayerStartedSprinting();
        public delegate void PlayerStoppedSprinting();
        public delegate void PlayerStartedJumping();
        public delegate void PlayerStoppedJumping();
        public delegate void PlayerCuffed();
        public delegate void PlayerUnCuffed();
        public delegate void PlayerStartedToGetUp();
        public delegate void PlayerStoppedToGetUp();
        public delegate void PlayerStartedToAimFromCover();
        public delegate void PlayerStoppedToAimFromCover();
        public delegate void PlayerStartedGettingJacked();
        public delegate void PlayerStoppedGettingJacked();
        public delegate void PlayerStartedGettingStunned();
        public delegate void PlayerStoppedGettingStunned();
        public delegate void PlayerStartedClimbing();
        public delegate void PlayerStoppedClimbing();
        public delegate void PlayerDied();
        public delegate void PlayerRevived();
        public delegate void PlayerStartedDiving();
        public delegate void PlayerStoppedDiving();
        public delegate void PlayerStartedDriveBy();
        public delegate void PlayerStoppedDriveBy();
        public delegate void PlayerStartedDucking();
        public delegate void PlayerStoppedDucking();
        public delegate void PlayerStartedFalling();
        public delegate void PlayerStoppedFalling();

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

        public void Process() // MAIN LOOP
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

        // TIMING EVENTS

        public void OnSecondPassed()
        {
            //if (debug) Utils.Log("OnSecondPassed");
            CheckPlayerSpawned();
            CheckVehicleEvents();
            CheckPlayerCuffed();
            CheckPlayerJumping(); // TODO liigutada on foot
            CheckPlayerSprinting(); // TODO liigutada on foot
            CheckPlayerRunning(); // TODO liigutada on foot
            CheckPlayerWalking(); // TODO liigutada on foot
            CheckPlayerGettingUp(); // TODO liigutada on foot
            CheckPlayerAimFromCover(); // TODO liigutada on foot
            CheckPlayerGettingJacked(); // TODO liigutada sõiduki
            CheckPlayerStunned();
            CheckPlayerClimbing(); // TODO liigutada on foot
            CheckPlayerDead();
            CheckPlayerDiving();
            CheckPlayerDriveBy(); // TODO liigutada sõiduki
            CheckPlayerDucking();
            CheckPlayerFalling();
        }

        public void OnHundredMilliSecondPassed()
        {
            CheckVehicleEnteringEvents();
        }

        // HELPER FUNCTIONS

        /*
         IsPedInAnyBoat
         IsPedInAnyHeli
         IsPedInAnyPlane
         IsPedInAnyPoliceVehicle
         IsPedInAnySub
         IsPedInAnyTaxi
         IsPedInAnyTrain
         IsPedInAnyVehicle JUBA OLEMAS JU?
         IsPedInCombat IsPedInMeleeCombat
         IsPedInCover IsPedInCoverFacingLeft IsPedInHighCover
         IsPedInFlyingVehicle
         IsPedInParachuteFreeFall
         IsPedInjured
         IsPedJacking
         IsPedJumpingOutOfVehicle
         IsPedOnAnyBike
         IsPedOnFoot
         IsPedOnVehicle
         IsPedOpeningADoor
         IsPedPerformingStealthKill
         IsPedPlantingBomb
         IsPedRagdoll
         IsPedReloading
         IsPedShooting
         IsPedSwimming
         IsPedSwimmingUnderWater
         IsPedTryingToEnterALockedVehicle
         IsPedVaulting
         IsPedWearingHelmet
         IsPedArmed
         IsPedWeaponReadyToShoot
         IsPauseMenuActive

            IsVehicleTyreBurst
            IsVehicleAlarmActivated
            IsVehicleAttachedToTowTruck
            IsVehicleAttachedToTrailer
            IsVehicleDamaged
            IsVehicleEngineOnFire
            IsVehicleInBurnout
            IsVehicleOnAllWheels
            IsVehicleStopped

            OnPlayerDamage
            OnVehicleDamage
            OnPlayerShootPlayer
            OnVehicleCrash

        IsControlPressed
        IsControlJustPressed
        IsControlReleased
             */

        public void CheckPlayerFalling()
        {
            bool state = API.IsPedFalling(pedHandle);

            if (state && !state_falling)
            {
                state_falling = state;
                OnPlayerStartedFalling?.Invoke();
            }
            else if (!state && state_falling)
            {
                state_falling = state;
                OnPlayerStoppedFalling?.Invoke();
            }
        }

        public void CheckPlayerDucking()
        {
            bool state = API.IsPedDucking(pedHandle);

            if (state && !state_ducking)
            {
                state_ducking = state;
                OnPlayerStartedDucking?.Invoke();
            }
            else if (!state && state_ducking)
            {
                state_ducking = state;
                OnPlayerStoppedDucking?.Invoke();
            }
        }

        public void CheckPlayerDriveBy()
        {
            bool state = API.IsPedDoingDriveby(pedHandle);

            if (state && !state_driveBying)
            {
                state_driveBying = state;
                OnPlayerStartedDriveBy?.Invoke();
            }
            else if (!state && state_driveBying)
            {
                state_driveBying = state;
                OnPlayerStoppedDriveBy?.Invoke();
            }
        }

        public void CheckPlayerDiving()
        {
            bool state = API.IsPedDiving(pedHandle);

            if (state && !state_diving)
            {
                state_diving = state;
                OnPlayerStartedDiving?.Invoke();
            }
            else if (!state && state_diving)
            {
                state_diving = state;
                OnPlayerStoppedDiving?.Invoke();
            }
        }

        public void CheckPlayerDead()
        {
            bool state = API.IsPedDeadOrDying(pedHandle, true);

            if (state && !state_dead)
            {
                state_dead = state;
                OnPlayerDied?.Invoke();
            }
            else if (!state && state_dead)
            {
                state_dead = state;
                OnPlayerRevived?.Invoke();
            }
        }

        public void CheckPlayerClimbing()
        {
            bool state = API.IsPedClimbing(pedHandle);

            if (state && !state_climbing)
            {
                state_climbing = state;
                OnPlayerStartedClimbing?.Invoke();
            }
            else if (!state && state_climbing)
            {
                state_climbing = state;
                OnPlayerStoppedClimbing?.Invoke();
            }
        }

        public void CheckPlayerStunned()
        {
            bool state = API.IsPedBeingStunned(pedHandle, 0);

            if (state && !state_stunned)
            {
                state_stunned = state;
                OnPlayerStartedGettingStunned?.Invoke();
            }
            else if (!state && state_stunned)
            {
                state_stunned = state;
                OnPlayerStoppedGettingStunned?.Invoke();
            }
        }

        public void CheckPlayerBeingStealthKilled()
        {
            bool state = API.IsPedBeingStealthKilled(pedHandle);

            if (state && !state_beingStealthKilled)
            {
                state_beingStealthKilled = state;
                OnPlayerStartedGettingJacked?.Invoke();
            }
            else if (!state && state_beingStealthKilled)
            {
                state_beingStealthKilled = state;
                OnPlayerStoppedGettingJacked?.Invoke();
            }
        }

        public void CheckPlayerGettingJacked()
        {
            bool state = API.IsPedBeingJacked(pedHandle);

            if (state && !state_jacked)
            {
                state_jacked = state;
                OnPlayerStartedGettingJacked?.Invoke();
            }
            else if (!state && state_jacked)
            {
                state_jacked = state;
                OnPlayerStoppedGettingJacked?.Invoke();
            }
        }

        public void CheckPlayerAimFromCover()
        {
            bool state = API.IsPedAimingFromCover(pedHandle);

            if (state && !state_aimingFromCover)
            {
                state_aimingFromCover = state;
                OnPlayerStartedToAimFromCover?.Invoke();
            }
            else if (!state && state_aimingFromCover)
            {
                state_aimingFromCover = state;
                OnPlayerStoppedToAimFromCover?.Invoke();
            }
        }

        public void CheckPlayerWalking()
        {
            bool state = API.IsPedWalking(pedHandle);

            if (state && !state_walking)
            {
                state_walking = state;
                OnPlayerStartedWalking?.Invoke();
            }
            else if (!state && state_walking)
            {
                state_walking = state;
                OnPlayerStoppedWalking?.Invoke();
            }
        }

        public void CheckPlayerRunning()
        {
            bool state = API.IsPedRunning(pedHandle);

            if (state && !state_running)
            {
                state_running = state;
                OnPlayerStartedRunning?.Invoke();
            }
            else if (!state && state_running)
            {
                state_running = state;
                OnPlayerStoppedRunning?.Invoke();
            }
        }

        public void CheckPlayerGettingUp()
        {
            bool state = API.IsPedGettingUp(pedHandle);

            if (state && !state_gettingUp)
            {
                state_gettingUp = state;
                OnPlayerStartedToGetUp?.Invoke();
            }
            else if (!state && state_gettingUp)
            {
                state_gettingUp = state;
                OnPlayerStoppedToGetUp?.Invoke();
            }
        }

        public void CheckPlayerCuffed()
        {
            bool state = API.IsPedCuffed(pedHandle);

            if (state && !state_cuffed)
            {
                state_cuffed = state;
                OnPlayerCuffed?.Invoke();
            }
            else if (!state && state_cuffed)
            {
                state_cuffed = state;
                OnPlayerUnCuffed?.Invoke();
            }
        }

        public void CheckPlayerJumping()
        {
            bool state = API.IsPedJumping(pedHandle);

            if (state && !state_jumping)
            {
                state_jumping = state;
                OnPlayerStartedJumping?.Invoke();
            }
            else if (!state && state_jumping)
            {
                state_jumping = state;
                OnPlayerStoppedJumping?.Invoke();
            }
        }

        public void CheckPlayerSprinting()
        {
            bool sprinting = API.IsPedSprinting(pedHandle);

            if (sprinting && !state_sprinting)
            {
                state_sprinting = sprinting;
                OnPlayerStartedSprinting?.Invoke();
            }
            else if (!sprinting && state_sprinting)
            {
                state_sprinting = sprinting;
                OnPlayerStoppedSprinting?.Invoke();
            }
        }

        public void CheckPlayerSpawned()
        {
            int pHandle = API.GetPlayerPed(playerHandle);

            if (pHandle != pedHandle)
            {
                if (debug) Utils.Log("OnSecondPassed");
                int pNetId = API.NetworkGetNetworkIdFromEntity(pHandle);
                pedHandle = pHandle;
                pedNetworkId = pNetId;
                OnPlayerSpawned?.Invoke();
            }
        }

        public void CheckVehicleEvents()
        {
            bool isInVehicle = API.IsPedInAnyVehicle(pedHandle, false);

            if (isInVehicle && !state_inVehicle)
            {
                int vHandle = API.GetVehiclePedIsIn(pedHandle, false);
                state_lastVehicleHandle = vHandle;
                state_inVehicle = true;
                state_tryingToEnterVehicle = false;
                CheckSeat();

                if (debug) Utils.Log("OnPlayerEnteredVehicle " + vHandle);

                state_tryingToEnterVehicle = false;
                OnPlayerEnteredVehicle?.Invoke(vHandle, state_vehicleSeat);
            }
            else if (!isInVehicle && state_inVehicle)
            {
                state_inVehicle = false;
                if (debug) Utils.Log("OnPlayerLeaveVehicle");
                OnPlayerLeaveVehicle?.Invoke(state_lastVehicleHandle, state_vehicleSeat);
                state_vehicleSeat = MEF_Vehicle.SEAT_NONE;
            }
        }

        public void CheckVehicleEnteringEvents()
        {
            bool isTryingToEnter = API.IsPedInAnyVehicle(pedHandle, true);

            if (isTryingToEnter && !state_tryingToEnterVehicle && !state_inVehicle)
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

                if (veh == 0)
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

        public void CheckSeat()
        {
            int seats = MEF_Vehicle.GetMaxNumberOfSeats(state_lastVehicleHandle);
            //Utils.Log("CheckSeat seats " + seats);

            for (int i = 0; i < MEF_Vehicle.seats.Length; i++)
            {
                int seatIdx = MEF_Vehicle.seats[i];
                int ped = API.GetPedInVehicleSeat(state_lastVehicleHandle, seatIdx);
                //Utils.Log("SEAT CHECKED " + i);

                if (ped == pedHandle)
                {
                    if (seatIdx != state_vehicleSeat)
                    {
                        state_vehicleSeat = seatIdx;
                        if (debug) Utils.Log("OnPlayerSeatChange " + state_vehicleSeat);
                        OnPlayerSeatChange?.Invoke(state_lastVehicleHandle, state_vehicleSeat);
                    }
                    //Utils.Log("SEAT BREAK " + i);
                    break;
                }
            }

            if (seats >= MEF_Vehicle.seats.Length) // NOTE(Caupo 06.02.2021): For cases where vehicle has more than 6 pre-defined seats for example bus.
            {
                int lastSeatIdx = MEF_Vehicle.seats.Last() + 1;
                int seatsToCheckMore = seats - lastSeatIdx;
                int lastSeat = seatsToCheckMore + lastSeatIdx;

                for (int seat = lastSeatIdx; seat < lastSeat; seat++)
                {
                    //Utils.Log("SEAT CHECKED-2: " + seat);

                    int ped = API.GetPedInVehicleSeat(state_lastVehicleHandle, seat);

                    if (ped == pedHandle)
                    {
                        if (seat != state_vehicleSeat)
                        {
                            state_vehicleSeat = seat;
                            if (debug) Utils.Log("OnPlayerSeatChange " + state_vehicleSeat);
                            OnPlayerSeatChange?.Invoke(state_lastVehicleHandle, state_vehicleSeat);
                        }
                        //Utils.Log("SEAT BREAK-2: " + seat);
                        break;
                    }
                }
            }
        }
    }
}
