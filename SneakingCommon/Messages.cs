using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SneakingCommon
{
    public enum WorldMessage
    {
        None,
        StopGuardPatrols,
        ResetGuards,
        ResetGuardsEndOfTurn,
        ResetGuardsEndOfMatch,
        BeginMatch,
        ResetMatch,
        LoadGuards,
        LoadPC,
        ResetPC,
        GuardsTurn,
        Refresh,
        NoiseGenerated,
        NoiseGeneratedByPC,
        NoiseGeneratedByGuard,
        NextGuardTurn,
        PCAttackedGuard,
        PlayerAttacks,
        PlayerEndedTurn,
        NextTurnForPlayer,
        PlayerLeftClickedTile,
        PlayerRightClickedTile,
        PlayerClickedSneaking,
        PlayerStartSneaking,
        PlayerEndSneaking,
        PlayerClickedAttack,
        PCMoved,
        GuardMoved,
        GuardsTurnOver,
        GameOver,
        //Behavior change messages
        SetGuardKnownNoiseAllGuards,
        SetGuardKnownNoiseFoVGuards,
        SetGuardKnownNoiseNone,
        SetPlayerEntryPoint,
        SetVisibilityBehaviorInGuards,
        SetPlayerVisibleStanding,
        SetPlayerVisibleSneaking,
        SetGuardFoHPerception,
        SetGuardFoHAll,
        SetVisibleGuardsFoV,
        SetVisibleGuardsAll,
        SetVisibleGuardsNone,
        SetPlayerFoVDebug,
        SetPlayerFoVCone,
        SetPlayerFoVDark,
        SetPlayerFoHNormal,
        SetPlayerFoHSilenced,
        SetPlayerNoiseDebug,
        SetPlayerNoiseVisible,
        SetGuardNoiseMapVisible,
        SetGuardNoiseMapHidden,
        SetGuardNoiseMapNoKnownNoise,
        SetPlayerVisibilityAll,
        SetPlayerVisibilityRayTraced,
        SetPlayerVisibilityNone
    }
    public enum GuardMessage
    {
        None,
        PreMove,
        PostMove,
        GuardMoved,
        NextToPlayer
    }
    public enum PlayerMessage
    {
        PCBeginSneaking,
        PCEndSneaking,
        PCMoved,
        PCAttackedGuard,
        TurnOver
    }
    public enum NoiseSources
    {
        Guard,
        Player,
        Object
    }
}
