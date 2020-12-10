using Microsoft.Xrm.Sdk;

namespace StandardProject.Crm.Extensions
{
    /// <summary>
    /// Extensions for Entity
    /// </summary>
    public static class EntityExtensions
    {
        /// <summary>
        /// Combine two entities
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="first">first entity, will combine all values from second</param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static T CombineFrom<T>(this T first, T second) where T : Entity, new()
        {
            T newEntity = new T();
            newEntity.Id = first.Id;

            foreach (var attr in first.Attributes.Keys)
            {
                newEntity[attr] = first[attr];
            }

            if (second != null)
            {
                foreach (var attr in second.Attributes.Keys)
                {
                    newEntity[attr] = second[attr];
                }
            }

            return newEntity;
        }


        public static T CombineTo<T>(this T second, T first) where T : Entity, new()
        {
            if (first == null)
                return second.CombineFrom(null);

            return first.CombineFrom(second);
        }

        public static T GetAliasedAttributeValue<T>(this Entity entity, string attributeName)
        {
            if (entity == null)
                return default(T);

            AliasedValue fieldAliasValue = entity.GetAttributeValue<AliasedValue>(attributeName);

            if (fieldAliasValue == null)
                return default(T);

            if (fieldAliasValue.Value != null && fieldAliasValue.Value.GetType() == typeof(T))
            {
                return (T)fieldAliasValue.Value;
            }

            return default(T);
        }

    }
}
