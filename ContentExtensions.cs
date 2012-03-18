using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Cms.Web;
using Umbraco.Hive;
using Umbraco.Framework.Context;
using Umbraco.Framework;
using Umbraco.Framework.Persistence.Model.Constants.AttributeDefinitions;
using System.Web.Helpers;

namespace Umbraco.Web.Areas.ReadOnlyJson
{
    public static class ContentExtensions
    {
        public static string PropertiesAsJson(this Umbraco.Cms.Web.Model.Content node, bool traverseChildren = false)
        {
            return Json.Encode(propertiesAsDictionary(node, traverseChildren));
        }

        private static Dictionary<string, object> propertiesAsDictionary(Umbraco.Cms.Web.Model.Content node, bool traverseChildren = false)
        {
            var nodePropertyDictionary = new Dictionary<string, object>();

            nodePropertyDictionary.Add("Id", node.Id.ToString());
            nodePropertyDictionary.Add("Name", node.Name);
            nodePropertyDictionary.Add("Created", node.UtcCreated.UtcDateTime.ToShortDateString());

            foreach (var attribute in node.Attributes.Where(na => !na.AttributeDefinition.Alias.StartsWith("system-internal")))
            {
                var value = attribute.DynamicValue;
                nodePropertyDictionary.Add(attribute.AttributeDefinition.Alias, value);
            }

            // add id's of children if there are any
            if (node.Children().Any())
            {
                var childrenList = new List<object>();
                foreach (var child in node.Children())
                {
                    if (traverseChildren)
                        childrenList.Add(propertiesAsDictionary(child, true));
                    else
                        childrenList.Add(new { Name = child.Name, Id = child.Id.ToString() });
                }
                nodePropertyDictionary.Add("Children", childrenList);
            }

            return nodePropertyDictionary;
        }



    }
}