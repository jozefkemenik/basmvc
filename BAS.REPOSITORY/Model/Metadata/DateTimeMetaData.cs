using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.Repository.Model
{
    public class DateTimeMetadata
    {
        List<int> _hours =  new List<int>();
        List<int> _minutes = new List<int>();
        List<int> _days = new List<int>();
        List<int> _months = new List<int>();
        List<int> _years = new List<int>();
        public DateTimeMetadata()
        {
   
            for (int i =0 ; i <= 23; i++)
            {
                _hours.Add(i);
            }

           
            for (int i = 0; i <= 59; i++)
            {
                _minutes.Add(i);

            }

            for (int i = 1; i <= 31; i++)
            {
                _days.Add(i);
            }

            for (int i = 1; i <= 12; i++)
            {
                _months.Add(i);

            }

                _years.Add(DateTime.Now.Year);
                _years.Add(DateTime.Now.Year+1);
        }

        [Required]
        public int? Hour { get; set; }
        [Required]
        public int? Minute { get; set; }
        [Required]
        public int? Day { get; set; }
        [Required]
        public int? Month { get; set; }
        [Required]
        public int? Year { get; set; }


        public DateTime DT {

            get {
                var now  = DateTime.Now;
                return new DateTime(Year.HasValue? Year.Value: now.Year,
                                    Month.HasValue ? Month.Value: now.Month, 
                                    Day.HasValue ? Day.Value : now.Day, 
                                    Hour.HasValue ? Hour.Value: 0, 
                                    Minute.HasValue ? Minute.Value: 0,
                                    0); }
            set {
                if (value != null)
                {
                    Year = value.Year;
                    Month = value.Month;
                    Day = value.Day;
                    Hour = value.Hour;
                    Minute = value.Minute;
                }
            }

        }

        public List<int> Hours { get { return _hours; } }
        public List<int> Minutes { get { return _minutes; } }
        public List<int> Days { get { return _days; } }
        public List<int> Months { get { return _months; } }
        public List<int> Years { get { return _years; } }

    }
}
