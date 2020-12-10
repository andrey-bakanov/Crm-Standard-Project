using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;

namespace StandardProject.Crm.Repository
{
    public interface IBaseRepository<T> where T : Entity
    {
        IOrganizationService Service { get; }

        void Associate(Guid entityId, string relationshipName, EntityReference entityReference);
        void Associate(Guid entityId, string relationshipName, List<EntityReference> entityReferences);
        Guid Create(T entity);
        void Delete(Guid entityId);
        void Disassociate(Guid entityId, string relationshipName, EntityReference entityReference);
        void Disassociate(Guid entityId, string relationshipName, List<EntityReference> entityReferences);
        List<Entity> Fetch(string fetchXml);
        T Retrieve(Guid id, ColumnSet columnSet);
        List<T> RetrieveAll(QueryExpression query);
        EntityCollection RetrieveMultiple(QueryExpression query);
        EntityCollection RetrieveMultipleFetch(FetchExpression fetch);
        void Update(T entity);
    }
}