using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using StandardProject.Crm.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StandardProject.Crm.Repository.Implementation
{

    /// <summary>
    /// Base repository
    /// </summary>
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        /// <summary>
        /// Organization service
        /// </summary>
        public IOrganizationService Service { get; }


        private readonly string _logicalName;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        protected BaseRepository(IOrganizationService service)
        {
            _logicalName = nameof(T).ToLowerInvariant();

            Service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// Create Entity
        /// </summary>
        /// <param name="entity">Entity for create</param>
        /// <returns>Guid of created entity in crm</returns>
        public Guid Create(T entity)
        {
            return Service.Create(entity);
        }

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="entity">Entity for update</param>
        public void Update(T entity)
        {
            Service.Update(entity);
        }

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="entityId">Guid of entity</param>
        public void Delete(Guid entityId)
        {
            Service.Delete(_logicalName, entityId);
        }

        /// <summary>
        /// Retrieve Entity
        /// </summary>
        /// <param name="id">Guid of entity</param>
        /// <param name="columnSet">Fields in entity for retrieve</param>
        /// <returns>Entity with fields and with id <param name="id"></param></returns>
        public T Retrieve(Guid id, ColumnSet columnSet)
        {
            return Service.Retrieve(_logicalName, id, columnSet).ToEntity<T>();
        }


        /// <summary>
        /// Retrieve multiple entities
        /// </summary>
        /// <param name="query">Query expression for retrieve multiple</param>
        /// <returns>List of entity from CRM find by query</returns>
        public EntityCollection RetrieveMultiple(QueryExpression query)
        {
            return Service.RetrieveMultiple(query);
        }

        /// <summary>
        /// Get all entities over 5000 count
        /// </summary>
        /// <typeparam name="T">Type of entity, example <see cref="Opportunity"/></typeparam>
        /// <param name="query">Query expression for search records</param>
        /// <returns>List of entity from CRM find by query</returns>
        public List<T> RetrieveAll(QueryExpression query)
        {
            return Service.RetrieveAll<T>(query);
        }

        /// <summary>
        /// Execute fetch xml and return result
        /// </summary>
        /// <param name="fetchXml">Fetch expression</param>
        /// <returns>List of entity from CRM find by fetch</returns>
        public List<Entity> Fetch(string fetchXml)
        {
            var fetchExpression = new FetchExpression(fetchXml);
            var fetchResult = RetrieveMultipleFetch(fetchExpression);
            return fetchResult.Entities.ToList();
        }

        /// <summary>
        /// Retrieve multiple entities
        /// </summary>
        /// <param name="fetch">Fetch expression for retrieve multiple</param>
        /// <returns>List of entity from CRM find by fetch</returns>
        public EntityCollection RetrieveMultipleFetch(FetchExpression fetch)
        {
            return Service.RetrieveMultiple(fetch);
        }

        /// <summary>
        /// Create link from entities by relationship
        /// </summary>
        /// <param name="entityId">Primary entity id</param>
        /// <param name="relationshipName">Relationship name</param>
        /// <param name="entityReferences">List of entity references for create link</param>
        public void Associate(Guid entityId, string relationshipName,
            List<EntityReference> entityReferences)
        {

            Service.Associate(_logicalName, entityId, new Relationship(relationshipName),
                new EntityReferenceCollection(entityReferences));
        }


        /// <summary>
        /// Create link from entities by relationship
        /// </summary>
        /// <param name="entityId">Primary entity id</param>
        /// <param name="relationshipName">Relationship name</param>
        /// <param name="entityReference">Entity references for create link</param>
        public void Associate(Guid entityId, string relationshipName,
            EntityReference entityReference)
        {
            Associate(entityId, relationshipName, new List<EntityReference>
            {
                entityReference
            });
        }

        /// <summary>
        /// Remove link from entities by relationship
        /// </summary>
        /// <param name="entityId">Primary entity id</param>
        /// <param name="relationshipName">Relationship name</param>
        /// <param name="entityReferences">List of entity references for remove</param>
        public void Disassociate(Guid entityId, string relationshipName,
            List<EntityReference> entityReferences)
        {

            Service.Disassociate(_logicalName, entityId, new Relationship(relationshipName),
                new EntityReferenceCollection(entityReferences));
        }

        /// <summary>
        /// Remove link from entities by relationship
        /// </summary>
        /// <param name="entityId">Primary entity id</param>
        /// <param name="relationshipName">Relationship name</param>
        /// <param name="entityReference">Entity reference for remove</param>
        public void Disassociate(Guid entityId, string relationshipName,
            EntityReference entityReference)
        {
            Disassociate(entityId, relationshipName, new List<EntityReference>
            {
                entityReference
            });
        }


        /// <summary>
        /// Create a query for specific entity
        /// </summary>
        /// <param name="columns">columns to retrieve</param>
        /// <returns>created QueryExpression instance</returns>
        protected QueryExpression CreateQuery(ColumnSet columns)
        {
            QueryExpression query = new QueryExpression(_logicalName);
            query.ColumnSet = columns;
            query.NoLock = true;

            return query;
        }
    }
}
