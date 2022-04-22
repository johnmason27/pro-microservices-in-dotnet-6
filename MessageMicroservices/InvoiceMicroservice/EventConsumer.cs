using MassTransit;
using MessageContracts;

namespace InvoiceMicroservice {
    public class EventConsumer : IConsumer<IInvoiceToCreate> {
        public async Task Consume(ConsumeContext<IInvoiceToCreate> context) {
            var newInvoiceNumber = new Random().Next(10000, 99999);

            Console.WriteLine($"Creating invoice {newInvoiceNumber} for customer: {context.Message.CustomerNumber}");

            context.Message.InvoiceItems.ForEach(item => {
                Console.WriteLine($"With itmes: Price: {item.Price}, Description: {item.Description}");
                Console.WriteLine($"Actual distance in miles: {item.ActualMileage}, BaseRate: {item.BaseRate}");
                Console.WriteLine($"Oversize: {item.IsOversized}, Refrigerated: {item.IsRefrigerated}, Haz Mat: {item.IsHazardousMaterial}");
            });

            await context.Publish<IInvoiceCreated>(new {
                InvoiceNumber = newInvoiceNumber,
                InvoiceData = new {
                    context.Message.CustomerNumber,
                    context.Message.InvoiceItems
                }
            });
        }
    }
}
