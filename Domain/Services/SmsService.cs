using System.Text;

namespace Domain.Services;

public class SmsService
{
    private readonly Random _random;

    public SmsService(Random reandom)
    {
        _random = reandom;
    }

    public string GenerateCode(int length)
    {
        const string chars = "0123456789";
        var codeBuilder = new StringBuilder(length);

        for (int i = 0; i < length; i++)
        {
            codeBuilder.Append(chars[_random.Next(chars.Length)]);
        }

        return codeBuilder.ToString();
    }

    public string SendSms(string phoneNumber)
    {
        try
        {
            string verificationCode = GenerateCode(4);

            // Пример с использованием Twilio SMS API (необходимо подключить пакет Twilio)
            // var twilio = new TwilioRestClient("your-account-sid", "your-auth-token");
            // twilio.SendMessage("your-twilio-phone-number", phoneNumber, "Your verification code is: " + verificationCode);

            return verificationCode;
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to send SMS: " + ex.Message);
        }
    }
}
