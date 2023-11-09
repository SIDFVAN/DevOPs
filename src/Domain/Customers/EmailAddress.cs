using System.Net.Mail;
using Blanche.Domain.Common;

namespace Blanche.Domain.Customers;

public class EmailAddress : ValueObject
{
    public string Value { get; private set;  } = default!;

    private EmailAddress() { }

    public EmailAddress(string value)
    {
        if (!IsValid(value))
            throw new ApplicationException($"Invalid {nameof(EmailAddress)}: {value}");

        Value = value.Trim();
    }

    private static bool IsValid(string emailaddress)
    {
        try
        {
            MailAddress m = new(emailaddress);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }
}
