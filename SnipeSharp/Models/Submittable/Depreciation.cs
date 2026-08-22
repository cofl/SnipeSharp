using SnipeSharp.EndPoint;
using SnipeSharp.Models.Enumerations;
using SnipeSharp.Serialization;
using System.Runtime.Serialization;

namespace SnipeSharp.Models
{
    /// <summary>
    /// A Depreciation.
    /// Depreciations are associated with objects and determine when End-Of-Life is relative to the PurchaseDate.
    /// </summary>
    [PathSegment("depreciations")]
    public sealed class Depreciation : AbstractBaseModel, IAvailableActions, IPatchable
    {
        /// <summary>Create a new Depreciation object.</summary>
        public Depreciation() { }

        /// <summary>Create a new Depreciation object with the supplied ID, for use with updating.</summary>
        internal Depreciation(int id)
        {
            Id = id;
        }

        /// <inheritdoc />
        /// <remarks>This field is required.</remarks>
        [DeserializeAs("name")]
        [SerializeAs("name", IsRequired = true)]
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

        /// <inheritdoc />
        /// <remarks>This field is required.</remarks>
        [DeserializeAs("months", DeserializeAs.MonthStringAsInt)]
        [SerializeAs("months", IsRequired = true)]
        [Patch(nameof(isMonthsModified))]
        public int? Months
        {
            get => months;
            set
            {
                isMonthsModified = true;
                months = value;
            }
        }
        private bool isMonthsModified = false;
        private int? months;

        /// <inheritdoc />
        [DeserializeAs("available_actions", DeserializeAs.AvailableActions)]
        public AvailableAction AvailableActions { get; private set; }

        void IPatchable.SetAllModifiedState(bool isModified)
        {
            isNameModified = isModified;
            isMonthsModified = isModified;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            ((IPatchable)this).SetAllModifiedState(false);
        }
    }
}
