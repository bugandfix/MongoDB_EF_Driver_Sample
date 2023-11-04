using System.Net;

namespace MongoDB_EF_Driver_Sample.Models;

public class ContactInfo
{
    public required Address ShippingAddress { get; set; }
    public Address? BillingAddress { get; set; }
    public required PhoneNumbers Phones { get; set; }
}

