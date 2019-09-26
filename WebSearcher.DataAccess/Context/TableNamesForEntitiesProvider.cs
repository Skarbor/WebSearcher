using System;
using System.Collections.Generic;
using WebSearcher.Entities;

namespace WebSearcher.DataAccess.Context
{
    public class TableNamesForEntitiesProvider
    {
        private readonly Dictionary<Type, string> _entityTypeToTableNameDictionary = new Dictionary<Type, string>();

        private void Initialize()
        {
            _entityTypeToTableNameDictionary.Add(typeof(WebPage), "WebPages");
            _entityTypeToTableNameDictionary.Add(typeof(WebPageConnection), "WebPageConnection");
        }

        public TableNamesForEntitiesProvider()
        {
            Initialize();
        }

        public string GetTableNameForEntityType(Type entityType)
        {
            return _entityTypeToTableNameDictionary[entityType];
        }
    }
}
