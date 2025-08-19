using Microsoft.Extensions.Configuration;
using Resturant_BLL.Services;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

public class PaymobService: IPaymobService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _integrationId;
    private readonly string _iframeId;

    public PaymobService(IConfiguration config)
    {
        _httpClient = new HttpClient();
        _apiKey = config["Paymob:ApiKey"];
        _integrationId = config["Paymob:IntegrationId"];
        _iframeId = config["Paymob:IframeId"];
    }

    public async Task<string> CreatePayment(decimal amount, string currency = "EGP")
    {
        //  Authentication Token
        var authResponse = await _httpClient.PostAsync(
            "https://accept.paymob.com/api/auth/tokens",
            new StringContent(JsonSerializer.Serialize(new { api_key = _apiKey }), Encoding.UTF8, "application/json")
        );

        var authContent = await authResponse.Content.ReadAsStringAsync();
        var authToken = JsonDocument.Parse(authContent).RootElement.GetProperty("token").GetString();

        // Create Order
        var orderPayload = new
        {
            amount_cents = (int)(amount),
            currency = currency,
            delivery_needed = "false",
            items = new object[] { }
        };

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

        var orderResponse = await _httpClient.PostAsync(
            "https://accept.paymob.com/api/ecommerce/orders",
            new StringContent(JsonSerializer.Serialize(orderPayload), Encoding.UTF8, "application/json")
        );

        var orderContent = await orderResponse.Content.ReadAsStringAsync();
        var orderId = JsonDocument.Parse(orderContent).RootElement.GetProperty("id").GetInt32();

        //  Payment Key
        var paymentPayload = new
        {
            auth_token = authToken,
            amount_cents = (int)(amount * 100),
            expiration = 3600,
            order_id = orderId,
            billing_data = new
            {
                apartment = "NA",
                email = "customer@example.com",
                floor = "NA",
                first_name = "John",
                street = "NA",
                building = "NA",
                phone_number = "+201000000000",
                shipping_method = "NA",
                postal_code = "NA",
                city = "NA",
                country = "NA",
                last_name = "Doe",
                state = "NA"
            },
            currency = currency,
            integration_id = int.Parse(_integrationId)
        };

        var paymentResponse = await _httpClient.PostAsync(
            "https://accept.paymob.com/api/acceptance/payment_keys",
            new StringContent(JsonSerializer.Serialize(paymentPayload), Encoding.UTF8, "application/json")
        );

        var paymentContent = await paymentResponse.Content.ReadAsStringAsync();
        var paymentKey = JsonDocument.Parse(paymentContent).RootElement.GetProperty("token").GetString();

        //  Iframe URL
        return $"https://accept.paymob.com/api/acceptance/iframes/{_iframeId}?payment_token={paymentKey}";
    }
}
