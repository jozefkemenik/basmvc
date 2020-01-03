using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Diagnostics;
using BAS.Resources;






namespace BAS.Core.Localization
{
    /// <summary>
    /// Wraps up XAML access to instance of WPFLocalize.Properties.Resources, list of available cultures, and method to change culture
    /// </summary>
    public class CultureResources
    {
        //only fetch installed cultures once
        private static bool bFoundInstalledCultures = false;

        private static IList<CultureInfo> pSupportedCultures = null;
        /// <summary>
        /// List of available cultures, enumerated at startup
        /// </summary>
        public static IList<CultureInfo> SupportedCultures
        {
            get
            {
                PopulateList();
                return pSupportedCultures;
            }
        }

        public static CultureInfo DefaultCulture
        {
           
            get
            {
                PopulateList();
                foreach (var item in pSupportedCultures)

                {
                    if (item.TextInfo.LCID==1051)
                        return item;
                    
                }
                return null;
            }
        }


        public static CultureInfo GetCultureInfoFromThreeISOLetters(string ISOletters)
        {
            var list = CultureInfo.GetCultures(CultureTypes.AllCultures);
            foreach (var item in list)
            {
                if (item.ThreeLetterISOLanguageName == ISOletters)
                    return item;
            }
            return null;
        }

        private static void PopulateList()
        {
            if (!bFoundInstalledCultures)
            {
                pSupportedCultures = new List<CultureInfo>();
                //determine which cultures are available to this application
                Debug.WriteLine("Get Installed cultures:");
                CultureInfo tCulture = new CultureInfo("");
                foreach (string dir in Directory.GetDirectories(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin")))
                {
                    try
                    {
                        //see if this directory corresponds to a valid culture name
                        DirectoryInfo dirinfo = new DirectoryInfo(dir);
                        tCulture = CultureInfo.GetCultureInfo(dirinfo.Name);

                        //determine if a resources dll exists in this directory that matches the executable name
                        if (dirinfo.GetFiles("BAS.Resources.resources.dll").Length > 0)
                        {
                            pSupportedCultures.Add(tCulture);
                            Debug.WriteLine(string.Format(" Found Culture: {0} [{1}]", tCulture.DisplayName, tCulture.Name));
                        }
                    }
                    catch (ArgumentException) //ignore exceptions generated for any unrelated directories in the bin folder
                    {
                    }
                }
                bFoundInstalledCultures = true;
            }
        }

        public CultureResources()
        {
            PopulateList();
        }

        /// <summary>
        /// Change the current culture used in the application.
        /// If the desired culture is available all localized elements are updated.
        /// </summary>
        /// <param name="culture">Culture to change to</param>
        public static void ChangeCulture(CultureInfo culture)
        {
            //remain on the current culture if the desired culture cannot be found
            // - otherwise it would revert to the default resources set, which may or may not be desired.
            if (pSupportedCultures.Contains(culture))
            {
                Resource.Culture = culture;
            }
            else
                Debug.WriteLine(string.Format("Culture [{0}] not available", culture));
        }


       

     
    }
}

