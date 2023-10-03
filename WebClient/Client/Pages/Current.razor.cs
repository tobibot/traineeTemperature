using global::System;
using global::System.Collections.Generic;
using global::System.Linq;
using global::System.Threading.Tasks;
using global::Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using WebClient.Client;
using WebClient.Client.Shared;

namespace WebClient.Client.Pages
{
    public partial class Current
    {
        private double currentTemperature = 3.14;
        private DateTime lastRefresh;
        private Timer timer;
        private static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri(GetBaseUrl()),
        };


        protected override void OnInitialized()
        {            
            timer = new Timer(RefreshTimerTick, null, 0, 60_000);
            GetCurrentTemperature();

            base.OnInitialized();
        }

        private void RefreshTimerTick(object? state) 
        {
            GetCurrentTemperature();
        }

        private void GetCurrentTemperature()
        {          

            using HttpResponseMessage response = sharedClient.GetAsync("currenttemperature").GetAwaiter().GetResult();

            response.EnsureSuccessStatusCode();

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            currentTemperature = double.Parse(jsonResponse.Replace(".", ","));
            lastRefresh = DateTime.Now;
        }

        private static string GetBaseUrl()
        {
            // ToDo: if debug
            return "https://currenttemperatureapi.azurewebsites.net";
        }
    }
}