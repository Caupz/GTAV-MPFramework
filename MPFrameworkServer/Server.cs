using System;
using CitizenFX.Core;

namespace MPFrameworkServer
{
    public class Server : BaseScript
    {
        // PLAYER RELATED EVENTS
        public static event PlayerSpawned OnPlayerSpawned;
        public static event PlayerStartedWalking OnPlayerStartedWalking;
        public static event PlayerStoppedWalking OnPlayerStoppedWalking;
        public static event PlayerStartedRunning OnPlayerStartedRunning;
        public static event PlayerStoppedRunning OnPlayerStoppedRunning;
        public static event PlayerStartedSprinting OnPlayerStartedSprinting;
        public static event PlayerStoppedSprinting OnPlayerStoppedSprinting;
        public static event PlayerStartedJumping OnPlayerStartedJumping;
        public static event PlayerStoppedJumping OnPlayerStoppedJumping;
        public static event PlayerCuffed OnPlayerCuffed;
        public static event PlayerUnCuffed OnPlayerUnCuffed;
        public static event PlayerStartedToGetUp OnPlayerStartedToGetUp;
        public static event PlayerStoppedToGetUp OnPlayerStoppedToGetUp;
        public static event PlayerStartedToAimFromCover OnPlayerStartedToAimFromCover;
        public static event PlayerStoppedToAimFromCover OnPlayerStoppedToAimFromCover;
        public static event PlayerStartedGettingJacked OnPlayerStartedGettingJacked; // TEST teist inimest vaja
        public static event PlayerStoppedGettingJacked OnPlayerStoppedGettingJacked; // TEST teist inimest vaja
        public static event PlayerStartedJacking OnPlayerStartedJacking; // TEST ei tööta?
        public static event PlayerStoppedJacking OnPlayerStoppedJacking; // TEST ei tööta?
        public static event PlayerStartedGettingStunned OnPlayerStartedGettingStunned; // TEST teist inimest vaja
        public static event PlayerStoppedGettingStunned OnPlayerStoppedGettingStunned; // TEST teist inimest vaja
        public static event PlayerStartedClimbing OnPlayerStartedClimbing;
        public static event PlayerStoppedClimbing OnPlayerStoppedClimbing;
        public static event PlayerDied OnPlayerDied;
        public static event PlayerRevived OnPlayerRevived;
        public static event PlayerStartedDiving OnPlayerStartedDiving;
        public static event PlayerStoppedDiving OnPlayerStoppedDiving;
        public static event PlayerStartedDriveBy OnPlayerStartedDriveBy;
        public static event PlayerStoppedDriveBy OnPlayerStoppedDriveBy;
        public static event PlayerStartedFalling OnPlayerStartedFalling;
        public static event PlayerStoppedFalling OnPlayerStoppedFalling;
        public static event PlayerStartedOnFoot OnPlayerStartedOnFoot;
        public static event PlayerStoppedOnFoot OnPlayerStoppedOnFoot;
        public static event PlayerEnteredMeleeCombat OnPlayerEnteredMeleeCombat;
        public static event PlayerLeftMeleeCombat OnPlayerLeftMeleeCombat;
        public static event PlayerEnteredCover OnPlayerEnteredCover;
        public static event PlayerLeftCover OnPlayerLeftCover;
        public static event PlayerEnteredParachuteFreefall OnPlayerEnteredParachuteFreefall;
        public static event PlayerLeftParachuteFreefall OnPlayerLeftParachuteFreefall;
        public static event PlayerStartedReloading OnPlayerStartedReloading;
        public static event PlayerStoppedReloading OnPlayerStoppedReloading;
        public static event PlayerStartedShooting OnPlayerStartedShooting;
        public static event PlayerStoppedShooting OnPlayerStoppedShooting;
        public static event PlayerStartedSwimming OnPlayerStartedSwimming;
        public static event PlayerStoppedSwimming OnPlayerStoppedSwimming;
        public static event PlayerStartedSwimmingUnderwater OnPlayerStartedSwimmingUnderwater;
        public static event PlayerStoppedSwimmingUnderwater OnPlayerStoppedSwimmingUnderwater;
        public static event PlayerStartedStealthKill OnPlayerStartedStealthKill;
        public static event PlayerStoppedStealthKill OnPlayerStoppedStealthKill;
        public static event PlayerStartedVaulting OnPlayerStartedVaulting;
        public static event PlayerStoppedVaulting OnPlayerStoppedVaulting;
        public static event PlayerStartedWearingHelmet OnPlayerStartedWearingHelmet;
        public static event PlayerStoppedWearingHelmet OnPlayerStoppedWearingHelmet;
        public static event PlayerEnteredMainMenu OnPlayerEnteredMainMenu;
        public static event PlayerLeftMainMenu OnPlayerLeftMainMenu;
        public static event PlayerReadyToShoot OnPlayerReadyToShoot;
        public static event PlayerNotReadyToShoot OnPlayerNotReadyToShoot;
        public static event PlayerStartedAiming OnPlayerStartedAiming;
        public static event PlayerStoppedAiming OnPlayerStoppedAiming;

        public static event PlayerHealthGain OnPlayerHealthGain;
        public static event PlayerHealthLoss OnPlayerHealthLoss;
        public static event PlayerArmourGain OnPlayerArmourGain;
        public static event PlayerArmourLoss OnPlayerArmourLoss;
        public static event PlayerWeaponChange OnPlayerWeaponChange;

        // VEHICLE RELATED EVENTS
        public static event PlayerTryingToEnterVehicle OnPlayerTryingToEnterVehicle;
        public static event PlayerEnteredVehicle OnPlayerEnteredVehicle;
        public static event PlayerLeaveVehicle OnPlayerLeaveVehicle;
        public static event PlayerSeatChange OnPlayerSeatChange;
        public static event PlayerSpawnIntoVehicle OnPlayerSpawnIntoVehicle;
        public static event PlayerEnteredBoat OnPlayerEnteredBoat;
        public static event PlayerLeftBoat OnPlayerLeftBoat;
        public static event PlayerEnteredHeli OnPlayerEnteredHeli;
        public static event PlayerLeftHeli OnPlayerLeftHeli;
        public static event PlayerEnteredPlane OnPlayerEnteredPlane;
        public static event PlayerLeftPlane OnPlayerLeftPlane;
        public static event PlayerEnteredPoliceVehicle OnPlayerEnteredPoliceVehicle;
        public static event PlayerLeftPoliceVehicle OnPlayerLeftPoliceVehicle;
        public static event PlayerEnteredSub OnPlayerEnteredSub;
        public static event PlayerLeftSub OnPlayerLeftSub;
        public static event PlayerEnteredTaxi OnPlayerEnteredTaxi;
        public static event PlayerLeftTaxi OnPlayerLeftTaxi;
        public static event PlayerEnteredTrain OnPlayerEnteredTrain;
        public static event PlayerLeftTrain OnPlayerLeftTrain;
        public static event PlayerEnteredFlyingVehicle OnPlayerEnteredFlyingVehicle;
        public static event PlayerLeftFlyingVehicle OnPlayerLeftFlyingVehicle;
        public static event PlayerStartedOnBike OnPlayerStartedOnBike;
        public static event PlayerStoppedOnBike OnPlayerStoppedOnBike;
        public static event PlayerStartedOnVehicle OnPlayerStartedOnVehicle;
        public static event PlayerStoppedOnVehicle OnPlayerStoppedOnVehicle;
        public static event PlayerStartedJumpingOutOfVehicle OnPlayerStartedJumpingOutOfVehicle;
        public static event PlayerStoppedJumpingOutOfVehicle OnPlayerStoppedJumpingOutOfVehicle;
        public static event PlayerStartedMovingVehicle OnPlayerStartedMovingVehicle;
        public static event PlayerStoppedVehicle OnPlayerStoppedVehicle;
        public static event PlayerStartedBurnouting OnPlayerStartedBurnouting;
        public static event PlayerStoppedBurnouting OnPlayerStoppedBurnouting;
        public static event VehicleHealthGain OnVehicleHealthGain;
        public static event VehicleHealthLoss OnVehicleHealthLoss;
        public static event VehicleCrash OnVehicleCrash;

        // VEHICLE RELATED DELEGATES
        public delegate void PlayerStartedBurnouting(Player player, int vehicleNetworkId);
        public delegate void PlayerStoppedBurnouting(Player player, int vehicleNetworkId);
        public delegate void PlayerStartedMovingVehicle(Player player, int vehicleNetworkId);
        public delegate void PlayerStoppedVehicle(Player player, int vehicleNetworkId);
        public delegate void PlayerStartedJumpingOutOfVehicle(Player player, int vehicleNetworkId);
        public delegate void PlayerStoppedJumpingOutOfVehicle(Player player, int vehicleNetworkId);
        public delegate void PlayerTryingToEnterVehicle(Player player, int vehicleNetworkId, int vehicleSeat);
        public delegate void PlayerEnteredVehicle(Player player, int vehicleNetworkId, int vehicleSeat);
        public delegate void PlayerLeaveVehicle(Player player, int vehicleNetworkId, int vehicleSeat);
        public delegate void PlayerSeatChange(Player player, int vehicleNetworkId, int vehicleSeat);
        public delegate void PlayerSpawnIntoVehicle(Player player, int vehicleNetworkId, int vehicleSeat);
        public delegate void PlayerEnteredBoat(Player player, int vehicleNetworkId, int vehicleSeat);
        public delegate void PlayerLeftBoat(Player player, int vehicleNetworkId, int vehicleSeat);
        public delegate void PlayerEnteredHeli(Player player, int vehicleNetworkId, int vehicleSeat);
        public delegate void PlayerLeftHeli(Player player, int vehicleNetworkId, int vehicleSeat);
        public delegate void PlayerEnteredPlane(Player player, int vehicleNetworkId, int vehicleSeat);
        public delegate void PlayerLeftPlane(Player player, int vehicleNetworkId, int vehicleSeat);
        public delegate void PlayerEnteredPoliceVehicle(Player player, int vehicleNetworkId, int vehicleSeat);
        public delegate void PlayerLeftPoliceVehicle(Player player, int vehicleNetworkId, int vehicleSeat);
        public delegate void PlayerEnteredSub(Player player, int vehicleNetworkId, int vehicleSeat);
        public delegate void PlayerLeftSub(Player player, int vehicleNetworkId, int vehicleSeat);
        public delegate void PlayerEnteredTaxi(Player player, int vehicleNetworkId, int vehicleSeat);
        public delegate void PlayerLeftTaxi(Player player, int vehicleNetworkId, int vehicleSeat);
        public delegate void PlayerEnteredTrain(Player player, int vehicleNetworkId, int vehicleSeat);
        public delegate void PlayerLeftTrain(Player player, int vehicleNetworkId, int vehicleSeat);
        public delegate void PlayerEnteredFlyingVehicle(Player player, int vehicleNetworkId, int vehicleSeat);
        public delegate void PlayerLeftFlyingVehicle(Player player, int vehicleNetworkId, int vehicleSeat);
        public delegate void PlayerStartedOnBike(Player player, int vehicleNetworkId, int vehicleSeat);
        public delegate void PlayerStoppedOnBike(Player player, int vehicleNetworkId, int vehicleSeat);
        public delegate void PlayerStartedOnVehicle(Player player);
        public delegate void PlayerStoppedOnVehicle(Player player);
        public delegate void VehicleHealthGain(Player player, int vehicleNetworkId, int vehicleHealth, float vehicleBodyHealth, float vehicleEngineHealth, float vehiclePetrolTankHealth);
        public delegate void VehicleHealthLoss(Player player, int vehicleNetworkId, int vehicleHealth, float vehicleBodyHealth, float vehicleEngineHealth, float vehiclePetrolTankHealth);
        public delegate void VehicleCrash(Player player, int vehicleNetworkId);

        // PLAYER RELATED DELEGATES
        public delegate void PlayerStartedAiming(Player player, uint weapon);
        public delegate void PlayerStoppedAiming(Player player, uint weapon);
        public delegate void PlayerReadyToShoot(Player player, uint weapon);
        public delegate void PlayerNotReadyToShoot(Player player);
        public delegate void PlayerEnteredMainMenu(Player player);
        public delegate void PlayerLeftMainMenu(Player player);
        public delegate void PlayerStartedWearingHelmet(Player player);
        public delegate void PlayerStoppedWearingHelmet(Player player);
        public delegate void PlayerStartedVaulting(Player player);
        public delegate void PlayerStoppedVaulting(Player player);
        public delegate void PlayerStartedStealthKill(Player player, uint weapon);
        public delegate void PlayerStoppedStealthKill(Player player, uint weapon);
        public delegate void PlayerStartedSwimmingUnderwater(Player player);
        public delegate void PlayerStoppedSwimmingUnderwater(Player player);
        public delegate void PlayerStartedSwimming(Player player);
        public delegate void PlayerStoppedSwimming(Player player);
        public delegate void PlayerStartedShooting(Player player, uint weapon);
        public delegate void PlayerStoppedShooting(Player player, uint weapon);
        public delegate void PlayerStartedWalking(Player player);
        public delegate void PlayerStoppedWalking(Player player);
        public delegate void PlayerSpawned(Player player);
        public delegate void PlayerStartedReloading(Player player, uint weapon);
        public delegate void PlayerStoppedReloading(Player player, uint weapon);
        public delegate void PlayerStartedRunning(Player player);
        public delegate void PlayerStoppedRunning(Player player);
        public delegate void PlayerStartedSprinting(Player player);
        public delegate void PlayerStoppedSprinting(Player player);
        public delegate void PlayerStartedJumping(Player player);
        public delegate void PlayerStoppedJumping(Player player);
        public delegate void PlayerCuffed(Player player);
        public delegate void PlayerUnCuffed(Player player);
        public delegate void PlayerStartedToGetUp(Player player);
        public delegate void PlayerStoppedToGetUp(Player player);
        public delegate void PlayerStartedToAimFromCover(Player player, uint weapon);
        public delegate void PlayerStoppedToAimFromCover(Player player, uint weapon);
        public delegate void PlayerStartedGettingJacked(Player player);
        public delegate void PlayerStoppedGettingJacked(Player player);
        public delegate void PlayerStartedGettingStunned(Player player);
        public delegate void PlayerStoppedGettingStunned(Player player);
        public delegate void PlayerStartedClimbing(Player player);
        public delegate void PlayerStoppedClimbing(Player player);
        public delegate void PlayerDied(Player player);
        public delegate void PlayerRevived(Player player);
        public delegate void PlayerStartedDiving(Player player);
        public delegate void PlayerStoppedDiving(Player player);
        public delegate void PlayerStartedDriveBy(Player player, uint weapon);
        public delegate void PlayerStoppedDriveBy(Player player, uint weapon);
        public delegate void PlayerStartedFalling(Player player);
        public delegate void PlayerStoppedFalling(Player player);
        public delegate void PlayerStartedOnFoot(Player player);
        public delegate void PlayerStoppedOnFoot(Player player);
        public delegate void PlayerEnteredMeleeCombat(Player player, uint weapon);
        public delegate void PlayerLeftMeleeCombat(Player player, uint weapon);
        public delegate void PlayerEnteredCover(Player player);
        public delegate void PlayerLeftCover(Player player);
        public delegate void PlayerEnteredParachuteFreefall(Player player);
        public delegate void PlayerLeftParachuteFreefall(Player player);
        public delegate void PlayerStartedJacking(Player player);
        public delegate void PlayerStoppedJacking(Player player);
        public delegate void PlayerHealthGain(Player player, int oldHealth, int newHealth);
        public delegate void PlayerHealthLoss(Player player, int oldHealth, int newHealth);
        public delegate void PlayerArmourGain(Player player, int oldArmour, int newArmour);
        public delegate void PlayerArmourLoss(Player player, int oldArmour, int newArmour);
        public delegate void PlayerWeaponChange(Player player, uint oldWeapon, uint newWeapon);

        public Server()
        {
            Debug.WriteLine("MP FRAMEWORK SERVER INITED");
            EventHandlers["OnPlayerSpawned"] += new Action<Player>(RemoteOnPlayerSpawned);
            EventHandlers["OnPlayerStartedWalking"] += new Action<Player>(RemoteOnPlayerStartedWalking);
            EventHandlers["OnPlayerStoppedWalking"] += new Action<Player>(RemoteOnPlayerStoppedWalking);
            EventHandlers["OnPlayerStartedRunning"] += new Action<Player>(RemoteOnPlayerStartedRunning);
            EventHandlers["OnPlayerStoppedRunning"] += new Action<Player>(RemoteOnPlayerStoppedRunning);
            EventHandlers["OnPlayerStartedSprinting"] += new Action<Player>(RemoteOnPlayerStartedSprinting);
            EventHandlers["OnPlayerStoppedSprinting"] += new Action<Player>(RemoteOnPlayerStoppedSprinting);
            EventHandlers["OnPlayerStartedJumping"] += new Action<Player>(RemoteOnPlayerStartedJumping);
            EventHandlers["OnPlayerStoppedJumping"] += new Action<Player>(RemoteOnPlayerStoppedJumping);
            EventHandlers["OnPlayerCuffed"] += new Action<Player>(RemoteOnPlayerCuffed);
            EventHandlers["OnPlayerUnCuffed"] += new Action<Player>(RemoteOnPlayerUnCuffed);
            EventHandlers["OnPlayerStartedToGetUp"] += new Action<Player>(RemoteOnPlayerStartedToGetUp);
            EventHandlers["OnPlayerStoppedToGetUp"] += new Action<Player>(RemoteOnPlayerStoppedToGetUp);
            EventHandlers["OnPlayerStartedToAimFromCover"] += new Action<Player, uint>(RemoteOnPlayerStartedToAimFromCover);
            EventHandlers["OnPlayerStoppedToAimFromCover"] += new Action<Player, uint>(RemoteOnPlayerStoppedToAimFromCover);
            EventHandlers["OnPlayerStartedGettingJacked"] += new Action<Player>(RemoteOnPlayerStartedGettingJacked);
            EventHandlers["OnPlayerStoppedGettingJacked"] += new Action<Player>(RemoteOnPlayerStoppedGettingJacked);
            EventHandlers["OnPlayerStartedJacking"] += new Action<Player>(RemoteOnPlayerStartedJacking);
            EventHandlers["OnPlayerStoppedJacking"] += new Action<Player>(RemoteOnPlayerStoppedJacking);
            EventHandlers["OnPlayerStartedGettingStunned"] += new Action<Player>(RemoteOnPlayerStartedGettingStunned);
            EventHandlers["OnPlayerStoppedGettingStunned"] += new Action<Player>(RemoteOnPlayerStoppedGettingStunned);
            EventHandlers["OnPlayerStartedClimbing"] += new Action<Player>(RemoteOnPlayerStartedClimbing);
            EventHandlers["OnPlayerStoppedClimbing"] += new Action<Player>(RemoteOnPlayerStoppedClimbing);
            EventHandlers["OnPlayerDied"] += new Action<Player>(RemoteOnPlayerDied);
            EventHandlers["OnPlayerRevived"] += new Action<Player>(RemoteOnPlayerRevived);
            EventHandlers["OnPlayerStartedDiving"] += new Action<Player>(RemoteOnPlayerStartedDiving);
            EventHandlers["OnPlayerStoppedDiving"] += new Action<Player>(RemoteOnPlayerStoppedDiving);
            EventHandlers["OnPlayerStartedDriveBy"] += new Action<Player, uint>(RemoteOnPlayerStartedDriveBy);
            EventHandlers["OnPlayerStoppedDriveBy"] += new Action<Player, uint>(RemoteOnPlayerStoppedDriveBy);
            EventHandlers["OnPlayerStartedFalling"] += new Action<Player>(RemoteOnPlayerStartedFalling);
            EventHandlers["OnPlayerStoppedFalling"] += new Action<Player>(RemoteOnPlayerStoppedFalling);
            EventHandlers["OnPlayerStartedOnFoot"] += new Action<Player>(RemoteOnPlayerStartedOnFoot);
            EventHandlers["OnPlayerStoppedOnFoot"] += new Action<Player>(RemoteOnPlayerStoppedOnFoot);
            EventHandlers["OnPlayerEnteredMeleeCombat"] += new Action<Player, uint>(RemoteOnPlayerEnteredMeleeCombat);
            EventHandlers["OnPlayerLeftMeleeCombat"] += new Action<Player, uint>(RemoteOnPlayerLeftMeleeCombat);
            EventHandlers["OnPlayerEnteredCover"] += new Action<Player>(RemoteOnPlayerEnteredCover);
            EventHandlers["OnPlayerLeftCover"] += new Action<Player>(RemoteOnPlayerLeftCover);
            EventHandlers["OnPlayerEnteredParachuteFreefall"] += new Action<Player>(RemoteOnPlayerEnteredParachuteFreefall);
            EventHandlers["OnPlayerLeftParachuteFreefall"] += new Action<Player>(RemoteOnPlayerLeftParachuteFreefall);
            EventHandlers["OnPlayerStartedReloading"] += new Action<Player, uint>(RemoteOnPlayerStartedReloading);
            EventHandlers["OnPlayerStoppedReloading"] += new Action<Player, uint>(RemoteOnPlayerStoppedReloading);
            EventHandlers["OnPlayerStartedShooting"] += new Action<Player, uint>(RemoteOnPlayerStartedShooting);
            EventHandlers["OnPlayerStoppedShooting"] += new Action<Player, uint>(RemoteOnPlayerStoppedShooting);
            EventHandlers["OnPlayerStartedSwimming"] += new Action<Player>(RemoteOnPlayerStartedSwimming);
            EventHandlers["OnPlayerStoppedSwimming"] += new Action<Player>(RemoteOnPlayerStoppedSwimming);
            EventHandlers["OnPlayerStartedSwimmingUnderwater"] += new Action<Player>(RemoteOnPlayerStartedSwimmingUnderwater);
            EventHandlers["OnPlayerStoppedSwimmingUnderwater"] += new Action<Player>(RemoteOnPlayerStoppedSwimmingUnderwater);
            EventHandlers["OnPlayerStartedStealthKill"] += new Action<Player, uint>(RemoteOnPlayerStartedStealthKill);
            EventHandlers["OnPlayerStoppedStealthKill"] += new Action<Player, uint>(RemoteOnPlayerStoppedStealthKill);
            EventHandlers["OnPlayerStartedVaulting"] += new Action<Player>(RemoteOnPlayerStartedVaulting);
            EventHandlers["OnPlayerStoppedVaulting"] += new Action<Player>(RemoteOnPlayerStoppedVaulting);
            EventHandlers["OnPlayerStartedWearingHelmet"] += new Action<Player>(RemoteOnPlayerStartedWearingHelmet);
            EventHandlers["OnPlayerStoppedWearingHelmet"] += new Action<Player>(RemoteOnPlayerStoppedWearingHelmet);
            EventHandlers["OnPlayerEnteredMainMenu"] += new Action<Player>(RemoteOnPlayerEnteredMainMenu);
            EventHandlers["OnPlayerLeftMainMenu"] += new Action<Player>(RemoteOnPlayerLeftMainMenu);
            EventHandlers["OnPlayerReadyToShoot"] += new Action<Player, uint>(RemoteOnPlayerReadyToShoot);
            EventHandlers["OnPlayerNotReadyToShoot"] += new Action<Player>(RemoteOnPlayerNotReadyToShoot);
            EventHandlers["OnPlayerStartedAiming"] += new Action<Player, uint>(RemoteOnPlayerStartedAiming);
            EventHandlers["OnPlayerStoppedAiming"] += new Action<Player, uint>(RemoteOnPlayerStoppedAiming);
            EventHandlers["OnPlayerHealthGain"] += new Action<Player, int, int>(RemoteOnPlayerHealthGain);
            EventHandlers["OnPlayerHealthLoss"] += new Action<Player, int, int>(RemoteOnPlayerHealthLoss);
            EventHandlers["OnPlayerArmourGain"] += new Action<Player, int, int>(RemoteOnPlayerArmourGain);
            EventHandlers["OnPlayerArmourLoss"] += new Action<Player, int, int>(RemoteOnPlayerArmourLoss);
            EventHandlers["OnPlayerTryingToEnterVehicle"] += new Action<Player, int, int>(RemoteOnPlayerTryingToEnterVehicle);
            EventHandlers["OnPlayerEnteredVehicle"] += new Action<Player, int, int>(RemoteOnPlayerEnteredVehicle);
            EventHandlers["OnPlayerLeaveVehicle"] += new Action<Player, int, int>(RemoteOnPlayerLeaveVehicle);
            EventHandlers["OnPlayerSeatChange"] += new Action<Player, int, int>(RemoteOnPlayerSeatChange);
            EventHandlers["OnPlayerSpawnIntoVehicle"] += new Action<Player, int, int>(RemoteOnPlayerSpawnIntoVehicle);
            EventHandlers["OnPlayerEnteredBoat"] += new Action<Player, int, int>(RemoteOnPlayerEnteredBoat);
            EventHandlers["OnPlayerLeftBoat"] += new Action<Player, int, int>(RemoteOnPlayerLeftBoat);
            EventHandlers["OnPlayerEnteredHeli"] += new Action<Player, int, int>(RemoteOnPlayerEnteredHeli);
            EventHandlers["OnPlayerLeftHeli"] += new Action<Player, int, int>(RemoteOnPlayerLeftHeli);
            EventHandlers["OnPlayerEnteredPlane"] += new Action<Player, int, int>(RemoteOnPlayerEnteredPlane);
            EventHandlers["OnPlayerLeftPlane"] += new Action<Player, int, int>(RemoteOnPlayerLeftPlane);
            EventHandlers["OnPlayerEnteredPoliceVehicle"] += new Action<Player, int, int>(RemoteOnPlayerEnteredPoliceVehicle);
            EventHandlers["OnPlayerLeftPoliceVehicle"] += new Action<Player, int, int>(RemoteOnPlayerLeftPoliceVehicle);
            EventHandlers["OnPlayerEnteredSub"] += new Action<Player, int, int>(RemoteOnPlayerEnteredSub);
            EventHandlers["OnPlayerLeftSub"] += new Action<Player, int, int>(RemoteOnPlayerLeftSub);
            EventHandlers["OnPlayerEnteredTaxi"] += new Action<Player, int, int>(RemoteOnPlayerEnteredTaxi);
            EventHandlers["OnPlayerLeftTaxi"] += new Action<Player, int, int>(RemoteOnPlayerLeftTaxi);
            EventHandlers["OnPlayerEnteredTrain"] += new Action<Player, int, int>(RemoteOnPlayerEnteredTrain);
            EventHandlers["OnPlayerLeftTrain"] += new Action<Player, int, int>(RemoteOnPlayerLeftTrain);
            EventHandlers["OnPlayerEnteredFlyingVehicle"] += new Action<Player, int, int>(RemoteOnPlayerEnteredFlyingVehicle);
            EventHandlers["OnPlayerLeftFlyingVehicle"] += new Action<Player, int, int>(RemoteOnPlayerLeftFlyingVehicle);
            EventHandlers["OnPlayerStartedOnBike"] += new Action<Player, int, int>(RemoteOnPlayerStartedOnBike);
            EventHandlers["OnPlayerStoppedOnBike"] += new Action<Player, int, int>(RemoteOnPlayerStoppedOnBike);
            EventHandlers["OnPlayerStartedOnVehicle"] += new Action<Player>(RemoteOnPlayerStartedOnVehicle);
            EventHandlers["OnPlayerStoppedOnVehicle"] += new Action<Player>(RemoteOnPlayerStoppedOnVehicle);
            EventHandlers["OnPlayerStartedJumpingOutOfVehicle"] += new Action<Player, int, int>(RemoteOnPlayerStartedJumpingOutOfVehicle);
            EventHandlers["OnPlayerStoppedJumpingOutOfVehicle"] += new Action<Player, int, int>(RemoteOnPlayerStoppedJumpingOutOfVehicle);
            EventHandlers["OnPlayerStartedMovingVehicle"] += new Action<Player, int>(RemoteOnPlayerStartedMovingVehicle);
            EventHandlers["OnPlayerStoppedVehicle"] += new Action<Player, int>(RemoteOnPlayerStoppedVehicle);
            EventHandlers["OnPlayerStartedBurnouting"] += new Action<Player, int>(RemoteOnPlayerStartedBurnouting);
            EventHandlers["OnPlayerStoppedBurnouting"] += new Action<Player, int>(RemoteOnPlayerStoppedBurnouting);
            EventHandlers["OnVehicleHealthGain"] += new Action<Player, int, int, float, float, float>(RemoteOnVehicleHealthGain);
            EventHandlers["OnVehicleHealthLoss"] += new Action<Player, int, int, float, float, float>(RemoteOnVehicleHealthLoss);
            EventHandlers["OnVehicleCrash"] += new Action<Player, int>(RemoteOnVehicleCrash);
            Debug.WriteLine("MP FRAMEWORK SERVER INITED - END");
        }

        public void RemoteOnPlayerSpawned([FromSource]Player client) {
            Utils.Log("RemoteOnPlayerSpawned");
            OnPlayerSpawned?.Invoke(client);
            Utils.Log("RemoteOnPlayerSpawned 2");
        }
        public void RemoteOnPlayerStartedWalking([FromSource]Player client) { OnPlayerStartedWalking?.Invoke(client); }
        public void RemoteOnPlayerStoppedWalking([FromSource]Player client) { OnPlayerStoppedWalking?.Invoke(client); }
        public void RemoteOnPlayerStartedRunning([FromSource]Player client) { OnPlayerStartedRunning?.Invoke(client); }
        public void RemoteOnPlayerStoppedRunning([FromSource]Player client) { OnPlayerStoppedRunning?.Invoke(client); }
        public void RemoteOnPlayerStartedSprinting([FromSource]Player client) { OnPlayerStartedSprinting?.Invoke(client); }
        public void RemoteOnPlayerStoppedSprinting([FromSource]Player client) { OnPlayerStoppedSprinting?.Invoke(client); }
        public void RemoteOnPlayerStartedJumping([FromSource]Player client) { OnPlayerStartedJumping?.Invoke(client); }
        public void RemoteOnPlayerStoppedJumping([FromSource]Player client) { OnPlayerStoppedJumping?.Invoke(client); }
        public void RemoteOnPlayerCuffed([FromSource]Player client) { OnPlayerCuffed?.Invoke(client); }
        public void RemoteOnPlayerUnCuffed([FromSource]Player client) { OnPlayerUnCuffed?.Invoke(client); }
        public void RemoteOnPlayerStartedToGetUp([FromSource]Player client) { OnPlayerStartedToGetUp?.Invoke(client); }
        public void RemoteOnPlayerStoppedToGetUp([FromSource]Player client) { OnPlayerStoppedToGetUp?.Invoke(client); }
        public void RemoteOnPlayerStartedToAimFromCover([FromSource]Player client, uint weapon) { OnPlayerStartedToAimFromCover?.Invoke(client, weapon); }
        public void RemoteOnPlayerStoppedToAimFromCover([FromSource]Player client, uint weapon) { OnPlayerStoppedToAimFromCover?.Invoke(client, weapon); }
        public void RemoteOnPlayerStartedGettingJacked([FromSource]Player client) { OnPlayerStartedGettingJacked?.Invoke(client); }
        public void RemoteOnPlayerStoppedGettingJacked([FromSource]Player client) { OnPlayerStoppedGettingJacked?.Invoke(client); }
        public void RemoteOnPlayerStartedJacking([FromSource]Player client) { OnPlayerStartedJacking?.Invoke(client); }
        public void RemoteOnPlayerStoppedJacking([FromSource]Player client) { OnPlayerStoppedJacking?.Invoke(client); }
        public void RemoteOnPlayerStartedGettingStunned([FromSource]Player client) { OnPlayerStartedGettingStunned?.Invoke(client); }
        public void RemoteOnPlayerStoppedGettingStunned([FromSource]Player client) { OnPlayerStoppedGettingStunned?.Invoke(client); }
        public void RemoteOnPlayerStartedClimbing([FromSource]Player client) { OnPlayerStartedClimbing?.Invoke(client); }
        public void RemoteOnPlayerStoppedClimbing([FromSource]Player client) { OnPlayerStoppedClimbing?.Invoke(client); }
        public void RemoteOnPlayerDied([FromSource]Player client) { OnPlayerDied?.Invoke(client); }
        public void RemoteOnPlayerRevived([FromSource]Player client) { OnPlayerRevived?.Invoke(client); }
        public void RemoteOnPlayerStartedDiving([FromSource]Player client) { OnPlayerStartedDiving?.Invoke(client); }
        public void RemoteOnPlayerStoppedDiving([FromSource]Player client) { OnPlayerStoppedDiving?.Invoke(client); }
        public void RemoteOnPlayerStartedDriveBy([FromSource]Player client, uint weapon) { OnPlayerStartedDriveBy?.Invoke(client, weapon); }
        public void RemoteOnPlayerStoppedDriveBy([FromSource]Player client, uint weapon) { OnPlayerStoppedDriveBy?.Invoke(client, weapon); }
        public void RemoteOnPlayerStartedFalling([FromSource]Player client) { OnPlayerStartedFalling?.Invoke(client); }
        public void RemoteOnPlayerStoppedFalling([FromSource]Player client) { OnPlayerStoppedFalling?.Invoke(client); }
        public void RemoteOnPlayerStartedOnFoot([FromSource]Player client) { OnPlayerStartedOnFoot?.Invoke(client); }
        public void RemoteOnPlayerStoppedOnFoot([FromSource]Player client) { OnPlayerStoppedOnFoot?.Invoke(client); }
        public void RemoteOnPlayerEnteredMeleeCombat([FromSource]Player client, uint weapon) { OnPlayerEnteredMeleeCombat?.Invoke(client, weapon); }
        public void RemoteOnPlayerLeftMeleeCombat([FromSource]Player client, uint weapon) { OnPlayerLeftMeleeCombat?.Invoke(client, weapon); }
        public void RemoteOnPlayerEnteredCover([FromSource]Player client) { OnPlayerEnteredCover?.Invoke(client); }
        public void RemoteOnPlayerLeftCover([FromSource]Player client) { OnPlayerLeftCover?.Invoke(client); }
        public void RemoteOnPlayerEnteredParachuteFreefall([FromSource]Player client) { OnPlayerEnteredParachuteFreefall?.Invoke(client); }
        public void RemoteOnPlayerLeftParachuteFreefall([FromSource]Player client) { OnPlayerLeftParachuteFreefall?.Invoke(client); }
        public void RemoteOnPlayerStartedReloading([FromSource]Player client, uint weapon) { OnPlayerStartedReloading?.Invoke(client, weapon); }
        public void RemoteOnPlayerStoppedReloading([FromSource]Player client, uint weapon) { OnPlayerStoppedReloading?.Invoke(client, weapon); }
        public void RemoteOnPlayerStartedShooting([FromSource]Player client, uint weapon) { OnPlayerStartedShooting?.Invoke(client, weapon); }
        public void RemoteOnPlayerStoppedShooting([FromSource]Player client, uint weapon) { OnPlayerStoppedShooting?.Invoke(client, weapon); }
        public void RemoteOnPlayerStartedSwimming([FromSource]Player client) { OnPlayerStartedSwimming?.Invoke(client); }
        public void RemoteOnPlayerStoppedSwimming([FromSource]Player client) { OnPlayerStoppedSwimming?.Invoke(client); }
        public void RemoteOnPlayerStartedSwimmingUnderwater([FromSource]Player client) { OnPlayerStartedSwimmingUnderwater?.Invoke(client); }
        public void RemoteOnPlayerStoppedSwimmingUnderwater([FromSource]Player client) { OnPlayerStoppedSwimmingUnderwater?.Invoke(client); }
        public void RemoteOnPlayerStartedStealthKill([FromSource]Player client, uint weapon) { OnPlayerStartedStealthKill?.Invoke(client, weapon); }
        public void RemoteOnPlayerStoppedStealthKill([FromSource]Player client, uint weapon) { OnPlayerStoppedStealthKill?.Invoke(client, weapon); }
        public void RemoteOnPlayerStartedVaulting([FromSource]Player client) { OnPlayerStartedVaulting?.Invoke(client); }
        public void RemoteOnPlayerStoppedVaulting([FromSource]Player client) { OnPlayerStoppedVaulting?.Invoke(client); }
        public void RemoteOnPlayerStartedWearingHelmet([FromSource]Player client) { OnPlayerStartedWearingHelmet?.Invoke(client); }
        public void RemoteOnPlayerStoppedWearingHelmet([FromSource]Player client) { OnPlayerStoppedWearingHelmet?.Invoke(client); }
        public void RemoteOnPlayerEnteredMainMenu([FromSource]Player client) { OnPlayerEnteredMainMenu?.Invoke(client); }
        public void RemoteOnPlayerLeftMainMenu([FromSource]Player client) { OnPlayerLeftMainMenu?.Invoke(client); }
        public void RemoteOnPlayerReadyToShoot([FromSource]Player client, uint weapon) { OnPlayerReadyToShoot?.Invoke(client, weapon); }
        public void RemoteOnPlayerNotReadyToShoot([FromSource]Player client) { OnPlayerNotReadyToShoot?.Invoke(client); }
        public void RemoteOnPlayerStartedAiming([FromSource]Player client, uint weapon) { OnPlayerStartedAiming?.Invoke(client, weapon); }
        public void RemoteOnPlayerStoppedAiming([FromSource]Player client, uint weapon) { OnPlayerStoppedAiming?.Invoke(client, weapon); }
        public void RemoteOnPlayerHealthGain([FromSource]Player client, int oldHealth, int newHealth) { OnPlayerHealthGain?.Invoke(client, oldHealth, newHealth); }
        public void RemoteOnPlayerHealthLoss([FromSource]Player client, int oldHealth, int newHealth) { OnPlayerHealthLoss?.Invoke(client, oldHealth, newHealth); }
        public void RemoteOnPlayerArmourGain([FromSource]Player client, int oldArmour, int newArmour) { OnPlayerArmourGain?.Invoke(client, oldArmour, newArmour); }
        public void RemoteOnPlayerArmourLoss([FromSource]Player client, int oldArmour, int newArmour) { OnPlayerArmourLoss?.Invoke(client, oldArmour, newArmour); }
        public void RemoteOnPlayerTryingToEnterVehicle([FromSource]Player client, int vehicleNetworkId, int vehicleSeat) { OnPlayerTryingToEnterVehicle?.Invoke(client, vehicleNetworkId, vehicleSeat); }
        public void RemoteOnPlayerEnteredVehicle([FromSource]Player client, int vehicleNetworkId, int vehicleSeat) { OnPlayerEnteredVehicle?.Invoke(client, vehicleNetworkId, vehicleSeat); }
        public void RemoteOnPlayerLeaveVehicle([FromSource]Player client, int vehicleNetworkId, int vehicleSeat) { OnPlayerLeaveVehicle?.Invoke(client, vehicleNetworkId, vehicleSeat); }
        public void RemoteOnPlayerSeatChange([FromSource]Player client, int vehicleNetworkId, int vehicleSeat) { OnPlayerSeatChange?.Invoke(client, vehicleNetworkId, vehicleSeat); }
        public void RemoteOnPlayerSpawnIntoVehicle([FromSource]Player client, int vehicleNetworkId, int seat) { OnPlayerSpawnIntoVehicle?.Invoke(client, vehicleNetworkId, seat); }
        public void RemoteOnPlayerEnteredBoat([FromSource]Player client, int vehicleNetworkId, int seat) { OnPlayerEnteredBoat?.Invoke(client, vehicleNetworkId, seat); }
        public void RemoteOnPlayerLeftBoat([FromSource]Player client, int vehicleNetworkId, int seat) { OnPlayerLeftBoat?.Invoke(client, vehicleNetworkId, seat); }
        public void RemoteOnPlayerEnteredHeli([FromSource]Player client, int vehicleNetworkId, int seat) { OnPlayerEnteredHeli?.Invoke(client, vehicleNetworkId, seat); }
        public void RemoteOnPlayerLeftHeli([FromSource]Player client, int vehicleNetworkId, int seat) { OnPlayerLeftHeli?.Invoke(client, vehicleNetworkId, seat); }
        public void RemoteOnPlayerEnteredPlane([FromSource]Player client, int vehicleNetworkId, int seat) { OnPlayerEnteredPlane?.Invoke(client, vehicleNetworkId, seat); }
        public void RemoteOnPlayerLeftPlane([FromSource]Player client, int vehicleNetworkId, int seat) { OnPlayerLeftPlane?.Invoke(client, vehicleNetworkId, seat); }
        public void RemoteOnPlayerEnteredPoliceVehicle([FromSource]Player client, int vehicleNetworkId, int seat) { OnPlayerEnteredPoliceVehicle?.Invoke(client, vehicleNetworkId, seat); }
        public void RemoteOnPlayerLeftPoliceVehicle([FromSource]Player client, int vehicleNetworkId, int seat) { OnPlayerLeftPoliceVehicle?.Invoke(client, vehicleNetworkId, seat); }
        public void RemoteOnPlayerEnteredSub([FromSource]Player client, int vehicleNetworkId, int seat) { OnPlayerEnteredSub?.Invoke(client, vehicleNetworkId, seat); }
        public void RemoteOnPlayerLeftSub([FromSource]Player client, int vehicleNetworkId, int seat) { OnPlayerLeftSub?.Invoke(client, vehicleNetworkId, seat); }
        public void RemoteOnPlayerEnteredTaxi([FromSource]Player client, int vehicleNetworkId, int seat) { OnPlayerEnteredTaxi?.Invoke(client, vehicleNetworkId, seat); }
        public void RemoteOnPlayerLeftTaxi([FromSource]Player client, int vehicleNetworkId, int seat) { OnPlayerLeftTaxi?.Invoke(client, vehicleNetworkId, seat); }
        public void RemoteOnPlayerEnteredTrain([FromSource]Player client, int vehicleNetworkId, int seat) { OnPlayerEnteredTrain?.Invoke(client, vehicleNetworkId, seat); }
        public void RemoteOnPlayerLeftTrain([FromSource]Player client, int vehicleNetworkId, int seat) { OnPlayerLeftTrain?.Invoke(client, vehicleNetworkId, seat); }
        public void RemoteOnPlayerEnteredFlyingVehicle([FromSource]Player client, int vehicleNetworkId, int seat) { OnPlayerEnteredFlyingVehicle?.Invoke(client, vehicleNetworkId, seat); }
        public void RemoteOnPlayerLeftFlyingVehicle([FromSource]Player client, int vehicleNetworkId, int seat) { OnPlayerLeftFlyingVehicle?.Invoke(client, vehicleNetworkId, seat); }
        public void RemoteOnPlayerStartedOnBike([FromSource]Player client, int vehicleNetworkId, int seat) { OnPlayerStartedOnBike?.Invoke(client, vehicleNetworkId, seat); }
        public void RemoteOnPlayerStoppedOnBike([FromSource]Player client, int vehicleNetworkId, int seat) { OnPlayerStoppedOnBike?.Invoke(client, vehicleNetworkId, seat); }
        public void RemoteOnPlayerStartedOnVehicle([FromSource]Player client) { OnPlayerStartedOnVehicle?.Invoke(client); }
        public void RemoteOnPlayerStoppedOnVehicle([FromSource]Player client) { OnPlayerStoppedOnVehicle?.Invoke(client); }
        public void RemoteOnPlayerStartedJumpingOutOfVehicle([FromSource]Player client, int vehicleNetworkId, int seat) { OnPlayerStartedJumpingOutOfVehicle?.Invoke(client, vehicleNetworkId); }
        public void RemoteOnPlayerStoppedJumpingOutOfVehicle([FromSource]Player client, int vehicleNetworkId, int seat) { OnPlayerStoppedJumpingOutOfVehicle?.Invoke(client, vehicleNetworkId); }
        public void RemoteOnPlayerStartedMovingVehicle([FromSource]Player client, int vehicleNetworkId) { OnPlayerStartedMovingVehicle?.Invoke(client, vehicleNetworkId); }
        public void RemoteOnPlayerStoppedVehicle([FromSource]Player client, int vehicleNetworkId) { OnPlayerStoppedVehicle?.Invoke(client, vehicleNetworkId); }
        public void RemoteOnPlayerStartedBurnouting([FromSource]Player client, int vehicleNetworkId) { OnPlayerStartedBurnouting?.Invoke(client, vehicleNetworkId); }
        public void RemoteOnPlayerStoppedBurnouting([FromSource]Player client, int vehicleNetworkId) { OnPlayerStoppedBurnouting?.Invoke(client, vehicleNetworkId); }
        public void RemoteOnVehicleHealthGain([FromSource]Player client, int vehicleNetworkId, int vehicleHealth, float vehicleBodyHealth, float vehicleEngineHealth, float vehiclePetrolTankHealth) { OnVehicleHealthGain?.Invoke(client, vehicleNetworkId, vehicleHealth, vehicleBodyHealth, vehicleEngineHealth, vehiclePetrolTankHealth); }
        public void RemoteOnVehicleHealthLoss([FromSource]Player client, int vehicleNetworkId, int vehicleHealth, float vehicleBodyHealth, float vehicleEngineHealth, float vehiclePetrolTankHealth) { OnVehicleHealthLoss?.Invoke(client, vehicleNetworkId, vehicleHealth, vehicleBodyHealth, vehicleEngineHealth, vehiclePetrolTankHealth); }
        public void RemoteOnVehicleCrash([FromSource]Player client, int vehicleNetworkId) { OnVehicleCrash?.Invoke(client, vehicleNetworkId); }
    }
}
