using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestaurantReservation.Domain.Services;

namespace RestaurantReservation.Infrastructure.Services
{
    /// <summary>
    /// Implementation of IEmailService using MailKit for SMTP.
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService"/> class.
        /// </summary>
        /// <param name="configuration">Configuration settings.</param>
        /// <param name="logger">Logger instance.</param>
        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        /// <inheritdoc />
        public Task SendEmailAsync(string toEmail, string subject, string body)
        {
            // should send an email
            // didn't implemented as this is for the test task :)
            
            return Task.CompletedTask;
        }
    }
}
