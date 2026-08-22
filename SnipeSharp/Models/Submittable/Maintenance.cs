using SnipeSharp.EndPoint;
using SnipeSharp.Models.Enumerations;
using SnipeSharp.Serialization;
using System;
using System.Runtime.Serialization;

namespace SnipeSharp.Models
{
    /// <summary>
    /// A Maintenance.
    /// Maintenances are operations performed on Assets, such as repair or reimaging.
    /// </summary>
    [PathSegment("maintenances")]
    public sealed class Maintenance : AbstractBaseModel, IAvailableActions, IPatchable
    {
        /// <summary>Create a new Maintenance object.</summary>
        public Maintenance() { }

        /// <summary>Create a new Maintenance object with the supplied ID, for use with updating.</summary>
        internal Maintenance(int id)
        {
            Id = id;
        }

        /// <value>Gets/sets asset this maintenance was performed on.</value>
        /// <remarks>
        /// <para>This field is required.</para>
        /// </remarks>
        [DeserializeAs("asset")]
        [SerializeAs("asset_id", SerializeAs.IdValue, IsRequired = true)]
        [Patch(nameof(isAssetModified))]
        public StubAsset Asset
        {
            get => asset;
            set
            {
                isAssetModified = true;
                asset = value;
            }
        }
        private bool isAssetModified = false;
        private StubAsset asset;

        /// <inheritdoc />
        /// <remarks>This field is required.</remarks>
        [DeserializeAs("name")]
        [SerializeAs("name")]
        [Patch(nameof(isNameModified))]
        public override string Name
        {
            get => name;
            set
            {
                isNameModified = true;
                name = value;
            }
        }
        private bool isNameModified = false;
        private string name;

        /// <value>Gets the location for the maintenance.</value>
        [DeserializeAs("location")]
        [SerializeAs("location_id", SerializeAs.IdValue)]
        [Patch(nameof(isLocationModified))]
        public Stub<Location> Location
        {
            get => location;
            set
            {
                isLocationModified = true;
                location = value;
            }
        }
        private bool isLocationModified = false;
        private Stub<Location> location;

        /// <value>Gets/sets the notes for the maintenance.</value>
        [DeserializeAs("notes")]
        [SerializeAs("notes")]
        public string Notes { get; set; }

        /// <value>Gets/sets the supplier for the maintenance.</value>
        [DeserializeAs("supplier")]
        [SerializeAs("supplier_id", SerializeAs.IdValue, IsRequired = true)]
        [Patch(nameof(isSupplierModified))]
        public Stub<Supplier> Supplier
        {
            get => supplier;
            set
            {
                isSupplierModified = true;
                supplier = value;
            }
        }
        private bool isSupplierModified = false;
        private Stub<Supplier> supplier;

        /// <value>Gets/sets the cost of the maintenance.</value>
        [DeserializeAs("cost")]
        [SerializeAs("cost")]
        [Patch(nameof(isMaintenanceCostModified))]
        public decimal? MaintenanceCost
        {
            get => maintenanceCost;
            set
            {
                isMaintenanceCostModified = true;
                maintenanceCost = value;
            }
        }
        private bool isMaintenanceCostModified = false;
        private decimal? maintenanceCost;

        /// <value>Gets/sets the type of the maintenance.</value>
        [DeserializeAs("asset_maintenance_type")]
        [SerializeAs("asset_maintenance_type", IsRequired = true)]
        [Patch(nameof(isMaintenanceTypeModified))]
        public MaintenanceType? MaintenanceType
        {
            get => maintenanceType;
            set
            {
                isMaintenanceTypeModified = true;
                maintenanceType = value;
            }
        }
        private bool isMaintenanceTypeModified = false;
        private MaintenanceType? maintenanceType;

        /// <value>Gets/sets if the maintenance is covered under warranty.</value>
        [DeserializeAs("is_warranty")]
        [SerializeAs("is_warranty")]
        [Patch(nameof(isIsWarrantyModified))]
        public bool? IsWarranty
        {
            get => isWarranty;
            set
            {
                isIsWarrantyModified = true;
                isWarranty = value;
            }
        }
        private bool isIsWarrantyModified = false;
        private bool? isWarranty;

        /// <value>Gets/sets the date the maintenance begins.</value>
        [DeserializeAs("start_date", DeserializeAs.DateObject)]
        [SerializeAs("start_date", SerializeAs.SimpleDateOnly, IsRequired = true)]
        [Patch(nameof(isStartDateModified))]
        public DateTime? StartDate
        {
            get => startDate;
            set
            {
                isStartDateModified = true;
                startDate = value;
            }
        }
        private bool isStartDateModified = false;
        private DateTime? startDate;

        /// <value>Gets/sets the duration of the maintenance.</value>
        [DeserializeAs("asset_maintenance_time", DeserializeAs.Timespan)]
        public TimeSpan? MaintenanceDuration { get; private set; }

        /// <value>Gets/sets the date the maintenance ends.</value>
        [DeserializeAs("completion_date", DeserializeAs.DateObject)]
        [SerializeAs("completion_date", SerializeAs.SimpleDateOnly)]
        [Patch(nameof(isCompletionDateModified))]
        public DateTime? CompletionDate
        {
            get => completionDate;
            set
            {
                isCompletionDateModified = true;
                completionDate = value;
            }
        }
        private bool isCompletionDateModified = false;
        private DateTime? completionDate;

        /// <value>Gets the user who created the maintenance.</value>
        [DeserializeAs("user_id")]
        public Stub<User> User { get; private set; }

        /// <inheritdoc />
        [DeserializeAs("available_actions", DeserializeAs.AvailableActions)]
        public AvailableAction AvailableActions { get; private set; }

        void IPatchable.SetAllModifiedState(bool isModified)
        {
            isAssetModified = isModified;
            isNameModified = isModified;
            isLocationModified = isModified;
            isSupplierModified = isModified;
            isMaintenanceCostModified = isModified;
            isMaintenanceTypeModified = isModified;
            isIsWarrantyModified = isModified;
            isStartDateModified = isModified;
            isCompletionDateModified = isModified;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            ((IPatchable)this).SetAllModifiedState(false);
        }
    }
}
