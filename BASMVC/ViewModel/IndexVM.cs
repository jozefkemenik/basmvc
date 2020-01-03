using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BASMVC.ViewModel
{
    public class IndexVM
    {
        public IndexVM()
        {
            NewVMCollection = new List<IndexVM>();
            EventVMCollection = new List<IndexVM>();
        }
        public ICollection<IndexVM> NewVMCollection { get; set; }

        public ICollection<IndexVM> EventVMCollection { get; set; }

    
            public int Id { get; set; }

            public string Location { get; set; }
            public string Text { get; set; }
            public string Title { get; set; }
            public System.DateTime IssueDate { get; set; }

            public int? AlbumId { get; set; }
            public bool HasAlbum
            {
                get
                {
                    return AlbumId != null;
                }
            }

            public string FilePath { get; set; }
            public string LinkToDetail { get; set; }

        public string IssueDateMonthShortText
        {
            get
            {

                var textMonth = "";
                switch (IssueDate.Month)
                {
                    case 1:
                        textMonth = "Jan";
                        break;
                    case 2:
                        textMonth = "Feb";
                        break;
                    case 3:
                        textMonth = "Mar";
                        break;
                    case 4:
                        textMonth = "Apr";
                        break;
                    case 5:
                        textMonth = "Máj";
                        break;
                    case 6:
                        textMonth = "Jún";
                        break;
                    case 7:
                        textMonth = "Júl";
                        break;
                    case 8:
                        textMonth = "Aug";
                        break;
                    case 9:
                        textMonth = "Sep";
                        break;
                    case 10:
                        textMonth = "Okt";
                        break;
                    case 11:
                        textMonth = "Nov";
                        break;
                    case 12:
                        textMonth = "Dec";
                        break;

                }
                return textMonth;
            }
        }


        public string IssueDateTimeText
        {
            get
            {
                return @String.Format("{0:00}:{1:00}", IssueDate.Hour, IssueDate.Minute);
            }
        }

        public string IssueDateText
        {
            get
            {
                return string.Format("{0:00}. {1:00}. {2:0000}", IssueDate.Day, IssueDate.Month, IssueDate.Year);
            }
        }


    }
}