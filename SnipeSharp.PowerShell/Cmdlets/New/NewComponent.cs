using System;
using System.Management.Automation;
using SnipeSharp.EndPoint;
using SnipeSharp.Models;
using SnipeSharp.PowerShell.BindingTypes;
using SnipeSharp.PowerShell.Attributes;

namespace SnipeSharp.PowerShell.Cmdlets.New
{
    [Cmdlet(VerbsCommon.New, nameof(Component))]
    [OutputType(typeof(Component))]
    public class NewComponent: PSCmdlet
    {
        /// <summary>
        /// The name of the component.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        /// How many of the component there are.
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ValueFromPipelineByPropertyName = true)]
        [ValidateRange(1, int.MaxValue)]
        public int Quantity { get; set; }
        
        /// <summary>
        /// The category of the component.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [ValidateIdentityNotNull]
        public ObjectBinding<Category> Category { get; set; }

        /// <summary>
        /// The company that owns the component.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public ObjectBinding<Company> Company { get; set; }

        /// <summary>
        /// The location of the component.
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public ObjectBinding<Location> Location { get; set; }

        /// <summary>
        /// When to start warning that the component is running low.
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public int MinimumQuantity { get; set; }

        /// <summary>
        /// The order the component was purchased in.
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string OrderNumber { get; set; }

        /// <summary>
        /// The cost the component was purchased for.
        /// </summary>
        /// <value></value>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public decimal PurchaseCost { get; set; }

        /// <summary>
        /// The date the component was purchased.
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public DateTime PurchaseDate { get; set; }

        /// <summary>
        /// The serial number(s) of the component(s).
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Serial { get; set; }

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            var item = new Component {
                Name = this.Name,
                Category = this.Category?.Object,
                Quantity = this.Quantity,
                Company = this.Company?.Object,
                Location = this.Location?.Object,
                OrderNumber = this.OrderNumber,
                Serial = this.Serial
            };
            if(MyInvocation.BoundParameters.ContainsKey(nameof(MinimumQuantity)))
                item.MinimumQuantity = this.MinimumQuantity;
            if(MyInvocation.BoundParameters.ContainsKey(nameof(PurchaseCost)))
                item.PurchaseCost = this.PurchaseCost;
            if(MyInvocation.BoundParameters.ContainsKey(nameof(PurchaseDate)))
                item.PurchaseDate = this.PurchaseDate;
            //TODO: error handling
            WriteObject(ApiHelper.Instance.Components.Create(item));
        }
    }
}
