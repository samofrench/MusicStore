using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStore.Enums
{
    public enum Audio
    {
        Mono,
        Stereo,
        ProcessedStereo
    }

    public enum Country
    {
        US,
        UK,
        France,
        Germany,
        Italy,
        Canada,
        Japan,
        Netherlands,
        Austria,
        Poland
    }

    public enum Key
    {
        CMajor,
        CMinor,
        CSharpMajor,
        CSharpMinor,
        DMajor,
        DMinor,
        EFlatMajor,
        EFlatMinor,
        EMajor,
        EMinor,
        FMajor,
        FMinor,
        FSharpMajor,
        FSharpMinor,
        GMajor,
        GMinor,
        AFlatMajor,
        AFlatMinor,
        AMajor,
        AMinor,
        BFlatMajor,
        BFlatMinor,
        BMajor,
        BMinor
    }

    public enum EnsembleType
    {
        Orchestra,
        StringQuartet,
        Choral
    }
}