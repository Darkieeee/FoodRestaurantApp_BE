using System.Security.Cryptography;
namespace FoodRestaurantApp_BE.Services.Abstracts
{
    public abstract class PayOsService
    {
        protected readonly string BASE_API_URL = "https://api-merchant.payos.vn";

        protected string? ClientId;
        protected string? ChecksumKey;
        protected string? ApiKey;

        protected readonly HMAC Hmac;

        private readonly IConfiguration _configuration;

        public enum PayOsProvidedService {
            OneTimePayment
        }

        public PayOsService(IConfiguration configuration)
        {
            _configuration = configuration;
            ClientId = _configuration["PaymentStrings:PayOs:ClientId"];
            ApiKey = _configuration["PaymentStrings:PayOs:ApiKey"];
            ChecksumKey = _configuration["PaymentStrings:PayOs:ChecksumKey"];
            Hmac = HashGenerator.CreateHMAC("SHA256", Convert.FromHexString(ChecksumKey!));
        }

        public string? GetClientId()
        {
            return ClientId;
        }

        public string? GetChecksumKey()
        {
            return ChecksumKey;
        }

        public string? GetApiKey()
        {
            return ApiKey;
        }

        public abstract PayOsProvidedService GetPaymentType();
    }
}