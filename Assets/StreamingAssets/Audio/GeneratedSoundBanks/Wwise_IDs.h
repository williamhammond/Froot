/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID BREAKROCK = 3614189553U;
        static const AkUniqueID BREATH = 1326786195U;
        static const AkUniqueID COLLECTAIRCAN = 416902311U;
        static const AkUniqueID COLLECTSOIL = 717635272U;
        static const AkUniqueID COLLECTWATER = 1695098014U;
        static const AkUniqueID DEATH = 779278001U;
        static const AkUniqueID ENTERBUBBLE = 1956601267U;
        static const AkUniqueID GROWTILE = 1913626152U;
        static const AkUniqueID HITROCK = 3660205123U;
        static const AkUniqueID LEAVESRUSTLE = 3272521878U;
        static const AkUniqueID PLAYFOOTSTEP = 1712852617U;
        static const AkUniqueID PLAYWIND = 1592727253U;
        static const AkUniqueID USEAIRCAN = 3282522952U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace PLAYERLIFE
        {
            static const AkUniqueID GROUP = 444815956U;

            namespace STATE
            {
                static const AkUniqueID ALIVE = 655265632U;
                static const AkUniqueID DEAD = 2044049779U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace PLAYERLIFE

    } // namespace STATES

    namespace SWITCHES
    {
        namespace MUSICMOOD
        {
            static const AkUniqueID GROUP = 339754815U;

            namespace SWITCH
            {
                static const AkUniqueID FARBASE = 1406768315U;
                static const AkUniqueID NEARBASE = 1869510436U;
            } // namespace SWITCH
        } // namespace MUSICMOOD

        namespace PLAYER_SURFACE
        {
            static const AkUniqueID GROUP = 2720040714U;

            namespace SWITCH
            {
                static const AkUniqueID DIRT = 2195636714U;
                static const AkUniqueID GRASS = 4248645337U;
                static const AkUniqueID ROCK = 2144363834U;
                static const AkUniqueID WATER = 2654748154U;
            } // namespace SWITCH
        } // namespace PLAYER_SURFACE

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID DISTANCEFROMTILE = 761609972U;
        static const AkUniqueID OXYGEN = 3660512661U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAIN = 3161908922U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID _2D_WORLD = 2309860507U;
        static const AkUniqueID _3D_WORLD = 1807657452U;
        static const AkUniqueID BREATH = 1326786195U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID PLAYER = 1069431850U;
        static const AkUniqueID UI = 1551306167U;
    } // namespace BUSSES

    namespace AUX_BUSSES
    {
        static const AkUniqueID OUTDOORREVERB = 3250445679U;
    } // namespace AUX_BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
