using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Security.Principal;
using System.ServiceModel.Description;

namespace StandardProject.Scheduler.Tasks._Core
{
    public static class OrganizationServiceFactory
    {
        public static IOrganizationService CreateCrmService()
        {
            NameValueCollection section = (NameValueCollection)ConfigurationManager.GetSection("crm");
            try
            {
                ClientCredentials credentials = new ClientCredentials();
                // On-Premise, non-IFD аутентификация
                if (!string.IsNullOrEmpty(section["UserName"]))
                {
                    credentials.Windows.AllowNtlm = false;
                    credentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Impersonation;
                    credentials.Windows.ClientCredential = new System.Net.NetworkCredential(section["UserName"],
                                                                                            section["UserName"],
                                                                                            section["Domain"]);
                }
                else
                {
                    credentials.Windows.ClientCredential = CredentialCache.DefaultNetworkCredentials;
                }

                Uri url = new Uri(section["Service"]);
                return new OrganizationServiceProxy(url, null, credentials, null);

            }
            catch (Exception exc)
            {
                throw new ApplicationException("Ошибка инициализации сервиса CRM " + exc.ToString());
            }
        }
    }
}
