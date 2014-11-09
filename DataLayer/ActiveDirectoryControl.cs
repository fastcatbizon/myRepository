using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;

namespace ExpansesControlSystem.DataLayer
{
    public class ActiveDirectoryControl
    {
        readonly DirectoryEntry _entry = new DirectoryEntry("LDAP://DomainName");
        private DirectorySearcher dSearch;

        public ActiveDirectoryControl(String name)
        {
            dSearch = new DirectorySearcher(_entry);
             dSearch.Filter = "(&(objectClass=user)(l=" + name + "))";
        }

        public string GetManager()
        {
            foreach (SearchResult sResultSet in dSearch.FindAll())
            {
                return GetProperty(sResultSet, "extensionAttribute15");
            }
            return null;
        }

        private  string GetProperty(SearchResult searchResult,string propertyName)
        {
            if (searchResult.Properties.Contains(propertyName))
            {
                return searchResult.Properties[propertyName][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }


    }
}