using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Infrastructure.Interfaces;
using Timesheet.Infrastructure.Models;

namespace Timesheet.Data
{
    public class TimesheetService : ITimesheetService
    {
        public bool ConfirmDate(string userEmail, DateTime timeEntryDate, string confirmingEmail)
        {
            HttpClient client = new HttpClient();

            var stringContent = new StringContent(string.Empty);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ACROWIRE", "AppId=3AD1CD99-D245-4B6A-A409-70E1DCA236B0");

            HttpResponseMessage response = client.PostAsync(new Uri(string.Format("http://timesheetapi.test.acrowire.com/api/team/v1/confirmdate?userEmail={0}&timeEntryDate={1}&confirmingEmail={2}", userEmail, timeEntryDate, confirmingEmail)), stringContent).Result;
            if (response.IsSuccessStatusCode)
            {
                string serviceResponse = response.Content.ReadAsStringAsync().Result;
                var jsonResponse = JsonConvert.DeserializeObject<bool>(serviceResponse);
                return jsonResponse;
            }

            return false;
        }

        public IEnumerable<TimesheetDailySummary> GetDailySummary(string email, DateTime startDate, DateTime endDate)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ACROWIRE", "AppId=3AD1CD99-D245-4B6A-A409-70E1DCA236B0");

            HttpResponseMessage response = client.GetAsync(new Uri(string.Format("http://timesheetapi.test.acrowire.com/api/team/v1/timesheet/{0}/timesheets?startDate={1}&endDate={2}", email, startDate, endDate))).Result;
            if (response.IsSuccessStatusCode)
            {
                string serviceResponse = response.Content.ReadAsStringAsync().Result;
                var jsonResponse = JsonConvert.DeserializeObject<IEnumerable<TimesheetDailySummary>>(serviceResponse);
                return jsonResponse;
            }

            var fakeList = new List<TimesheetDailySummary>();
            fakeList.Add(new TimesheetDailySummary
            {
                BillableHours = 1,
                AverageLatency = 1,
                DateTime = DateTime.Now,
                IsConfirmed = false,
                Email = "fakeEmail@fake.com",
                Hours = 5,
                IsException = false,
                TotalLatency = 5,
                EntiresCount = 5
            });

            return fakeList;
        }

        public IEnumerable<TimesheetData> GetTimesheetData(string email, DateTime startDate, DateTime endDate)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ACROWIRE", "AppId=3AD1CD99-D245-4B6A-A409-70E1DCA236B0");

            HttpResponseMessage response = client.GetAsync(new Uri(string.Format("http://timesheetapi.test.acrowire.com/api/team/v1/timesheet/{0}/timesheets?startDate={1}&endDate={2}", email, startDate, endDate))).Result;
            if (response.IsSuccessStatusCode)
            {
                string serviceResponse = response.Content.ReadAsStringAsync().Result;
                var jsonResponse = JsonConvert.DeserializeObject<IEnumerable<TimesheetData>>(serviceResponse);
                return jsonResponse;
            }

            var fakeList = new List<TimesheetData>();
            fakeList.Add(new TimesheetData
            {
                PmpId = 1,
                Company = "Truextend",
                ConfirmDateTime = DateTime.Now,
                IsConfirmed = false,
                Email = "fakeEmail@fake.com",
                Hours = 5,
                TicketTitle = "FakeTicketTitle",
                Project = "PRISM",
                IsBillable = true
            });

            return fakeList;
        }
    }
}
