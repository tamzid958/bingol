using Microsoft.AspNetCore.Http;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Bingol.Services
{
    /// <summary>
    /// SSLCommerz SDK
    /// Author: SSLWIRELESS LTD.
    /// Developed By: Md. Shahnawaz Ahmed
    /// </summary>
    public class SslCommerzGatewayProcessor
    {
        private List<string> key_list;
        private string generated_hash;
        private readonly string Store_ID;
        private readonly string Store_Pass;
        private bool StoreTestMode;
        private string error;

        private string _sslCzUrl = "https://securepay.sslcommerz.com/";
        private const string SubmitUrl = "gwprocess/v4/api.php";
        private const string ValidationUrl = "validator/api/validationserverAPI.php";
        protected string CheckingUrl = "validator/api/merchantTransIDvalidationAPI.php";

        public SslCommerzGatewayProcessor(string storeId, string storePass, bool storeTestMode = false)
        {
            System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)0x00000C00;

            if (storeId != "" && storePass != "")
            {
                this.Store_ID = storeId;
                this.Store_Pass = storePass;
                this.SetSSLCzTestMode(storeTestMode);
            }
            else
            {
                throw new Exception("Please provide Store ID and Password to initialize SSLCommerz");
            }
        }

        public string InitiateTransaction(NameValueCollection postData, bool getGateWayList = false)
        {
            postData.Add("store_id", this.Store_ID);
            postData.Add("store_passwd", this.Store_Pass);
            string response = this.SendPost(postData);
            try
            {
                SSLCommerzInitResponse resp = new JavaScriptSerializer().Deserialize<SSLCommerzInitResponse>(response);
                if (resp.Status == "SUCCESS")
                {
                    if (getGateWayList)
                    {
                        // We will work on it!
                    }
                    else
                    {
                        return resp.GatewayPageUrl.ToString();
                    }
                }
                else
                {
                    throw new Exception("Unable to get data from SSLCommerz. Please contact your manager!");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }

            return response;
        }
        public bool OrderValidate(string MerchantTrxID, string MerchantTrxAmount, string MerchantTrxCurrency, HttpRequest req)
        {
            bool hash_verified = this.Ipn_hash_verify(req);
            if (hash_verified)
            {

                string json = string.Empty;

                string EncodedValID = System.Web.HttpUtility.UrlEncode(req.Form["val_id"]);
                string EncodedStoreID = System.Web.HttpUtility.UrlEncode(this.Store_ID);
                string EncodedStorePassword = System.Web.HttpUtility.UrlEncode(this.Store_Pass);

                string validate_url = this._sslCzUrl + ValidationUrl + "?val_id=" + EncodedValID + "&store_id=" + EncodedStoreID + "&store_passwd=" + EncodedStorePassword + "&v=1&format=json";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(validate_url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream resStream = response.GetResponseStream();
                using (StreamReader reader = new StreamReader(resStream))
                {
                    json = reader.ReadToEnd();
                }
                if (json != "")
                {
                    SslCommerzValidatorResponse resp = new JavaScriptSerializer().Deserialize<SslCommerzValidatorResponse>(json);

                    if (resp.Status == "VALID" || resp.Status == "VALIDATED")
                    {
                        if (MerchantTrxCurrency == "BDT")
                        {
                            if (MerchantTrxID == resp.Tran_id && (Math.Abs(Convert.ToDecimal(MerchantTrxAmount) - Convert.ToDecimal(resp.Amount)) < 1) && MerchantTrxCurrency == "BDT")
                            {
                                return true;
                            }
                            else
                            {
                                this.error = "Amount not matching";
                                return false;
                            }
                        }
                        else
                        {
                            if (MerchantTrxID == resp.Tran_id && (Math.Abs(Convert.ToDecimal(MerchantTrxAmount) - Convert.ToDecimal(resp.Currency_amount)) < 1) && MerchantTrxCurrency == resp.Currency_type)
                            {
                                return true;
                            }
                            else
                            {
                                this.error = "Currency Amount not matching";
                                return false;
                            }

                        }
                    }
                    else
                    {
                        this.error = "This transaction is either expired or fails";
                        return false;
                    }
                }
                else
                {
                    this.error = "Unable to get Transaction JSON status";
                    return false;

                }
            }
            else
            {
                this.error = "Unable to verify hash";
                return false;
            }
        }
        protected void SetSSLCzTestMode(bool mode)
        {
            this.StoreTestMode = mode;
            if (mode)
            {
                //this.Store_ID = "testbox";
                //this.Store_Pass = "qwerty";
                this._sslCzUrl = "https://sandbox.sslcommerz.com/";
            }
        }

        protected string SendPost(NameValueCollection PostData)
        {
            Console.WriteLine(this._sslCzUrl + SubmitUrl);
            string response = SslCommerzGatewayProcessor.Post(this._sslCzUrl + SubmitUrl, PostData);
            return response;
        }
        public static string Post(string uri, NameValueCollection PostData)
        {
            byte[] response = null;
            using (WebClient client = new WebClient())
            {
                response = client.UploadValues(uri, PostData);
            }
            return System.Text.Encoding.UTF8.GetString(response);
        }
        /// <summary>
        /// SSLCommerz IPN Hash Verify method
        /// </summary>
        /// <param name="req"></param>
        /// <param name="pass"></param>
        /// <returns>Boolean - True or False</returns>
        public Boolean Ipn_hash_verify(HttpRequest req)
        {

            // Check For verify_sign and verify_key parameters
            if (req.Form["verify_sign"] != "" && req.Form["verify_key"] != "")
            {
                // Get the verify key
                String verify_key = req.Form["verify_key"];
                if (verify_key != "")
                {

                    // Split key string by comma to make a list array
                    key_list = verify_key.Split(',').ToList<String>();

                    // Initiate a key value pair list array
                    List<KeyValuePair<String, String>> data_array = new List<KeyValuePair<string, string>>();

                    // Store key and value of post in a list
                    foreach (String k in key_list)
                    {
                        data_array.Add(new KeyValuePair<string, string>(k, req.Form[k]));
                    }

                    // Store Hashed Password in list
                    String hashed_pass = this.MD5(this.Store_Pass);
                    data_array.Add(new KeyValuePair<string, string>("store_passwd", hashed_pass));

                    // Sort Array
                    data_array.Sort(
                        delegate (KeyValuePair<string, string> pair1,
                        KeyValuePair<string, string> pair2)
                        {
                            return pair1.Key.CompareTo(pair2.Key);
                        }
                    );


                    // Concat and make String from array
                    String hash_string = "";
                    foreach (var kv in data_array)
                    {
                        hash_string += kv.Key + "=" + kv.Value + "&";
                    }
                    // Trim & from end of this string
                    hash_string = hash_string.TrimEnd('&');

                    // Make hash by hash_string and store
                    generated_hash = this.MD5(hash_string);

                    // Check if generated hash and verify_sign match or not
                    if (generated_hash == req.Form["verify_sign"])
                    {
                        return true; // Matched
                    }
                }

                return false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Make PHP like MD5 Hashing
        /// </summary>
        /// <param name="s"></param>
        /// <returns>md5 Hashed String</returns>
        public String MD5(String s)
        {
            byte[] asciiBytes = ASCIIEncoding.ASCII.GetBytes(s);
            byte[] hashedBytes = System.Security.Cryptography.MD5.Create()
                .ComputeHash(asciiBytes);
            string hashedString = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            return hashedString;
        }

        public class Gw
        {
            public string Visa { get; set; }
            public string Master { get; set; }
            public string Amex { get; set; }
            public string Othercards { get; set; }
            public string Internetbanking { get; set; }
            public string Mobilebanking { get; set; }
        }

        public class Desc
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public string Logo { get; set; }
            public string Gw { get; set; }
        }

        public class SSLCommerzInitResponse
        {
            public string Status { get; set; }
            public string Failedreason { get; set; }
            public string Sessionkey { get; set; }
            public Gw Gw { get; set; }
            public string RedirectGatewayURL { get; set; }
            public string RedirectGatewayURLFailed { get; set; }
            public string GatewayPageUrl { get; set; }
            public string StoreBanner { get; set; }
            public string StoreLogo { get; set; }
            public List<Desc> Desc { get; set; }
            public string IsDirectPayEnable { get; set; }
        }

        private abstract class SslCommerzValidatorResponse
        {
            public string Status { get; set; }
            public string Tran_date { get; set; }
            public string Tran_id { get; set; }
            public string Val_id { get; set; }
            public string Amount { get; set; }
            public string Store_amount { get; set; }
            public string Currency { get; set; }
            public string Bank_tran_id { get; set; }
            public string Card_type { get; set; }
            public string Card_no { get; set; }
            public string Card_issuer { get; set; }
            public string Card_brand { get; set; }
            public string Card_issuer_country { get; set; }
            public string Card_issuer_country_code { get; set; }
            public string Currency_type { get; set; }
            public string Currency_amount { get; set; }
            public string Currency_rate { get; set; }
            public string Base_fair { get; set; }
            public string Value_a { get; set; }
            public string Value_b { get; set; }
            public string Value_c { get; set; }
            public string Value_d { get; set; }
            public string Emi_instalment { get; set; }
            public string Emi_amount { get; set; }
            public string Emi_description { get; set; }
            public string Emi_issuer { get; set; }
            public string Account_details { get; set; }
            public string Risk_title { get; set; }
            public string Risk_level { get; set; }
            public string APIConnect { get; set; }
            public string Validated_on { get; set; }
            public string Gw_version { get; set; }
            public string Token_key { get; set; }
            public string Shipping_method { get; set; }
            public string Num_of_item { get; set; }
            public string Product_name { get; set; }
            public string Product_profile { get; set; }
            public string Product_category { get; set; }
        }
    }
}