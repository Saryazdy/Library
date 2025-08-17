using Library.Application.Common.Interfaces;
using Library.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.EventHandlers
{
    public sealed class BookCreatedEventHandler : INotificationHandler<BookCreatedEvent>
    {
        private readonly IEmailService _email;
        private readonly ISmsService _sms;

        public BookCreatedEventHandler(IEmailService email, ISmsService sms)
        {
            _email = email; _sms = sms;
        }

        public async Task Handle(BookCreatedEvent notification, CancellationToken ct)
        {
            var subject = $"New book: {notification.Book.Title}";
            var body = $"Book '{notification.Book.Title}' created at {notification.OccurredOn:u}";
            await _email.SendAsync("admin@library.local", subject, body, ct);
            await _sms.SendAsync("+989100000000", $"New book: {notification.Book.Title}", ct);
        }
    }
}
