using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace StandardProject.Crm.Extensions
{
    /// <summary>
    /// Extension for IOrganizationService
    /// </summary>
    public static class OrganizationServiceExtensions
    {

        /// <summary>
        /// User properties
        /// </summary>
        public sealed class UserInfo
        {

            /// <summary>
            /// User identifier
            /// </summary>
            public Guid UserId
            {
                get;
                set;
            }

            /// <summary>
            /// Businessunit identifier
            /// </summary>
            public Guid BusinessUnitId
            {
                get;
                set;
            }

            /// <summary>
            /// Organization identifier
            /// </summary>
            public Guid OrganizationId
            {
                get;
                set;
            }
        }

        /// <summary>
        /// return Entity with all fields
        /// </summary>
        /// <param name="crmService">Service</param>
        /// <param name="entityTypeName">Entity logical name</param>
        /// <param name="entityId">Entity id</param>
        /// <returns></returns>
        public static Entity Retrieve(
                this IOrganizationService crmService,
                string entityTypeName,
                Guid entityId)
        {
            return crmService.Retrieve(entityTypeName, entityId, new ColumnSet(true));
        }


        /// <summary>
        /// return Entity with all fields
        /// </summary>
        /// <param name="crmService">Service</param>
        /// <param name="entityRef">Entity reference</param>
        /// <returns></returns>
        public static Entity Retrieve(
                this IOrganizationService crmService,
                EntityReference entityRef)
        {
            return crmService.Retrieve(entityRef.LogicalName, entityRef.Id, new ColumnSet(true));
        }



        /// <summary>
        /// returns Entity with all fields
        /// </summary>
        /// <param name="crmService">Service</param>
        /// <param name="entityTypeName">Entity logical name</param>
        /// <param name="entityId">Entity id</param>
        /// <param name="attrs">attributes to retrieve</param>
        /// <returns></returns>
        public static Entity Retrieve(
            this IOrganizationService crmService,
            string entityTypeName,
            Guid entityId,
            params string[] attrs)
        {
            if (attrs.Length == 0)
            {
                return crmService.Retrieve(entityTypeName, entityId, new ColumnSet(true));
            }
            else
            {
                ColumnSet set = new ColumnSet(attrs);
                return crmService.Retrieve(entityTypeName, entityId, set);
            }
        }


        /// <summary>
        /// Returns current user info
        /// </summary>
        /// <param name="crmService"></param>
        /// <returns></returns>
        public static UserInfo CurrentUserInfo(this IOrganizationService crmService)
        {
            WhoAmIRequest userRequest = new WhoAmIRequest();

            WhoAmIResponse user =
                (WhoAmIResponse)crmService.Execute(userRequest);

            return new UserInfo { BusinessUnitId = user.BusinessUnitId, OrganizationId = user.OrganizationId, UserId = user.UserId };
        }



        /// <summary>
        /// Returns titles and values for OptionSet(PicklistAttribute)
        /// </summary>
        /// <param name="service">Organization service</param>
        /// <param name="entityName">entity logical name</param>
        /// <param name="attrName">attribute logical name</param>
        /// <returns></returns>
        public static IDictionary<int, string> GetPicklistValues(this IOrganizationService service, string entityName, string attrName)
        {

            Dictionary<int, string> lic = new Dictionary<int, string>();

            RetrieveAttributeRequest attributeRequest = new RetrieveAttributeRequest();
            attributeRequest.EntityLogicalName = entityName;
            attributeRequest.LogicalName = attrName;
            var attributeResponse = (RetrieveAttributeResponse)service.Execute(attributeRequest);
            var picklist = (PicklistAttributeMetadata)attributeResponse.AttributeMetadata;

            for (int i = 0; i < picklist.OptionSet.Options.Count; i++)
            {
                lic.Add(picklist.OptionSet.Options[i].Value.Value, picklist.OptionSet.Options[i].Label.LocalizedLabels[0].Label);
            }

            return lic;
        }

        /// <summary>
        /// Returns titles and values for OptionSet(StatusAttribute)
        /// </summary>
        /// <param name="service">Organization service</param>
        /// <param name="entityName">entity logical name</param>
        /// <param name="attrName">attribute logical name</param>
        /// <returns></returns>
        public static IDictionary<int, string> GetStatusValues(this IOrganizationService service, string entityName, string attrName)
        {

            Dictionary<int, string> lic = new Dictionary<int, string>();

            RetrieveAttributeRequest attributeRequest = new RetrieveAttributeRequest();
            attributeRequest.EntityLogicalName = entityName;
            attributeRequest.LogicalName = attrName;
            var attributeResponse = (RetrieveAttributeResponse)service.Execute(attributeRequest);
            var statuslist = (StatusAttributeMetadata)attributeResponse.AttributeMetadata;

            for (int i = 0; i < statuslist.OptionSet.Options.Count; i++)
            {
                lic.Add(statuslist.OptionSet.Options[i].Value.Value, statuslist.OptionSet.Options[i].Label.LocalizedLabels[0].Label);
            }

            return lic;
        }

        /// <summary>
        /// Returns titles and values for OptionSet(StateAttribute)
        /// </summary>
        /// <param name="service">Organization service</param>
        /// <param name="entityName">entity logical name</param>
        /// <param name="attrName">attribute logical name</param>
        /// <returns></returns>
        public static IDictionary<int, string> GetStateValues(this IOrganizationService service, string entityName, string attrName)
        {

            Dictionary<int, string> lic = new Dictionary<int, string>();

            RetrieveAttributeRequest attributeRequest = new RetrieveAttributeRequest();
            attributeRequest.EntityLogicalName = entityName;
            attributeRequest.LogicalName = attrName;
            var attributeResponse = (RetrieveAttributeResponse)service.Execute(attributeRequest);
            var statelist = (StateAttributeMetadata)attributeResponse.AttributeMetadata;

            for (int i = 0; i < statelist.OptionSet.Options.Count; i++)
            {
                var value = statelist.OptionSet.Options[i].Value;
                if (value != null && !lic.ContainsKey(key: value.Value))
                    if (!lic.ContainsKey(statelist.OptionSet.Options[i].Value.Value))
                        lic.Add(statelist.OptionSet.Options[i].Value.Value, statelist.OptionSet.Options[i].Label.LocalizedLabels[0].Label);
            }

            return lic;
        }

        /// <summary>
        /// Returns titles and values for OptionSet(PicklistAttribute)
        /// </summary>
        /// <param name="service">Organization service</param>
        /// <param name="entityName">entity logical name</param>
        /// <param name="attrName">attribute logical name</param>
        /// <param name="userLang">user language code</param>
        /// <returns></returns>
        public static IDictionary<int, string> GetPicklistValues(this IOrganizationService service, string entityName, string attrName, int userLang)
        {

            Dictionary<int, string> lic = new Dictionary<int, string>();

            RetrieveAttributeRequest attributeRequest = new RetrieveAttributeRequest();
            attributeRequest.EntityLogicalName = entityName;
            attributeRequest.LogicalName = attrName;
            var attributeResponse = (RetrieveAttributeResponse)service.Execute(attributeRequest);
            var picklist = (PicklistAttributeMetadata)attributeResponse.AttributeMetadata;

            for (int i = 0; i < picklist.OptionSet.Options.Count; i++)
            {
                string value = null;
                foreach (LocalizedLabel label in picklist.OptionSet.Options[i].Label.LocalizedLabels)
                {
                    if (label.LanguageCode == userLang)
                    {
                        value = label.Label;
                    }
                }

                lic.Add(picklist.OptionSet.Options[i].Value.Value, value);
            }

            return lic;
        }

        /// <summary>
        /// Returns titles and values for OptionSet(StatusAttribute)
        /// </summary>
        /// <param name="service">Organization service</param>
        /// <param name="entityName">entity logical name</param>
        /// <param name="attrName">attribute logical name</param>
        /// <param name="userLang">user language code</param>
        /// <returns></returns>
        public static IDictionary<int, string> GetStatusValues(this IOrganizationService service, string entityName, string attrName, int userLang)
        {

            Dictionary<int, string> lic = new Dictionary<int, string>();

            RetrieveAttributeRequest attributeRequest = new RetrieveAttributeRequest();
            attributeRequest.EntityLogicalName = entityName;
            attributeRequest.LogicalName = attrName;
            var attributeResponse = (RetrieveAttributeResponse)service.Execute(attributeRequest);
            var statuslist = (StatusAttributeMetadata)attributeResponse.AttributeMetadata;

            for (int i = 0; i < statuslist.OptionSet.Options.Count; i++)
            {
                string value = null;
                foreach (LocalizedLabel label in statuslist.OptionSet.Options[i].Label.LocalizedLabels)
                {
                    if (label.LanguageCode == userLang)
                    {
                        value = label.Label;
                    }
                }

                lic.Add(statuslist.OptionSet.Options[i].Value.Value, value);
            }

            return lic;
        }

        /// <summary>
        /// Returns titles and values for OptionSet(StateAttribute)
        /// </summary>
        /// <param name="service">Organization service</param>
        /// <param name="entityName">entity logical name</param>
        /// <param name="attrName">attribute logical name</param>
        /// <param name="userLang">user language code</param>
        /// <returns></returns>
        public static IDictionary<int, string> GetStateValues(this IOrganizationService service, string entityName, string attrName, int userLang)
        {

            Dictionary<int, string> lic = new Dictionary<int, string>();

            RetrieveAttributeRequest attributeRequest = new RetrieveAttributeRequest();
            attributeRequest.EntityLogicalName = entityName;
            attributeRequest.LogicalName = attrName;
            var attributeResponse = (RetrieveAttributeResponse)service.Execute(attributeRequest);
            var statelist = (StateAttributeMetadata)attributeResponse.AttributeMetadata;

            for (int i = 0; i < statelist.OptionSet.Options.Count; i++)
            {
                string value = null;
                foreach (LocalizedLabel label in statelist.OptionSet.Options[i].Label.LocalizedLabels)
                {
                    if (label.LanguageCode == userLang)
                    {
                        value = label.Label;
                    }
                }

                lic.Add(statelist.OptionSet.Options[i].Value.Value, value);
            }

            return lic;
        }

        /// <summary>
        /// Gets titles and values for any kind of OptionSet
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entityName">entity logical name</param>
        /// <param name="attrName">attribute logical name</param>
        /// <param name="userLang">user language code</param>
        /// <returns></returns>
        public static IDictionary<int, string> GetEnumTypeValues(this IOrganizationService service, string entityName, string attrName, int userLang)
        {

            RetrieveAttributeRequest attributeRequest = new RetrieveAttributeRequest();
            attributeRequest.EntityLogicalName = entityName;
            attributeRequest.LogicalName = attrName;
            RetrieveAttributeResponse attributeResponse = (RetrieveAttributeResponse)service.Execute(attributeRequest);


            if (attributeResponse.AttributeMetadata.AttributeType == AttributeTypeCode.State)
            {
                return GetStateValues(service, entityName, attrName, userLang);
            }
            if (attributeResponse.AttributeMetadata.AttributeType == AttributeTypeCode.Status)
            {
                return GetStatusValues(service, entityName, attrName, userLang);
            }
            else
            {
                return GetPicklistValues(service, entityName, attrName, userLang);
            }
        }

        /// <summary>
        /// Returns current user language code
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static int GetUserLanguagecode(this IOrganizationService service)
        {
            UserInfo user = CurrentUserInfo(service);
            RetrieveUserSettingsSystemUserRequest userSettingreq = new RetrieveUserSettingsSystemUserRequest();
            userSettingreq.EntityId = user.UserId;
            userSettingreq.ColumnSet = new ColumnSet(new string[] { "uilanguageid" });

            RetrieveUserSettingsSystemUserResponse userSettingres = (RetrieveUserSettingsSystemUserResponse)service.Execute(userSettingreq);

            return Convert.ToInt32(userSettingres.Entity.Attributes["uilanguageid"], CultureInfo.InvariantCulture);
        }


        /// <summary>
        /// Returns content of webresource
        /// </summary>
        /// <param name="service">Service</param>
        /// <param name="sourceName">webresource logical name</param>
        /// <returns>content as string</returns>
        public static string GetWebResourceContent(this IOrganizationService service, string sourceName)
        {
            // определение веб ресурса по имени
            var query = new QueryExpression
            {
                EntityName = "webresource",
                ColumnSet = new ColumnSet("content"),
                Criteria = new FilterExpression(LogicalOperator.And)
                {
                    Conditions =
                                {
                                    new ConditionExpression("name", ConditionOperator.Equal, sourceName)
                                }
                }
            };
            //Получаем веб ресурс
            var webResources = service.RetrieveMultiple(query);

            if (webResources.Entities.Count > 0)
            {
                Entity webResource = webResources[0];
                byte[] binary = Convert.FromBase64String(webResource.GetAttributeValue<string>("content"));
                return Encoding.UTF8.GetString(binary);
            }

            return String.Empty;
        }

        /// <summary>
        /// Returns the result of a query by collecting the results 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static List<T> RetrieveAll<T>(this IOrganizationService service,
            QueryExpression query) where T : Entity
        {
            query.PageInfo = new PagingInfo
            {
                PageNumber = 1,
                PagingCookie = null
            };

            query.NoLock = true;
            var result = new List<T>();

            while (true)
            {
                var queryResult = service.RetrieveMultiple(query);

                if (queryResult.Entities.Count == 0)
                {
                    break;
                }

                result.AddRange(queryResult.Entities.Select(e => e.ToEntity<T>()));

                if (!queryResult.MoreRecords)
                {
                    break;
                }

                query.PageInfo.PageNumber++;
                query.PageInfo.PagingCookie = queryResult.PagingCookie;
            }

            return result;
        }
    }
}
