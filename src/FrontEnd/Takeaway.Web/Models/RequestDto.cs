using static Takeaway.Web.Utility.SD;

namespace Takeaway.Web.Models
{
	public class RequestDto
	{
		public ApiType ApiType { get; set; } = ApiType.GET;
		public string Url { get; set; } = null!;
		public object? Data { get; set; }
		public string AccessToken { get; set; } = string.Empty;
	}
}
