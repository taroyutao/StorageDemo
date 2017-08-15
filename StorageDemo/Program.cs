using System;
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Table; // Namespace for Table storage types

namespace StorageDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=tablestorage1;AccountKey=vvt/x6rCpsulg1cGwOEUfcpmwPe+A1nDFvrLUAA9hutHt6aNvxTvZyO1QUICTkAvBQ4wFUfchK82+j6r+I8Q6Q==;EndpointSuffix=core.chinacloudapi.cn");

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
   
            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference("people");

            // Create the table if it doesn't exist.
            table.CreateIfNotExists();

            // Create a new customer entity.
            CustomerEntity customer1 = new CustomerEntity("AAA", "BBB");
            customer1.Email = "Walter@contoso.com";
            customer1.PhoneNumber = "425-555-0101";

            // Create the TableOperation object that inserts the customer entity.
            TableOperation insertOperation = TableOperation.Insert(customer1);

            // Execute the insert operation.
            table.Execute(insertOperation);

            Console.WriteLine("Insert success!");

            Console.ReadKey(true);
        }
    }

    public class CustomerEntity : TableEntity
    {
        public CustomerEntity(string lastName, string firstName)
        {
            this.PartitionKey = lastName;
            this.RowKey = firstName;
        }

        public CustomerEntity() { }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
