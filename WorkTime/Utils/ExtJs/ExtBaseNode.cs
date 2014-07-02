using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkTime.Utils.Items;
using WorkTime.Utils.Objects;

namespace WorkTime.Utils.ExtJs
{
    public class ExtBaseNode<TEntity>//:IExtNode<TEntity>//, IParentedItem<TEntity>
    {
        private int Id { get; set; }
        private int? ParentId { get; set; }
        private TEntity Parent { get; set; }
        public IQueryable<ExtBaseNode<TEntity>> children { get; set; }

        public static Func<TEntity, ExtBaseNode<TEntity>> EntityToNode = (item) => new ExtBaseNode<TEntity>(item)
        {
            Id = item.GetPropertyValue<int>("Id"),
            ParentId = item.GetPropertyValue<int?>("ParentId"),
            id = item.GetPropertyValue<int>("Id"),
            text = item.HasOwnProperty("Name") ? item.GetPropertyValue<string>("Name") : (item.HasOwnProperty("Title") ? item.GetPropertyValue<string>("Title") : String.Empty),
            description = item.HasOwnProperty("Description") ? item.GetPropertyValue<string>("Description") : (item.HasOwnProperty("Comment") ? item.GetPropertyValue<string>("Comment") : String.Empty),
            leaf = !(item.HasOwnProperty("HasChild") ? item.GetPropertyValue<bool>("HasChild") : (item.HasOwnProperty("Children") ? item.GetPropertyValue<IQueryable<TEntity>>("Children").Count() > 0 : false)),
            href = item.HasOwnProperty("Url") ? item.GetPropertyValue<string>("Url") : (item.HasOwnProperty("URL") ? item.GetPropertyValue<string>("URL") : (item.HasOwnProperty("Href") ? item.GetPropertyValue<string>("Href") : String.Empty)),
        };

        public static Func<TEntity, ExtBaseNode<TEntity>> EntityToTree = (item) => new ExtBaseNode<TEntity>(item, true)
        {
            Id = item.GetPropertyValue<int>("Id"),
            ParentId = item.GetPropertyValue<int?>("ParentId"),
            id = item.GetPropertyValue<int>("Id"),
            text = item.HasOwnProperty("Name") ? item.GetPropertyValue<string>("Name") : (item.HasOwnProperty("Title") ? item.GetPropertyValue<string>("Title") : String.Empty),
            description = item.HasOwnProperty("Description") ? item.GetPropertyValue<string>("Description") : (item.HasOwnProperty("Comment") ? item.GetPropertyValue<string>("Comment") : String.Empty),
            leaf = !(item.HasOwnProperty("HasChild") ? item.GetPropertyValue<bool>("HasChild") : (item.HasOwnProperty("Children") ? item.GetPropertyValue<IQueryable<TEntity>>("Children").Count() > 0 : false)),
            href = item.HasOwnProperty("Url") ? item.GetPropertyValue<string>("Url") : (item.HasOwnProperty("URL") ? item.GetPropertyValue<string>("URL") : (item.HasOwnProperty("Href") ? item.GetPropertyValue<string>("Href") : String.Empty)),
        };


        public int id { get; set; }
        public string text { get; set; }
        public string description { get; set; }
        public bool leaf { get; set; }
        public string href { get; set; }
        public List<IItem> subitems { get; set; }
        public ExtBaseNode() { }
        public ExtBaseNode(TEntity Item, bool IncludeChidren = false)
        {
            if (IncludeChidren)
            {
                if (Item.HasOwnProperty<IQueryable<TEntity>>("Chidren"))
                    this.children = Item.GetPropertyValue<IQueryable<TEntity>>("Chidren").Select(x => EntityToTree(x));
            }
        }
    }
}