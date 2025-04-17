using ChatApp.Domain.Events;
using CQRS;
using FluentEmail.Core;

namespace ChatApp.Application.Commons.EventHandlers
{
    public class SendMailEventHandler : IEventHandler<SendMailEvent>
    {
        private readonly IFluentEmail fluentEmail;

        public SendMailEventHandler(IFluentEmail fluentEmail)
        {
            this.fluentEmail = fluentEmail;
        }

        public async Task HandleAsync(
            SendMailEvent tevent,
            CancellationToken cancellationToken = default
        )
        {
            var result = await fluentEmail
                .To(tevent.To)
                .Subject(tevent.Subject)
                .Body(tevent.Body)
                .SendAsync(cancellationToken);

            System.Console.WriteLine("[Send Mail] Occured At: ", tevent.OccuredAt);
            System.Console.WriteLine("[Send Mail] Event Id: ", tevent.EventId);

            if (!result.Successful)
            {
                foreach (var error in result.ErrorMessages)
                {
                    System.Console.WriteLine(error);
                }

                throw new Exception(
                    "[Send Mail Error] Something went wrong while sending email to client"
                );
            }
        }
    }
}
