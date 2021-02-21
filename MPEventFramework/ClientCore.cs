using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using API = CitizenFX.Core.Native.API;

namespace MPFrameworkClient
{
    public class ClientCore : BaseScript
    {
        // NOTE (07.02.2021): I know I could have used reflection to get code lesser rows longer but it uses more CPU time. But the goal here is to save CPU time as much as possible.

        public bool debug = false;
        const int maxControls = 360;


        // IDS
        public static int PlayerHandle { get; protected set; }
        public static int PlayerNetworkId { get; protected set; }
        public static int PedHandle { get; protected set; }
        public static int PedNetworkId { get; protected set; }

        public static int VehicleHandle { get; protected set; }
        public static int VehicleNetworkId { get; protected set; }
        // IDS END

        // STATES
        public static Dictionary<int, bool> ControlsPressed { get; protected set; }

        int pedHealth = MEF_Player.HEALTH_NONE;
        int pedArmour = MEF_Player.ARMOUR_NONE;
        int vehicleHealth = MEF_Vehicle.HEALTH_NONE;
        float vehicleBodyHealth = MEF_Vehicle.HEALTH_NONE;
        float vehicleEngineHealth = MEF_Vehicle.HEALTH_NONE;
        float vehiclePetrolTankHealth = MEF_Vehicle.HEALTH_NONE;
        float vehicleSpeed = 0;
        Vector3 currentPos = new Vector3(0, 0, 0);
        uint currentWeapon = 0;

        bool state_inVehicle = false;
        int state_vehicleSeat = MEF_Vehicle.SEAT_NONE;
        bool state_tryingToEnterVehicle = false;

        bool state_btn_lmb = false;
        bool state_btn_rmb = false;

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

        int previousControlMilliSecond = 0;
        int previouseSecond = 0;
        int previouseMinute = 0;
        int previouseHour = 0;
        int previouseMilliSecond = 0;

        public event SecondPassed OnSecondPassed;
        public event HundredMilliSecondPassed OnHundredMilliSecondPassed;
        public event MinutePassed OnMinutePassed;
        public event HourPassed OnHourPassed;
        public delegate void SecondPassed();
        public delegate void HundredMilliSecondPassed();
        public delegate void MinutePassed();
        public delegate void HourPassed();

        public event KeyPressed OnKeyPressed;
        public delegate void KeyPressed(int key);
        public event KeyReleased OnKeyReleased;
        public delegate void KeyReleased(int key);

        // PLAYER RELATED EVENTS
        public event PlayerSpawned OnPlayerSpawned;
        public event PlayerStartedWalking OnPlayerStartedWalking;
        public event PlayerStoppedWalking OnPlayerStoppedWalking;
        public event PlayerStartedRunning OnPlayerStartedRunning;
        public event PlayerStoppedRunning OnPlayerStoppedRunning;
        public event PlayerStartedSprinting OnPlayerStartedSprinting;
        public event PlayerStoppedSprinting OnPlayerStoppedSprinting;
        public event PlayerStartedJumping OnPlayerStartedJumping;
        public event PlayerStoppedJumping OnPlayerStoppedJumping;
        public event PlayerCuffed OnPlayerCuffed;
        public event PlayerUnCuffed OnPlayerUnCuffed;
        public event PlayerStartedToGetUp OnPlayerStartedToGetUp;
        public event PlayerStoppedToGetUp OnPlayerStoppedToGetUp;
        public event PlayerStartedToAimFromCover OnPlayerStartedToAimFromCover;
        public event PlayerStoppedToAimFromCover OnPlayerStoppedToAimFromCover;
        public event PlayerStartedGettingJacked OnPlayerStartedGettingJacked; // TEST teist inimest vaja
        public event PlayerStoppedGettingJacked OnPlayerStoppedGettingJacked; // TEST teist inimest vaja
        public event PlayerStartedJacking OnPlayerStartedJacking; // TEST ei tööta?
        public event PlayerStoppedJacking OnPlayerStoppedJacking; // TEST ei tööta?
        public event PlayerStartedGettingStunned OnPlayerStartedGettingStunned; // TEST teist inimest vaja
        public event PlayerStoppedGettingStunned OnPlayerStoppedGettingStunned; // TEST teist inimest vaja
        public event PlayerStartedClimbing OnPlayerStartedClimbing;
        public event PlayerStoppedClimbing OnPlayerStoppedClimbing;
        public event PlayerDied OnPlayerDied;
        public event PlayerRevived OnPlayerRevived;
        public event PlayerStartedDiving OnPlayerStartedDiving;
        public event PlayerStoppedDiving OnPlayerStoppedDiving;
        public event PlayerStartedDriveBy OnPlayerStartedDriveBy;
        public event PlayerStoppedDriveBy OnPlayerStoppedDriveBy;
        public event PlayerStartedFalling OnPlayerStartedFalling;
        public event PlayerStoppedFalling OnPlayerStoppedFalling;
        public event PlayerStartedOnFoot OnPlayerStartedOnFoot;
        public event PlayerStoppedOnFoot OnPlayerStoppedOnFoot;
        public event PlayerEnteredMeleeCombat OnPlayerEnteredMeleeCombat;
        public event PlayerLeftMeleeCombat OnPlayerLeftMeleeCombat;
        public event PlayerEnteredCover OnPlayerEnteredCover;
        public event PlayerLeftCover OnPlayerLeftCover;
        public event PlayerEnteredParachuteFreefall OnPlayerEnteredParachuteFreefall;
        public event PlayerLeftParachuteFreefall OnPlayerLeftParachuteFreefall;
        public event PlayerStartedReloading OnPlayerStartedReloading;
        public event PlayerStoppedReloading OnPlayerStoppedReloading;
        public event PlayerStartedShooting OnPlayerStartedShooting;
        public event PlayerStoppedShooting OnPlayerStoppedShooting;
        public event PlayerStartedSwimming OnPlayerStartedSwimming;
        public event PlayerStoppedSwimming OnPlayerStoppedSwimming;
        public event PlayerStartedSwimmingUnderwater OnPlayerStartedSwimmingUnderwater;
        public event PlayerStoppedSwimmingUnderwater OnPlayerStoppedSwimmingUnderwater;
        public event PlayerStartedStealthKill OnPlayerStartedStealthKill;
        public event PlayerStoppedStealthKill OnPlayerStoppedStealthKill;
        public event PlayerStartedVaulting OnPlayerStartedVaulting;
        public event PlayerStoppedVaulting OnPlayerStoppedVaulting;
        public event PlayerStartedWearingHelmet OnPlayerStartedWearingHelmet;
        public event PlayerStoppedWearingHelmet OnPlayerStoppedWearingHelmet;
        public event PlayerEnteredMainMenu OnPlayerEnteredMainMenu;
        public event PlayerLeftMainMenu OnPlayerLeftMainMenu;
        public event PlayerReadyToShoot OnPlayerReadyToShoot;
        public event PlayerNotReadyToShoot OnPlayerNotReadyToShoot;
        public event PlayerStartedAiming OnPlayerStartedAiming;
        public event PlayerStoppedAiming OnPlayerStoppedAiming;

        public event PlayerHealthGain OnPlayerHealthGain;
        public event PlayerHealthLoss OnPlayerHealthLoss;
        public event PlayerArmourGain OnPlayerArmourGain;
        public event PlayerArmourLoss OnPlayerArmourLoss;
        public event PlayerWeaponChange OnPlayerWeaponChange;

        // VEHICLE RELATED EVENTS
        public event PlayerTryingToEnterVehicle OnPlayerTryingToEnterVehicle;
        public event PlayerEnteredVehicle OnPlayerEnteredVehicle;
        public event PlayerLeaveVehicle OnPlayerLeaveVehicle;
        public event PlayerSeatChange OnPlayerSeatChange;
        public event PlayerSpawnIntoVehicle OnPlayerSpawnIntoVehicle;
        public event PlayerEnteredBoat OnPlayerEnteredBoat;
        public event PlayerLeftBoat OnPlayerLeftBoat;
        public event PlayerEnteredHeli OnPlayerEnteredHeli;
        public event PlayerLeftHeli OnPlayerLeftHeli;
        public event PlayerEnteredPlane OnPlayerEnteredPlane;
        public event PlayerLeftPlane OnPlayerLeftPlane;
        public event PlayerEnteredPoliceVehicle OnPlayerEnteredPoliceVehicle;
        public event PlayerLeftPoliceVehicle OnPlayerLeftPoliceVehicle;
        public event PlayerEnteredSub OnPlayerEnteredSub;
        public event PlayerLeftSub OnPlayerLeftSub;
        public event PlayerEnteredTaxi OnPlayerEnteredTaxi;
        public event PlayerLeftTaxi OnPlayerLeftTaxi;
        public event PlayerEnteredTrain OnPlayerEnteredTrain;
        public event PlayerLeftTrain OnPlayerLeftTrain;
        public event PlayerEnteredFlyingVehicle OnPlayerEnteredFlyingVehicle;
        public event PlayerLeftFlyingVehicle OnPlayerLeftFlyingVehicle;
        public event PlayerStartedOnBike OnPlayerStartedOnBike;
        public event PlayerStoppedOnBike OnPlayerStoppedOnBike;
        public event PlayerStartedOnVehicle OnPlayerStartedOnVehicle;
        public event PlayerStoppedOnVehicle OnPlayerStoppedOnVehicle;
        public event PlayerStartedJumpingOutOfVehicle OnPlayerStartedJumpingOutOfVehicle;
        public event PlayerStoppedJumpingOutOfVehicle OnPlayerStoppedJumpingOutOfVehicle;
        public event PlayerStartedMovingVehicle OnPlayerStartedMovingVehicle;
        public event PlayerStoppedVehicle OnPlayerStoppedVehicle;
        public event PlayerStartedBurnouting OnPlayerStartedBurnouting;
        public event PlayerStoppedBurnouting OnPlayerStoppedBurnouting;
        public event VehicleHealthGain OnVehicleHealthGain;
        public event VehicleHealthLoss OnVehicleHealthLoss;
        public event VehicleCrash OnVehicleCrash;

        // VEHICLE RELATED DELEGATES
        public delegate void PlayerStartedBurnouting(int vehicleHandle);
        public delegate void PlayerStoppedBurnouting(int vehicleHandle);
        public delegate void PlayerStartedMovingVehicle(int vehicleHandle);
        public delegate void PlayerStoppedVehicle(int vehicleHandle);
        public delegate void PlayerStartedJumpingOutOfVehicle(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerStoppedJumpingOutOfVehicle(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerTryingToEnterVehicle(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerEnteredVehicle(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerLeaveVehicle(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerSeatChange(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerSpawnIntoVehicle(int vehicleHandle);
        public delegate void PlayerEnteredBoat(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerLeftBoat(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerEnteredHeli(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerLeftHeli(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerEnteredPlane(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerLeftPlane(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerEnteredPoliceVehicle(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerLeftPoliceVehicle(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerEnteredSub(int vehicleHandle);
        public delegate void PlayerLeftSub(int vehicleHandle);
        public delegate void PlayerEnteredTaxi(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerLeftTaxi(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerEnteredTrain(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerLeftTrain(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerEnteredFlyingVehicle(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerLeftFlyingVehicle(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerStartedOnBike(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerStoppedOnBike(int vehicleHandle, int vehicleSeat);
        public delegate void PlayerStartedOnVehicle();
        public delegate void PlayerStoppedOnVehicle();
        public delegate void VehicleHealthGain(int vehicleHandle, int vehicleHealth, float vehicleBodyHealth, float vehicleEngineHealth, float vehiclePetrolTankHealth);
        public delegate void VehicleHealthLoss(int vehicleHandle, int vehicleHealth, float vehicleBodyHealth, float vehicleEngineHealth, float vehiclePetrolTankHealth);
        public delegate void VehicleCrash(int vehicleHandle);

        // PLAYER RELATED DELEGATES
        public delegate void PlayerStartedAiming(uint weapon);
        public delegate void PlayerStoppedAiming(uint weapon);
        public delegate void PlayerReadyToShoot(uint weapon);
        public delegate void PlayerNotReadyToShoot();
        public delegate void PlayerEnteredMainMenu();
        public delegate void PlayerLeftMainMenu();
        public delegate void PlayerStartedWearingHelmet();
        public delegate void PlayerStoppedWearingHelmet();
        public delegate void PlayerStartedVaulting();
        public delegate void PlayerStoppedVaulting();
        public delegate void PlayerStartedStealthKill(uint weapon);
        public delegate void PlayerStoppedStealthKill(uint weapon);
        public delegate void PlayerStartedSwimmingUnderwater();
        public delegate void PlayerStoppedSwimmingUnderwater();
        public delegate void PlayerStartedSwimming();
        public delegate void PlayerStoppedSwimming();
        public delegate void PlayerStartedShooting(uint weapon, int ammo);
        public delegate void PlayerStoppedShooting(uint weapon, int ammo);
        public delegate void PlayerStartedWalking();
        public delegate void PlayerStoppedWalking();
        public delegate void PlayerSpawned(int newPedHandle, int newPedNetworkId, float x, float y, float z);
        public delegate void PlayerStartedReloading(uint weapon);
        public delegate void PlayerStoppedReloading(uint weapon);
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
        public delegate void PlayerStartedToAimFromCover(uint weapon);
        public delegate void PlayerStoppedToAimFromCover(uint weapon);
        public delegate void PlayerStartedGettingJacked();
        public delegate void PlayerStoppedGettingJacked();
        public delegate void PlayerStartedGettingStunned();
        public delegate void PlayerStoppedGettingStunned();
        public delegate void PlayerStartedClimbing();
        public delegate void PlayerStoppedClimbing();
        public delegate void PlayerDied(float x, float y, float z);
        public delegate void PlayerRevived(float x, float y, float z);
        public delegate void PlayerStartedDiving();
        public delegate void PlayerStoppedDiving();
        public delegate void PlayerStartedDriveBy(int vehicleHandle, uint weapon);
        public delegate void PlayerStoppedDriveBy(int vehicleHandle, uint weapon);
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
        public delegate void PlayerHealthGain(int oldHealth, int newHealth);
        public delegate void PlayerHealthLoss(int oldHealth, int newHealth);
        public delegate void PlayerArmourGain(int oldArmour, int newArmour);
        public delegate void PlayerArmourLoss(int oldArmour, int newArmour);
        public delegate void PlayerWeaponChange(uint oldWeapon, uint newWeapon);

        public ClientCore()
        {
            Utils.Log("MP FRAMEWORK: Created by Caupo Helvik (https://caupo.ee)");
            DateTime dt = DateTime.Now;
            InitPlayerIds();
            InitSystemVariables();
            InitEventHandlers();
        }

        public void InitEventHandlers()
        {
            EventHandlers["SetWeatherTransition"] += new Action<uint, uint, float>(SetWeatherTransition);
            EventHandlers["SetWind"] += new Action<int, float>(SetWind);
            EventHandlers["SetClock"] += new Action<int, int, int>(SetClock);
        }

        public void SetClock(int hour, int minute, int second)
        {
            API.SetClockTime(hour, minute, second);
        }

        public void SetWind(int wind, float direction)
        {
            API.SetWind(wind);
            API.SetWindDirection(direction);
        }

        public void SetWeatherTransition(uint weatherFrom, uint weatherTo, float percentageOfWeatherTo)
        {
            API.SetWeatherTypeTransition(weatherFrom, weatherTo, percentageOfWeatherTo);
        }

        private void InitControls()
        {
            ControlsPressed = new Dictionary<int, bool>();

            for (int i = 0; i <= maxControls; i++)
            {
                ControlsPressed.Add(i, false);
            }
        }

        private void InitSystemVariables()
        {
            DateTime dt = DateTime.Now;
            previouseMinute = dt.Minute;
            previouseSecond = dt.Second;
            previouseMilliSecond = dt.Millisecond;
            previousControlMilliSecond = dt.Millisecond;
            InitControls();
        }

        private void InitPlayerIds()
        {
            PlayerHandle = API.PlayerId();
            PlayerNetworkId = API.NetworkGetNetworkIdFromEntity(PlayerHandle);
            PedHandle = API.GetPlayerPed(PlayerHandle);
            PedNetworkId = API.NetworkGetNetworkIdFromEntity(PedHandle);

            if(debug) Utils.Log("InitPlayerIds pedHandle [" + PedHandle + "] pedNetworkId [" + PedNetworkId + "]");
        }

        public async Task Process() // MAIN LOOP
        {
            //int startTick = Environment.TickCount;

            await Delay(0);
            DateTime dt = DateTime.Now;

            if (previouseSecond != dt.Second)
            {
                previouseSecond = dt.Second;

                if (previouseMinute != dt.Minute)
                {
                    CallbackOnMinutePassed();
                    previouseMinute = dt.Minute;

                    if(previouseHour != dt.Hour)
                    {
                        OnHourPassed?.Invoke();
                        previouseHour = dt.Hour;
                    }
                }

                CallbackOnSecondPassed(dt.Hour, dt.Minute, dt.Second);
            }

            if (Math.Abs(previouseMilliSecond - dt.Millisecond) >= 100)
            {
                previouseMilliSecond = dt.Millisecond;
                CallbackOnHundredMilliSecondPassed();
            }

            if(OnKeyPressed != null || OnKeyReleased != null)
            {
                if (Math.Abs(previousControlMilliSecond - dt.Millisecond) >= 20)
                {
                    previousControlMilliSecond = dt.Millisecond;
                    CallbackOnControlMillisecondUpdate();
                }
            }

            //Utils.Log("TICKS SPENT: " + (Environment.TickCount - startTick));
        }

        private void CallbackOnControlMillisecondUpdate()
        {
            for (int i = 0; i <= maxControls; i++)
            {
                bool controlPressed = API.IsControlPressed(0, i);

                if(ControlsPressed[i] != controlPressed)
                {
                    if(controlPressed)
                    {
                        OnKeyPressed?.Invoke(i);
                    }
                    else
                    {
                        OnKeyReleased?.Invoke(i);
                    }

                    ControlsPressed[i] = controlPressed;
                }
            }
        }

        // TIMING EVENTS

        private void CallbackOnMinutePassed()
        {
            OnMinutePassed?.Invoke();
        }

        private void CallbackOnSecondPassed(int hour, int minute, int second)
        {
            UpdateCurrentPos();
            
            //if (debug) Utils.Log("OnSecondPassed");
            CheckPlayerSpawned();
            CheckVehicleEvents();
            CheckPlayerOnFoot();
            CheckPlayerCuffed();
            CheckPlayerGettingJacked();
            CheckPlayerStunned();
            CheckPlayerDead();
            CheckPlayerFalling();
            CheckPlayerWearingHelmet();
            CheckPlayerMainMenu();
            CheckPlayerParachuteFreefall();
            CheckPlayerHealth();
            CheckPlayerArmour();

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
                CheckPlayerSwimming();
            }

            if (state_inVehicle)
            {
                CheckPlayerDriveBy();
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
                UpdateVehicleSpeed();
            }

            if(state_swimming)
            {
                CheckPlayerSwimmingUnderwater();
            }

            OnSecondPassed?.Invoke();
        }

        private void CallbackOnHundredMilliSecondPassed()
        {
            CheckVehicleEnteringEvents();
            UpdateCurrentWeapon();
            CheckPlayerReloading();
            CheckPlayerJumpingOutOfVehicle();
            CheckPlayerAiming();
            CheckPlayerDiving();
            CheckPlayerJacking();
            CheckPlayerShooting();

            if (!state_shooting)
            {
                CheckPlayerReadyToShoot();
            }

            if (state_inVehicle)
            {
                CheckPlayerStoppingVehicle();
                CheckVehicleHealth();
            }

            if(state_onFoot)
            {
                CheckPlayerStealthKilling();
                CheckPlayerVaulting();
            }

            OnHundredMilliSecondPassed?.Invoke();
        }

        private void UpdateVehicleHealth(int vHealth, float vBodyHealth, float vEngineHealth, float vPetrolTankHealth)
        {
            vehicleHealth = vHealth;
            vehicleBodyHealth = vBodyHealth;
            vehicleEngineHealth = vEngineHealth;
            vehiclePetrolTankHealth = vPetrolTankHealth;
        }

        private void CheckVehicleHealth()
        {
            int vHealth = API.GetEntityHealth(VehicleHandle);
            float vBodyHealth = API.GetVehicleBodyHealth(VehicleHandle);
            float vEngineHealth = API.GetVehicleEngineHealth(VehicleHandle);
            float vPetrolTankHealth = API.GetVehiclePetrolTankHealth(VehicleHandle);

            if (vHealth > vehicleHealth || vBodyHealth > vehicleBodyHealth || vEngineHealth > vehicleEngineHealth || vPetrolTankHealth > vehiclePetrolTankHealth)
            {
                if(debug) Utils.Log("OnVehicleHealthGain");
                UpdateVehicleHealth(vHealth, vBodyHealth, vEngineHealth, vPetrolTankHealth);
                OnVehicleHealthGain?.Invoke(VehicleHandle, vehicleHealth, vehicleBodyHealth, vehicleEngineHealth, vehiclePetrolTankHealth);
                TriggerServerEvent("OnVehicleHealthGain", VehicleNetworkId, vehicleHealth, vehicleBodyHealth, vehicleEngineHealth, vehiclePetrolTankHealth);
            }
            if (vHealth < vehicleHealth || vBodyHealth < vehicleBodyHealth || vEngineHealth < vehicleEngineHealth || vPetrolTankHealth < vehiclePetrolTankHealth)
            {
                UpdateVehicleHealth(vHealth, vBodyHealth, vEngineHealth, vPetrolTankHealth);

                float speed = MEF_Vehicle.GetSpeedInKmh(VehicleHandle);
                float speedQuanfitsent = vehicleSpeed / 1.33f;

                //Utils.Log("vHealth: " + vHealth + " speed:" + speed + " speedQuanfitsent: " + speedQuanfitsent);

                if (speed < speedQuanfitsent)
                {
                    if (debug) Utils.Log("OnVehicleCrash");
                    OnVehicleCrash?.Invoke(VehicleHandle);
                    TriggerServerEvent("OnVehicleCrash");
                    vehicleSpeed = speed;
                }

                if (debug) Utils.Log("OnVehicleHealthLoss");
                OnVehicleHealthLoss?.Invoke(VehicleHandle, vehicleHealth, vehicleBodyHealth, vehicleEngineHealth, vehiclePetrolTankHealth);
                TriggerServerEvent("OnVehicleHealthLoss", VehicleNetworkId, vehicleHealth, vehicleBodyHealth, vehicleEngineHealth, vehiclePetrolTankHealth);
            }
        }
        private void CheckPlayerHealth()
        {
            int pHealth = API.GetEntityHealth(PedHandle);

            if (pHealth > pedHealth)
            {
                if (debug) Utils.Log("OnPlayerHealthGain");
                OnPlayerHealthGain?.Invoke(pedHealth, pHealth);
                TriggerServerEvent("OnPlayerHealthGain", pedHealth, pHealth);
                pedHealth = pHealth;
            }
            else if (pHealth < pedHealth)
            {
                if (debug) Utils.Log("OnPlayerHealthLoss");
                OnPlayerHealthLoss?.Invoke(pedHealth, pHealth);
                TriggerServerEvent("OnPlayerHealthLoss", pedHealth, pHealth);
                pedHealth = pHealth;
            }
        }

        private void CheckPlayerArmour()
        {
            int pArmour = API.GetPedArmour(PedHandle);

            if (pArmour > pedArmour)
            {
                if (debug) Utils.Log("OnPlayerArmourGain");
                OnPlayerArmourGain?.Invoke(pedArmour, pArmour);
                TriggerServerEvent("OnPlayerArmourGain", pedArmour, pArmour);
                pedArmour = pArmour;
            }
            else if (pArmour < pedArmour)
            {
                if (debug) Utils.Log("OnPlayerArmourLoss");
                OnPlayerArmourLoss?.Invoke(pedArmour, pArmour);
                TriggerServerEvent("OnPlayerArmourLoss", pedArmour, pArmour);
                pedArmour = pArmour;
            }
        }

        private void ResetPlayerRelatedStates()
        {
            if (state_swimmingUnderwater)
            {
                state_swimmingUnderwater = false;
                if (debug) Utils.Log("OnPlayerStoppedSwimmingUnderwater");
                OnPlayerStoppedSwimmingUnderwater?.Invoke();
                TriggerServerEvent("OnPlayerStoppedSwimmingUnderwater");
            }
            if (state_inCombat)
            {
                state_inCombat = false;
                if (debug) Utils.Log("OnPlayerLeftMeleeCombat");
                OnPlayerLeftMeleeCombat?.Invoke();
                TriggerServerEvent("OnPlayerLeftMeleeCombat", currentWeapon);
            }
            if (state_swimming)
            {
                state_swimming = false;
                if (debug) Utils.Log("OnPlayerStoppedSwimming");
                OnPlayerStoppedSwimming?.Invoke();
                TriggerServerEvent("OnPlayerStoppedSwimming");
            }
            if (state_jacking)
            {
                state_jacking = false;
                if (debug) Utils.Log("OnPlayerStoppedJacking");
                OnPlayerStoppedJacking?.Invoke();
                TriggerServerEvent("OnPlayerStoppedJacking");
            }
            if (state_inCover)
            {
                state_inCover = false;
                if (debug) Utils.Log("OnPlayerLeftCover");
                OnPlayerLeftCover?.Invoke();
                TriggerServerEvent("OnPlayerLeftCover");
            }

            if (state_inCombat)
            {
                state_inCombat = false;
                if (debug) Utils.Log("OnPlayerLeftMeleeCombat");
                OnPlayerLeftMeleeCombat?.Invoke();
                TriggerServerEvent("OnPlayerLeftMeleeCombat", currentWeapon);
            }

            if (state_onVehicle)
            {
                state_onVehicle = false;
                if (debug) Utils.Log("OnPlayerStoppedOnVehicle");
                OnPlayerStoppedOnVehicle?.Invoke();
                TriggerServerEvent("OnPlayerStoppedOnVehicle");
            }

            if (state_climbing)
            {
                state_climbing = false;
                if (debug) Utils.Log("OnPlayerStoppedClimbing");
                OnPlayerStoppedClimbing?.Invoke();
                TriggerServerEvent("OnPlayerStoppedClimbing");
            }

            if (state_aimingFromCover)
            {
                state_aimingFromCover = false;
                if (debug) Utils.Log("OnPlayerStoppedToAimFromCover");
                OnPlayerStoppedToAimFromCover?.Invoke(currentWeapon);
                TriggerServerEvent("OnPlayerStoppedToAimFromCover", currentWeapon);
            }

            if (state_gettingUp)
            {
                state_gettingUp = false;
                if (debug) Utils.Log("OnPlayerStoppedToGetUp");
                OnPlayerStoppedToGetUp?.Invoke();
                TriggerServerEvent("OnPlayerStoppedToGetUp");
            }

            if (state_walking)
            {
                state_walking = false;
                if (debug) Utils.Log("OnPlayerStoppedWalking");
                OnPlayerStoppedWalking?.Invoke();
                TriggerServerEvent("OnPlayerStoppedWalking");
            }

            if (state_running)
            {
                state_running = false;
                if (debug) Utils.Log("OnPlayerStoppedRunning");
                OnPlayerStoppedRunning?.Invoke();
                TriggerServerEvent("OnPlayerStoppedRunning");
            }

            if (state_sprinting)
            {
                state_sprinting = false;
                if (debug) Utils.Log("OnPlayerStoppedSprinting");
                OnPlayerStoppedSprinting?.Invoke();
                TriggerServerEvent("OnPlayerStoppedSprinting");
            }

            if(state_jumping)
            {
                state_jumping = false;
                if (debug) Utils.Log("OnPlayerStartedJumping");
                OnPlayerStartedJumping?.Invoke();
                TriggerServerEvent("OnPlayerStartedJumping");
            }

            if (state_vaulting)
            {
                state_vaulting = false;
                if (debug) Utils.Log("OnPlayerStoppedVaulting");
                OnPlayerStoppedVaulting?.Invoke();
                TriggerServerEvent("OnPlayerStoppedVaulting");
            }

            if(state_stealthKilling)
            {
                state_stealthKilling = false;
                if (debug) Utils.Log("OnPlayerStoppedStealthKill");
                OnPlayerStoppedStealthKill?.Invoke(currentWeapon);
                TriggerServerEvent("OnPlayerStoppedStealthKill", currentWeapon);
            }
        }

        private void ResetVehicleRelatedStates()
        {
            state_vehicleStopped = false;

            if(state_vehicleBurnouting)
            {
                state_vehicleBurnouting = false;
                if (debug) Utils.Log("OnPlayerStoppedBurnouting");
                OnPlayerStoppedBurnouting?.Invoke(VehicleHandle);
                TriggerServerEvent("OnPlayerStoppedBurnouting", VehicleNetworkId);
            }

            if(state_onBike)
            {
                state_onBike = false;
                if (debug) Utils.Log("OnPlayerStoppedOnBike");
                OnPlayerStoppedOnBike?.Invoke(VehicleHandle, state_vehicleSeat);
                TriggerServerEvent("OnPlayerStoppedOnBike", VehicleNetworkId, state_vehicleSeat);
            }

            if(state_inTrain)
            {
                state_inTrain = false;
                if (debug) Utils.Log("OnPlayerLeftTrain");
                OnPlayerLeftTrain?.Invoke(VehicleHandle, state_vehicleSeat);
                TriggerServerEvent("OnPlayerLeftTrain", VehicleNetworkId, state_vehicleSeat);
            }

            if(state_inTaxi)
            {
                state_inTaxi = false;
                if (debug) Utils.Log("OnPlayerLeftTaxi");
                OnPlayerLeftTaxi?.Invoke(VehicleHandle, state_vehicleSeat);
                TriggerServerEvent("OnPlayerLeftTaxi", VehicleNetworkId, state_vehicleSeat);
            }

            if(state_inSub)
            {
                state_inSub = false;
                if (debug) Utils.Log("OnPlayerLeftSub");
                OnPlayerLeftSub?.Invoke(VehicleHandle);
                TriggerServerEvent("OnPlayerLeftSub", VehicleNetworkId, state_vehicleSeat);
            }

            if(state_inPoliceVehicle)
            {
                state_inPoliceVehicle = false;
                if (debug) Utils.Log("OnPlayerLeftPoliceVehicle");
                OnPlayerLeftPoliceVehicle?.Invoke(VehicleHandle, state_vehicleSeat);
                TriggerServerEvent("OnPlayerLeftPoliceVehicle", VehicleNetworkId, state_vehicleSeat);
            }

            if(state_inPlane)
            {
                state_inPlane = false;
                if (debug) Utils.Log("OnPlayerLeftPlane");
                OnPlayerLeftPlane?.Invoke(VehicleHandle, state_vehicleSeat);
                TriggerServerEvent("OnPlayerLeftPlane", VehicleNetworkId, state_vehicleSeat);
            }

            if(state_inHeli)
            {
                state_inHeli = false;
                if (debug) Utils.Log("OnPlayerLeftHeli");
                OnPlayerLeftHeli?.Invoke(VehicleHandle, state_vehicleSeat);
                TriggerServerEvent("OnPlayerLeftHeli", VehicleNetworkId, state_vehicleSeat);
            }

            if(state_inFlyingVehicle)
            {
                state_inFlyingVehicle = false;
                if (debug) Utils.Log("OnPlayerLeftFlyingVehicle");
                OnPlayerLeftFlyingVehicle?.Invoke(VehicleHandle, state_vehicleSeat);
                TriggerServerEvent("OnPlayerLeftFlyingVehicle", VehicleNetworkId, state_vehicleSeat);
            }

            if(state_driveBying)
            {
                state_driveBying = false;
                if (debug) Utils.Log("OnPlayerStoppedDriveBy");
                OnPlayerStoppedDriveBy?.Invoke(VehicleHandle, currentWeapon);
                TriggerServerEvent("OnPlayerStoppedDriveBy", VehicleNetworkId, state_vehicleSeat);
            }

            if(state_inBoat)
            {
                state_inBoat = false;
                if(debug) Utils.Log("OnPlayerLeftBoat");
                OnPlayerLeftBoat?.Invoke(VehicleHandle, state_vehicleSeat);
                TriggerServerEvent("OnPlayerLeftBoat", VehicleNetworkId, state_vehicleSeat);
            }
        }

        // HELPER FUNCTIONS

        private void CheckPlayerAiming()
        {
            bool state = API.GetPedConfigFlag(PedHandle, 78, true);

            if (state && !state_aiming)
            {
                state_aiming = state;
                if (debug) Utils.Log("OnPlayerStartedAiming");
                OnPlayerStartedAiming?.Invoke(currentWeapon);
                TriggerServerEvent("OnPlayerStartedAiming", currentWeapon);
            }
            else if (!state && state_aiming)
            {
                state_aiming = state;
                if (debug) Utils.Log("OnPlayerStoppedAiming");
                OnPlayerStoppedAiming?.Invoke(currentWeapon);
                TriggerServerEvent("OnPlayerStoppedAiming", currentWeapon);
            }
        }
        private void CheckPlayerBurnouting()
        {
            if (state_vehicleSeat != MEF_Vehicle.SEAT_DRIVER)
            {
                return;
            }

            bool state = API.IsVehicleInBurnout(VehicleHandle);

            if (state && !state_vehicleBurnouting)
            {
                state_vehicleBurnouting = state;
                if (debug) Utils.Log("OnPlayerStartedBurnouting");
                OnPlayerStartedBurnouting?.Invoke(VehicleHandle);
                TriggerServerEvent("OnPlayerStartedBurnouting", VehicleNetworkId);
            }
            else if (!state && state_vehicleBurnouting)
            {
                state_vehicleBurnouting = state;
                if (debug) Utils.Log("OnPlayerStoppedBurnouting");
                OnPlayerStoppedBurnouting?.Invoke(VehicleHandle);
                TriggerServerEvent("OnPlayerStoppedBurnouting", VehicleNetworkId);
            }
        }
        private void CheckPlayerStoppingVehicle()
        {
            if(state_vehicleSeat != MEF_Vehicle.SEAT_DRIVER)
            {
                return;
            }

            if (vehicleSpeed == 0 && !state_vehicleStopped)
            {
                state_vehicleStopped = true;
                if (debug) Utils.Log("OnPlayerStoppedVehicle");
                OnPlayerStoppedVehicle?.Invoke(VehicleHandle);
                TriggerServerEvent("OnPlayerStoppedVehicle", VehicleNetworkId);
            }
            else if ((vehicleSpeed > 0 || vehicleSpeed < 0) && state_vehicleStopped)
            {
                state_vehicleStopped = false;
                if (debug) Utils.Log("OnPlayerStartedMovingVehicle");
                OnPlayerStartedMovingVehicle?.Invoke(VehicleHandle);
                TriggerServerEvent("OnPlayerStartedMovingVehicle", VehicleNetworkId);
            }
        }
        private void CheckPlayerReadyToShoot()
        {
            bool state = API.IsPedWeaponReadyToShoot(PedHandle);

            if (state && !state_readyToShoot)
            {
                state_readyToShoot = state;
                if (debug) Utils.Log("OnPlayerReadyToShoot");
                OnPlayerReadyToShoot?.Invoke(currentWeapon);
                TriggerServerEvent("OnPlayerReadyToShoot", currentWeapon);
            }
            else if (!state && state_readyToShoot)
            {
                state_readyToShoot = state;
                if (debug) Utils.Log("OnPlayerNotReadyToShoot");
                OnPlayerNotReadyToShoot?.Invoke();
                TriggerServerEvent("OnPlayerNotReadyToShoot");
            }
        }
        private void CheckPlayerMainMenu()
        {
            bool state = API.IsPauseMenuActive();

            if (state && !state_mainMenu)
            {
                state_mainMenu = state;
                if (debug) Utils.Log("OnPlayerEnteredMainMenu");
                OnPlayerEnteredMainMenu?.Invoke();
                TriggerServerEvent("OnPlayerEnteredMainMenu");
            }
            else if (!state && state_mainMenu)
            {
                state_mainMenu = state;
                if (debug) Utils.Log("OnPlayerLeftMainMenu");
                OnPlayerLeftMainMenu?.Invoke();
                TriggerServerEvent("OnPlayerLeftMainMenu");
            }
        }
        private void CheckPlayerWearingHelmet()
        {
            bool state = API.IsPedWearingHelmet(PedHandle);

            if (state && !state_wearingHelmet)
            {
                state_wearingHelmet = state;
                if (debug) Utils.Log("OnPlayerStartedWearingHelmet");
                OnPlayerStartedWearingHelmet?.Invoke();
                TriggerServerEvent("OnPlayerStartedWearingHelmet");
            }
            else if (!state && state_wearingHelmet)
            {
                state_wearingHelmet = state;
                if (debug) Utils.Log("OnPlayerStoppedWearingHelmet");
                OnPlayerStoppedWearingHelmet?.Invoke();
                TriggerServerEvent("OnPlayerStoppedWearingHelmet");
            }
        }
        private void CheckPlayerJumpingOutOfVehicle()
        {
            bool state = API.IsPedJumpingOutOfVehicle(PedHandle);

            if (state && !state_jumpingOutOfVehicle)
            {
                state_jumpingOutOfVehicle = state;
                if (debug) Utils.Log("OnPlayerStartedJumpingOutOfVehicle");
                OnPlayerStartedJumpingOutOfVehicle?.Invoke(VehicleHandle, state_vehicleSeat);
                TriggerServerEvent("OnPlayerStartedJumpingOutOfVehicle", VehicleNetworkId, state_vehicleSeat);
            }
            else if (!state && state_jumpingOutOfVehicle)
            {
                state_jumpingOutOfVehicle = state;
                if (debug) Utils.Log("OnPlayerStoppedJumpingOutOfVehicle");
                OnPlayerStoppedJumpingOutOfVehicle?.Invoke(VehicleHandle, state_vehicleSeat);
                TriggerServerEvent("OnPlayerStoppedJumpingOutOfVehicle", VehicleNetworkId, state_vehicleSeat);
            }
        }
        private void CheckPlayerVaulting()
        {
            bool state = API.IsPedVaulting(PedHandle);

            if (state && !state_vaulting)
            {
                state_vaulting = state;
                if (debug) Utils.Log("OnPlayerStartedVaulting");
                OnPlayerStartedVaulting?.Invoke();
                TriggerServerEvent("OnPlayerStartedVaulting");
            }
            else if (!state && state_vaulting)
            {
                state_vaulting = state;
                if (debug) Utils.Log("OnPlayerStoppedVaulting");
                OnPlayerStoppedVaulting?.Invoke();
                TriggerServerEvent("OnPlayerStoppedVaulting");
            }
        }
        private void CheckPlayerStealthKilling()
        {
            bool state = API.IsPedPerformingStealthKill(PedHandle);

            if (state && !state_stealthKilling)
            {
                state_stealthKilling = state;
                if (debug) Utils.Log("OnPlayerStartedStealthKill");
                OnPlayerStartedStealthKill?.Invoke(currentWeapon);
                TriggerServerEvent("OnPlayerStartedStealthKill", currentWeapon);
            }
            else if (!state && state_stealthKilling)
            {
                state_stealthKilling = state;
                if (debug) Utils.Log("OnPlayerStoppedStealthKill");
                OnPlayerStoppedStealthKill?.Invoke(currentWeapon);
                TriggerServerEvent("OnPlayerStoppedStealthKill", currentWeapon);
            }
        }
        private void CheckPlayerSwimmingUnderwater()
        {
            bool state = API.IsPedSwimmingUnderWater(PedHandle);

            if (state && !state_swimmingUnderwater)
            {
                state_swimmingUnderwater = state;
                if (debug) Utils.Log("OnPlayerStartedSwimmingUnderwater");
                OnPlayerStartedSwimmingUnderwater?.Invoke();
                TriggerServerEvent("OnPlayerStartedSwimmingUnderwater");
            }
            else if (!state && state_swimmingUnderwater)
            {
                state_swimmingUnderwater = state;
                if (debug) Utils.Log("OnPlayerStoppedSwimmingUnderwater");
                OnPlayerStoppedSwimmingUnderwater?.Invoke();
                TriggerServerEvent("OnPlayerStoppedSwimmingUnderwater");
            }
        }
        private void CheckPlayerSwimming()
        {
            bool state = API.IsPedSwimming(PedHandle);

            if (state && !state_swimming)
            {
                state_swimming = state;
                if (debug) Utils.Log("OnPlayerStartedSwimming");
                OnPlayerStartedSwimming?.Invoke();
                TriggerServerEvent("OnPlayerStartedSwimming");
            }
            else if (!state && state_swimming)
            {
                state_swimming = state;
                if (debug) Utils.Log("OnPlayerStoppedSwimming");
                OnPlayerStoppedSwimming?.Invoke();
                TriggerServerEvent("OnPlayerStoppedSwimming");
            }
        }
        public int GetCurrentWeaponAmmo()
        {
            return API.GetAmmoInPedWeapon(PedHandle, currentWeapon);
        }
        private void CheckPlayerShooting()
        {
            bool state_btn_lmb = API.IsControlPressed(0, 24);

            if (state_btn_lmb && !state_shooting && !state_reloading)
            {
                state_shooting = state_btn_lmb;
                if (debug) Utils.Log("OnPlayerStartedShooting");
                OnPlayerStartedShooting?.Invoke(currentWeapon, GetCurrentWeaponAmmo());
                TriggerServerEvent("OnPlayerStartedShooting", currentWeapon);
            }
            else if (state_shooting && (!state_btn_lmb || state_reloading))
            {
                state_shooting = false;
                if (debug) Utils.Log("OnPlayerStoppedShooting");
                OnPlayerStoppedShooting?.Invoke(currentWeapon, GetCurrentWeaponAmmo());
                TriggerServerEvent("OnPlayerStoppedShooting", currentWeapon);
            }
        }
        private void CheckPlayerReloading()
        {
            bool state = API.IsPedReloading(PedHandle);

            if (state && !state_reloading)
            {
                state_reloading = state;
                if (debug) Utils.Log("OnPlayerStartedReloading");
                OnPlayerStartedReloading?.Invoke(currentWeapon);
                TriggerServerEvent("OnPlayerStartedReloading", currentWeapon);
            }
            else if (!state && state_reloading)
            {
                state_reloading = state;
                if (debug) Utils.Log("OnPlayerStoppedReloading");
                OnPlayerStoppedReloading?.Invoke(currentWeapon);
                TriggerServerEvent("OnPlayerStoppedReloading", currentWeapon);
            }
        }
        private void CheckPlayerJacking()
        {
            bool state = API.IsPedJacking(PedHandle);

            if (state && !state_jacking)
            {
                state_jacking = state;
                if (debug) Utils.Log("OnPlayerStartedJacking");
                OnPlayerStartedJacking?.Invoke();
                TriggerServerEvent("OnPlayerStartedJacking");
            }
            else if (!state && state_jacking)
            {
                state_jacking = state;
                if (debug) Utils.Log("OnPlayerStoppedJacking");
                OnPlayerStoppedJacking?.Invoke();
                TriggerServerEvent("OnPlayerStoppedJacking");
            }
        }
        private void CheckPlayerParachuteFreefall()
        {
            bool state = API.IsPedInParachuteFreeFall(PedHandle);

            if (state && !state_parachuteFreefall)
            {
                state_parachuteFreefall = state;
                if (debug) Utils.Log("OnPlayerEnteredParachuteFreefall");
                OnPlayerEnteredParachuteFreefall?.Invoke();
                TriggerServerEvent("OnPlayerEnteredParachuteFreefall");
            }
            else if (!state && state_parachuteFreefall)
            {
                state_parachuteFreefall = state;
                if (debug) Utils.Log("OnPlayerLeftParachuteFreefall");
                OnPlayerLeftParachuteFreefall?.Invoke();
                TriggerServerEvent("OnPlayerLeftParachuteFreefall");
            }
        }
        private void CheckPlayerCover()
        {
            bool state = API.IsPedInCover(PedHandle, false);
            if (!state) state = API.IsPedInCover(PedHandle, true);

            if (state && !state_inCover)
            {
                state_inCover = state;
                if (debug) Utils.Log("OnPlayerEnteredCover");
                OnPlayerEnteredCover?.Invoke();
                TriggerServerEvent("OnPlayerEnteredCover");
            }
            else if (!state && state_inCover)
            {
                state_inCover = state;
                if (debug) Utils.Log("OnPlayerLeftCover");
                OnPlayerLeftCover?.Invoke();
                TriggerServerEvent("OnPlayerLeftCover");
            }
        }
        private void CheckPlayerMeleeCombat()
        {
            bool state = API.IsPedInMeleeCombat(PedHandle);

            if (state && !state_inCombat)
            {
                state_inCombat = state;
                if (debug) Utils.Log("OnPlayerEnteredMeleeCombat");
                OnPlayerEnteredMeleeCombat?.Invoke();
                TriggerServerEvent("OnPlayerEnteredMeleeCombat", currentWeapon);
            }
            else if (!state && state_inCombat)
            {
                state_inCombat = state;
                if (debug) Utils.Log("OnPlayerLeftMeleeCombat");
                OnPlayerLeftMeleeCombat?.Invoke();
                TriggerServerEvent("OnPlayerLeftMeleeCombat", currentWeapon);
            }
        }
        private void CheckPlayerOnVehicle()
        {
            bool state = API.IsPedOnVehicle(PedHandle);

            if (state && !state_onVehicle)
            {
                state_onVehicle = state;
                if (debug) Utils.Log("OnPlayerStartedOnVehicle");
                OnPlayerStartedOnVehicle?.Invoke();
                TriggerServerEvent("OnPlayerStartedOnVehicle");
            }
            else if (!state && state_onVehicle)
            {
                state_onVehicle = state;
                if (debug) Utils.Log("OnPlayerStoppedOnVehicle");
                OnPlayerStoppedOnVehicle?.Invoke();
                TriggerServerEvent("OnPlayerStoppedOnVehicle");
            }
        }
        private void CheckPlayerOnBike()
        {
            bool state = API.IsPedOnAnyBike(PedHandle);

            if (state && !state_onBike)
            {
                state_onBike = state;
                if (debug) Utils.Log("OnPlayerStartedOnBike");
                OnPlayerStartedOnBike?.Invoke(VehicleHandle, state_vehicleSeat);
                TriggerServerEvent("OnPlayerStartedOnBike", VehicleNetworkId, state_vehicleSeat);
            }
            else if (!state && state_onBike)
            {
                state_onBike = state;
                if (debug) Utils.Log("OnPlayerStoppedOnBike");
                OnPlayerStoppedOnBike?.Invoke(VehicleHandle, state_vehicleSeat);
                TriggerServerEvent("OnPlayerStoppedOnBike", VehicleNetworkId, state_vehicleSeat);
            }
        }
        private void CheckPlayerOnFoot()
        {
            bool state = API.IsPedOnFoot(PedHandle);

            if (state && !state_onFoot)
            {
                state_onFoot = state;
                if (debug) Utils.Log("OnPlayerStartedOnFoot");
                OnPlayerStartedOnFoot?.Invoke();
                TriggerServerEvent("OnPlayerStartedOnFoot");
            }
            else if (!state && state_onFoot)
            {
                state_onFoot = state;
                if (debug) Utils.Log("ResetPlayerRelatedStates");
                OnPlayerStoppedOnFoot?.Invoke();
                TriggerServerEvent("OnPlayerStoppedOnFoot");
                ResetPlayerRelatedStates();
            }
        }
        private void CheckPlayerInFlyingVehicle()
        {
            bool state = API.IsPedInFlyingVehicle(PedHandle);

            if (state && !state_inFlyingVehicle)
            {
                state_inFlyingVehicle = state;
                if (debug) Utils.Log("OnPlayerEnteredFlyingVehicle");
                OnPlayerEnteredFlyingVehicle?.Invoke(VehicleHandle, state_vehicleSeat);
                TriggerServerEvent("OnPlayerEnteredFlyingVehicle", VehicleNetworkId, state_vehicleSeat);
            }
            else if (!state && state_inFlyingVehicle)
            {
                state_inFlyingVehicle = state;
                if (debug) Utils.Log("OnPlayerLeftFlyingVehicle");
                OnPlayerLeftFlyingVehicle?.Invoke(VehicleHandle, state_vehicleSeat);
                TriggerServerEvent("OnPlayerLeftFlyingVehicle", VehicleNetworkId, state_vehicleSeat);
            }
        }
        private void CheckPlayerInAnyHeli()
        {
            bool state = API.IsPedInAnyHeli(PedHandle);

            if (state && !state_inHeli)
            {
                state_inHeli = state;
                if (debug) Utils.Log("OnPlayerEnteredHeli");
                OnPlayerEnteredHeli?.Invoke(VehicleHandle, state_vehicleSeat);
                TriggerServerEvent("OnPlayerEnteredHeli", VehicleNetworkId, state_vehicleSeat);
            }
            else if (!state && state_inHeli)
            {
                state_inHeli = state;
                if (debug) Utils.Log("OnPlayerLeftHeli");
                OnPlayerLeftHeli?.Invoke(VehicleHandle, state_vehicleSeat);
                TriggerServerEvent("OnPlayerLeftHeli", VehicleNetworkId, state_vehicleSeat);
            }
        }
        private void CheckPlayerInAnyPlane()
        {
            bool state = API.IsPedInAnyPlane(PedHandle);

            if (state && !state_inPlane)
            {
                state_inPlane = state;
                if (debug) Utils.Log("OnPlayerEnteredPlane");
                OnPlayerEnteredPlane?.Invoke(VehicleHandle, state_vehicleSeat);
                TriggerServerEvent("OnPlayerEnteredPlane", VehicleNetworkId, state_vehicleSeat);
            }
            else if (!state && state_inPlane)
            {
                state_inPlane = state;
                if (debug) Utils.Log("OnPlayerLeftPlane");
                OnPlayerLeftPlane?.Invoke(VehicleHandle, state_vehicleSeat);
                TriggerServerEvent("OnPlayerLeftPlane", VehicleNetworkId, state_vehicleSeat);
            }
        }
        private void CheckPlayerInAnyPoliceVehicle()
        {
            bool state = API.IsPedInAnyPoliceVehicle(PedHandle);

            if (state && !state_inPoliceVehicle)
            {
                state_inPoliceVehicle = state;
                if (debug) Utils.Log("OnPlayerEnteredPoliceVehicle");
                OnPlayerEnteredPoliceVehicle?.Invoke(VehicleHandle, state_vehicleSeat);
                TriggerServerEvent("OnPlayerEnteredPoliceVehicle", VehicleNetworkId, state_vehicleSeat);
            }
            else if (!state && state_inPoliceVehicle)
            {
                state_inPoliceVehicle = state;
                if (debug) Utils.Log("OnPlayerLeftPoliceVehicle");
                OnPlayerLeftPoliceVehicle?.Invoke(VehicleHandle, state_vehicleSeat);
                TriggerServerEvent("OnPlayerLeftPoliceVehicle", VehicleNetworkId, state_vehicleSeat);
            }
        }
        private void CheckPlayerInAnySub()
        {
            bool state = API.IsPedInAnySub(PedHandle);

            if (state && !state_inSub)
            {
                state_inSub = state;
                if (debug) Utils.Log("OnPlayerEnteredSub");
                OnPlayerEnteredSub?.Invoke(VehicleHandle);
                TriggerServerEvent("OnPlayerEnteredSub", VehicleNetworkId, state_vehicleSeat);
            }
            else if (!state && state_inSub)
            {
                state_inSub = state;
                if (debug) Utils.Log("OnPlayerLeftSub");
                OnPlayerLeftSub?.Invoke(VehicleHandle);
                TriggerServerEvent("OnPlayerLeftSub", VehicleNetworkId, state_vehicleSeat);
            }
        }
        private void CheckPlayerInAnyTaxi()
        {
            bool state = API.IsPedInAnyTaxi(PedHandle);

            if (state && !state_inTaxi)
            {
                state_inTaxi = state;
                if (debug) Utils.Log("OnPlayerEnteredTaxi");
                OnPlayerEnteredTaxi?.Invoke(VehicleHandle, state_vehicleSeat);
                TriggerServerEvent("OnPlayerEnteredTaxi", VehicleNetworkId, state_vehicleSeat);
            }
            else if (!state && state_inTaxi)
            {
                state_inTaxi = state;
                if (debug) Utils.Log("OnPlayerLeftTaxi");
                OnPlayerLeftTaxi?.Invoke(VehicleHandle, state_vehicleSeat);
                TriggerServerEvent("OnPlayerLeftTaxi", VehicleNetworkId, state_vehicleSeat);
            }
        }
        private void CheckPlayerInAnyTrain()
        {
            bool state = API.IsPedInAnyTrain(PedHandle);

            if (state && !state_inTrain)
            {
                state_inTrain = state;
                if (debug) Utils.Log("OnPlayerEnteredTrain");
                OnPlayerEnteredTrain?.Invoke(VehicleHandle, state_vehicleSeat);
                TriggerServerEvent("OnPlayerEnteredTrain", VehicleNetworkId, state_vehicleSeat);
            }
            else if (!state && state_inTrain)
            {
                state_inTrain = state;
                if (debug) Utils.Log("OnPlayerLeftTrain");
                OnPlayerLeftTrain?.Invoke(VehicleHandle, state_vehicleSeat);
                TriggerServerEvent("OnPlayerLeftTrain", VehicleNetworkId, state_vehicleSeat);
            }
        }
        private void CheckPlayerInAnyBoat()
        {
            bool state = API.IsPedInAnyBoat(PedHandle);

            if (state && !state_inBoat)
            {
                state_inBoat = state;
                if (debug) Utils.Log("OnPlayerEnteredBoat");
                OnPlayerEnteredBoat?.Invoke(VehicleHandle, state_vehicleSeat);
                TriggerServerEvent("OnPlayerEnteredBoat", VehicleNetworkId, state_vehicleSeat);
            }
            else if (!state && state_inBoat)
            {
                state_inBoat = state;
                if (debug) Utils.Log("OnPlayerLeftBoat");
                OnPlayerLeftBoat?.Invoke(VehicleHandle, state_vehicleSeat);
                TriggerServerEvent("OnPlayerLeftBoat", VehicleNetworkId, state_vehicleSeat);
            }
        }
        private void CheckPlayerFalling()
        {
            bool state = API.IsPedFalling(PedHandle);

            if (state && !state_falling)
            {
                state_falling = state;
                if (debug) Utils.Log("OnPlayerStartedFalling");
                OnPlayerStartedFalling?.Invoke();
                TriggerServerEvent("OnPlayerStartedFalling");
            }
            else if (!state && state_falling)
            {
                state_falling = state;
                if (debug) Utils.Log("OnPlayerStoppedFalling");
                OnPlayerStoppedFalling?.Invoke();
                TriggerServerEvent("OnPlayerStoppedFalling");
            }
        }

        private void CheckPlayerDriveBy()
        {
            bool state = API.IsPedDoingDriveby(PedHandle);

            if (state && !state_driveBying)
            {
                state_driveBying = state;
                if (debug) Utils.Log("OnPlayerStartedDriveBy");
                OnPlayerStartedDriveBy?.Invoke(VehicleHandle, currentWeapon);
                TriggerServerEvent("OnPlayerStartedDriveBy", currentWeapon);
            }
            else if (!state && state_driveBying)
            {
                state_driveBying = state;
                if (debug) Utils.Log("OnPlayerStoppedDriveBy");
                OnPlayerStoppedDriveBy?.Invoke(VehicleHandle, currentWeapon);
                TriggerServerEvent("OnPlayerStoppedDriveBy", currentWeapon);
            }
        }

        private void CheckPlayerDiving()
        {
            bool state = API.IsPedDiving(PedHandle);

            if (state && !state_diving)
            {
                state_diving = state;
                if (debug) Utils.Log("OnPlayerStartedDiving");
                OnPlayerStartedDiving?.Invoke();
                TriggerServerEvent("OnPlayerStartedDiving");
            }
            else if (!state && state_diving)
            {
                state_diving = state;
                if (debug) Utils.Log("OnPlayerStoppedDiving");
                OnPlayerStoppedDiving?.Invoke();
                TriggerServerEvent("OnPlayerStoppedDiving");
            }
        }

        private void CheckPlayerDead()
        {
            bool state = API.IsPedDeadOrDying(PedHandle, true);

            if (state && !state_dead)
            {
                state_dead = state;
                // TODO GetPedCauseOfDeath Returns the hash of the weapon/model/object that killed the ped.
                if (debug) Utils.Log("OnPlayerDied");
                UpdateCurrentPos();
                OnPlayerDied?.Invoke(currentPos.X, currentPos.Y, currentPos.Z);
                TriggerServerEvent("OnPlayerDied", currentPos.X, currentPos.Y, currentPos.Z);
            }
            else if (!state && state_dead)
            {
                state_dead = state;
                if (debug) Utils.Log("OnPlayerRevived");
                UpdateCurrentPos();
                OnPlayerRevived?.Invoke(currentPos.X, currentPos.Y, currentPos.Z);
                TriggerServerEvent("OnPlayerRevived", currentPos.X, currentPos.Y, currentPos.Z);
            }
        }

        private void UpdateCurrentPos()
        {
            currentPos = GetCurrentPos();
        }
        private void UpdateCurrentWeapon()
        {
            uint newWeapon = GetCurrentWeaponHashUint();

            if(newWeapon != currentWeapon)
            {
                if (debug) Utils.Log(String.Format("OnPlayerWeaponChange {0} newWeapon {1}", currentWeapon, newWeapon));
                OnPlayerWeaponChange?.Invoke(currentWeapon, newWeapon);
                TriggerServerEvent("OnPlayerWeaponChange", currentWeapon, newWeapon);
                currentWeapon = newWeapon;
            }
        }

        public static Vector3 GetCurrentPos()
        {
            return API.GetEntityCoords(PedHandle, true);
        }

        public static uint GetCurrentWeaponHashUint()
        {
            return (uint)API.HudWeaponWheelGetSelectedHash();
        }

        public int GetCurrentWeaponHash()
        {
            return API.HudWeaponWheelGetSelectedHash();
        }

        private void CheckPlayerClimbing()
        {
            bool state = API.IsPedClimbing(PedHandle);

            if (state && !state_climbing)
            {
                state_climbing = state;
                if (debug) Utils.Log("OnPlayerStartedClimbing");
                OnPlayerStartedClimbing?.Invoke();
                TriggerServerEvent("OnPlayerStartedClimbing");
            }
            else if (!state && state_climbing)
            {
                state_climbing = state;
                if (debug) Utils.Log("OnPlayerStoppedClimbing");
                OnPlayerStoppedClimbing?.Invoke();
                TriggerServerEvent("OnPlayerStoppedClimbing");
            }
        }

        private void CheckPlayerStunned()
        {
            bool state = API.IsPedBeingStunned(PedHandle, 0);

            if (state && !state_stunned)
            {
                state_stunned = state;
                if (debug) Utils.Log("OnPlayerStartedGettingStunned");
                OnPlayerStartedGettingStunned?.Invoke();
                TriggerServerEvent("OnPlayerStartedGettingStunned");
            }
            else if (!state && state_stunned)
            {
                state_stunned = state;
                if (debug) Utils.Log("OnPlayerStoppedGettingStunned");
                OnPlayerStoppedGettingStunned?.Invoke();
                TriggerServerEvent("OnPlayerStoppedGettingStunned");
            }
        }

        private void CheckPlayerBeingStealthKilled()
        {
            bool state = API.IsPedBeingStealthKilled(PedHandle);

            if (state && !state_beingStealthKilled)
            {
                state_beingStealthKilled = state;
                if (debug) Utils.Log("OnPlayerStartedGettingJacked");
                OnPlayerStartedGettingJacked?.Invoke();
                TriggerServerEvent("OnPlayerStartedGettingJacked");
            }
            else if (!state && state_beingStealthKilled)
            {
                state_beingStealthKilled = state;
                if (debug) Utils.Log("OnPlayerStoppedGettingJacked");
                OnPlayerStoppedGettingJacked?.Invoke();
                TriggerServerEvent("OnPlayerStoppedGettingJacked");
            }
        }

        private void CheckPlayerGettingJacked()
        {
            bool state = API.IsPedBeingJacked(PedHandle);

            if (state && !state_jacked)
            {
                state_jacked = state;
                if (debug) Utils.Log("OnPlayerStartedGettingJacked");
                OnPlayerStartedGettingJacked?.Invoke();
                TriggerServerEvent("OnPlayerStartedGettingJacked");
            }
            else if (!state && state_jacked)
            {
                state_jacked = state;
                if (debug) Utils.Log("OnPlayerStoppedGettingJacked");
                OnPlayerStoppedGettingJacked?.Invoke();
                TriggerServerEvent("OnPlayerStoppedGettingJacked");
            }
        }

        private void CheckPlayerAimFromCover()
        {
            bool state = API.IsPedAimingFromCover(PedHandle);

            if (state && !state_aimingFromCover)
            {
                state_aimingFromCover = state;
                if (debug) Utils.Log("OnPlayerStartedToAimFromCover");
                OnPlayerStartedToAimFromCover?.Invoke(currentWeapon);
                TriggerServerEvent("OnPlayerStartedToAimFromCover", currentWeapon);
            }
            else if (!state && state_aimingFromCover)
            {
                state_aimingFromCover = state;
                if (debug) Utils.Log("OnPlayerStoppedToAimFromCover");
                OnPlayerStoppedToAimFromCover?.Invoke(currentWeapon);
                TriggerServerEvent("OnPlayerStoppedToAimFromCover", currentWeapon);
            }
        }

        private void CheckPlayerWalking()
        {
            bool state = API.IsPedWalking(PedHandle);

            if (state && !state_walking)
            {
                state_walking = state;
                if (debug) Utils.Log("OnPlayerStartedWalking");
                OnPlayerStartedWalking?.Invoke();
                TriggerServerEvent("OnPlayerStartedWalking");
            }
            else if (!state && state_walking)
            {
                state_walking = state;
                if (debug) Utils.Log("OnPlayerStoppedWalking");
                OnPlayerStoppedWalking?.Invoke();
                TriggerServerEvent("OnPlayerStoppedWalking");
            }
        }

        private void CheckPlayerRunning()
        {
            bool state = API.IsPedRunning(PedHandle);

            if (state && !state_running)
            {
                state_running = state;
                if (debug) Utils.Log("OnPlayerStartedRunning");
                OnPlayerStartedRunning?.Invoke();
                TriggerServerEvent("OnPlayerStartedRunning");
            }
            else if (!state && state_running)
            {
                state_running = state;
                if (debug) Utils.Log("OnPlayerStoppedRunning");
                OnPlayerStoppedRunning?.Invoke();
                TriggerServerEvent("OnPlayerStoppedRunning");
            }
        }

        private void CheckPlayerGettingUp()
        {
            bool state = API.IsPedGettingUp(PedHandle);

            if (state && !state_gettingUp)
            {
                state_gettingUp = state;
                if (debug) Utils.Log("OnPlayerStartedToGetUp");
                OnPlayerStartedToGetUp?.Invoke();
                TriggerServerEvent("OnPlayerStartedToGetUp");
            }
            else if (!state && state_gettingUp)
            {
                state_gettingUp = state;
                if (debug) Utils.Log("OnPlayerStoppedToGetUp");
                OnPlayerStoppedToGetUp?.Invoke();
                TriggerServerEvent("OnPlayerStoppedToGetUp");
            }
        }

        private void CheckPlayerCuffed()
        {
            bool state = API.IsPedCuffed(PedHandle);

            if (state && !state_cuffed)
            {
                state_cuffed = state;
                if (debug) Utils.Log("OnPlayerCuffed");
                OnPlayerCuffed?.Invoke();
                TriggerServerEvent("OnPlayerCuffed");
            }
            else if (!state && state_cuffed)
            {
                state_cuffed = state;
                if (debug) Utils.Log("OnPlayerUnCuffed");
                OnPlayerUnCuffed?.Invoke();
                TriggerServerEvent("OnPlayerUnCuffed");
            }
        }

        private void CheckPlayerJumping()
        {
            bool state = API.IsPedJumping(PedHandle);

            if (state && !state_jumping)
            {
                state_jumping = state;
                if (debug) Utils.Log("OnPlayerStartedJumping");
                OnPlayerStartedJumping?.Invoke();
                TriggerServerEvent("OnPlayerStartedJumping");
            }
            else if (!state && state_jumping)
            {
                state_jumping = state;
                if (debug) Utils.Log("OnPlayerStoppedJumping");
                OnPlayerStoppedJumping?.Invoke();
                TriggerServerEvent("OnPlayerStoppedJumping");
            }
        }

        private void CheckPlayerSprinting()
        {
            bool sprinting = API.IsPedSprinting(PedHandle);

            if (sprinting && !state_sprinting)
            {
                state_sprinting = sprinting;
                if (debug) Utils.Log("OnPlayerStartedSprinting");
                OnPlayerStartedSprinting?.Invoke();
                TriggerServerEvent("OnPlayerStartedSprinting");
            }
            else if (!sprinting && state_sprinting)
            {
                state_sprinting = sprinting;
                if (debug) Utils.Log("OnPlayerStoppedSprinting");
                OnPlayerStoppedSprinting?.Invoke();
                TriggerServerEvent("OnPlayerStoppedSprinting");
            }
        }

        private void CheckPlayerSpawned()
        {
            int pHandle = API.GetPlayerPed(PlayerHandle);

            if (pHandle != PedHandle)
            {
                int pNetId = API.NetworkGetNetworkIdFromEntity(pHandle);
                PedHandle = pHandle;
                PedNetworkId = pNetId;
                if (debug) Utils.Log("OnPlayerSpawned");
                OnPlayerSpawned?.Invoke(PedHandle, PedNetworkId, currentPos.X, currentPos.Y, currentPos.Z);
                TriggerServerEvent("OnPlayerSpawned");

                pedHealth = API.GetEntityHealth(PedHandle);
                pedArmour = API.GetPedArmour(PedHandle);
            }
        }

        private void CheckVehicleEvents()
        {
            bool isInVehicle = API.IsPedInAnyVehicle(PedHandle, false);

            if (isInVehicle && !state_inVehicle)
            {
                int vHandle = API.GetVehiclePedIsIn(PedHandle, false);
                VehicleHandle = vHandle;
                VehicleNetworkId = API.NetworkGetNetworkIdFromEntity(vHandle);
                state_inVehicle = true;
                state_tryingToEnterVehicle = false;
                CheckSeat();

                if (debug) Utils.Log("OnPlayerEnteredVehicle " + vHandle);

                state_tryingToEnterVehicle = false;
                OnPlayerEnteredVehicle?.Invoke(VehicleHandle, state_vehicleSeat);
                TriggerServerEvent("OnPlayerEnteredVehicle", VehicleNetworkId, state_vehicleSeat);
                UpdateVehicleSpeed();
                vehicleHealth = API.GetEntityHealth(VehicleHandle);
                vehicleBodyHealth = API.GetVehicleBodyHealth(VehicleHandle);
                vehicleEngineHealth = API.GetVehicleEngineHealth(VehicleHandle);
                vehiclePetrolTankHealth = API.GetVehiclePetrolTankHealth(VehicleHandle);
            }
            else if (!isInVehicle && state_inVehicle)
            {
                state_inVehicle = false;
                if (debug) Utils.Log("OnPlayerLeaveVehicle");
                OnPlayerLeaveVehicle?.Invoke(VehicleHandle, state_vehicleSeat);
                TriggerServerEvent("OnPlayerLeaveVehicle", VehicleNetworkId, state_vehicleSeat);
                state_vehicleSeat = MEF_Vehicle.SEAT_NONE;
                ResetVehicleRelatedStates();
            }
        }

        private void UpdateVehicleSpeed()
        {
            vehicleSpeed = MEF_Vehicle.GetSpeedInKmh(VehicleHandle);
        }

        private void CheckVehicleEnteringEvents()
        {
            bool isTryingToEnter = API.IsPedInAnyVehicle(PedHandle, true);

            if (isTryingToEnter && !state_tryingToEnterVehicle && !state_inVehicle)
            {
                state_tryingToEnterVehicle = isTryingToEnter;
                int veh = API.GetVehiclePedIsEntering(PedHandle);
                // int veh2 = API.GetVehiclePedIsTryingToEnter(pedHandle); // NOTE(Caupo 06.02.2021): This is giving value 0 when almost in vehicle, so it is not so reliable.
                int seatTryingtoEnter = API.GetSeatPedIsTryingToEnter(PedHandle);

                if (seatTryingtoEnter != state_vehicleSeat)
                {
                    state_vehicleSeat = seatTryingtoEnter;
                    if (debug) Utils.Log("OnPlayerSeatChange");
                    OnPlayerSeatChange?.Invoke(veh, state_vehicleSeat);
                    TriggerServerEvent("OnPlayerSeatChange", VehicleNetworkId, state_vehicleSeat);
                }

                if (veh == 0)
                {
                    veh = API.GetVehiclePedIsIn(PedHandle, false);
                    if(debug) Utils.Log("BASE OnPlayerSpawnIntoVehicle]VEH: [" + veh + "] SEAT: [" + seatTryingtoEnter + "] vHandle: " + veh);
                    OnPlayerSpawnIntoVehicle?.Invoke(veh);
                    TriggerServerEvent("OnPlayerSpawnIntoVehicle", VehicleNetworkId);
                }
                else
                {
                    if (debug) Utils.Log("BASE OnTryingToEnterVehicle]VEH: [" + veh + "] SEAT: [" + seatTryingtoEnter + "]");
                    OnPlayerTryingToEnterVehicle?.Invoke(veh, state_vehicleSeat);
                    TriggerServerEvent("OnPlayerTryingToEnterVehicle", VehicleNetworkId, state_vehicleSeat);
                }
            }
        }

        private void CheckSeat()
        {
            int seats = MEF_Vehicle.GetMaxNumberOfSeats(VehicleHandle);
            //Utils.Log("CheckSeat seats " + seats);

            for (int i = 0; i < MEF_Vehicle.seats.Length; i++)
            {
                int seatIdx = MEF_Vehicle.seats[i];
                int ped = API.GetPedInVehicleSeat(VehicleHandle, seatIdx);
                //Utils.Log("SEAT CHECKED " + i);

                if (ped == PedHandle)
                {
                    if (seatIdx != state_vehicleSeat)
                    {
                        state_vehicleSeat = seatIdx;
                        if (debug) Utils.Log("OnPlayerSeatChange " + state_vehicleSeat);
                        OnPlayerSeatChange?.Invoke(VehicleHandle, state_vehicleSeat);
                        TriggerServerEvent("OnPlayerSeatChange", VehicleNetworkId, state_vehicleSeat);
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

                    int ped = API.GetPedInVehicleSeat(VehicleHandle, seat);

                    if (ped == PedHandle)
                    {
                        if (seat != state_vehicleSeat)
                        {
                            state_vehicleSeat = seat;
                            if (debug) Utils.Log("OnPlayerSeatChange " + state_vehicleSeat);
                            OnPlayerSeatChange?.Invoke(VehicleHandle, state_vehicleSeat);
                            TriggerServerEvent("OnPlayerSeatChange", VehicleNetworkId, state_vehicleSeat);
                        }
                        //Utils.Log("SEAT BREAK-2: " + seat);
                        break;
                    }
                }
            }
        }
    }
}
