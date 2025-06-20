using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.DirectoryServices;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;
using System.DirectoryServices.AccountManagement;

namespace SACI.Negocio
{
    public class GetADUsers
    {
        string dominio = string.Empty;
        StringCollection groupids = new StringCollection();



        public bool GetSessionUserAD(string Ususario)
        {
            try
            {
                dominio = getDomainName();
                DirectoryEntry de = GetDirectoryEntry();
                DirectorySearcher deSearch = new DirectorySearcher();
                deSearch.SearchRoot = de;
                deSearch.Filter = "(SAMAccountName=" + Ususario + ")";
                deSearch.PropertiesToLoad.Add("cn");
                SearchResultCollection results = deSearch.FindAll();

                if (results.Count == 0)
                {
                    return false;

                }
                else
                {
                    return true;
                }

            }
            catch (Exception)
            {

                return false;
            }
        }








        public List<UserDetail> SearchUsers(string text)
        {
            if (String.IsNullOrWhiteSpace(text)) return new List<UserDetail>();
            using (var context = new PrincipalContext(ContextType.Domain))
            {
                //var User = HttpContext.Current.User;
                //var principal = UserPrincipal.FindByIdentity(context, User.Identity.Name);              
                var ctx = new PrincipalContext(ContextType.Domain, "CORP", "DC=res,DC=corp,DC=com");
                UserPrincipal userPrin = new UserPrincipal(ctx);

                if (text.Contains(","))
                    userPrin.DisplayName = text + "*";
                else
                    userPrin.Name = text + "*";
                //userPrin.GivenName //userPrin.Surname
                var searcher = new System.DirectoryServices.AccountManagement.PrincipalSearcher();
                searcher.QueryFilter = userPrin;
                var results = searcher.FindAll();
                if (results.Count() == 0)
                {
                    userPrin.Name = "*";
                    userPrin.DisplayName = "*";
                    userPrin.SamAccountName = text + "*";
                    results = searcher.FindAll();
                }
                if (results.Count() == 0)
                {
                    userPrin.Name = "*";
                    userPrin.DisplayName = "*";
                    userPrin.SamAccountName = "*";
                    userPrin.Surname = text + "*";
                    results = searcher.FindAll();
                }
                if (results.Count() == 0)
                {
                    userPrin.Name = "*";
                    userPrin.DisplayName = "*";
                    userPrin.SamAccountName = "*";
                    userPrin.Surname = "*";
                    userPrin.GivenName = text + "*";
                    results = searcher.FindAll();
                }
                //  UserPrincipal tt = (UserPrincipal) results.Skip(1).FirstOrDefault();
                // var ty = tt.GetType();

                var listUsers = results.Take(20).Select(e => (UserPrincipal)e).Select(e => new UserDetail()
                {

                    // DisplayName = LOVConstants.DisplayName(e.GivenName, e.Surname),

                    FirstName = e.GivenName,
                    LastName = e.Surname,
                    Email = e.UserPrincipalName,
                    LoginId = e.SamAccountName
                }).ToList();
                ctx.Dispose();

                return listUsers;
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ususario"></param>
        /// <returns></returns>
        public List<string> GetUserAD()
        {
            List<string> Users = new List<string>();
            List<string> Properties = new List<string>();
            try
            {
                dominio = getDomainName();
                DirectoryEntry de = GetDirectoryEntry();

                DirectorySearcher deSearch = new DirectorySearcher();
                deSearch.SearchRoot = de;
                deSearch.Filter = "(objectClass=user)";
                //deSearch.Filter = "(&(objectCategory=Person)(sAMAccountName=*)(|(memberOf=cn=CORP,ou=users,DC=res,DC=hbi,DC=net)))";
                deSearch.PropertiesToLoad.Add("cn");
                SearchResultCollection results = deSearch.FindAll();
                // Recorremos toda la lista de grupos devueltos
                foreach (SearchResult sr in results)
                {
                    String sUsuario = (String)sr.Properties["samaccountname"][0];
                    // A partir de aquí hacer lo que corresponda con cada grupo
                    Users.Add(sUsuario);
                }

                return Users;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ususario"></param>
        /// <returns></returns>
        public DataTable GetUsersProperties(string Ususario)
        {
            List<string> Properties = new List<string>();
            DataTable dt = new DataTable();
            try
            {
                dominio = getDomainName();
                DirectoryEntry de = GetDirectoryEntry();

                DirectorySearcher deSearch = new DirectorySearcher();
                deSearch.SearchRoot = de;
                deSearch.Filter = "(SAMAccountName=" + Ususario + ")";
                deSearch.PropertiesToLoad.Add("cn");
                SearchResultCollection results = deSearch.FindAll();
                dt = getUserLDAPProperties(de.Path, Ususario);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<string> GetAllADRoles(string Filter)
        {
            IList<string> list = new List<string>();
            using (var context = new PrincipalContext(ContextType.Domain, "CORP", "DC=res,DC=corp,DC=com"))
            {
                // define a "query-by-example" principal - here, we search for a GroupPrincipal 
                GroupPrincipal qbeGroup = new GroupPrincipal(context);

                // create your principal searcher passing in the QBE principal    
                PrincipalSearcher srch = new PrincipalSearcher(qbeGroup);

                // find all matches

                foreach (var found in srch.FindAll())
                {
                    if (!string.IsNullOrWhiteSpace(found.Name))
                    {
                        if (found.Name.StartsWith(Filter))
                        {
                            list.Add(found.Name);
                        }
                    }

                }
                if (list != null)
                {
                    return list.ToList();
                }
            }
            return null;
        }



        public List<string> GetAllADUsaer()
        {
            IList<string> list = new List<string>();
            using (var context = new PrincipalContext(ContextType.Domain))
            {
                // define a "query-by-example" principal - here, we search for a GroupPrincipal 

                var ctx = new PrincipalContext(ContextType.Domain, "CORP", "DC=res,DC=corp,DC=com");
                UserPrincipal userPrin = new UserPrincipal(ctx);
                // create your principal searcher passing in the QBE principal    


                var searcher = new System.DirectoryServices.AccountManagement.PrincipalSearcher();
                searcher.QueryFilter = userPrin;
                var results = searcher.FindAll();

                foreach (var found in results)
                {
                    list.Add(found.SamAccountName);

                }
                if (list != null)
                {
                    return list.ToList();
                }
            }
            return null;
        }



        public static DataTable getUserLDAPProperties(string LDAPURL, string Usuario)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Col1", typeof(string));
            dt.Columns.Add("Col2", typeof(string));

            DirectoryEntry entries = new DirectoryEntry(LDAPURL);
            DirectorySearcher searcher = new DirectorySearcher(
                entries, "(&(objectCategory=person)(objectClass=user))");
            searcher.Filter = "(SAMAccountName=" + Usuario + ")";
            try
            {
                foreach (SearchResult result in searcher.FindAll())
                {
                    foreach (string property in result.GetDirectoryEntry().Properties.PropertyNames)
                    {
                        if (result.Properties[property].Count > 0) //properties.Add(result.Properties[property][0].ToString());
                        {
                            dt.Rows.Add(property, result.Properties[property][0].ToString());
                        }
                    }
                    break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        public List<string> GetGroupAD(string Ususario)
        {
            List<string> Gpo = new List<string>();
            try
            {
                dominio = getDomainName();
                DirectoryEntry de = GetDirectoryEntry();

                DirectorySearcher deSearch = new DirectorySearcher();
                deSearch.SearchRoot = de;
                deSearch.Filter = "(SAMAccountName=" + Ususario + ")";
                deSearch.PropertiesToLoad.Add("memberOf");
                SearchResultCollection results = deSearch.FindAll();


                SearchResult res = deSearch.FindOne();
                DirectoryEntry usuario = res.GetDirectoryEntry();

                usuario.RefreshCache(new string[] { "tokenGroups" });

                StringBuilder sids = new StringBuilder();
                sids.Append("(|");
                foreach (byte[] sid in usuario.Properties["tokenGroups"])
                {
                    sids.Append("(objectSid=");
                    for (int indice = 0; indice < sid.Length; indice++)
                    {
                        sids.AppendFormat("\\{0}", sid[indice].ToString("X2"));
                    }
                    sids.AppendFormat(")");
                }
                sids.Append(")");



                ///fin

                // Creamos un objeto DirectorySearcher con el filtro antes generado y buscamos todas la coincidencias
                DirectorySearcher ds = new DirectorySearcher(de, sids.ToString());
                SearchResultCollection src = ds.FindAll();

                // Recorremos toda la lista de grupos devueltos
                foreach (SearchResult sr in src)
                {
                    String sGrupo = (String)sr.Properties["samAccountName"][0];
                    // A partir de aquí hacer lo que corresponda con cada grupo
                    Gpo.Add(sGrupo);
                }

                return Gpo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<String> Groups()
        {
            List<String> groups = new List<String>();
            foreach (System.Security.Principal.IdentityReference group in
                System.Web.HttpContext.Current.Request.LogonUserIdentity.Groups)
            {
                groups.Add(group.Translate(typeof
                    (System.Security.Principal.NTAccount)).ToString());
            }
            return groups;
        }



        public string GetUsersPrimaryGroup(string samAccountName)
        {
            try
            {
                DirectoryEntry de = GetDirectoryEntry();
                DirectorySearcher srch = default(DirectorySearcher);
                SearchResult userResult = srch.FindOne();
                DirectoryEntry user = new DirectoryEntry(userResult.Path);
                byte[] userSid = user.Properties["objectSid"][0] as byte[];
                user.RefreshCache(new string[] { "primaryGroupId" });
                int primaryGroupID = (int)user.Properties["primaryGroupId"][0];
                byte[] rid = BitConverter.GetBytes(primaryGroupID);
                for (int i = 0; i < rid.Length; i++)
                {
                    userSid.SetValue(rid[i], new long[] { userSid.Length - (rid.Length - i) });
                }
                //We do not want to dispose untill we have the group name, which is why we assign instead of return
                string primaryGroupName = de.Properties["sAMAccountName"][0].ToString();
                return primaryGroupName;

            }
            catch (Exception ex)
            {
                //throw to catch in calling method (we want the details/can trace better)
                throw ex;
            }

        }


        public List<string> obtenergrupo(string user)
        {
            List<string> Gpo = new List<string>();
            DirectoryEntry de = GetDirectoryEntry();
            DirectoryEntry objADAM = default(DirectoryEntry);
            DirectoryEntry objGroupEntry = default(DirectoryEntry);
            DirectorySearcher objSearchADAM = default(DirectorySearcher);
            SearchResultCollection objSearchResults = default(SearchResultCollection);
            SearchResult myResult = null;

            objADAM = new DirectoryEntry(de.Path);

            objADAM.RefreshCache();
            objSearchADAM = new DirectorySearcher(objADAM);
            objSearchADAM.Filter = "(&(objectClass=group))";
            objSearchADAM.SearchScope = SearchScope.Subtree;
            objSearchResults = objSearchADAM.FindAll();

            // Enumerate groups 
            try
            {
                if (objSearchResults.Count != 0)
                {
                    foreach (SearchResult objResult in objSearchResults)
                    {
                        myResult = objResult;
                        objGroupEntry = objResult.GetDirectoryEntry();
                        String sGrupo = objGroupEntry.Name;
                        // A partir de aquí hacer lo que corresponda con cada grupo
                        Gpo.Add(sGrupo);
                    }
                }
                else
                {
                    throw new Exception("No groups found");
                }
                return Gpo;
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }




        /// <summary>
        /// METODO QUE OBTIENE EL NOMBRE DEL DOMINIO
        /// </summary>
        /// <returns></returns>
        public string getDomainName()
        {
            return IPGlobalProperties.GetIPGlobalProperties().DomainName;
        }



        /// <summary>
        /// OBTIENE EL DIRECTORIO DEL ACTIVE DIRECTORY
        /// </summary>        
        public string getLDAPDomainName(string domainName)
        {
            StringBuilder sb = new StringBuilder();
            string[] dcItems = domainName.Split(".".ToCharArray());
            sb.Append("LDAP://");
            foreach (string item in dcItems)
            {
                sb.AppendFormat("DC={0},", item);
            }
            return sb.ToString().Substring(0, sb.ToString().Length - 1);
        }


        public DirectoryEntry GetDirectoryEntry()
        {
            DirectoryEntry de = new DirectoryEntry();
            de.Path = getLDAPDomainName(dominio);
            de.AuthenticationType = AuthenticationTypes.Secure;
            return de;
        }



        public List<UserDetail> GetUsersInGroup(string group)
        {
            List<UserDetail> users = new List<UserDetail>();
            dominio = getDomainName();
            List<string> groupMemebers = new List<string>();

            DirectoryEntry de = GetDirectoryEntry();
            DirectorySearcher ds = new DirectorySearcher(de, "(objectClass=person)");
            ds.SearchRoot = de;
            ds.Filter = "(&(objectClass=group)(cn=" + group + "))";
            foreach (SearchResult result in ds.FindAll())
            {
                var dir = result.GetDirectoryEntry();
                var list = dir.Invoke("Members");
                IEnumerable entries = (IEnumerable)list;
                foreach (var entry in entries)
                {
                    DirectoryEntry member = new DirectoryEntry(entry);
                    if (member.SchemaClassName == "group")
                    {
                        List<UserDetail> usersInGroup =
                            GetUsersInGroup(member.Properties["name"][0].ToString());
                        foreach (UserDetail aduser in usersInGroup)
                        {
                            if (!users.ToDictionary(u => u.FirstName).ContainsKey(aduser.FirstName))
                            {
                                users.Add(aduser);
                            }
                        }
                    }
                }
            }
            return users;
        }




    }
}
