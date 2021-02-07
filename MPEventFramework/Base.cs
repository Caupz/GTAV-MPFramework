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
        public bool enableRealtimeGametime = false;
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
        bool state_aiming = false;
        bool state_aimingFromCover = false;
        bool state_jacked = false;
        bool state_jacking = false;
        bool state_beingStealthKilled = false;
        bool state_stunned = false;
        bool state_climbing = false;
        bool state_dead = false;
        bool state_diving = false;
        bool state_driveBying = false;
        bool state_ducking = false;
        bool state_falling = false;
        bool state_inBoat = false;
        bool state_inHeli = false;
        bool state_inPlane = false;
        bool state_inPoliceVehicle = false;
        bool state_inSub = false;
        bool state_inTaxi = false;
        bool state_inTrain = false;
        bool state_inFlyingVehicle = false;
        bool state_onFoot = false;
        bool state_onBike = false;
        bool state_onVehicle = false;
        bool state_inCombat = false;
        bool state_inCover = false;
        bool state_parachuteFreefall = false;
        bool state_reloading = false;
        bool state_shooting = false;
        bool state_swimming = false;
        bool state_swimmingUnderwater = false;
        bool state_stealthKilling = false;
        bool state_vaulting = false;
        bool state_jumpingOutOfVehicle = false;
        bool state_wearingHelmet = false;
        bool state_mainMenu = false;
        bool state_readyToShoot = false;
        bool state_vehicleStopped = false;
        bool state_vehicleBurnouting = false;
        // STATES END

        bool getNewPed = true;
        int previouseSecond = 0;
        int previouseMinute = 0;
        int previouseMilliSecond = 0;

        public event SecondPassed OnSecondPassed; // TEST
        public event HundredSecondPassed OnHundredSecondPassed; // TEST
        public delegate void SecondPassed();
        public delegate void HundredSecondPassed();

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
        public event PlayerStartedJacking OnPlayerStartedJacking; // TEST
        public event PlayerStoppedJacking OnPlayerStoppedJacking; // TEST
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
        public event PlayerStartedOnFoot OnPlayerStartedOnFoot; // TEST
        public event PlayerStoppedOnFoot OnPlayerStoppedOnFoot; // TEST
        public event PlayerEnteredMeleeCombat OnPlayerEnteredMeleeCombat; // TEST
        public event PlayerLeftMeleeCombat OnPlayerLeftMeleeCombat; // TEST
        public event PlayerEnteredCover OnPlayerEnteredCover; // TEST
        public event PlayerLeftCover OnPlayerLeftCover; // TEST
        public event PlayerEnteredParachuteFreefall OnPlayerEnteredParachuteFreefall; // TEST
        public event PlayerLeftParachuteFreefall OnPlayerLeftParachuteFreefall; // TEST
        public event PlayerStartedReloading OnPlayerStartedReloading; // TEST
        public event PlayerStoppedReloading OnPlayerStoppedReloading; // TEST
        public event PlayerStartedShooting OnPlayerStartedShooting; // TEST
        public event PlayerStoppedShooting OnPlayerStoppedShooting; // TEST
        public event PlayerStartedSwimming OnPlayerStartedSwimming; // TEST
        public event PlayerStoppedSwimming OnPlayerStoppedSwimming; // TEST
        public event PlayerStartedSwimmingUnderwater OnPlayerStartedSwimmingUnderwater; // TEST
        public event PlayerStoppedSwimmingUnderwater OnPlayerStoppedSwimmingUnderwater; // TEST
        public event PlayerStartedStealthKill OnPlayerStartedStealthKill; // TEST
        public event PlayerStoppedStealthKill OnPlayerStoppedStealthKill; // TEST
        public event PlayerStartedVaulting OnPlayerStartedVaulting; // TEST
        public event PlayerStoppedVaulting OnPlayerStoppedVaulting; // TEST
        public event PlayerStartedWearingHelmet OnPlayerStartedWearingHelmet; // TEST
        public event PlayerStoppedWearingHelmet OnPlayerStoppedWearingHelmet; // TEST
        public event PlayerEnteredMainMenu OnPlayerEnteredMainMenu; // TEST
        public event PlayerLeftMainMenu OnPlayerLeftMainMenu; // TEST
        public event PlayerReadyToShoot OnPlayerReadyToShoot; // TEST
        public event PlayerNotReadyToShoot OnPlayerNotReadyToShoot; // TEST
        public event PlayerStartedAiming OnPlayerStartedAiming; // TEST
        public event PlayerStoppedAiming OnPlayerStoppedAiming; // TEST

        // VEHICLE RELATED EVENTS
        public event PlayerTryingToEnterVehicle OnPlayerTryingToEnterVehicle;
        public event PlayerEnteredVehicle OnPlayerEnteredVehicle;
        public event PlayerLeaveVehicle OnPlayerLeaveVehicle;
        public event PlayerSeatChange OnPlayerSeatChange;
        public event PlayerSpawnIntoVehicle OnPlayerSpawnIntoVehicle;
        public event PlayerEnteredBoat OnPlayerEnteredBoat; // TEST
        public event PlayerLeftBoat OnPlayerLeftBoat; // TEST
        public event PlayerEnteredHeli OnPlayerEnteredHeli; // TEST
        public event PlayerLeftHeli OnPlayerLeftHeli; // TEST
        public event PlayerEnteredPlane OnPlayerEnteredPlane; // TEST
        public event PlayerLeftPlane OnPlayerLeftPlane; // TEST
        public event PlayerEnteredPoliceVehicle OnPlayerEnteredPoliceVehicle; // TEST
        public event PlayerLeftPoliceVehicle OnPlayerLeftPoliceVehicle; // TEST
        public event PlayerEnteredSub OnPlayerEnteredSub; // TEST
        public event PlayerLeftSub OnPlayerLeftSub; // TEST
        public event PlayerEnteredTaxi OnPlayerEnteredTaxi; // TEST
        public event PlayerLeftTaxi OnPlayerLeftTaxi; // TEST
        public event PlayerEnteredTrain OnPlayerEnteredTrain; // TEST
        public event PlayerLeftTrain OnPlayerLeftTrain; // TEST
        public event PlayerEnteredFlyingVehicle OnPlayerEnteredFlyingVehicle; // TEST
        public event PlayerLeftFlyingVehicle OnPlayerLeftFlyingVehicle; // TEST
        public event PlayerStartedOnBike OnPlayerStartedOnBike; // TEST
        public event PlayerStoppedOnBike OnPlayerStoppedOnBike; // TEST
        public event PlayerStartedOnVehicle OnPlayerStartedOnVehicle; // TEST
        public event PlayerStoppedOnVehicle OnPlayerStoppedOnVehicle; // TEST
        public event PlayerStartedJumpingOutOfVehicle OnPlayerStartedJumpingOutOfVehicle; // TEST
        public event PlayerStoppedJumpingOutOfVehicle OnPlayerStoppedJumpingOutOfVehicle; // TEST
        public event PlayerStartedMovingVehicle OnPlayerStartedMovingVehicle; // TEST
        public event PlayerStoppedVehicle OnPlayerStoppedVehicle; // TEST
        public event PlayerStartedBurnouting OnPlayerStartedBurnouting; // TEST
        public event PlayerStoppedBurnouting OnPlayerStoppedBurnouting; // TEST

        // VEHICLE RELATED DELEGATES
        public delegate void PlayerStartedBurnouting();
        public delegate void PlayerStoppedBurnouting();
        public delegate void PlayerStartedMovingVehicle();
        public delegate void PlayerStoppedVehicle();
        public delegate void PlayerStartedJumpingOutOfVehicle();
        public delegate void PlayerStoppedJumpingOutOfVehicle();
        public delegate void PlayerTryingToEnterVehicle(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerEnteredVehicle(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerLeaveVehicle(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerSeatChange(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerSpawnIntoVehicle(int vehicleHandle);
        public delegate void PlayerEnteredBoat();
        public delegate void PlayerLeftBoat();
        public delegate void PlayerEnteredHeli();
        public delegate void PlayerLeftHeli();
        public delegate void PlayerEnteredPlane();
        public delegate void PlayerLeftPlane();
        public delegate void PlayerEnteredPoliceVehicle();
        public delegate void PlayerLeftPoliceVehicle();
        public delegate void PlayerEnteredSub();
        public delegate void PlayerLeftSub();
        public delegate void PlayerEnteredTaxi();
        public delegate void PlayerLeftTaxi();
        public delegate void PlayerEnteredTrain();
        public delegate void PlayerLeftTrain();
        public delegate void PlayerEnteredFlyingVehicle();
        public delegate void PlayerLeftFlyingVehicle();
        public delegate void PlayerStartedOnBike();
        public delegate void PlayerStoppedOnBike();
        public delegate void PlayerStartedOnVehicle();
        public delegate void PlayerStoppedOnVehicle();

        // PLAYER RELATED DELEGATES
        public delegate void PlayerStartedAiming();
        public delegate void PlayerStoppedAiming();
        public delegate void PlayerReadyToShoot();
        public delegate void PlayerNotReadyToShoot();
        public delegate void PlayerEnteredMainMenu();
        public delegate void PlayerLeftMainMenu();
        public delegate void PlayerStartedWearingHelmet();
        public delegate void PlayerStoppedWearingHelmet();
        public delegate void PlayerStartedVaulting();
        public delegate void PlayerStoppedVaulting();
        public delegate void PlayerStartedStealthKill();
        public delegate void PlayerStoppedStealthKill();
        public delegate void PlayerStartedSwimmingUnderwater();
        public delegate void PlayerStoppedSwimmingUnderwater();
        public delegate void PlayerStartedSwimming();
        public delegate void PlayerStoppedSwimming();
        public delegate void PlayerStartedShooting();
        public delegate void PlayerStoppedShooting();
        public delegate void PlayerStartedWalking();
        public delegate void PlayerStoppedWalking();
        public delegate void PlayerSpawned();
        public delegate void PlayerStartedReloading();
        public delegate void PlayerStoppedReloading();
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
        public delegate void PlayerStartedOnFoot();
        public delegate void PlayerStoppedOnFoot();
        public delegate void PlayerEnteredMeleeCombat();
        public delegate void PlayerLeftMeleeCombat();
        public delegate void PlayerEnteredCover();
        public delegate void PlayerLeftCover();
        public delegate void PlayerEnteredParachuteFreefall();
        public delegate void PlayerLeftParachuteFreefall();
        public delegate void PlayerStartedJacking();
        public delegate void PlayerStoppedJacking();

        public Base()
        {
            InitPlayerIds();
            InitSystemVariables();
        }

        public void InitSystemVariables()
        {
            DateTime dt = DateTime.Now;
            previouseMinute = dt.Minute;
            previouseSecond = dt.Second;
            previouseMilliSecond = dt.Millisecond;
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

                if (previouseMinute != dt.Minute)
                {
                    CallbackOnMinutePassed(dt.Hour, dt.Minute);
                    previouseMinute = dt.Minute;
                }

                CallbackOnSecondPassed(dt.Hour, dt.Minute, dt.Second);
            }

            if(Math.Abs(previouseMilliSecond - dt.Millisecond) >= 100)
            {
                previouseMilliSecond = dt.Millisecond;
                CallbackOnHundredMilliSecondPassed();
            }
        }

        // TIMING EVENTS

        public void CallbackOnMinutePassed(int hour, int minute)
        {
        }

        public void CallbackOnSecondPassed(int hour, int minute, int second)
        {
            if(enableRealtimeGametime)
            {
                API.SetClockTime(hour, minute, second);
            }

            //if (debug) Utils.Log("OnSecondPassed");
            CheckPlayerSpawned();
            CheckVehicleEvents();
            CheckPlayerOnFoot();
            CheckPlayerCuffed();
            CheckPlayerGettingJacked();
            CheckPlayerStunned();
            CheckPlayerDead();
            CheckPlayerDiving();
            CheckPlayerFalling();
            CheckPlayerWearingHelmet();
            CheckPlayerMainMenu();
            CheckPlayerParachuteFreefall();

            if (state_onFoot)
            {
                CheckPlayerJumping();
                CheckPlayerSprinting();
                CheckPlayerRunning();
                CheckPlayerWalking();
                CheckPlayerGettingUp();
                CheckPlayerAimFromCover();
                CheckPlayerClimbing();
                CheckPlayerOnVehicle();
                CheckPlayerMeleeCombat();
                CheckPlayerCover();
                CheckPlayerJacking();
                CheckPlayerSwimming();
            }

            if (state_inVehicle)
            {
                CheckPlayerDriveBy();
                CheckPlayerDucking();
                CheckPlayerInAnyBoat();
                CheckPlayerInAnyHeli();
                CheckPlayerInAnyPlane();
                CheckPlayerInAnyPoliceVehicle();
                CheckPlayerInAnySub();
                CheckPlayerInAnyTaxi();
                CheckPlayerInAnyTrain();
                CheckPlayerInFlyingVehicle();
                CheckPlayerOnBike();
                CheckPlayerBurnouting();
            }

            if(state_swimming)
            {
                CheckPlayerSwimmingUnderwater();
            }

            OnSecondPassed?.Invoke();
        }

        public void CallbackOnHundredMilliSecondPassed()
        {
            CheckVehicleEnteringEvents();
            CheckPlayerReloading();
            CheckPlayerShooting();
            CheckPlayerJumpingOutOfVehicle();
            CheckPlayerReadyToShoot();
            CheckPlayerAiming();

            if (state_inVehicle)
            {
                CheckPlayerStoppingVehicle();
            }

            if(state_onFoot)
            {
                CheckPlayerStealthKilling();
                CheckPlayerVaulting();
            }

            OnHundredSecondPassed?.Invoke();
        }

        public void ResetPlayerRelatedStates()
        {
            if (state_swimmingUnderwater)
            {
                state_swimmingUnderwater = false;
                OnPlayerStoppedSwimmingUnderwater();
            }
            if (state_inCombat)
            {
                state_inCombat = false;
                OnPlayerLeftMeleeCombat();
            }
            if (state_swimming)
            {
                state_swimming = false;
                OnPlayerStoppedSwimming();
            }
            if (state_jacking)
            {
                state_jacking = false;
                OnPlayerStoppedJacking();
            }
            if (state_inCover)
            {
                state_inCover = false;
                OnPlayerLeftCover();
            }

            if (state_inCombat)
            {
                state_inCombat = false;
                OnPlayerLeftMeleeCombat();
            }

            if (state_onVehicle)
            {
                state_onVehicle = false;
                OnPlayerStoppedOnVehicle?.Invoke();
            }

            if (state_climbing)
            {
                state_climbing = false;
                OnPlayerStoppedClimbing?.Invoke();
            }

            if (state_aimingFromCover)
            {
                state_aimingFromCover = false;
                OnPlayerStoppedToAimFromCover?.Invoke();
            }

            if (state_gettingUp)
            {
                state_gettingUp = false;
                OnPlayerStoppedToGetUp?.Invoke();
            }

            if (state_walking)
            {
                state_walking = false;
                OnPlayerStoppedWalking?.Invoke();
            }

            if (state_running)
            {
                state_running = false;
                OnPlayerStoppedRunning?.Invoke();
            }

            if (state_sprinting)
            {
                state_sprinting = false;
                OnPlayerStoppedSprinting?.Invoke();
            }

            if(state_jumping)
            {
                state_jumping = false;
                OnPlayerStartedJumping?.Invoke();
            }

            if (state_vaulting)
            {
                state_vaulting = false;
                OnPlayerStoppedVaulting?.Invoke();
            }

            if(state_stealthKilling)
            {
                state_stealthKilling = false;
                OnPlayerStoppedStealthKill?.Invoke();
            }
        }

        public void ResetVehicleRelatedStates()
        {
            state_vehicleStopped = false;

            if(state_vehicleBurnouting)
            {
                state_vehicleBurnouting = false;
                OnPlayerStoppedBurnouting();
            }

            if(state_onBike)
            {
                state_onBike = false;
                OnPlayerStoppedOnBike();
            }

            if(state_inTrain)
            {
                state_inTrain = false;
                OnPlayerLeftTrain?.Invoke();
            }

            if(state_inTaxi)
            {
                state_inTaxi = false;
                OnPlayerLeftTaxi?.Invoke();
            }

            if(state_inSub)
            {
                state_inSub = false;
                OnPlayerLeftSub?.Invoke();
            }

            if(state_inPoliceVehicle)
            {
                state_inPoliceVehicle = false;
                OnPlayerLeftPoliceVehicle?.Invoke();
            }

            if(state_inPlane)
            {
                state_inPlane = false;
                OnPlayerLeftPlane?.Invoke();
            }

            if(state_inHeli)
            {
                state_inHeli = false;
                OnPlayerLeftHeli?.Invoke();
            }

            if(state_inFlyingVehicle)
            {
                state_inFlyingVehicle = false;
                OnPlayerLeftFlyingVehicle?.Invoke();
            }

            if(state_driveBying)
            {
                state_driveBying = false;
                OnPlayerStoppedDriveBy?.Invoke();
            }

            if(state_ducking)
            {
                state_ducking = false;
                OnPlayerStoppedDucking?.Invoke();
            }

            if(state_inBoat)
            {
                state_inBoat = false;
                OnPlayerLeftBoat();
            }
        }

        // HELPER FUNCTIONS

        /*
         OnPlayerDamage healthiga mängida GetEntityHealth GetPedArmour GetPedLastDamageBone
         OnVehicleDamage healthiga mängida GetEntityHealth GetVehicleBodyHealth GetVehicleEngineHealth GetVehiclePetrolTankHealth GetVehicleWheelHealth
         OnPlayerShootPlayer healthiga mängida ja raytrace
         OnVehicleCrash healthiga mängida ja kiirust ka arvestada

         IsControlPressed
         IsControlJustPressed
         IsControlReleased
             */

        public void CheckPlayerAiming()
        {
            bool state = API.GetPedConfigFlag(pedHandle, 78, true);

            if (state && !state_aiming)
            {
                state_aiming = state;
                OnPlayerStartedAiming?.Invoke();
            }
            else if (!state && state_aiming)
            {
                state_aiming = state;
                OnPlayerStoppedAiming?.Invoke();
            }
        }
        public void CheckPlayerBurnouting()
        {
            bool state = API.IsVehicleInBurnout(state_lastVehicleHandle);

            if (state && !state_vehicleBurnouting)
            {
                state_vehicleBurnouting = state;
                OnPlayerStartedBurnouting?.Invoke();
            }
            else if (!state && state_vehicleBurnouting)
            {
                state_vehicleBurnouting = state;
                OnPlayerStoppedBurnouting?.Invoke();
            }
        }
        public void CheckPlayerStoppingVehicle()
        {
            bool state = API.IsVehicleStopped(pedHandle);

            if (state && !state_vehicleStopped)
            {
                state_vehicleStopped = state;
                OnPlayerStoppedVehicle?.Invoke();
            }
            else if (!state && state_vehicleStopped)
            {
                state_vehicleStopped = state;
                OnPlayerStartedMovingVehicle?.Invoke();
            }
        }
        public void CheckPlayerReadyToShoot()
        {
            bool state = API.IsPedWeaponReadyToShoot(pedHandle);

            if (state && !state_readyToShoot)
            {
                state_readyToShoot = state;
                OnPlayerReadyToShoot?.Invoke();
            }
            else if (!state && state_readyToShoot)
            {
                state_readyToShoot = state;
                OnPlayerNotReadyToShoot?.Invoke();
            }
        }
        public void CheckPlayerMainMenu()
        {
            bool state = API.IsPauseMenuActive();

            if (state && !state_mainMenu)
            {
                state_mainMenu = state;
                OnPlayerEnteredMainMenu?.Invoke();
            }
            else if (!state && state_mainMenu)
            {
                state_mainMenu = state;
                OnPlayerLeftMainMenu?.Invoke();
            }
        }
        public void CheckPlayerWearingHelmet()
        {
            bool state = API.IsPedWearingHelmet(pedHandle);

            if (state && !state_wearingHelmet)
            {
                state_wearingHelmet = state;
                OnPlayerStartedWearingHelmet?.Invoke();
            }
            else if (!state && state_wearingHelmet)
            {
                state_wearingHelmet = state;
                OnPlayerStoppedWearingHelmet?.Invoke();
            }
        }
        public void CheckPlayerJumpingOutOfVehicle()
        {
            bool state = API.IsPedJumpingOutOfVehicle(pedHandle);

            if (state && !state_jumpingOutOfVehicle)
            {
                state_jumpingOutOfVehicle = state;
                OnPlayerStartedJumpingOutOfVehicle?.Invoke();
            }
            else if (!state && state_jumpingOutOfVehicle)
            {
                state_jumpingOutOfVehicle = state;
                OnPlayerStoppedJumpingOutOfVehicle?.Invoke();
            }
        }
        public void CheckPlayerVaulting()
        {
            bool state = API.IsPedVaulting(pedHandle);

            if (state && !state_vaulting)
            {
                state_vaulting = state;
                OnPlayerStartedVaulting?.Invoke();
            }
            else if (!state && state_vaulting)
            {
                state_vaulting = state;
                OnPlayerStoppedVaulting?.Invoke();
            }
        }
        public void CheckPlayerStealthKilling()
        {
            bool state = API.IsPedPerformingStealthKill(pedHandle);

            if (state && !state_stealthKilling)
            {
                state_stealthKilling = state;
                OnPlayerStartedStealthKill?.Invoke();
            }
            else if (!state && state_stealthKilling)
            {
                state_stealthKilling = state;
                OnPlayerStoppedStealthKill?.Invoke();
            }
        }
        public void CheckPlayerSwimmingUnderwater()
        {
            bool state = API.IsPedSwimmingUnderWater(pedHandle);

            if (state && !state_swimmingUnderwater)
            {
                state_swimmingUnderwater = state;
                OnPlayerStartedSwimmingUnderwater?.Invoke();
            }
            else if (!state && state_swimmingUnderwater)
            {
                state_swimmingUnderwater = state;
                OnPlayerStoppedSwimmingUnderwater?.Invoke();
            }
        }
        public void CheckPlayerSwimming()
        {
            bool state = API.IsPedSwimming(pedHandle);

            if (state && !state_swimming)
            {
                state_swimming = state;
                OnPlayerStartedSwimming?.Invoke();
            }
            else if (!state && state_swimming)
            {
                state_swimming = state;
                OnPlayerStoppedSwimming?.Invoke();
            }
        }
        public void CheckPlayerShooting()
        {
            bool state = API.IsPedShooting(pedHandle);

            if (state && !state_shooting)
            {
                CheckPlayerReadyToShoot();
                state_shooting = state;
                OnPlayerStartedShooting?.Invoke();
            }
            else if (!state && state_shooting)
            {
                state_shooting = state;
                OnPlayerStoppedShooting?.Invoke();
            }
        }
        public void CheckPlayerReloading()
        {
            bool state = API.IsPedReloading(pedHandle);

            if (state && !state_reloading)
            {
                state_reloading = state;
                OnPlayerStartedReloading?.Invoke();
            }
            else if (!state && state_reloading)
            {
                state_reloading = state;
                OnPlayerStoppedReloading?.Invoke();
            }
        }
        public void CheckPlayerJacking()
        {
            bool state = API.IsPedJacking(pedHandle);

            if (state && !state_jacking)
            {
                state_jacking = state;
                OnPlayerStartedJacking?.Invoke();
            }
            else if (!state && state_jacking)
            {
                state_jacking = state;
                OnPlayerStoppedJacking?.Invoke();
            }
        }
        public void CheckPlayerParachuteFreefall()
        {
            bool state = API.IsPedInParachuteFreeFall(pedHandle);

            if (state && !state_parachuteFreefall)
            {
                state_parachuteFreefall = state;
                OnPlayerEnteredParachuteFreefall?.Invoke();
            }
            else if (!state && state_parachuteFreefall)
            {
                state_parachuteFreefall = state;
                OnPlayerLeftParachuteFreefall?.Invoke();
            }
        }
        public void CheckPlayerCover()
        {
            bool state = API.IsPedInCover(pedHandle, false);
            if (!state) state = API.IsPedInCover(pedHandle, true);

            if (state && !state_inCover)
            {
                state_inCover = state;
                OnPlayerEnteredCover?.Invoke();
            }
            else if (!state && state_inCover)
            {
                state_inCover = state;
                OnPlayerLeftCover?.Invoke();
            }
        }
        public void CheckPlayerMeleeCombat()
        {
            bool state = API.IsPedInMeleeCombat(pedHandle);

            if (state && !state_inCombat)
            {
                state_inCombat = state;
                OnPlayerEnteredMeleeCombat?.Invoke();
            }
            else if (!state && state_inCombat)
            {
                state_inCombat = state;
                OnPlayerLeftMeleeCombat?.Invoke();
            }
        }
        public void CheckPlayerOnVehicle()
        {
            bool state = API.IsPedOnVehicle(pedHandle);

            if (state && !state_onVehicle)
            {
                state_onVehicle = state;
                OnPlayerStartedOnVehicle?.Invoke();
            }
            else if (!state && state_onVehicle)
            {
                state_onVehicle = state;
                OnPlayerStoppedOnVehicle?.Invoke();
            }
        }
        public void CheckPlayerOnBike()
        {
            bool state = API.IsPedOnAnyBike(pedHandle);

            if (state && !state_onBike)
            {
                state_onBike = state;
                OnPlayerStartedOnFoot?.Invoke();
            }
            else if (!state && state_onBike)
            {
                state_onBike = state;
                OnPlayerStoppedOnFoot?.Invoke();
            }
        }
        public void CheckPlayerOnFoot()
        {
            bool state = API.IsPedOnFoot(pedHandle);

            if (state && !state_onFoot)
            {
                state_onFoot = state;
                OnPlayerStartedOnFoot?.Invoke();
            }
            else if (!state && state_onFoot)
            {
                state_onFoot = state;
                OnPlayerStoppedOnFoot?.Invoke();
                ResetPlayerRelatedStates();
            }
        }
        public void CheckPlayerInFlyingVehicle()
        {
            bool state = API.IsPedInFlyingVehicle(pedHandle);

            if (state && !state_inFlyingVehicle)
            {
                state_inFlyingVehicle = state;
                OnPlayerEnteredFlyingVehicle?.Invoke();
            }
            else if (!state && state_inFlyingVehicle)
            {
                state_inFlyingVehicle = state;
                OnPlayerLeftFlyingVehicle?.Invoke();
            }
        }
        public void CheckPlayerInAnyHeli()
        {
            bool state = API.IsPedInAnyHeli(pedHandle);

            if (state && !state_inHeli)
            {
                state_inHeli = state;
                OnPlayerEnteredHeli?.Invoke();
            }
            else if (!state && state_inHeli)
            {
                state_inHeli = state;
                OnPlayerLeftHeli?.Invoke();
            }
        }
        public void CheckPlayerInAnyPlane()
        {
            bool state = API.IsPedInAnyPlane(pedHandle);

            if (state && !state_inPlane)
            {
                state_inPlane = state;
                OnPlayerEnteredPlane?.Invoke();
            }
            else if (!state && state_inPlane)
            {
                state_inPlane = state;
                OnPlayerLeftPlane?.Invoke();
            }
        }
        public void CheckPlayerInAnyPoliceVehicle()
        {
            bool state = API.IsPedInAnyPoliceVehicle(pedHandle);

            if (state && !state_inPoliceVehicle)
            {
                state_inPoliceVehicle = state;
                OnPlayerEnteredPoliceVehicle?.Invoke();
            }
            else if (!state && state_inPoliceVehicle)
            {
                state_inPoliceVehicle = state;
                OnPlayerLeftPoliceVehicle?.Invoke();
            }
        }
        public void CheckPlayerInAnySub()
        {
            bool state = API.IsPedInAnySub(pedHandle);

            if (state && !state_inSub)
            {
                state_inSub = state;
                OnPlayerEnteredSub?.Invoke();
            }
            else if (!state && state_inSub)
            {
                state_inSub = state;
                OnPlayerLeftSub?.Invoke();
            }
        }
        public void CheckPlayerInAnyTaxi()
        {
            bool state = API.IsPedInAnyTaxi(pedHandle);

            if (state && !state_inTaxi)
            {
                state_inTaxi = state;
                OnPlayerEnteredTaxi?.Invoke();
            }
            else if (!state && state_inTaxi)
            {
                state_inTaxi = state;
                OnPlayerLeftTaxi?.Invoke();
            }
        }
        public void CheckPlayerInAnyTrain()
        {
            bool state = API.IsPedInAnyTrain(pedHandle);

            if (state && !state_inTrain)
            {
                state_inTrain = state;
                OnPlayerEnteredTrain?.Invoke();
            }
            else if (!state && state_inTrain)
            {
                state_inTrain = state;
                OnPlayerLeftTrain?.Invoke();
            }
        }
        public void CheckPlayerInAnyBoat()
        {
            bool state = API.IsPedInAnyBoat(pedHandle);

            if (state && !state_inBoat)
            {
                state_inBoat = state;
                OnPlayerEnteredBoat?.Invoke();
            }
            else if (!state && state_inBoat)
            {
                state_inBoat = state;
                OnPlayerLeftBoat?.Invoke();
            }
        }
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
                // TODO GetPedCauseOfDeath Returns the hash of the weapon/model/object that killed the ped.
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
                ResetVehicleRelatedStates();
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
