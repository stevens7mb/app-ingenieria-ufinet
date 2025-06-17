using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace app_ingenieria_ufinet.Data;

public partial class Municipalityshape
{
    public int OgrFid { get; set; }

    public Geometry? OgrGeometry { get; set; }

    public string? Gid2 { get; set; }

    public string? Gid0 { get; set; }

    public string? Country { get; set; }

    public string? Gid1 { get; set; }

    public string? Name1 { get; set; }

    public string? NlName1 { get; set; }

    public string? Name2 { get; set; }

    public string? Varname2 { get; set; }

    public string? NlName2 { get; set; }

    public string? Type2 { get; set; }

    public string? Engtype2 { get; set; }

    public string? Cc2 { get; set; }

    public string? Hasc2 { get; set; }

    public int? IdMunicipality { get; set; }
}
