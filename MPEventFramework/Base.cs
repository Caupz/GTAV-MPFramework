using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using API = CitizenFX.Core.Native.API;

namespace MPEventFramework
{
    public class Base : BaseScript
    {
        // NOTE (07.02.2021): I know I could have used reflection to get code lesser rows longer but it uses more CPU time. But the goal here is to save CPU time as much as possible.

        /* TODO?
        IsControlPressed
        IsControlJustPressed
        IsControlReleased
        */

        const bool debug = true;

        // TIME SYSTEM
        public bool enableRealtimeGametime = true;

        // WIND SYSTEM
        public bool enableRandomWinds = true;
        public int maxWindSpeed = 70;
        public int minWindSpeed = 0;
        public int currentWind = 0;
        public float currentWindDirection = 0;

        // WEATHER SYSTEM
        public bool enableRandomWeathers = true;
        public bool enableSnowyWeathers = false;
        public bool enableSnowOnly = false;
        public int weatherUpdateIntervalInMinutes = 10;
        int currentWeatherUpdateInMinutes = 0;
        int currentWeather = 0;
        int previouseWeather = 0;
        float weatherTransition = 0;
        List<string> previouslySelectedWeathers = MEF_Weathers.weathersWithSnow;
        List<string> selectedWeathers = MEF_Weathers.weathersWithSnow;
        const float weatherTransitionPerSecond = 0.0167f;

        // PLAYER IDS
        public int playerHandle { get; protected set; }
        public int playerNetworkId { get; protected set; }
        public int pedHandle { get; protected set; }
        public int pedNetworkId { get; protected set; }
        // PLAYER IDS END

        // STATES
        int pedHealth = MEF_Player.HEALTH_NONE;
        int pedArmour = MEF_Player.ARMOUR_NONE;
        int vehicleHealth = MEF_Vehicle.HEALTH_NONE;
        float vehicleBodyHealth = MEF_Vehicle.HEALTH_NONE;
        float vehicleEngineHealth = MEF_Vehicle.HEALTH_NONE;
        float vehiclePetrolTankHealth = MEF_Vehicle.HEALTH_NONE;
        float vehicleSpeed = 0;

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

        int previouseSecond = 0;
        int previouseMinute = 0;
        int previouseHour = 0;
        int previouseMilliSecond = 0;

        public event SecondPassed OnSecondPassed;
        public event HundredSecondPassed OnHundredSecondPassed;
        public event MinutePassed OnMinutePassed;
        public event HourPassed OnHourPassed;
        public delegate void SecondPassed();
        public delegate void HundredSecondPassed();
        public delegate void MinutePassed();
        public delegate void HourPassed();

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
        public event PlayerCuffed OnPlayerCuffed; // TEST CMD vaja mis cuffib
        public event PlayerUnCuffed OnPlayerUnCuffed; // TEST CMD vaja mis uncuffib
        public event PlayerStartedToGetUp OnPlayerStartedToGetUp;
        public event PlayerStoppedToGetUp OnPlayerStoppedToGetUp;
        public event PlayerStartedToAimFromCover OnPlayerStartedToAimFromCover; // TEST CMD vaja teha mis relva annab
        public event PlayerStoppedToAimFromCover OnPlayerStoppedToAimFromCover; // TEST CMD vaja teha mis relva annab
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
        public event PlayerStartedDiving OnPlayerStartedDiving; // TEST ei tööta?
        public event PlayerStoppedDiving OnPlayerStoppedDiving; // TEST ei tööta?
        public event PlayerStartedDriveBy OnPlayerStartedDriveBy;
        public event PlayerStoppedDriveBy OnPlayerStoppedDriveBy;
        public event PlayerStartedDucking OnPlayerStartedDucking; // TEST EI TOIMI
        public event PlayerStoppedDucking OnPlayerStoppedDucking; // TEST EI TOIMI
        public event PlayerStartedFalling OnPlayerStartedFalling;
        public event PlayerStoppedFalling OnPlayerStoppedFalling;
        public event PlayerStartedOnFoot OnPlayerStartedOnFoot;
        public event PlayerStoppedOnFoot OnPlayerStoppedOnFoot;
        public event PlayerEnteredMeleeCombat OnPlayerEnteredMeleeCombat;
        public event PlayerLeftMeleeCombat OnPlayerLeftMeleeCombat;
        public event PlayerEnteredCover OnPlayerEnteredCover;
        public event PlayerLeftCover OnPlayerLeftCover;
        public event PlayerEnteredParachuteFreefall OnPlayerEnteredParachuteFreefall; // TEST CMD vaja teha mis relva annab
        public event PlayerLeftParachuteFreefall OnPlayerLeftParachuteFreefall; // TEST CMD vaja teha mis relva annab
        public event PlayerStartedReloading OnPlayerStartedReloading; // TEST CMD vaja teha mis relva annab
        public event PlayerStoppedReloading OnPlayerStoppedReloading; // TEST CMD vaja teha mis relva annab
        public event PlayerStartedShooting OnPlayerStartedShooting; // TEST CMD vaja teha mis relva annab
        public event PlayerStoppedShooting OnPlayerStoppedShooting; // TEST CMD vaja teha mis relva annab
        public event PlayerStartedSwimming OnPlayerStartedSwimming;
        public event PlayerStoppedSwimming OnPlayerStoppedSwimming;
        public event PlayerStartedSwimmingUnderwater OnPlayerStartedSwimmingUnderwater;
        public event PlayerStoppedSwimmingUnderwater OnPlayerStoppedSwimmingUnderwater;
        public event PlayerStartedStealthKill OnPlayerStartedStealthKill; // TEST CMD vaja teha mis relva annab
        public event PlayerStoppedStealthKill OnPlayerStoppedStealthKill; // TEST CMD vaja teha mis relva annab
        public event PlayerStartedVaulting OnPlayerStartedVaulting;
        public event PlayerStoppedVaulting OnPlayerStoppedVaulting;
        public event PlayerStartedWearingHelmet OnPlayerStartedWearingHelmet; // TEST motikas ei hakanud kiivrit kandma
        public event PlayerStoppedWearingHelmet OnPlayerStoppedWearingHelmet; // TEST motikas ei hakanud kiivrit kandma
        public event PlayerEnteredMainMenu OnPlayerEnteredMainMenu;
        public event PlayerLeftMainMenu OnPlayerLeftMainMenu;
        public event PlayerReadyToShoot OnPlayerReadyToShoot; // TEST CMD vaja teha mis relva annab
        public event PlayerNotReadyToShoot OnPlayerNotReadyToShoot; // TEST CMD vaja teha mis relva annab
        public event PlayerStartedAiming OnPlayerStartedAiming; // TEST CMD vaja teha mis relva annab
        public event PlayerStoppedAiming OnPlayerStoppedAiming; // TEST CMD vaja teha mis relva annab

        public event PlayerHealthGain OnPlayerHealthGain;
        public event PlayerHealthLoss OnPlayerHealthLoss;
        public event PlayerArmourGain OnPlayerArmourGain; // TEST CMD vaja teha mis armi annab
        public event PlayerArmourLoss OnPlayerArmourLoss; // TEST CMD vaja teha mis armi annab

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
        public event PlayerStartedMovingVehicle OnPlayerStartedMovingVehicle; // TEST EI TÖÖTA
        public event PlayerStoppedVehicle OnPlayerStoppedVehicle; // TEST EI TÖÖTA
        public event PlayerStartedBurnouting OnPlayerStartedBurnouting;
        public event PlayerStoppedBurnouting OnPlayerStoppedBurnouting;
        public event VehicleHealthGain OnVehicleHealthGain;
        public event VehicleHealthLoss OnVehicleHealthLoss;
        public event VehicleCrash OnVehicleCrash; // TEST. Peab logima mis kiirused ja healthi muutused.

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
        public delegate void VehicleHealthGain(int vehicleHealth, float vehicleBodyHealth, float vehicleEngineHealth, float vehiclePetrolTankHealth);
        public delegate void VehicleHealthLoss(int vehicleHealth, float vehicleBodyHealth, float vehicleEngineHealth, float vehiclePetrolTankHealth);
        public delegate void VehicleCrash();

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
        public delegate void PlayerHealthGain();
        public delegate void PlayerHealthLoss();
        public delegate void PlayerArmourGain();
        public delegate void PlayerArmourLoss();

        public Base()
        {
            DateTime dt = DateTime.Now;
            InitPlayerIds();
            InitSystemVariables();
            UpdateWind();
            UpdateWeather();
            UpdateTime(dt.Hour, dt.Minute, dt.Second);
        }

        public void TransitionWeather()
        {
            if (enableRandomWeathers && weatherTransition > 0.0)
            {
                weatherTransition += weatherTransitionPerSecond;

                if (weatherTransition > 1.0f)
                {
                    weatherTransition = 1.0f;
                    SetCurrentWeatherState();
                    weatherTransition = 0f;
                }
                else
                {
                    SetCurrentWeatherState();
                }
            }
        }

        public void UpdateWeather()
        {
            if(enableRandomWeathers)
            {
                currentWeatherUpdateInMinutes--;

                if (currentWeatherUpdateInMinutes <= 0)
                {
                    previouslySelectedWeathers = selectedWeathers;
                    GetNewSelectedWeathers();
                    SetCurrentWeatherState();
                    GetNewWeatherUpdates();
                }
            }
        }

        public void GetNewSelectedWeathers()
        {
            if (enableSnowOnly)
            {
                selectedWeathers = MEF_Weathers.snowWeathers;
            }
            else
            {
                if (enableSnowyWeathers)
                {
                    selectedWeathers = MEF_Weathers.weathersWithSnow;
                }
                else
                {
                    selectedWeathers = MEF_Weathers.weathersWithoutSnow;
                }
            }
        }

        public void SetCurrentWeatherState()
        {
            Utils.Log("previouslySelectedWeathers.Count: "+ previouslySelectedWeathers.Count+ " previouseWeather" + previouseWeather+ " selectedWeathers.Count:"+ selectedWeathers.Count+ " currentWeather: "+ currentWeather+ " weatherTransition: "+ weatherTransition);
            API.SetWeatherTypeTransition((uint)API.GetHashKey(previouslySelectedWeathers[previouseWeather]), (uint)API.GetHashKey(selectedWeathers[currentWeather]), weatherTransition);
        }

        public void GetNewWeatherUpdates()
        {
            weatherTransition = weatherTransitionPerSecond;
            previouseWeather = currentWeather;
            currentWeather = Utils.GetRandom(0, selectedWeathers.Count);
            currentWeatherUpdateInMinutes = weatherUpdateIntervalInMinutes;
        }

        public void UpdateWind()
        {
            if(enableRandomWinds)
            {
                GetRandomWind();
                GetRandomWindDirection();
                ApplyCurrentWind();
            }
        }

        public void GetRandomWind()
        {
            currentWind = Utils.GetRandom(minWindSpeed, maxWindSpeed);
        }

        public void GetRandomWindDirection()
        {
            currentWindDirection = Utils.GetRandom(0, 8);
        }

        public void ApplyCurrentWind()
        {
            API.SetWind(currentWind);
            API.SetWindDirection(currentWindDirection);
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

            Utils.Log("InitPlayerIds pedHandle [" + pedHandle + "] pedNetworkId [" + pedNetworkId + "]");
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

            if(Math.Abs(previouseMilliSecond - dt.Millisecond) >= 100)
            {
                previouseMilliSecond = dt.Millisecond;
                CallbackOnHundredMilliSecondPassed();
            }
        }

        // TIMING EVENTS

        public void CallbackOnMinutePassed()
        {
            UpdateWind();
            UpdateWeather();
            OnMinutePassed?.Invoke();
        }

        private void UpdateTime(int hour, int minute, int second)
        {
            if (enableRealtimeGametime)
            {
                API.SetClockTime(hour, minute, second);
            }
        }

        public void CallbackOnSecondPassed(int hour, int minute, int second)
        {
            UpdateTime(hour, minute, second);
            TransitionWeather();

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
                CheckVehicleHealth();
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
            CheckPlayerDiving();
            CheckPlayerJacking();

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

        public void UpdateVehicleHealth(int vHealth, float vBodyHealth, float vEngineHealth, float vPetrolTankHealth)
        {
            vehicleHealth = vHealth;
            vehicleBodyHealth = vBodyHealth;
            vehicleEngineHealth = vEngineHealth;
            vehiclePetrolTankHealth = vPetrolTankHealth;
        }

        public void CheckVehicleHealth()
        {
            int vHealth = API.GetEntityHealth(state_lastVehicleHandle);
            float vBodyHealth = API.GetVehicleBodyHealth(state_lastVehicleHandle);
            float vEngineHealth = API.GetVehicleEngineHealth(state_lastVehicleHandle);
            float vPetrolTankHealth = API.GetVehiclePetrolTankHealth(state_lastVehicleHandle);

            if (vHealth > vehicleHealth || vBodyHealth > vehicleBodyHealth || vEngineHealth > vehicleEngineHealth || vPetrolTankHealth > vehiclePetrolTankHealth)
            {
                if(debug) Utils.Log("OnVehicleHealthGain");
                UpdateVehicleHealth(vHealth, vBodyHealth, vEngineHealth, vPetrolTankHealth);
                OnVehicleHealthGain?.Invoke(vehicleHealth, vehicleBodyHealth, vehicleEngineHealth, vehiclePetrolTankHealth);
            }
            if (vHealth < vehicleHealth || vBodyHealth < vehicleBodyHealth || vEngineHealth < vehicleEngineHealth || vPetrolTankHealth < vehiclePetrolTankHealth)
            {
                UpdateVehicleHealth(vHealth, vBodyHealth, vEngineHealth, vPetrolTankHealth);

                float speed = MEF_Vehicle.GetSpeedInKmh(state_lastVehicleHandle);
                float speedQuanfitsent = vehicleSpeed / 2;

                if(speed < speedQuanfitsent)
                {
                    if (debug) Utils.Log("OnVehicleCrash");
                    OnVehicleCrash?.Invoke();
                    vehicleSpeed = speed;
                }

                if (debug) Utils.Log("OnVehicleHealthLoss");
                OnVehicleHealthLoss?.Invoke(vehicleHealth, vehicleBodyHealth, vehicleEngineHealth, vehiclePetrolTankHealth);
            }
        }
        public void CheckPlayerHealth()
        {
            int pHealth = API.GetEntityHealth(pedHandle);

            if (pHealth > pedHealth)
            {
                if (debug) Utils.Log("OnPlayerHealthGain");
                OnPlayerHealthGain?.Invoke();
                pedHealth = pHealth;
            }
            else if (pHealth < pedHealth)
            {
                if (debug) Utils.Log("OnPlayerHealthLoss");
                OnPlayerHealthLoss?.Invoke();
                pedHealth = pHealth;
            }
        }

        public void CheckPlayerArmour()
        {
            int pArmour = API.GetPedArmour(pedHandle);

            if (pArmour > pedArmour)
            {
                if (debug) Utils.Log("OnPlayerArmourGain");
                OnPlayerArmourGain?.Invoke();
                pedArmour = pArmour;
            }
            else if (pArmour < pedArmour)
            {
                if (debug) Utils.Log("OnPlayerArmourLoss");
                OnPlayerArmourLoss?.Invoke();
                pedArmour = pArmour;
            }
        }

        public void ResetPlayerRelatedStates()
        {
            if (state_swimmingUnderwater)
            {
                state_swimmingUnderwater = false;
                if (debug) Utils.Log("OnPlayerStoppedSwimmingUnderwater");
                OnPlayerStoppedSwimmingUnderwater();
            }
            if (state_inCombat)
            {
                state_inCombat = false;
                if (debug) Utils.Log("OnPlayerLeftMeleeCombat");
                OnPlayerLeftMeleeCombat();
            }
            if (state_swimming)
            {
                state_swimming = false;
                if (debug) Utils.Log("OnPlayerStoppedSwimming");
                OnPlayerStoppedSwimming();
            }
            if (state_jacking)
            {
                state_jacking = false;
                if (debug) Utils.Log("OnPlayerStoppedJacking");
                OnPlayerStoppedJacking();
            }
            if (state_inCover)
            {
                state_inCover = false;
                if (debug) Utils.Log("OnPlayerLeftCover");
                OnPlayerLeftCover();
            }

            if (state_inCombat)
            {
                state_inCombat = false;
                if (debug) Utils.Log("OnPlayerLeftMeleeCombat");
                OnPlayerLeftMeleeCombat();
            }

            if (state_onVehicle)
            {
                state_onVehicle = false;
                if (debug) Utils.Log("OnPlayerStoppedOnVehicle");
                OnPlayerStoppedOnVehicle?.Invoke();
            }

            if (state_climbing)
            {
                state_climbing = false;
                if (debug) Utils.Log("OnPlayerStoppedClimbing");
                OnPlayerStoppedClimbing?.Invoke();
            }

            if (state_aimingFromCover)
            {
                state_aimingFromCover = false;
                if (debug) Utils.Log("OnPlayerStoppedToAimFromCover");
                OnPlayerStoppedToAimFromCover?.Invoke();
            }

            if (state_gettingUp)
            {
                state_gettingUp = false;
                if (debug) Utils.Log("OnPlayerStoppedToGetUp");
                OnPlayerStoppedToGetUp?.Invoke();
            }

            if (state_walking)
            {
                state_walking = false;
                if (debug) Utils.Log("OnPlayerStoppedWalking");
                OnPlayerStoppedWalking?.Invoke();
            }

            if (state_running)
            {
                state_running = false;
                if (debug) Utils.Log("OnPlayerStoppedRunning");
                OnPlayerStoppedRunning?.Invoke();
            }

            if (state_sprinting)
            {
                state_sprinting = false;
                if (debug) Utils.Log("OnPlayerStoppedSprinting");
                OnPlayerStoppedSprinting?.Invoke();
            }

            if(state_jumping)
            {
                state_jumping = false;
                if (debug) Utils.Log("OnPlayerStartedJumping");
                OnPlayerStartedJumping?.Invoke();
            }

            if (state_vaulting)
            {
                state_vaulting = false;
                if (debug) Utils.Log("OnPlayerStoppedVaulting");
                OnPlayerStoppedVaulting?.Invoke();
            }

            if(state_stealthKilling)
            {
                state_stealthKilling = false;
                if (debug) Utils.Log("OnPlayerStoppedStealthKill");
                OnPlayerStoppedStealthKill?.Invoke();
            }
        }

        public void ResetVehicleRelatedStates()
        {
            state_vehicleStopped = false;

            if(state_vehicleBurnouting)
            {
                state_vehicleBurnouting = false;
                if (debug) Utils.Log("OnPlayerStoppedBurnouting");
                OnPlayerStoppedBurnouting?.Invoke();
            }

            if(state_onBike)
            {
                state_onBike = false;
                if (debug) Utils.Log("OnPlayerStoppedOnBike");
                OnPlayerStoppedOnBike?.Invoke();
            }

            if(state_inTrain)
            {
                state_inTrain = false;
                if (debug) Utils.Log("OnPlayerLeftTrain");
                OnPlayerLeftTrain?.Invoke();
            }

            if(state_inTaxi)
            {
                state_inTaxi = false;
                if (debug) Utils.Log("OnPlayerLeftTaxi");
                OnPlayerLeftTaxi?.Invoke();
            }

            if(state_inSub)
            {
                state_inSub = false;
                if (debug) Utils.Log("OnPlayerLeftSub");
                OnPlayerLeftSub?.Invoke();
            }

            if(state_inPoliceVehicle)
            {
                state_inPoliceVehicle = false;
                if (debug) Utils.Log("OnPlayerLeftPoliceVehicle");
                OnPlayerLeftPoliceVehicle?.Invoke();
            }

            if(state_inPlane)
            {
                state_inPlane = false;
                if (debug) Utils.Log("OnPlayerLeftPlane");
                OnPlayerLeftPlane?.Invoke();
            }

            if(state_inHeli)
            {
                state_inHeli = false;
                if (debug) Utils.Log("OnPlayerLeftHeli");
                OnPlayerLeftHeli?.Invoke();
            }

            if(state_inFlyingVehicle)
            {
                state_inFlyingVehicle = false;
                if (debug) Utils.Log("OnPlayerLeftFlyingVehicle");
                OnPlayerLeftFlyingVehicle?.Invoke();
            }

            if(state_driveBying)
            {
                state_driveBying = false;
                if (debug) Utils.Log("OnPlayerStoppedDriveBy");
                OnPlayerStoppedDriveBy?.Invoke();
            }

            if(state_ducking)
            {
                state_ducking = false;
                if (debug) Utils.Log("OnPlayerStoppedDucking");
                OnPlayerStoppedDucking?.Invoke();
            }

            if(state_inBoat)
            {
                state_inBoat = false;
                if(debug) Utils.Log("OnPlayerLeftBoat");
                OnPlayerLeftBoat?.Invoke();
            }
        }

        // HELPER FUNCTIONS

        public void CheckPlayerAiming()
        {
            bool state = API.GetPedConfigFlag(pedHandle, 78, true);

            if (state && !state_aiming)
            {
                state_aiming = state;
                if (debug) Utils.Log("OnPlayerStartedAiming");
                OnPlayerStartedAiming?.Invoke();
            }
            else if (!state && state_aiming)
            {
                state_aiming = state;
                if (debug) Utils.Log("OnPlayerStoppedAiming");
                OnPlayerStoppedAiming?.Invoke();
            }
        }
        public void CheckPlayerBurnouting()
        {
            bool state = API.IsVehicleInBurnout(state_lastVehicleHandle);

            if (state && !state_vehicleBurnouting)
            {
                state_vehicleBurnouting = state;
                if (debug) Utils.Log("OnPlayerStartedBurnouting");
                OnPlayerStartedBurnouting?.Invoke();
            }
            else if (!state && state_vehicleBurnouting)
            {
                state_vehicleBurnouting = state;
                if (debug) Utils.Log("OnPlayerStoppedBurnouting");
                OnPlayerStoppedBurnouting?.Invoke();
            }
        }
        public void CheckPlayerStoppingVehicle()
        {
            bool state = API.IsVehicleStopped(pedHandle);

            if (state && !state_vehicleStopped)
            {
                state_vehicleStopped = state;
                if (debug) Utils.Log("OnPlayerStoppedVehicle");
                OnPlayerStoppedVehicle?.Invoke();
            }
            else if (!state && state_vehicleStopped)
            {
                state_vehicleStopped = state;
                if (debug) Utils.Log("OnPlayerStartedMovingVehicle");
                OnPlayerStartedMovingVehicle?.Invoke();
            }
        }
        public void CheckPlayerReadyToShoot()
        {
            bool state = API.IsPedWeaponReadyToShoot(pedHandle);

            if (state && !state_readyToShoot)
            {
                state_readyToShoot = state;
                if (debug) Utils.Log("OnPlayerReadyToShoot");
                OnPlayerReadyToShoot?.Invoke();
            }
            else if (!state && state_readyToShoot)
            {
                state_readyToShoot = state;
                if (debug) Utils.Log("OnPlayerNotReadyToShoot");
                OnPlayerNotReadyToShoot?.Invoke();
            }
        }
        public void CheckPlayerMainMenu()
        {
            bool state = API.IsPauseMenuActive();

            if (state && !state_mainMenu)
            {
                state_mainMenu = state;
                if (debug) Utils.Log("OnPlayerEnteredMainMenu");
                OnPlayerEnteredMainMenu?.Invoke();
            }
            else if (!state && state_mainMenu)
            {
                state_mainMenu = state;
                if (debug) Utils.Log("OnPlayerLeftMainMenu");
                OnPlayerLeftMainMenu?.Invoke();
            }
        }
        public void CheckPlayerWearingHelmet()
        {
            bool state = API.IsPedWearingHelmet(pedHandle);

            if (state && !state_wearingHelmet)
            {
                state_wearingHelmet = state;
                if (debug) Utils.Log("OnPlayerStartedWearingHelmet");
                OnPlayerStartedWearingHelmet?.Invoke();
            }
            else if (!state && state_wearingHelmet)
            {
                state_wearingHelmet = state;
                if (debug) Utils.Log("OnPlayerStoppedWearingHelmet");
                OnPlayerStoppedWearingHelmet?.Invoke();
            }
        }
        public void CheckPlayerJumpingOutOfVehicle()
        {
            bool state = API.IsPedJumpingOutOfVehicle(pedHandle);

            if (state && !state_jumpingOutOfVehicle)
            {
                state_jumpingOutOfVehicle = state;
                if (debug) Utils.Log("OnPlayerStartedJumpingOutOfVehicle");
                OnPlayerStartedJumpingOutOfVehicle?.Invoke();
            }
            else if (!state && state_jumpingOutOfVehicle)
            {
                state_jumpingOutOfVehicle = state;
                if (debug) Utils.Log("OnPlayerStoppedJumpingOutOfVehicle");
                OnPlayerStoppedJumpingOutOfVehicle?.Invoke();
            }
        }
        public void CheckPlayerVaulting()
        {
            bool state = API.IsPedVaulting(pedHandle);

            if (state && !state_vaulting)
            {
                state_vaulting = state;
                if (debug) Utils.Log("OnPlayerStartedVaulting");
                OnPlayerStartedVaulting?.Invoke();
            }
            else if (!state && state_vaulting)
            {
                state_vaulting = state;
                if (debug) Utils.Log("OnPlayerStoppedVaulting");
                OnPlayerStoppedVaulting?.Invoke();
            }
        }
        public void CheckPlayerStealthKilling()
        {
            bool state = API.IsPedPerformingStealthKill(pedHandle);

            if (state && !state_stealthKilling)
            {
                state_stealthKilling = state;
                if (debug) Utils.Log("OnPlayerStartedStealthKill");
                OnPlayerStartedStealthKill?.Invoke();
            }
            else if (!state && state_stealthKilling)
            {
                state_stealthKilling = state;
                if (debug) Utils.Log("OnPlayerStoppedStealthKill");
                OnPlayerStoppedStealthKill?.Invoke();
            }
        }
        public void CheckPlayerSwimmingUnderwater()
        {
            bool state = API.IsPedSwimmingUnderWater(pedHandle);

            if (state && !state_swimmingUnderwater)
            {
                state_swimmingUnderwater = state;
                if (debug) Utils.Log("OnPlayerStartedSwimmingUnderwater");
                OnPlayerStartedSwimmingUnderwater?.Invoke();
            }
            else if (!state && state_swimmingUnderwater)
            {
                state_swimmingUnderwater = state;
                if (debug) Utils.Log("OnPlayerStoppedSwimmingUnderwater");
                OnPlayerStoppedSwimmingUnderwater?.Invoke();
            }
        }
        public void CheckPlayerSwimming()
        {
            bool state = API.IsPedSwimming(pedHandle);

            if (state && !state_swimming)
            {
                state_swimming = state;
                if (debug) Utils.Log("OnPlayerStartedSwimming");
                OnPlayerStartedSwimming?.Invoke();
            }
            else if (!state && state_swimming)
            {
                state_swimming = state;
                if (debug) Utils.Log("OnPlayerStoppedSwimming");
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
                if (debug) Utils.Log("OnPlayerStartedShooting");
                OnPlayerStartedShooting?.Invoke();
            }
            else if (!state && state_shooting)
            {
                state_shooting = state;
                if (debug) Utils.Log("OnPlayerStoppedShooting");
                OnPlayerStoppedShooting?.Invoke();
            }
        }
        public void CheckPlayerReloading()
        {
            bool state = API.IsPedReloading(pedHandle);

            if (state && !state_reloading)
            {
                state_reloading = state;
                if (debug) Utils.Log("OnPlayerStartedReloading");
                OnPlayerStartedReloading?.Invoke();
            }
            else if (!state && state_reloading)
            {
                state_reloading = state;
                if (debug) Utils.Log("OnPlayerStoppedReloading");
                OnPlayerStoppedReloading?.Invoke();
            }
        }
        public void CheckPlayerJacking()
        {
            bool state = API.IsPedJacking(pedHandle);

            if (state && !state_jacking)
            {
                state_jacking = state;
                if (debug) Utils.Log("OnPlayerStartedJacking");
                OnPlayerStartedJacking?.Invoke();
            }
            else if (!state && state_jacking)
            {
                state_jacking = state;
                if (debug) Utils.Log("OnPlayerStoppedJacking");
                OnPlayerStoppedJacking?.Invoke();
            }
        }
        public void CheckPlayerParachuteFreefall()
        {
            bool state = API.IsPedInParachuteFreeFall(pedHandle);

            if (state && !state_parachuteFreefall)
            {
                state_parachuteFreefall = state;
                if (debug) Utils.Log("OnPlayerEnteredParachuteFreefall");
                OnPlayerEnteredParachuteFreefall?.Invoke();
            }
            else if (!state && state_parachuteFreefall)
            {
                state_parachuteFreefall = state;
                if (debug) Utils.Log("OnPlayerLeftParachuteFreefall");
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
                if (debug) Utils.Log("OnPlayerEnteredCover");
                OnPlayerEnteredCover?.Invoke();
            }
            else if (!state && state_inCover)
            {
                state_inCover = state;
                if (debug) Utils.Log("OnPlayerLeftCover");
                OnPlayerLeftCover?.Invoke();
            }
        }
        public void CheckPlayerMeleeCombat()
        {
            bool state = API.IsPedInMeleeCombat(pedHandle);

            if (state && !state_inCombat)
            {
                state_inCombat = state;
                if (debug) Utils.Log("OnPlayerEnteredMeleeCombat");
                OnPlayerEnteredMeleeCombat?.Invoke();
            }
            else if (!state && state_inCombat)
            {
                state_inCombat = state;
                if (debug) Utils.Log("OnPlayerLeftMeleeCombat");
                OnPlayerLeftMeleeCombat?.Invoke();
            }
        }
        public void CheckPlayerOnVehicle()
        {
            bool state = API.IsPedOnVehicle(pedHandle);

            if (state && !state_onVehicle)
            {
                state_onVehicle = state;
                if (debug) Utils.Log("OnPlayerStartedOnVehicle");
                OnPlayerStartedOnVehicle?.Invoke();
            }
            else if (!state && state_onVehicle)
            {
                state_onVehicle = state;
                if (debug) Utils.Log("OnPlayerStoppedOnVehicle");
                OnPlayerStoppedOnVehicle?.Invoke();
            }
        }
        public void CheckPlayerOnBike()
        {
            bool state = API.IsPedOnAnyBike(pedHandle);

            if (state && !state_onBike)
            {
                state_onBike = state;
                if (debug) Utils.Log("OnPlayerStartedOnBike");
                OnPlayerStartedOnBike?.Invoke();
            }
            else if (!state && state_onBike)
            {
                state_onBike = state;
                if (debug) Utils.Log("OnPlayerStoppedOnBike");
                OnPlayerStoppedOnBike?.Invoke();
            }
        }
        public void CheckPlayerOnFoot()
        {
            bool state = API.IsPedOnFoot(pedHandle);

            if (state && !state_onFoot)
            {
                state_onFoot = state;
                if (debug) Utils.Log("OnPlayerStartedOnFoot");
                OnPlayerStartedOnFoot?.Invoke();
            }
            else if (!state && state_onFoot)
            {
                state_onFoot = state;
                if (debug) Utils.Log("ResetPlayerRelatedStates");
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
                if (debug) Utils.Log("OnPlayerEnteredFlyingVehicle");
                OnPlayerEnteredFlyingVehicle?.Invoke();
            }
            else if (!state && state_inFlyingVehicle)
            {
                state_inFlyingVehicle = state;
                if (debug) Utils.Log("OnPlayerLeftFlyingVehicle");
                OnPlayerLeftFlyingVehicle?.Invoke();
            }
        }
        public void CheckPlayerInAnyHeli()
        {
            bool state = API.IsPedInAnyHeli(pedHandle);

            if (state && !state_inHeli)
            {
                state_inHeli = state;
                if (debug) Utils.Log("OnPlayerEnteredHeli");
                OnPlayerEnteredHeli?.Invoke();
            }
            else if (!state && state_inHeli)
            {
                state_inHeli = state;
                if (debug) Utils.Log("OnPlayerLeftHeli");
                OnPlayerLeftHeli?.Invoke();
            }
        }
        public void CheckPlayerInAnyPlane()
        {
            bool state = API.IsPedInAnyPlane(pedHandle);

            if (state && !state_inPlane)
            {
                state_inPlane = state;
                if (debug) Utils.Log("OnPlayerEnteredPlane");
                OnPlayerEnteredPlane?.Invoke();
            }
            else if (!state && state_inPlane)
            {
                state_inPlane = state;
                if (debug) Utils.Log("OnPlayerLeftPlane");
                OnPlayerLeftPlane?.Invoke();
            }
        }
        public void CheckPlayerInAnyPoliceVehicle()
        {
            bool state = API.IsPedInAnyPoliceVehicle(pedHandle);

            if (state && !state_inPoliceVehicle)
            {
                state_inPoliceVehicle = state;
                if (debug) Utils.Log("OnPlayerEnteredPoliceVehicle");
                OnPlayerEnteredPoliceVehicle?.Invoke();
            }
            else if (!state && state_inPoliceVehicle)
            {
                state_inPoliceVehicle = state;
                if (debug) Utils.Log("OnPlayerLeftPoliceVehicle");
                OnPlayerLeftPoliceVehicle?.Invoke();
            }
        }
        public void CheckPlayerInAnySub()
        {
            bool state = API.IsPedInAnySub(pedHandle);

            if (state && !state_inSub)
            {
                state_inSub = state;
                if (debug) Utils.Log("OnPlayerEnteredSub");
                OnPlayerEnteredSub?.Invoke();
            }
            else if (!state && state_inSub)
            {
                state_inSub = state;
                if (debug) Utils.Log("OnPlayerLeftSub");
                OnPlayerLeftSub?.Invoke();
            }
        }
        public void CheckPlayerInAnyTaxi()
        {
            bool state = API.IsPedInAnyTaxi(pedHandle);

            if (state && !state_inTaxi)
            {
                state_inTaxi = state;
                if (debug) Utils.Log("OnPlayerEnteredTaxi");
                OnPlayerEnteredTaxi?.Invoke();
            }
            else if (!state && state_inTaxi)
            {
                state_inTaxi = state;
                if (debug) Utils.Log("OnPlayerLeftTaxi");
                OnPlayerLeftTaxi?.Invoke();
            }
        }
        public void CheckPlayerInAnyTrain()
        {
            bool state = API.IsPedInAnyTrain(pedHandle);

            if (state && !state_inTrain)
            {
                state_inTrain = state;
                if (debug) Utils.Log("OnPlayerEnteredTrain");
                OnPlayerEnteredTrain?.Invoke();
            }
            else if (!state && state_inTrain)
            {
                state_inTrain = state;
                if (debug) Utils.Log("OnPlayerLeftTrain");
                OnPlayerLeftTrain?.Invoke();
            }
        }
        public void CheckPlayerInAnyBoat()
        {
            bool state = API.IsPedInAnyBoat(pedHandle);

            if (state && !state_inBoat)
            {
                state_inBoat = state;
                if (debug) Utils.Log("OnPlayerEnteredBoat");
                OnPlayerEnteredBoat?.Invoke();
            }
            else if (!state && state_inBoat)
            {
                state_inBoat = state;
                if (debug) Utils.Log("OnPlayerLeftBoat");
                OnPlayerLeftBoat?.Invoke();
            }
        }
        public void CheckPlayerFalling()
        {
            bool state = API.IsPedFalling(pedHandle);

            if (state && !state_falling)
            {
                state_falling = state;
                if (debug) Utils.Log("OnPlayerStartedFalling");
                OnPlayerStartedFalling?.Invoke();
            }
            else if (!state && state_falling)
            {
                state_falling = state;
                if (debug) Utils.Log("OnPlayerStoppedFalling");
                OnPlayerStoppedFalling?.Invoke();
            }
        }

        public void CheckPlayerDucking()
        {
            bool state = API.IsPedDucking(pedHandle);

            if (state && !state_ducking)
            {
                state_ducking = state;
                if (debug) Utils.Log("OnPlayerStartedDucking");
                OnPlayerStartedDucking?.Invoke();
            }
            else if (!state && state_ducking)
            {
                state_ducking = state;
                if (debug) Utils.Log("OnPlayerStoppedDucking");
                OnPlayerStoppedDucking?.Invoke();
            }
        }

        public void CheckPlayerDriveBy()
        {
            bool state = API.IsPedDoingDriveby(pedHandle);

            if (state && !state_driveBying)
            {
                state_driveBying = state;
                if (debug) Utils.Log("OnPlayerStartedDriveBy");
                OnPlayerStartedDriveBy?.Invoke();
            }
            else if (!state && state_driveBying)
            {
                state_driveBying = state;
                if (debug) Utils.Log("OnPlayerStoppedDriveBy");
                OnPlayerStoppedDriveBy?.Invoke();
            }
        }

        public void CheckPlayerDiving()
        {
            bool state = API.IsPedDiving(pedHandle);

            if (state && !state_diving)
            {
                state_diving = state;
                if (debug) Utils.Log("OnPlayerStartedDiving");
                OnPlayerStartedDiving?.Invoke();
            }
            else if (!state && state_diving)
            {
                state_diving = state;
                if (debug) Utils.Log("OnPlayerStoppedDiving");
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
                if (debug) Utils.Log("OnPlayerDied");
                OnPlayerDied?.Invoke();
            }
            else if (!state && state_dead)
            {
                state_dead = state;
                if (debug) Utils.Log("OnPlayerRevived");
                OnPlayerRevived?.Invoke();
            }
        }

        public void CheckPlayerClimbing()
        {
            bool state = API.IsPedClimbing(pedHandle);

            if (state && !state_climbing)
            {
                state_climbing = state;
                if (debug) Utils.Log("OnPlayerStartedClimbing");
                OnPlayerStartedClimbing?.Invoke();
            }
            else if (!state && state_climbing)
            {
                state_climbing = state;
                if (debug) Utils.Log("OnPlayerStoppedClimbing");
                OnPlayerStoppedClimbing?.Invoke();
            }
        }

        public void CheckPlayerStunned()
        {
            bool state = API.IsPedBeingStunned(pedHandle, 0);

            if (state && !state_stunned)
            {
                state_stunned = state;
                if (debug) Utils.Log("OnPlayerStartedGettingStunned");
                OnPlayerStartedGettingStunned?.Invoke();
            }
            else if (!state && state_stunned)
            {
                state_stunned = state;
                if (debug) Utils.Log("OnPlayerStoppedGettingStunned");
                OnPlayerStoppedGettingStunned?.Invoke();
            }
        }

        public void CheckPlayerBeingStealthKilled()
        {
            bool state = API.IsPedBeingStealthKilled(pedHandle);

            if (state && !state_beingStealthKilled)
            {
                state_beingStealthKilled = state;
                if (debug) Utils.Log("OnPlayerStartedGettingJacked");
                OnPlayerStartedGettingJacked?.Invoke();
            }
            else if (!state && state_beingStealthKilled)
            {
                state_beingStealthKilled = state;
                if (debug) Utils.Log("OnPlayerStoppedGettingJacked");
                OnPlayerStoppedGettingJacked?.Invoke();
            }
        }

        public void CheckPlayerGettingJacked()
        {
            bool state = API.IsPedBeingJacked(pedHandle);

            if (state && !state_jacked)
            {
                state_jacked = state;
                if (debug) Utils.Log("OnPlayerStartedGettingJacked");
                OnPlayerStartedGettingJacked?.Invoke();
            }
            else if (!state && state_jacked)
            {
                state_jacked = state;
                if (debug) Utils.Log("OnPlayerStoppedGettingJacked");
                OnPlayerStoppedGettingJacked?.Invoke();
            }
        }

        public void CheckPlayerAimFromCover()
        {
            bool state = API.IsPedAimingFromCover(pedHandle);

            if (state && !state_aimingFromCover)
            {
                state_aimingFromCover = state;
                if (debug) Utils.Log("OnPlayerStartedToAimFromCover");
                OnPlayerStartedToAimFromCover?.Invoke();
            }
            else if (!state && state_aimingFromCover)
            {
                state_aimingFromCover = state;
                if (debug) Utils.Log("OnPlayerStoppedToAimFromCover");
                OnPlayerStoppedToAimFromCover?.Invoke();
            }
        }

        public void CheckPlayerWalking()
        {
            bool state = API.IsPedWalking(pedHandle);

            if (state && !state_walking)
            {
                state_walking = state;
                if (debug) Utils.Log("OnPlayerStartedWalking");
                OnPlayerStartedWalking?.Invoke();
            }
            else if (!state && state_walking)
            {
                state_walking = state;
                if (debug) Utils.Log("OnPlayerStoppedWalking");
                OnPlayerStoppedWalking?.Invoke();
            }
        }

        public void CheckPlayerRunning()
        {
            bool state = API.IsPedRunning(pedHandle);

            if (state && !state_running)
            {
                state_running = state;
                if (debug) Utils.Log("OnPlayerStartedRunning");
                OnPlayerStartedRunning?.Invoke();
            }
            else if (!state && state_running)
            {
                state_running = state;
                if (debug) Utils.Log("OnPlayerStoppedRunning");
                OnPlayerStoppedRunning?.Invoke();
            }
        }

        public void CheckPlayerGettingUp()
        {
            bool state = API.IsPedGettingUp(pedHandle);

            if (state && !state_gettingUp)
            {
                state_gettingUp = state;
                if (debug) Utils.Log("OnPlayerStartedToGetUp");
                OnPlayerStartedToGetUp?.Invoke();
            }
            else if (!state && state_gettingUp)
            {
                state_gettingUp = state;
                if (debug) Utils.Log("OnPlayerStoppedToGetUp");
                OnPlayerStoppedToGetUp?.Invoke();
            }
        }

        public void CheckPlayerCuffed()
        {
            bool state = API.IsPedCuffed(pedHandle);

            if (state && !state_cuffed)
            {
                state_cuffed = state;
                if (debug) Utils.Log("OnPlayerCuffed");
                OnPlayerCuffed?.Invoke();
            }
            else if (!state && state_cuffed)
            {
                state_cuffed = state;
                if (debug) Utils.Log("OnPlayerUnCuffed");
                OnPlayerUnCuffed?.Invoke();
            }
        }

        public void CheckPlayerJumping()
        {
            bool state = API.IsPedJumping(pedHandle);

            if (state && !state_jumping)
            {
                state_jumping = state;
                if (debug) Utils.Log("OnPlayerStartedJumping");
                OnPlayerStartedJumping?.Invoke();
            }
            else if (!state && state_jumping)
            {
                state_jumping = state;
                if (debug) Utils.Log("OnPlayerStoppedJumping");
                OnPlayerStoppedJumping?.Invoke();
            }
        }

        public void CheckPlayerSprinting()
        {
            bool sprinting = API.IsPedSprinting(pedHandle);

            if (sprinting && !state_sprinting)
            {
                state_sprinting = sprinting;
                if (debug) Utils.Log("OnPlayerStartedSprinting");
                OnPlayerStartedSprinting?.Invoke();
            }
            else if (!sprinting && state_sprinting)
            {
                state_sprinting = sprinting;
                if (debug) Utils.Log("OnPlayerStoppedSprinting");
                OnPlayerStoppedSprinting?.Invoke();
            }
        }

        public void CheckPlayerSpawned()
        {
            int pHandle = API.GetPlayerPed(playerHandle);

            if (pHandle != pedHandle)
            {
                int pNetId = API.NetworkGetNetworkIdFromEntity(pHandle);
                pedHandle = pHandle;
                pedNetworkId = pNetId;
                if (debug) Utils.Log("OnPlayerSpawned");
                OnPlayerSpawned?.Invoke();

                pedHealth = API.GetEntityHealth(pedHandle);
                pedArmour = API.GetPedArmour(pedHandle);
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
                OnPlayerEnteredVehicle?.Invoke(state_lastVehicleHandle, state_vehicleSeat);
                vehicleSpeed = MEF_Vehicle.GetSpeedInKmh(state_lastVehicleHandle);
                vehicleHealth = API.GetEntityHealth(state_lastVehicleHandle);
                vehicleBodyHealth = API.GetVehicleBodyHealth(state_lastVehicleHandle);
                vehicleEngineHealth = API.GetVehicleEngineHealth(state_lastVehicleHandle);
                vehiclePetrolTankHealth = API.GetVehiclePetrolTankHealth(state_lastVehicleHandle);
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
                    if (debug) Utils.Log("OnPlayerSeatChange");
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
