using Blanche.Shared.Customers;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace Blanche.Client.Customer.Components
{
    public partial class CustomerDetail
    {
        [Parameter]
        public CustomerDto Customer { get; set; }

        private EditContext EditContext;
        private ValidationMessageStore validationMessages;
        private bool valid;

        protected override async Task OnInitializedAsync()
        {
            // Read customer when loggen

            EditContext = new EditContext(Customer);
            validationMessages = new(EditContext);

        }

        public bool CheckCustomerDetails()
        {

            return EditContext.Validate();
        }


    }
} 
