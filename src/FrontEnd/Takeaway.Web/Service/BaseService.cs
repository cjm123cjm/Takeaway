using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using Takeaway.Web.Models;
using Takeaway.Web.Service.IService;
using Takeaway.Web.Utility;

namespace Takeaway.Web.Service
{
	public class BaseService : IBaseService
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public BaseService(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
		{
			try
			{
				HttpClient client = _httpClientFactory.CreateClient("TakeawayAPI");
				HttpRequestMessage message = new();
				message.Headers.Add("Accept", "application/json");

				message.RequestUri = new Uri(requestDto.Url);
				if (requestDto.Data != null)
				{
					message.Content = new StringContent(
						JsonConvert.SerializeObject(requestDto.Data),
						Encoding.UTF8,
						"application/json");
				}

				switch (requestDto.ApiType)
				{
					case SD.ApiType.POST:
						message.Method = HttpMethod.Post;
						break;
					case SD.ApiType.PUT:
						message.Method = HttpMethod.Put;
						break;
					case SD.ApiType.DELETE:
						message.Method = HttpMethod.Delete;
						break;
					default:
						message.Method = HttpMethod.Get;
						break;
				}

				HttpResponseMessage? apiResponse = await client.SendAsync(message);
				switch (apiResponse.StatusCode)
				{
					case HttpStatusCode.NotFound:
						return new() { IsSuccess = false, Message = "Not Found" };
					case HttpStatusCode.Forbidden:
						return new() { IsSuccess = false, Message = "Access Denied" };
					case HttpStatusCode.Unauthorized:
						return new() { IsSuccess = false, Message = "Unauthorized" };
					case HttpStatusCode.InternalServerError:
						return new() { IsSuccess = false, Message = "Internal Server Error" };
					default:
						var apiContent = await apiResponse.Content.ReadAsStringAsync();
						var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
						return apiResponseDto;
				}
			}
			catch (Exception ex)
			{
				ResponseDto responseDto = new ResponseDto
				{
					IsSuccess = false,
					Message = ex.Message.ToString()
				};
				return responseDto;
			}
		}
	}
}
