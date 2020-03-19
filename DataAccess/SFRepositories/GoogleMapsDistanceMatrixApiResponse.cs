using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming

namespace DataAccess.SFRepositories
{
#pragma warning disable IDE1006 // Naming Styles

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class GoogleMapsDistanceMatrixApiResponse
    {
        public string[] destination_addresses { get; set; }
        public string[] origin_addresses { get; set; }
        public Row[] rows { get; set; }
        public string status { get; set; }
        public string error_message { get; set; }
    }

    public class Row
    {
        public Element[] elements { get; set; }
    }

    public class Element
    {
        public Distance distance { get; set; }
        public Duration duration { get; set; }
        public Duration_In_Traffic duration_in_traffic { get; set; }
        public string status { get; set; }
    }

    public class Distance
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class Duration
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class Duration_In_Traffic
    {
        public string text { get; set; }
        public int value { get; set; }
    }
#pragma warning restore IDE1006 // Naming Styles


}
