using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project.model
{
    public class englandKings
    {

        public Dictionary<string, int>? Houses;

        public Dictionary<string, int>? Names;
        public Monarchs monarchs { get; set; }
        public int id { get; set; }
        public string? name { get; set; }
        public string? fname { get; set; }
        public string? city { get; set; }
        public int startyear { get; set; }
        public int endyear { get; set; }
        public int totalyear { get; set; }

        public string? house { get; set; }

        public englandKings(int id, string nm, string cty, string yrs, string hse, Dictionary<string, int> rulingHouses, Dictionary<string, int> commonNames)
        {

            try
            {

                this.id = id;
                this.name = nm;
                this.city = cty;
                this.house = hse;
                this.Houses = rulingHouses;
                this.Names = commonNames;
                this.fname = nm.Split(' ')[0];
                //calcualte the years
                if (yrs != null)
                {
                    if (yrs.Contains("-"))
                    {
                        string[] parts = yrs.Split("-");
                        this.startyear = int.Parse(parts[0]);
                        if (int.TryParse(parts[1], out int endyearcorrect))
                        {
                            this.endyear = endyearcorrect;
                        }
                        else
                        {
                            this.endyear = DateTime.Now.Year;
                        }
                        this.totalyear = this.endyear - this.startyear;
                    }
                    else
                    {
                        this.startyear = int.Parse(yrs);
                        this.endyear = this.startyear;
                        this.totalyear = 1;
                    }
                }

                //handle question 1
            }
            catch (Exception)
            {
                Console.WriteLine("An unexpected error occurred.");
            }


        }

    }

    public class Monarchs
    {
        public string? name { get; set; }
        public int years { get; set; }
    }
    
    
    public class Kings
    {
        public int id { get; set; }
        public string? nm { get; set; }
        public string? cty { get; set; }
        public string? hse { get; set; }
        public string? yrs { get; set; }
    }

}