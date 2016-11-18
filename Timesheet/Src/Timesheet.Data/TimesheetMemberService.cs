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
    public class TimesheetMemberService : ITimesheetMemberService
    {
        public IEnumerable<TeamMember> GetActiveMembers()
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ACROWIRE", "AppId=3AD1CD99-D245-4B6A-A409-70E1DCA236B0");

            HttpResponseMessage response = client.GetAsync(new Uri("http://timesheetapi.test.acrowire.com/api/team/v1/members/active")).Result;
            if (response.IsSuccessStatusCode)
            {
                string serviceResponse = response.Content.ReadAsStringAsync().Result;
                var jsonResponse = JsonConvert.DeserializeObject<IEnumerable<TeamMember>>(serviceResponse);
                return jsonResponse;
            }

            var fakeList = new List<TeamMember>();
            fakeList.Add(new TeamMember
            {
                PmpId = 1,
                Email = "weimar.coro@truextend.com",
                FullName = "Weimar Ariel Coro Coronado",
                Company = "Truextend",
                PublicId = Guid.NewGuid()
            });

            return fakeList;
        }

        public TeamMember GetMemberByEmail()
        {
            throw new NotImplementedException();
        }
    }
}
