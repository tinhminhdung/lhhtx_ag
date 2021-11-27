using Common.Model.GHTK;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace VS.Common.Service
{
    public interface IGHTKPostService
    {
        Task<PriceGhtkResultModel> GetPrice(PriceGhtkRequestModel model);
        Task<string> CreateOrder();
        Task<string> OrderStatus();
        Task<string> CancelOrder();
        Task<string> PrintOrder();
    }

    public class GHTKPostService : IGHTKPostService
    {
        //Nếu lấy từ config thì dùng thằng này để lấy
        private readonly IConfiguration configuration;
        private readonly IPostConnect postService;
        public static string Host = "https://dev.ghtk.vn";
        //public static string Host = "https://services.giaohangtietkiem.vn";
        public static string Token = "3d6C65727cF5A5791E495236d31Be8E64cc7f4ea";

        public GHTKPostService(IConfiguration configuration,
            IPostConnect postService)
        {
            this.configuration = configuration;
            this.postService = postService;
        }

        /// <summary>
        /// Lấy giá vận chuyển của một đơn hàng
        /// </summary>
        /// <returns></returns>
        public async Task<PriceGhtkResultModel> GetPrice(PriceGhtkRequestModel model)
        {
            var strContent = JsonConvert.SerializeObject(model,
                           new JsonSerializerSettings
                           {
                               ContractResolver = new CamelCasePropertyNamesContractResolver()
                           });
            string path = string.Format("/services/shipment/fee?{0}", WebUtility.UrlEncode(strContent));
            var result = await postService.CallAsync<PriceGhtkResultModel>(Host, path, Token, HttpMethod.Get, null);
            if (result != null)
            {
                if (result.fee != null)
                {
                    var fee = result.fee.fee;
                    var insurance_fee = result.fee.insurance_fee;
                }
                   
            }
            return result;
        }

        /// <summary>
        /// Tạo yêu cầu đơn hàng trên GHTK
        /// </summary>
        /// <returns></returns>
        public async Task<string> CreateOrder()
        {
            return null;
        }

        /// <summary>
        /// Kiểm tra trạng thái một đơn hàng
        /// </summary>
        /// <returns></returns>
        public async Task<string> OrderStatus()
        {
            return null;
        }

        /// <summary>
        /// Hủy bỏ một đơn hàng
        /// </summary>
        /// <returns></returns>
        public async Task<string> CancelOrder()
        {
            return null;
        }

        /// <summary>
        /// In nhãn đơn hàng để đóng gói
        /// </summary>
        /// <returns></returns>
        public async Task<string> PrintOrder()
        {
            return null;
        }
    }
}