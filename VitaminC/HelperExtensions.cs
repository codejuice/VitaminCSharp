using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace VitaminC
{
    public static class HelperExtensions
    {



        #region General
        public static int toInt(this string data)
        {
            int rval = 0;
            int.TryParse(data, out rval);

            return rval;
        }

        public static byte[] ToByteArray(this string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string ToString(this byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
        #endregion
        #region Documents


        public static string ToFriendlyFileString(this long number)
        {
            string formattedNumber;
            if (number > 1073741824)
                formattedNumber = String.Format("{0} GB", number /
                1073741824);
            else if (number > 1048576)
                formattedNumber = String.Format("{0} MB", (decimal)(number / 1048576));
            else if (number > 1024)
                formattedNumber = String.Format("{0} KB", number / 1024);
            else
                formattedNumber = String.Format("{0} bytes", number);
            return formattedNumber;
        }

        #endregion
        #region Timezones
        public static List<TimeZoneInfo> GetTimeZones()
        {
            var timeZoneInfos = TimeZoneInfo.GetSystemTimeZones().ToList();

            return timeZoneInfos;
        }
        public static TimeZoneInfo GetTimeZoneByKey(this string timezoneid)
        {
            var tzs = TimeZoneInfo.GetSystemTimeZones();

            var tz = tzs.Where(w => w.Id == timezoneid).FirstOrDefault();


            return tz;
        }
        #endregion
        #region Cryptography

        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static string GetRandomPassword(int length)
        {
            try
            {
                char[] chars = "abcdefghjkmnpqrstuvwxyz23456789ABCDEFGHJKMNPQRSTUVWXYZ".ToCharArray();
                string password = string.Empty;
                Random random = new Random();

                for (int i = 0; i < length; i++)
                {
                    int x = random.Next(1, chars.Length);
                    //Don't Allow Repetation of Characters
                    if (!password.Contains(chars.GetValue(x).ToString()))
                        password += chars.GetValue(x);
                    else
                        i--;
                }
                return password;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public static byte[] Sha1Hash(this byte[] data)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                return sha1.ComputeHash(data);
            }
        }
        #endregion
    }
}