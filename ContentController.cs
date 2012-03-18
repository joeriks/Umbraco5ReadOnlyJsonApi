using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Cms.Web;
using Umbraco.Hive;
using Umbraco.Framework.Context;
using Umbraco.Framework;
using Umbraco.Framework.Persistence.Model.Constants.AttributeDefinitions;

namespace Umbraco.Web.Areas.ReadOnlyJson
{
    /// <summary>
    /// Returns all properties as Json for any Node by Hive Id + it's childrens Hive Id's
    /// usage: /ReadOnlyJson/content/byid/{hiveid}
    /// </summary>
    /// 
    public class ContentController : Controller
    {
        public IHiveManager Hive { get; set; }

        public ContentController(IHiveManager hiveManager)
        {
            Hive = hiveManager;
        }

        public ActionResult ById(string id)
        {
            // get node by id (you can find the id of a particular node in the Umbraco UI, properties tab)
            var node = Hive.Cms().Content.GetById(id);
            if (node != null)
            {
                return Content(node.PropertiesAsJson(false));
            }
            else
                return null;
        }
        public ActionResult TreeById(string id)
        {
            // get node by id (you can find the id of a particular node in the Umbraco UI, properties tab)
            var node = Hive.Cms().Content.GetById(id);
            if (node != null)
            {
                return Content(node.PropertiesAsJson(true));
            }
            else
                return null;
        }
    }
}
