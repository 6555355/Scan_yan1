// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Micolor_CleanParameter.cs" company="">
//   
// </copyright>
// <summary>
//   The micolor clean type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace EpsonControlLibrary
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The micolor clean type.
    /// </summary>
    public enum MicolorCleanType : byte
    {
        /// <summary>
        /// The none.
        /// </summary>
        None = 0, 

        /// <summary>
        /// The suck only.
        /// </summary>
        SuckOnly, 

        /// <summary>
        /// The suck and wipe.
        /// </summary>
        SuckAndWipe, 

        /// <summary>
        /// The suck wipe and flash.
        /// </summary>
        SuckWipeAndFlash, 

        /// <summary>
        /// The flash only.
        /// </summary>
        FlashOnly, 

        /// <summary>
        /// The pre clean.
        /// </summary>
        PreClean
    }

    /// <summary>
    /// The cleansectio n_ epso n_ micolor.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CLEANSECTION_EPSON_MICOLOR
    {
        /// <summary>
        /// The type.
        /// </summary>
        public MicolorCleanType type;

        /// <summary>
        /// The loop times.
        /// </summary>
        public byte LoopTimes; // 循环次数。

        /// <summary>
        /// The suck ink time.
        /// </summary>
        public ushort SuckInkTime; // unit:0.1s;

        /// <summary>
        /// The stay time.
        /// </summary>
        public ushort StayTime; // unit:0.1s;

        /// <summary>
        /// The release time.
        /// </summary>
        public ushort ReleaseTime; // unit:0.1s;

        /// <summary>
        /// The flash freq interval.
        /// </summary>
        public ushort FlashFreqInterval; // unit: us.

        /// <summary>
        /// The flash time.
        /// </summary>
        public byte FlashTime; // unit:0.1s.

        /// <summary>
        /// The flash cycle.
        /// </summary>
        public byte FlashCycle; // unit:0.1s.

        /// <summary>
        /// The flash idle in cycle.
        /// </summary>
        public byte FlashIdleInCycle; // unit:0.1s.

        /// <summary>
        /// The suck speed.
        /// </summary>
        public byte SuckSpeed;

        /// <summary>
        /// The move speed.
        /// </summary>
        public byte MoveSpeed;

        /// <summary>
        /// The option.
        /// </summary>
        public byte Option; // bit0-1 is suck speed. based on Speed_Interval in struct CLEANPARA_EPSON_MICOLOR
    }

    /// <summary>
    /// The cleanpar a_ epso n_ micolor.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CleanparaEpsonMicolor
    {
        // motor part:
        /// <summary>
        /// The carriage_ x_ wipe_ speed.
        /// </summary>
        public byte Carriage_X_Wipe_Speed;

        /// <summary>
        /// The speed_ interval.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Speed_Interval; // unit:ms

        /// <summary>
        /// The rotate dir.
        /// </summary>
        public byte RotateDir; // Electric Rotate dir's Pos dir: (A_L,B_L)->(A_H,B_L)->(A_H,B_H)->(A_L,B_H)->(A_L,B_L).

        // Pos dir : hide(idle status)->Wipe->Suck.
        // Neg dir : Suck->Wipe->hide.
        /// <summary>
        /// The wiper_ y_ hide to wipe distance.
        /// </summary>
        public ushort Wiper_Y_HideToWipeDistance; // the rotated step from hide status to Wipe status.

        /// <summary>
        /// The wiper_ y_ wipe to suck distance.
        /// </summary>
        public ushort Wiper_Y_WipeToSuckDistance; // the rotated step from Wipe status to suck status.

        /// <summary>
        /// The wiper_ y_ suck to hide distance.
        /// </summary>
        public ushort Wiper_Y_SuckToHideDistance; // the rotated step from Suck status to hide status.

        /// <summary>
        /// The section count.
        /// </summary>
        public byte sectionCount;

        // position part
        /// <summary>
        /// The carriage_ x_ suck pos.
        /// </summary>
        public short Carriage_X_SuckPos;

        /// <summary>
        /// The carriage_ x_ release pos.
        /// </summary>
        public ushort Carriage_X_ReleasePos;

        /// <summary>
        /// The carriage_ x_ wipe pos_ start.
        /// </summary>
        public ushort Carriage_X_WipePos_Start;

        /// <summary>
        /// The carriage_ x_ wipe pos_ end.
        /// </summary>
        public ushort Carriage_X_WipePos_End;

        /// <summary>
        /// The carriage_ x_ flash pos.
        /// </summary>
        public ushort Carriage_X_FlashPos;

        public ushort Wiper_Y_HideToWipeDistance_1;
        public ushort Carriage_X_WipePos_1_Start;
        public ushort Carriage_X_WipePos_1_End;

        // user part:
        /// <summary>
        /// The sections.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public CLEANSECTION_EPSON_MICOLOR[] sections;
    }
}