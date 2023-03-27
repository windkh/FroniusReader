
namespace FroniusReader.Model
{
    using System.Globalization;
    using System.Linq;
    using OxyPlot;
    using OxyPlot.Axes;
    using System.Collections.Generic;
    using Helper;
    using System;
    using System.Threading.Tasks;
    using DataTypes;
    using Newtonsoft.Json.Linq;
    using RestSharp;

    public class FroniusModel : IFroniusModel
    {
        #region Constants

        private const string CHANNEL_DC_CURRENT_1 = "Current_DC_String_1";

        private const string CHANNEL_DC_CURRENT_2 = "Current_DC_String_2";

        private const string CHANNEL_DC_VOLTAGE_1 = "Voltage_DC_String_1";

        private const string CHANNEL_DC_VOLTAGE_2 = "Voltage_DC_String_2";

        private const string CHANNEL_ENERGY_PRODUCED = "EnergyReal_WAC_Sum_Produced";

        private const string CHANNEL_ENERGY_CONSUMED = "EnergyReal_WAC_Sum_Consumed";

        private const string CHANNEL_TEMPERATURE_POWERSTAGE = "Temperature_Powerstage";

        private const string CHANNEL_POWER_REAL_PAC_SUM = "PowerReal_PAC_Sum";

        private const string CHANNEL_DC_POWER_1 = "Power_DC_String_1";

        private const string CHANNEL_DC_POWER_2 = "Power_DC_String_2";

        #endregion

        #region Fields

        private RestClient _restClient;
        private string _address;
        private readonly IRestResponse _notFoundResponse = new NotFoundResponse();

        #endregion

        #region Constructor

        public FroniusModel()
        {
        }

        #endregion

        #region Public Properties

        public string Address
        {
            get
            {
                return _address;
            }
        }

        #endregion

        #region Public Methods

        public ApiVersion Connect(string address)
        {
            string url = "http://" + address + "/solar_api/";
            RestClient restClient = new RestClient(url);
            
            ApiVersion apiVersion = null;
            try
            {
                apiVersion = GetApiVersion(restClient);
            }
            catch (Exception)
            {
                // did not work
            }

            if (apiVersion != null)
            {
                string versionedUrl = "http://" + address + apiVersion.BaseURL;
                _restClient = new RestClient(versionedUrl);
                _address = address;
            }
            else
            {
                _restClient = null;
                // error address can not be used.
            }

            return apiVersion;
        }

        public async Task<SmartMeterRealTimeData> GetSmartMeterRealtimeDataAsync()
        {
            SmartMeterRealTimeData realTimeData = null;

            IRestResponse response = await GetRestApiAsync(_restClient, "GetMeterRealtimeData.cgi?Scope=System");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                JObject parsedObject = JObject.Parse(response.Content);
                JToken data = parsedObject.SelectToken("Body.Data.0");
                if (data != null)
                {
                    realTimeData = data.ToObject<SmartMeterRealTimeData>();
                }
            }

            return realTimeData;
        }

        public async Task<InverterRealTimeData> GetInverterRealtimeDataAsync()
        {
            InverterRealTimeData realTimeData = null;

            IRestResponse response = await GetRestApiAsync(_restClient, "GetInverterRealtimeData.cgi?Scope=Device&DeviceId=1&DataCollection=CommonInverterData");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                JObject parsedObject = JObject.Parse(response.Content);
                JToken data = parsedObject.SelectToken("Body.Data");
                if (data != null)
                {
                    realTimeData = data.ToObject<InverterRealTimeData>();
                }
            }

            return realTimeData;
        }

        public async Task<ArchiveData> GetArchiveDataAsync(
            DateTime startDate,
            DateTime endDate,
            bool getChannelCurrentDCString1,
            bool getChannelCurrentDCString2,
            bool getChannelVoltageDCString1,
            bool getChannelVoltageDCString2,
            bool getChannelPowerDCString1,
            bool getChannelPowerDCString2,
            bool getChannelEnergyRealWACSumProduced,
            bool getChannelEnergyRealWACSumConsumed,
            bool getChannelTemperaturePowerstage,
            bool getChannelPowerRealSum
            )
        {
            ArchiveData archiveData = null;

            string startDateString = FroniusDateTimeConverter.ToFroniusDateTimeString(startDate);
            string endDateString = FroniusDateTimeConverter.ToFroniusDateTimeString(endDate);

            string restString = "GetArchiveData.cgi?Scope=System&StartDate=" + startDateString + "&EndDate=" + endDateString;

            if (getChannelCurrentDCString1 || getChannelPowerDCString1)
            {
                restString += "&Channel=" + CHANNEL_DC_CURRENT_1;
            }

            if (getChannelCurrentDCString2 || getChannelPowerDCString2)
            {
                restString += "&Channel=" + CHANNEL_DC_CURRENT_2;
            }

            if (getChannelVoltageDCString1 || getChannelPowerDCString1)
            {
                restString += "&Channel=" + CHANNEL_DC_VOLTAGE_1;
            }

            if (getChannelVoltageDCString2 || getChannelPowerDCString2)
            {
                restString += "&Channel=" + CHANNEL_DC_VOLTAGE_2;
            }

            if (getChannelEnergyRealWACSumProduced)
            {
                restString += "&Channel=" + CHANNEL_ENERGY_PRODUCED;
            }

            if (getChannelEnergyRealWACSumConsumed)
            {
                restString += "&Channel=" + CHANNEL_ENERGY_CONSUMED;
            }

            if (getChannelTemperaturePowerstage)
            {
                restString += "&Channel=" + CHANNEL_TEMPERATURE_POWERSTAGE;
            }

            if (getChannelPowerRealSum)
            {
                restString += "&Channel=" + CHANNEL_POWER_REAL_PAC_SUM;
            }

            IRestResponse response = await GetRestApiAsync(_restClient, restString);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                JObject parsedObject = JObject.Parse(response.Content);
                JToken data = parsedObject.SelectToken("Body.Data.inverter/1.Data");
                if (data != null)
                {
                    List<Channel> channels = new List<Channel>();

                    if (getChannelCurrentDCString1)
                    {
                        TryAddChannel(CHANNEL_DC_CURRENT_1, data, channels, startDate);
                    }

                    if (getChannelCurrentDCString2)
                    {
                        TryAddChannel(CHANNEL_DC_CURRENT_2, data, channels, startDate);
                    }

                    if (getChannelVoltageDCString1)
                    {
                        TryAddChannel(CHANNEL_DC_VOLTAGE_1, data, channels, startDate);
                    }

                    if (getChannelVoltageDCString2)
                    {
                        TryAddChannel(CHANNEL_DC_VOLTAGE_2, data, channels, startDate);
                    }

                    if (getChannelEnergyRealWACSumProduced)
                    {
                        TryAddChannel(CHANNEL_ENERGY_PRODUCED, data, channels, startDate);
                    }

                    if (getChannelEnergyRealWACSumConsumed)
                    {
                        TryAddChannel(CHANNEL_ENERGY_CONSUMED, data, channels, startDate);
                    }

                    if (getChannelTemperaturePowerstage)
                    {
                        TryAddChannel(CHANNEL_TEMPERATURE_POWERSTAGE, data, channels, startDate);
                    }

                    if (getChannelPowerRealSum)
                    {
                        TryAddChannel(CHANNEL_POWER_REAL_PAC_SUM, data, channels, startDate);
                    }

                    if (getChannelPowerDCString1)
                    {
                        TryAddChannel(CHANNEL_DC_VOLTAGE_1, CHANNEL_DC_CURRENT_1, CHANNEL_DC_POWER_1, "W", data,
                            channels, startDate);
                    }

                    if (getChannelPowerDCString2)
                    {
                        TryAddChannel(CHANNEL_DC_VOLTAGE_2, CHANNEL_DC_CURRENT_2, CHANNEL_DC_POWER_2, "W", data,
                            channels, startDate);
                    }

                    archiveData = new ArchiveData(channels);
                }
            }

            return archiveData;
        }

        #endregion

        #region Private Methods

        private ApiVersion GetApiVersion(RestClient restClient)
        {
            ApiVersion apiVersion = null;
            IRestResponse response = GetRestApiAsync(restClient, "GetAPIVersion.cgi").Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                JObject parsedObject = JObject.Parse(response.Content);
                apiVersion = parsedObject.ToObject<ApiVersion>();
            }

            return apiVersion;
        }

        private Task<IRestResponse> GetRestApiAsync(RestClient restClient, string restCall)
        {
            Task<IRestResponse> task = new Task<IRestResponse>(() =>
            {
                IRestResponse response;
                if (restClient != null)
                {
                    RestRequest request = new RestRequest(restCall, Method.GET)
                    {
                        RequestFormat = DataFormat.Json,
                        OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; }
                    };

                    response = restClient.Execute(request);
                }
                else
                {
                    response = _notFoundResponse;
                }

                return response;
            });

            task.Start(TaskScheduler.Default);
            return task;
        }

        private void TryAddChannel(string channelName1, string channelName2, string name, string unit, JToken data, List<Channel> channels, DateTime startDate)
        {
            Channel channel1 = TryGetChannel(channelName1, data, startDate);
            if (channel1 != null)
            {
                Channel channel2 = TryGetChannel(channelName2, data, startDate);
                if (channel2 != null)
                {
                    IReadOnlyList<DataPoint> points = CreateProduct(channel1, channel2);
                    Channel channel = new Channel(name, unit, points);
                    channels.Add(channel);
                }
            }
        }

        private IReadOnlyList<DataPoint> CreateProduct(Channel channel1, Channel channel2)
        {
            List<DataPoint> points = new List<DataPoint>();
            int count1 = channel1.Points.Count;
            int count2 = channel2.Points.Count;
            if (count1 == count2)
            {
                for (int i = 0; i < count1; i++)
                {
                    DataPoint point1 = channel1.Points[i];
                    DataPoint point2 = channel2.Points[i];

                    if (point1.X == point2.X)
                    {
                        DataPoint point = new DataPoint(point1.X, point1.Y * point2.Y);
                        points.Add(point);
                    }
                    else
                    {
                        // does not match, product would be wrong
                    }
                }
            }

            return points;
        }

        private void TryAddChannel(string channelName, JToken data, List<Channel> channels, DateTime startDate)
        {
            Channel channel = TryGetChannel(channelName, data, startDate);
            if (channel != null)
            {
                channels.Add(channel);
            }
        }

        private Channel TryGetChannel(string channelName, JToken data, DateTime startDate)
        {
            Channel channel;

            JToken selectedUnit = data.SelectToken(channelName + ".Unit");
            if (selectedUnit != null)
            {
                string channelUnit = selectedUnit.Value<string>();

                JToken selectedValues = data.SelectToken(channelName + ".Values");

                List<DataPoint> points = new List<DataPoint>();
                foreach (JToken token in selectedValues)
                {
                    JProperty property = (JProperty)token;
                    double xValue = double.Parse(property.Name, CultureInfo.InvariantCulture); // seconds since start
                    DateTime xDateTime = startDate + TimeSpan.FromSeconds(xValue);
                    double x = DateTimeAxis.ToDouble(xDateTime);
                    double y = property.Value.Value<double>();
                    DataPoint point = new DataPoint(x, y);
                    points.Add(point);
                }

                points = points.OrderBy(p => p.X).ToList();
                channel = new Channel(channelName, channelUnit, points);
            }
            else
            {
                channel = null;
            }

            return channel;
        }

        #endregion
    }
}
