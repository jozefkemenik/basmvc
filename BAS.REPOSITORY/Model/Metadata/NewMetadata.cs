using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BAS.Repository.Model
{

    [MetadataType(typeof(NewMetadata))]
    public partial class New_t
    {
        public ICollection<int?> AlbumIds { get; set; }
        DateTimeMetadata _issueDateMetadata;
        public DateTimeMetadata IssueDateMetadata
        {
            get
            {
                if (_issueDateMetadata==null)
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

    public class NewMetadata
    {

        [Required]
        public string Location { get; set; }

        [Required]
        public System.DateTime IssueDate { get; set; }

        [Required]
        public Nullable<int> AlbumId { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string Title { get; set; }
    }


}
