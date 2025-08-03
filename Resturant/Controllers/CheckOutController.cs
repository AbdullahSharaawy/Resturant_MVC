using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Services;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Nodes;

namespace Resturant_PL.Controllers
{
    public class CheckOutController : Controller
    {
       private readonly IReservationService reservationService;
        private readonly IReservedTableService reservedTableService;

        private string PaypalClientId {  get; set; }
        private string PaypalSecret { get; set; }
        private string PaypalUrl { get; set; }
        public CheckOutController(IConfiguration configuration, IReservationService reservationService, IReservedTableService reservedTableService)
        {
            PaypalClientId = configuration["PaypalSettings:ClientID"];
            PaypalSecret = configuration["PaypalSettings:Secret"];
            PaypalUrl = configuration["PaypalSettings:Url"];
            this.reservationService = reservationService;
            this.reservedTableService = reservedTableService;
        }
        public IActionResult Index()
        {
            ViewBag.PaypalClientId = PaypalClientId;
            var checkOutDTOJson = TempData["CheckOutDTO"] as string;
            var checkOutDTO = checkOutDTOJson != null
                ? JsonConvert.DeserializeObject<CheckOutDTO>(checkOutDTOJson)
                : null;
            return View("Index",checkOutDTO);
        }
        [HttpPost]
        public async Task<JsonResult> CreateOrder([FromBody] JsonObject data)
        {
            var totalAmount = data?["amount"]?.ToString();
           // var checkOutDTOJson = data?["CheckOutDTO"]?.ToString();
           

            if (totalAmount == null)
            {
                return new JsonResult(new { Id = "" });
            }
            JsonObject createOrderRequest = new JsonObject();
            createOrderRequest.Add("intent", "CAPTURE");
            JsonObject amount = new JsonObject();
            amount.Add("currency_code", "USD");
            amount.Add("value", totalAmount);

            JsonObject purchaseUnit1 = new JsonObject();
            purchaseUnit1.Add("amount", amount);

            JsonArray purchaseUnits = new JsonArray();
            purchaseUnits.Add(purchaseUnit1);

            createOrderRequest.Add("purchase_units", purchaseUnits);

            string accessToken = await GetPaypalAccessToken();
            string url = PaypalUrl + "/v2/checkout/orders";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                requestMessage.Content = new StringContent(createOrderRequest.ToString(), null, "application/json");

                var httpResponse = await client.SendAsync(requestMessage);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var strResponse = await httpResponse.Content.ReadAsStringAsync();
                    var jsonResponse = JsonNode.Parse(strResponse);

                    if (jsonResponse != null)
                    {
                        string paypalOrderId = jsonResponse["id"]?.ToString() ?? "";

                        return new JsonResult(new { Id = paypalOrderId/*,checkOutDTO= checkOutDTOJson*/ });
                    }
                }

            }

            return new JsonResult(new { Id = "" });
        }
        [HttpPost]
        public async Task<JsonResult> CompleteOrder([FromBody] JsonObject data)
        {
            var orderId = data?["orderID"]?.ToString();
            //var checkOutDTOJson = data?["CheckOutDTO"]?.ToString();
            //var checkOutDTO = checkOutDTOJson != null
            //    ? JsonConvert.DeserializeObject<CheckOutDTO>(checkOutDTOJson)
            //    : null;
          
            if (orderId == null)
            {
                return new JsonResult("error");
            }

            string accessToken = await GetPaypalAccessToken();
            string url = PaypalUrl + $"/v2/checkout/orders/{orderId}/capture";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                requestMessage.Content = new StringContent("", null, "application/json");

                var httpResponse = await client.SendAsync(requestMessage);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var strResponse = await httpResponse.Content.ReadAsStringAsync();
                    var jsonResponse = JsonNode.Parse(strResponse);

                    if (jsonResponse != null)
                    {
                        string paypalOrderStatus = jsonResponse["status"]?.ToString() ?? "";
                        if (paypalOrderStatus == "COMPLETED")
                        {
                            // save the order in the database
                            //reservationService.Create(checkOutDTO.reservation);
                            //foreach (var r in checkOutDTO.reservedTable)
                            //{
                            //    reservedTableService.Create(r);
                            //}
                            return new JsonResult("success");
                        }
                    }
                }

            }

            return new JsonResult("error");
        }


        //public async Task<string> Token()
        // {
        //     return await GetPaypalAccessToken();
        // }
        private async Task<string> GetPaypalAccessToken()
        {
            string accessToken="";

            try
            {
                   
        string url = PaypalUrl + "/v1/oauth2/token";

                using (var client = new HttpClient())
                {
                    // Create basic authentication header
                    string credentials = $"{PaypalClientId}:{PaypalSecret}";
                    string credentialsBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));

                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + credentialsBase64);
                    // Notice the space after "Basic"   

                    // Prepare request content
                    var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                    requestMessage.Content=new StringContent("grant_type=client_credentials", null, "application/x-www-form-urlencoded");
                    // Send request
                    var response = await client.SendAsync( requestMessage);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var tokenResponse = JsonNode.Parse(responseContent);
                        if(tokenResponse != null)
                        {
                            accessToken = tokenResponse["access_token"]?.ToString()??"";
                        }
                    }
                   
                }
                
            }
            catch (Exception ex)
            {
                // Log error here
                throw new Exception("Failed to get PayPal access token", ex);
            }
            return accessToken;
        }

        // Helper class for token response
        
    }
}
