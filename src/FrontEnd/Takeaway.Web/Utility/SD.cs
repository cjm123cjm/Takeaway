﻿namespace Takeaway.Web.Utility
{
	public class SD
	{
		public static string CouponAPIBase { get; set; }
		public static string AuthAPIBase { get; set; }
		public static string RoleName = "ADMIN";
		public static string RoleCustomer = "CUSTOMER";
        public enum ApiType
		{
			GET,
			POST,
			PUT,
			DELETE
		}
	}
}
