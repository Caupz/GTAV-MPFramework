using System;
using CitizenFX.Core;

namespace MPFrameworkServer
{
    public class Server : BaseScript
    {
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
        public delegate void PlayerStartedBurnouting(Player player);
        public delegate void PlayerStoppedBurnouting(Player player);
        public delegate void PlayerStartedMovingVehicle(Player player);
        public delegate void PlayerStoppedVehicle(Player player);
        public delegate void PlayerStartedJumpingOutOfVehicle(Player player);
        public delegate void PlayerStoppedJumpingOutOfVehicle(Player player);
        public delegate void PlayerTryingToEnterVehicle(Player player, int vehicleNetworkId, int vehicleSeat);
        public delegate void PlayerEnteredVehicle(Player player, int vehicleNetworkId, int vehicleSeat);
        public delegate void PlayerLeaveVehicle(Player player, int vehicleNetworkId, int vehicleSeat);
        public delegate void PlayerSeatChange(Player player, int vehicleNetworkId, int vehicleSeat);
        public delegate void PlayerSpawnIntoVehicle(Player player, int vehicleNetworkId);
        public delegate void PlayerEnteredBoat(Player player);
        public delegate void PlayerLeftBoat(Player player);
        public delegate void PlayerEnteredHeli(Player player);
        public delegate void PlayerLeftHeli(Player player);
        public delegate void PlayerEnteredPlane(Player player);
        public delegate void PlayerLeftPlane(Player player);
        public delegate void PlayerEnteredPoliceVehicle(Player player);
        public delegate void PlayerLeftPoliceVehicle(Player player);
        public delegate void PlayerEnteredSub(Player player);
        public delegate void PlayerLeftSub(Player player);
        public delegate void PlayerEnteredTaxi(Player player);
        public delegate void PlayerLeftTaxi(Player player);
        public delegate void PlayerEnteredTrain(Player player);
        public delegate void PlayerLeftTrain(Player player);
        public delegate void PlayerEnteredFlyingVehicle(Player player);
        public delegate void PlayerLeftFlyingVehicle(Player player);
        public delegate void PlayerStartedOnBike(Player player);
        public delegate void PlayerStoppedOnBike(Player player);
        public delegate void PlayerStartedOnVehicle(Player player);
        public delegate void PlayerStoppedOnVehicle(Player player);
        public delegate void VehicleHealthGain(Player player, int vehicleNetworkId, int vehicleHealth, float vehicleBodyHealth, float vehicleEngineHealth, float vehiclePetrolTankHealth);
        public delegate void VehicleHealthLoss(Player player, int vehicleNetworkId, int vehicleHealth, float vehicleBodyHealth, float vehicleEngineHealth, float vehiclePetrolTankHealth);
        public delegate void VehicleCrash(Player player);

        // PLAYER RELATED DELEGATES
        public delegate void PlayerStartedAiming(Player player);
        public delegate void PlayerStoppedAiming(Player player);
        public delegate void PlayerReadyToShoot(Player player);
        public delegate void PlayerNotReadyToShoot(Player player);
        public delegate void PlayerEnteredMainMenu(Player player);
        public delegate void PlayerLeftMainMenu(Player player);
        public delegate void PlayerStartedWearingHelmet(Player player);
        public delegate void PlayerStoppedWearingHelmet(Player player);
        public delegate void PlayerStartedVaulting(Player player);
        public delegate void PlayerStoppedVaulting(Player player);
        public delegate void PlayerStartedStealthKill(Player player);
        public delegate void PlayerStoppedStealthKill(Player player);
        public delegate void PlayerStartedSwimmingUnderwater(Player player);
        public delegate void PlayerStoppedSwimmingUnderwater(Player player);
        public delegate void PlayerStartedSwimming(Player player);
        public delegate void PlayerStoppedSwimming(Player player);
        public delegate void PlayerStartedShooting(Player player);
        public delegate void PlayerStoppedShooting(Player player);
        public delegate void PlayerStartedWalking(Player player);
        public delegate void PlayerStoppedWalking(Player player);
        public delegate void PlayerSpawned(Player player);
        public delegate void PlayerStartedReloading(Player player);
        public delegate void PlayerStoppedReloading(Player player);
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
        public delegate void PlayerStartedToAimFromCover(Player player);
        public delegate void PlayerStoppedToAimFromCover(Player player);
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
        public delegate void PlayerStartedDriveBy(Player player);
        public delegate void PlayerStoppedDriveBy(Player player);
        public delegate void PlayerStartedFalling(Player player);
        public delegate void PlayerStoppedFalling(Player player);
        public delegate void PlayerStartedOnFoot(Player player);
        public delegate void PlayerStoppedOnFoot(Player player);
        public delegate void PlayerEnteredMeleeCombat(Player player);
        public delegate void PlayerLeftMeleeCombat(Player player);
        public delegate void PlayerEnteredCover(Player player);
        public delegate void PlayerLeftCover(Player player);
        public delegate void PlayerEnteredParachuteFreefall(Player player);
        public delegate void PlayerLeftParachuteFreefall(Player player);
        public delegate void PlayerStartedJacking(Player player);
        public delegate void PlayerStoppedJacking(Player player);
        public delegate void PlayerHealthGain(Player player);
        public delegate void PlayerHealthLoss(Player player);
        public delegate void PlayerArmourGain(Player player);
        public delegate void PlayerArmourLoss(Player player);

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
            EventHandlers["OnPlayerStartedToAimFromCover"] += new Action<Player>(RemoteOnPlayerStartedToAimFromCover);
            EventHandlers["OnPlayerStoppedToAimFromCover"] += new Action<Player>(RemoteOnPlayerStoppedToAimFromCover);
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
            EventHandlers["OnPlayerStartedDriveBy"] += new Action<Player>(RemoteOnPlayerStartedDriveBy);
            EventHandlers["OnPlayerStoppedDriveBy"] += new Action<Player>(RemoteOnPlayerStoppedDriveBy);
            EventHandlers["OnPlayerStartedFalling"] += new Action<Player>(RemoteOnPlayerStartedFalling);
            EventHandlers["OnPlayerStoppedFalling"] += new Action<Player>(RemoteOnPlayerStoppedFalling);
            EventHandlers["OnPlayerStartedOnFoot"] += new Action<Player>(RemoteOnPlayerStartedOnFoot);
            EventHandlers["OnPlayerStoppedOnFoot"] += new Action<Player>(RemoteOnPlayerStoppedOnFoot);
            EventHandlers["OnPlayerEnteredMeleeCombat"] += new Action<Player>(RemoteOnPlayerEnteredMeleeCombat);
            EventHandlers["OnPlayerLeftMeleeCombat"] += new Action<Player>(RemoteOnPlayerLeftMeleeCombat);
            EventHandlers["OnPlayerEnteredCover"] += new Action<Player>(RemoteOnPlayerEnteredCover);
            EventHandlers["OnPlayerLeftCover"] += new Action<Player>(RemoteOnPlayerLeftCover);
            EventHandlers["OnPlayerEnteredParachuteFreefall"] += new Action<Player>(RemoteOnPlayerEnteredParachuteFreefall);
            EventHandlers["OnPlayerLeftParachuteFreefall"] += new Action<Player>(RemoteOnPlayerLeftParachuteFreefall);
            EventHandlers["OnPlayerStartedReloading"] += new Action<Player>(RemoteOnPlayerStartedReloading);
            EventHandlers["OnPlayerStoppedReloading"] += new Action<Player>(RemoteOnPlayerStoppedReloading);
            EventHandlers["OnPlayerStartedShooting"] += new Action<Player>(RemoteOnPlayerStartedShooting);
            EventHandlers["OnPlayerStoppedShooting"] += new Action<Player>(RemoteOnPlayerStoppedShooting);
            EventHandlers["OnPlayerStartedSwimming"] += new Action<Player>(RemoteOnPlayerStartedSwimming);
            EventHandlers["OnPlayerStoppedSwimming"] += new Action<Player>(RemoteOnPlayerStoppedSwimming);
            EventHandlers["OnPlayerStartedSwimmingUnderwater"] += new Action<Player>(RemoteOnPlayerStartedSwimmingUnderwater);
            EventHandlers["OnPlayerStoppedSwimmingUnderwater"] += new Action<Player>(RemoteOnPlayerStoppedSwimmingUnderwater);
            EventHandlers["OnPlayerStartedStealthKill"] += new Action<Player>(RemoteOnPlayerStartedStealthKill);
            EventHandlers["OnPlayerStoppedStealthKill"] += new Action<Player>(RemoteOnPlayerStoppedStealthKill);
            EventHandlers["OnPlayerStartedVaulting"] += new Action<Player>(RemoteOnPlayerStartedVaulting);
            EventHandlers["OnPlayerStoppedVaulting"] += new Action<Player>(RemoteOnPlayerStoppedVaulting);
            EventHandlers["OnPlayerStartedWearingHelmet"] += new Action<Player>(RemoteOnPlayerStartedWearingHelmet);
            EventHandlers["OnPlayerStoppedWearingHelmet"] += new Action<Player>(RemoteOnPlayerStoppedWearingHelmet);
            EventHandlers["OnPlayerEnteredMainMenu"] += new Action<Player>(RemoteOnPlayerEnteredMainMenu);
            EventHandlers["OnPlayerLeftMainMenu"] += new Action<Player>(RemoteOnPlayerLeftMainMenu);
            EventHandlers["OnPlayerReadyToShoot"] += new Action<Player>(RemoteOnPlayerReadyToShoot);
            EventHandlers["OnPlayerNotReadyToShoot"] += new Action<Player>(RemoteOnPlayerNotReadyToShoot);
            EventHandlers["OnPlayerStartedAiming"] += new Action<Player>(RemoteOnPlayerStartedAiming);
            EventHandlers["OnPlayerStoppedAiming"] += new Action<Player>(RemoteOnPlayerStoppedAiming);
            EventHandlers["OnPlayerHealthGain"] += new Action<Player>(RemoteOnPlayerHealthGain);
            EventHandlers["OnPlayerHealthLoss"] += new Action<Player>(RemoteOnPlayerHealthLoss);
            EventHandlers["OnPlayerArmourGain"] += new Action<Player>(RemoteOnPlayerArmourGain);
            EventHandlers["OnPlayerArmourLoss"] += new Action<Player>(RemoteOnPlayerArmourLoss);
            EventHandlers["OnPlayerTryingToEnterVehicle"] += new Action<Player, int, int>(RemoteOnPlayerTryingToEnterVehicle);
            EventHandlers["OnPlayerEnteredVehicle"] += new Action<Player, int, int>(RemoteOnPlayerEnteredVehicle);
            EventHandlers["OnPlayerLeaveVehicle"] += new Action<Player, int, int>(RemoteOnPlayerLeaveVehicle);
            EventHandlers["OnPlayerSeatChange"] += new Action<Player, int, int>(RemoteOnPlayerSeatChange);
            EventHandlers["OnPlayerSpawnIntoVehicle"] += new Action<Player, int>(RemoteOnPlayerSpawnIntoVehicle);
            EventHandlers["OnPlayerEnteredBoat"] += new Action<Player>(RemoteOnPlayerEnteredBoat);
            EventHandlers["OnPlayerLeftBoat"] += new Action<Player>(RemoteOnPlayerLeftBoat);
            EventHandlers["OnPlayerEnteredHeli"] += new Action<Player>(RemoteOnPlayerEnteredHeli);
            EventHandlers["OnPlayerLeftHeli"] += new Action<Player>(RemoteOnPlayerLeftHeli);
            EventHandlers["OnPlayerEnteredPlane"] += new Action<Player>(RemoteOnPlayerEnteredPlane);
            EventHandlers["OnPlayerLeftPlane"] += new Action<Player>(RemoteOnPlayerLeftPlane);
            EventHandlers["OnPlayerEnteredPoliceVehicle"] += new Action<Player>(RemoteOnPlayerEnteredPoliceVehicle);
            EventHandlers["OnPlayerLeftPoliceVehicle"] += new Action<Player>(RemoteOnPlayerLeftPoliceVehicle);
            EventHandlers["OnPlayerEnteredSub"] += new Action<Player>(RemoteOnPlayerEnteredSub);
            EventHandlers["OnPlayerLeftSub"] += new Action<Player>(RemoteOnPlayerLeftSub);
            EventHandlers["OnPlayerEnteredTaxi"] += new Action<Player>(RemoteOnPlayerEnteredTaxi);
            EventHandlers["OnPlayerLeftTaxi"] += new Action<Player>(RemoteOnPlayerLeftTaxi);
            EventHandlers["OnPlayerEnteredTrain"] += new Action<Player>(RemoteOnPlayerEnteredTrain);
            EventHandlers["OnPlayerLeftTrain"] += new Action<Player>(RemoteOnPlayerLeftTrain);
            EventHandlers["OnPlayerEnteredFlyingVehicle"] += new Action<Player>(RemoteOnPlayerEnteredFlyingVehicle);
            EventHandlers["OnPlayerLeftFlyingVehicle"] += new Action<Player>(RemoteOnPlayerLeftFlyingVehicle);
            EventHandlers["OnPlayerStartedOnBike"] += new Action<Player>(RemoteOnPlayerStartedOnBike);
            EventHandlers["OnPlayerStoppedOnBike"] += new Action<Player>(RemoteOnPlayerStoppedOnBike);
            EventHandlers["OnPlayerStartedOnVehicle"] += new Action<Player>(RemoteOnPlayerStartedOnVehicle);
            EventHandlers["OnPlayerStoppedOnVehicle"] += new Action<Player>(RemoteOnPlayerStoppedOnVehicle);
            EventHandlers["OnPlayerStartedJumpingOutOfVehicle"] += new Action<Player>(RemoteOnPlayerStartedJumpingOutOfVehicle);
            EventHandlers["OnPlayerStoppedJumpingOutOfVehicle"] += new Action<Player>(RemoteOnPlayerStoppedJumpingOutOfVehicle);
            EventHandlers["OnPlayerStartedMovingVehicle"] += new Action<Player>(RemoteOnPlayerStartedMovingVehicle);
            EventHandlers["OnPlayerStoppedVehicle"] += new Action<Player>(RemoteOnPlayerStoppedVehicle);
            EventHandlers["OnPlayerStartedBurnouting"] += new Action<Player>(RemoteOnPlayerStartedBurnouting);
            EventHandlers["OnPlayerStoppedBurnouting"] += new Action<Player>(RemoteOnPlayerStoppedBurnouting);
            EventHandlers["OnVehicleHealthGain"] += new Action<Player, int, int, float, float, float>(RemoteOnVehicleHealthGain);
            EventHandlers["OnVehicleHealthLoss"] += new Action<Player, int, int, float, float, float>(RemoteOnVehicleHealthLoss);
            EventHandlers["OnVehicleCrash"] += new Action<Player>(RemoteOnVehicleCrash);
            Debug.WriteLine("MP FRAMEWORK SERVER INITED - END");
        }

        public void RemoteOnPlayerSpawned([FromSource]Player client) { OnPlayerSpawned?.Invoke(client); }
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
        public void RemoteOnPlayerStartedToAimFromCover([FromSource]Player client) { OnPlayerStartedToAimFromCover?.Invoke(client); }
        public void RemoteOnPlayerStoppedToAimFromCover([FromSource]Player client) { OnPlayerStoppedToAimFromCover?.Invoke(client); }
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
        public void RemoteOnPlayerStartedDriveBy([FromSource]Player client) { OnPlayerStartedDriveBy?.Invoke(client); }
        public void RemoteOnPlayerStoppedDriveBy([FromSource]Player client) { OnPlayerStoppedDriveBy?.Invoke(client); }
        public void RemoteOnPlayerStartedFalling([FromSource]Player client) { OnPlayerStartedFalling?.Invoke(client); }
        public void RemoteOnPlayerStoppedFalling([FromSource]Player client) { OnPlayerStoppedFalling?.Invoke(client); }
        public void RemoteOnPlayerStartedOnFoot([FromSource]Player client) { OnPlayerStartedOnFoot?.Invoke(client); }
        public void RemoteOnPlayerStoppedOnFoot([FromSource]Player client) { OnPlayerStoppedOnFoot?.Invoke(client); }
        public void RemoteOnPlayerEnteredMeleeCombat([FromSource]Player client) { OnPlayerEnteredMeleeCombat?.Invoke(client); }
        public void RemoteOnPlayerLeftMeleeCombat([FromSource]Player client) { OnPlayerLeftMeleeCombat?.Invoke(client); }
        public void RemoteOnPlayerEnteredCover([FromSource]Player client) { OnPlayerEnteredCover?.Invoke(client); }
        public void RemoteOnPlayerLeftCover([FromSource]Player client) { OnPlayerLeftCover?.Invoke(client); }
        public void RemoteOnPlayerEnteredParachuteFreefall([FromSource]Player client) { OnPlayerEnteredParachuteFreefall?.Invoke(client); }
        public void RemoteOnPlayerLeftParachuteFreefall([FromSource]Player client) { OnPlayerLeftParachuteFreefall?.Invoke(client); }
        public void RemoteOnPlayerStartedReloading([FromSource]Player client) { OnPlayerStartedReloading?.Invoke(client); }
        public void RemoteOnPlayerStoppedReloading([FromSource]Player client) { OnPlayerStoppedReloading?.Invoke(client); }
        public void RemoteOnPlayerStartedShooting([FromSource]Player client) { OnPlayerStartedShooting?.Invoke(client); }
        public void RemoteOnPlayerStoppedShooting([FromSource]Player client) { OnPlayerStoppedShooting?.Invoke(client); }
        public void RemoteOnPlayerStartedSwimming([FromSource]Player client) { OnPlayerStartedSwimming?.Invoke(client); }
        public void RemoteOnPlayerStoppedSwimming([FromSource]Player client) { OnPlayerStoppedSwimming?.Invoke(client); }
        public void RemoteOnPlayerStartedSwimmingUnderwater([FromSource]Player client) { OnPlayerStartedSwimmingUnderwater?.Invoke(client); }
        public void RemoteOnPlayerStoppedSwimmingUnderwater([FromSource]Player client) { OnPlayerStoppedSwimmingUnderwater?.Invoke(client); }
        public void RemoteOnPlayerStartedStealthKill([FromSource]Player client) { OnPlayerStartedStealthKill?.Invoke(client); }
        public void RemoteOnPlayerStoppedStealthKill([FromSource]Player client) { OnPlayerStoppedStealthKill?.Invoke(client); }
        public void RemoteOnPlayerStartedVaulting([FromSource]Player client) { OnPlayerStartedVaulting?.Invoke(client); }
        public void RemoteOnPlayerStoppedVaulting([FromSource]Player client) { OnPlayerStoppedVaulting?.Invoke(client); }
        public void RemoteOnPlayerStartedWearingHelmet([FromSource]Player client) { OnPlayerStartedWearingHelmet?.Invoke(client); }
        public void RemoteOnPlayerStoppedWearingHelmet([FromSource]Player client) { OnPlayerStoppedWearingHelmet?.Invoke(client); }
        public void RemoteOnPlayerEnteredMainMenu([FromSource]Player client) { OnPlayerEnteredMainMenu?.Invoke(client); }
        public void RemoteOnPlayerLeftMainMenu([FromSource]Player client) { OnPlayerLeftMainMenu?.Invoke(client); }
        public void RemoteOnPlayerReadyToShoot([FromSource]Player client) { OnPlayerReadyToShoot?.Invoke(client); }
        public void RemoteOnPlayerNotReadyToShoot([FromSource]Player client) { OnPlayerNotReadyToShoot?.Invoke(client); }
        public void RemoteOnPlayerStartedAiming([FromSource]Player client) { OnPlayerStartedAiming?.Invoke(client); }
        public void RemoteOnPlayerStoppedAiming([FromSource]Player client) { OnPlayerStoppedAiming?.Invoke(client); }
        public void RemoteOnPlayerHealthGain([FromSource]Player client) { OnPlayerHealthGain?.Invoke(client); }
        public void RemoteOnPlayerHealthLoss([FromSource]Player client) { OnPlayerHealthLoss?.Invoke(client); }
        public void RemoteOnPlayerArmourGain([FromSource]Player client) { OnPlayerArmourGain?.Invoke(client); }
        public void RemoteOnPlayerArmourLoss([FromSource]Player client) { OnPlayerArmourLoss?.Invoke(client); }
        public void RemoteOnPlayerTryingToEnterVehicle([FromSource]Player client, int vehicleNetworkId, int vehicleSeat) { OnPlayerTryingToEnterVehicle?.Invoke(client, vehicleNetworkId, vehicleSeat); }
        public void RemoteOnPlayerEnteredVehicle([FromSource]Player client, int vehicleNetworkId, int vehicleSeat) { OnPlayerEnteredVehicle?.Invoke(client, vehicleNetworkId, vehicleSeat); }
        public void RemoteOnPlayerLeaveVehicle([FromSource]Player client, int vehicleNetworkId, int vehicleSeat) { OnPlayerLeaveVehicle?.Invoke(client, vehicleNetworkId, vehicleSeat); }
        public void RemoteOnPlayerSeatChange([FromSource]Player client, int vehicleNetworkId, int vehicleSeat) { OnPlayerSeatChange?.Invoke(client, vehicleNetworkId, vehicleSeat); }
        public void RemoteOnPlayerSpawnIntoVehicle([FromSource]Player client, int vehicleNetworkId) { OnPlayerSpawnIntoVehicle?.Invoke(client, vehicleNetworkId); }
        public void RemoteOnPlayerEnteredBoat([FromSource]Player client) { OnPlayerEnteredBoat?.Invoke(client); }
        public void RemoteOnPlayerLeftBoat([FromSource]Player client) { OnPlayerLeftBoat?.Invoke(client); }
        public void RemoteOnPlayerEnteredHeli([FromSource]Player client) { OnPlayerEnteredHeli?.Invoke(client); }
        public void RemoteOnPlayerLeftHeli([FromSource]Player client) { OnPlayerLeftHeli?.Invoke(client); }
        public void RemoteOnPlayerEnteredPlane([FromSource]Player client) { OnPlayerEnteredPlane?.Invoke(client); }
        public void RemoteOnPlayerLeftPlane([FromSource]Player client) { OnPlayerLeftPlane?.Invoke(client); }
        public void RemoteOnPlayerEnteredPoliceVehicle([FromSource]Player client) { OnPlayerEnteredPoliceVehicle?.Invoke(client); }
        public void RemoteOnPlayerLeftPoliceVehicle([FromSource]Player client) { OnPlayerLeftPoliceVehicle?.Invoke(client); }
        public void RemoteOnPlayerEnteredSub([FromSource]Player client) { OnPlayerEnteredSub?.Invoke(client); }
        public void RemoteOnPlayerLeftSub([FromSource]Player client) { OnPlayerLeftSub?.Invoke(client); }
        public void RemoteOnPlayerEnteredTaxi([FromSource]Player client) { OnPlayerEnteredTaxi?.Invoke(client); }
        public void RemoteOnPlayerLeftTaxi([FromSource]Player client) { OnPlayerLeftTaxi?.Invoke(client); }
        public void RemoteOnPlayerEnteredTrain([FromSource]Player client) { OnPlayerEnteredTrain?.Invoke(client); }
        public void RemoteOnPlayerLeftTrain([FromSource]Player client) { OnPlayerLeftTrain?.Invoke(client); }
        public void RemoteOnPlayerEnteredFlyingVehicle([FromSource]Player client) { OnPlayerEnteredFlyingVehicle?.Invoke(client); }
        public void RemoteOnPlayerLeftFlyingVehicle([FromSource]Player client) { OnPlayerLeftFlyingVehicle?.Invoke(client); }
        public void RemoteOnPlayerStartedOnBike([FromSource]Player client) { OnPlayerStartedOnBike?.Invoke(client); }
        public void RemoteOnPlayerStoppedOnBike([FromSource]Player client) { OnPlayerStoppedOnBike?.Invoke(client); }
        public void RemoteOnPlayerStartedOnVehicle([FromSource]Player client) { OnPlayerStartedOnVehicle?.Invoke(client); }
        public void RemoteOnPlayerStoppedOnVehicle([FromSource]Player client) { OnPlayerStoppedOnVehicle?.Invoke(client); }
        public void RemoteOnPlayerStartedJumpingOutOfVehicle([FromSource]Player client) { OnPlayerStartedJumpingOutOfVehicle?.Invoke(client); }
        public void RemoteOnPlayerStoppedJumpingOutOfVehicle([FromSource]Player client) { OnPlayerStoppedJumpingOutOfVehicle?.Invoke(client); }
        public void RemoteOnPlayerStartedMovingVehicle([FromSource]Player client) { OnPlayerStartedMovingVehicle?.Invoke(client); }
        public void RemoteOnPlayerStoppedVehicle([FromSource]Player client) { OnPlayerStoppedVehicle?.Invoke(client); }
        public void RemoteOnPlayerStartedBurnouting([FromSource]Player client) { OnPlayerStartedBurnouting?.Invoke(client); }
        public void RemoteOnPlayerStoppedBurnouting([FromSource]Player client) { OnPlayerStoppedBurnouting?.Invoke(client); }
        public void RemoteOnVehicleHealthGain([FromSource]Player client, int vehicleNetworkId, int vehicleHealth, float vehicleBodyHealth, float vehicleEngineHealth, float vehiclePetrolTankHealth) { OnVehicleHealthGain?.Invoke(client, vehicleNetworkId, vehicleHealth, vehicleBodyHealth, vehicleEngineHealth, vehiclePetrolTankHealth); }
        public void RemoteOnVehicleHealthLoss([FromSource]Player client, int vehicleNetworkId, int vehicleHealth, float vehicleBodyHealth, float vehicleEngineHealth, float vehiclePetrolTankHealth) { OnVehicleHealthLoss?.Invoke(client, vehicleNetworkId, vehicleHealth, vehicleBodyHealth, vehicleEngineHealth, vehiclePetrolTankHealth); }
        public void RemoteOnVehicleCrash([FromSource]Player client) { OnVehicleCrash?.Invoke(client); }
    }
}
