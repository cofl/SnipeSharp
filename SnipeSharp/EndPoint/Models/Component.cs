﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using SnipeSharp.Serialization;
using static SnipeSharp.Serialization.FieldConverter;

namespace SnipeSharp.EndPoint.Models
{
    [EndPointInformation("components", "")]
    public class Component : CommonEndPointModel
    {
        [Field("id")]
        public override long Id { get; set; }

        [Field("name")]
        public override string Name { get; set; }

        [Field("image")]
        public Uri ImageUri { get; set; }

        [Field("serial")]
        public string Serial { get; set; }

        [Field("location", SerializeAs = "location_id", FieldConverter = SerializeToId)]
        public Location Location { get; set; }

        [Field("qty")]
        public int Quantity { get; set; }

        [Field("min_amt")]
        public int? MinimumQuantity { get; set; }

        [Field("category", SerializeAs = "category_id", FieldConverter = SerializeToId)]
        public Category Category { get; set; }

        [Field("order_number")]
        public string OrderNumber { get; set; }

        [Field("purchase_date", FieldConverter = ExtractDateTime)]
        public DateTime? PurchaseDate { get; set; }

        [Field("purchase_cost")]
        public decimal? PurchaseCost { get; set; }

        [Field("remaining")]
        public int? RemainingQuantity { get; set; }

        [Field("company", SerializeAs = "company_id", FieldConverter = SerializeToId)]
        public Company Company { get; set; }

        [Field("created_at", FieldConverter = ExtractDateTime)]
        public override DateTime? CreatedAt { get; set; }

        [Field("updated_at", FieldConverter = ExtractDateTime)]
        public override DateTime? UpdatedAt { get; set; }

        [Field("user_can_checkout", CanSerialize = false)]
        public bool? CanUserCheckOut { get; set; }

        [Field("available_actions")]
        public Dictionary<AvailableAction, bool> AvailableActions { get; set; }
    }

    public sealed class ComponentAsset : ApiObject
    {
        [Field("assigned_pivot_id")]
        public int ComponentId { get; set; }
        
        [Field("id")]
        public int AssetId { get; set; }
        
        [Field("name")]
        public string Name { get; set; }
        
        [Field("qty")]
        public int Quantity { get; set; }

        [Field("type")]
        public string Type { get; set; }
        
        [Field("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Field("available_actions")]
        public Dictionary<AvailableAction, bool> AvailableActions { get; set; }
    }
}