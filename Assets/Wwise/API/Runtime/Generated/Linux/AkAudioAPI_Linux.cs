##if UNITY_EDITOR || (UNITY_STANDALONE_LINUX && !UNITY_EDITOR)
//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public enum AkAudioAPI {
  AkAPI_PulseAudio = 1 << 0,
  AkAPI_ALSA = 1 << 1,
  AkAPI_Default = AkAPI_PulseAudio|AkAPI_ALSA
}
#endif // ##if UNITY_EDITOR || (UNITY_STANDALONE_LINUX && !UNITY_EDITOR)