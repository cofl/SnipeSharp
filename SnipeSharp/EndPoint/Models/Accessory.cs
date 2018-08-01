﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using SnipeSharp.Serialization;
using static SnipeSharp.Serialization.FieldConverter;

namespace SnipeSharp.EndPoint.Models
{
    [EndPointInformation("accessories", "")]
    public class Accessory : CommonEndPointModel
    {
        [Field("id")]
        public override long Id { get; set; }

        [Field("name")]
        public override string Name { get; set; }

        [Field("company", "company_id", converter: CommonModelConverter)]
        public Company Company { get; set; }

        [Field("manufacturer", "manufacturer_id", converter: CommonModelConverter)]
        public Manufacturer Manufacturer { get; set; }

        [Field("supplier", "supplier_id", converter: CommonModelConverter)]
        public Supplier Supplier { get; set; }

        [Field("model_number")]
        public string ModelNumber { get; set; }

        [Field("category", "category_id", converter: CommonModelConverter)]
        public Category Category { get; set; }

        [Field("location", "location_id", converter: CommonModelConverter)]
        public Location Location { get; set; }

        [Field("qty")]
        public int Quantity { get; set; }

        [Field("purchase_date", converter: DateTimeConverter)]
        public DateTime? PurchaseDate { get; set; }

        [Field("purchase_cost")]
        public decimal? PurchaseCost { get; set; }

        [Field("order_number")]
        public string OrderNumber { get; set; }

        [Field("min_qty")]
        public int? MinimumQuantity { get; set; }

        [Field("remaining_qty")]
        public int? RemainingQuantity { get; set; }

        [Field("image")]
        public Uri ImageUri { get; set; }

        [Field("created_at", converter: DateTimeConverter)]
        public override DateTime? CreatedAt { get; set; }

        [Field("updated_at", converter: DateTimeConverter)]
        public override DateTime? UpdatedAt { get; set; }

        [Field("available_actions")]
        public Dictionary<AvailableAction, bool> AvailableActions { get; set; }

        [Field("user_can_checkout")]
        public bool? CanUserCheckOut { get; set; }
    }

    public sealed class AccessoryCheckOut : ApiObject
    {
        [Field("assigned_pivot_id")]
        public int AccessoryId { get; set; }
        
        [Field("id")]
        public int UserId { get; set; }
        
        [Field("username")]
        public string UserName { get; set; }
        
        [Field("name")]
        public string Name { get; set; }
        
        [Field("first_name")]
        public string FirstName { get; set; }
        
        [Field("last_name")]
        public string LastName { get; set; }
        
        [Field("employee_number")]
        public string EmployeeNumber { get; set; }
        
        [Field("type")]
        public string Type { get; set; }

        [Field("available_actions")]
        public Dictionary<AvailableAction, bool> AvailableActions { get; set; }
    }
}