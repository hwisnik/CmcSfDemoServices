using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DataAccess.SFRepositories
{
    public class TomTomDistanceApi
    {
        //private static readonly int _useTraffic = 1;
        // private static readonly int _useTollroads = 1;

        public static async Task<string> CalculateRoute(double startLat, double startLong, double endLat, double endLong, DateTime appointmentDateTime)
        {

            var startDateTime = appointmentDateTime.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'");
            
            //Sample request  Paste into browser for testing
            //https://csv.webfleet.com/extern?account=services-245&username=ERMS&password=Cmcenergy2&lang=en&action=calcRouteSimpleExtern&use_traffic=0&outputformat=json&start_latitude=39953968&start_longitude=-75169546&end_latitude=39911687&end_longitude=-75252347&start_day=tue&start_time=13:30:00&use_traffic=1

            //var uri = "https://csv.telematics.tomtom.com/";   //deprecated
            var uri = "https://csv.webfleet.com/";
            uri += "extern?account=services-245&username=ERMS&password=Cmcenergy2&lang=en&action=calcRouteSimpleExtern&use_traffic=0";
            uri += "&outputformat=json";
            //uri += "&route_type=" + TomTomRouteTypes.Quickest;
            //uri += "&use_traffic=" + _useTraffic;
            //uri += "&start_datetime=" + DateTime.Now;
            //uri += "&use_tollroads=" + _useTollroads;
            uri += "&start_latitude=" + Convert.ToInt64(startLat * 1e6);
            uri += "&start_longitude=" + Convert.ToInt64(startLong * 1e6);
            uri += "&end_latitude=" + Convert.ToInt64(endLat * 1e6);
            uri += "&end_longitude=" + Convert.ToInt64(endLong * 1e6);
            uri += "&start_day=" + appointmentDateTime.ToString("ddd").ToLower();
            uri += "&start_time=" + appointmentDateTime.ToString("HH:mm:ss");
            uri += "&use_traffic=" + 1;

            //uri += "&columnfilter=distance,time";


            var hc = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            var responseMessage = await hc.GetAsync(uri);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Driving Time Calculation Error:{responseMessage.Content}");
            }
            var timedist = await responseMessage.Content.ReadAsStringAsync();
            return timedist;

        }


        //private static class TomTomRouteTypes
        //{
        //    public const int Quickest = 0;
        //    public const int Shortest = 1;
        //    public const int AvoidMotorway = 2;
        //    public const int Walk = 3;
        //    public const int Bicycle = 4;

        //}


        //public class TomTomOutput
        //{
        //    public List<TomTomResponse> Property1 { get; set; }
        //}

        [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
        public class TomTomResponse
        {
            // ReSharper disable InconsistentNaming
#pragma warning disable IDE1006 // Naming Styles
            public string end_day { get; set; } //day of week
            public string end_time { get; set; }    //arrival time of day
            public int distance { get; set; } //in meters
            public string time { get; set; }   //in seconds
            public int delay { get; set; }
            public int timezone_offset { get; set; }
            // ReSharper restore InconsistentNaming
#pragma warning restore IDE1006 // Naming Styles

        }

    }
}


