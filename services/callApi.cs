using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using project.model;
using System.Text.Json;

namespace project.services
{
    public class callApi
    {
        private readonly HttpClient _httpclient = new HttpClient();
        private readonly string _url = "https://gist.githubusercontent.com/christianpanton/10d65ccef9f29de3acd49d97ed423736/raw/b09563bc0c4b318132c7a738e679d4f984ef0048/kings";

        public async Task<List<englandKings>> GetData()
        {
            var data = new List<englandKings>();
            try
            {
                var response = await _httpclient.GetStreamAsync(_url);
                var refactoreddata = await JsonSerializer.DeserializeAsync<List<Kings>>(response);
                var sharedmonarchs = new Monarchs();
                var sharedhouses = new Dictionary<string, int>();
                var sharednames = new Dictionary<string, int>();
                foreach (var rd in refactoreddata)
                {
                    var myking = new englandKings(rd.id, rd.nm, rd.cty, rd.yrs, rd.hse, sharedhouses, sharednames);
                    if (myking.totalyear > sharedmonarchs.years)
                    {
                        sharedmonarchs.years = myking.totalyear;
                        sharedmonarchs.name = myking.name;
                    }
                    myking.monarchs = sharedmonarchs;
                    if (!sharedhouses.ContainsKey(myking.house))
                    {
                        sharedhouses.Add(myking.house, 0);
                    }
                    sharedhouses[myking.house] += myking.totalyear;
                    if (!sharednames.ContainsKey(myking.fname))
                    {
                        sharednames.Add(myking.fname, 0);
                    }
                    sharednames[myking.fname]++;
                    data.Add(myking);
                }
                return data;

            }
            catch (Exception)
            {
                return data;
            }


        }

    }
    
    public class Options
    {


        public Dictionary<int, string> OptionsList = new Dictionary<int, string>
            {
                { 1, "How many monarchs are there in the list?" },
                { 2, "Which monarch ruled the longest (and for how long)? "},
                { 3, "Which house ruled the longest (and for how long)? "},
                { 4, "What was the most common first name? "},
            };

        public string handleOptions(int optionkey, List<englandKings> eg)
        {
            try
            {
switch (optionkey)
            {
                case 1:
                    return $"The number of monarchs are: {eg.Count}";
                    break;
                case 2:
                    return $"{eg[0].monarchs.name} ruled longest and for {eg[0].monarchs.years} year(s)";
                    break;
                case 3:
                    if (eg[0].Houses != null) { }
                    int maxrule = 0;
                    string house = "";
                    foreach (var h in eg[0].Houses)
                    {
                        if (h.Value > maxrule)
                        {
                            maxrule = h.Value;
                            house = h.Key;
                        }

                    }
                    return $"The {house} ruled longest for {maxrule} year(s)";
                    break;
                case 4:
                    var maxname = eg[0].Names.Aggregate((l, r) => l.Value > r.Value ? l : r);
                    return $"The most common first name is {maxname.Key}";
                    break;
                default:
                    return "Will add other options once i am officially a staff";
                    break;

            }
            }
            catch (Exception ex)
            {
                return "Unable to Process the Data Try Again Later.";
            }
            
        }
        
         
            
    }
}