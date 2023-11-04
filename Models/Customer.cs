using MongoDB.Bson;
using MongoDB_EF_Driver_Sample.Enums;

namespace MongoDB_EF_Driver_Sample.Models;

public class Customer
{
    public ObjectId Id { get; set; }
    public required string Name { get; set; }
    public required Shipment shipment { get; set; }
    public required ContactInfo ContactInfo { get; set; }
}