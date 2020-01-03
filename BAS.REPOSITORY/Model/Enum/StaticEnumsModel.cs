
using BAS.Repository.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BAS.Repository.Model
{
    // Days of Week
    [LocalizationEnum]
    public enum WeekDay
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }


    //Months
    [LocalizationEnum]
    public enum Months
    {
        January,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }

    [LocalizationEnum]
    public enum eGender
    {
        Male = 0,
        Female = 1
    }


    [LocalizationEnum]
    public enum eProfileType
    {
        Public = 0,
        Private = 1
    }


    [LocalizationEnum]
    public enum eTransactionType
    {
        Buy = 0,
        Sell = 1
    }

    [LocalizationEnum]
    public enum eStatusType
    {
        New = 0,
        AfterNew = 1,
        Used = 2,
        Bad =3

    }




}