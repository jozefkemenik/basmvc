using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.Repository.Model
{

    [MetadataType(typeof(EventMetadata))]
    public partial class Event_t
    {

        DateTimeMetadata _issueDateMetadata;
        public DateTimeMetadata IssueDateMetadata
        {
            get
            {
                if (_issueDateMetadata == null)
                    _issueDateMetadata = new DateTimeMetadata() { DT = IssueDate };
                return _issueDateMetadata;
            }
            set
            {
                _issueDateMetadata = value;
            }
        }


        public void UpdateIssueDateByTime()
        {

            if (_issueDateMetadata != null)
            {
                IssueDate = new DateTime(IssueDate.Year,
                    IssueDate.Month,
                    IssueDate.Day,
                    _issueDateMetadata.DT.Hour,
                    _issueDateMetadata.DT.Minute, 0);
            }
        }
    }


    public class EventMetadata
    {
        [Required]
        public string Location { get; set; }

        [Required]
        public System.DateTime IssueDate { get; set; }


        [Required]
        public string Title { get; set; }
    }
}
