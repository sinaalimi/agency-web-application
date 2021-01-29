using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.ViewModel.Home
{
    public class BenchmarkViewModel
    {
        /// <summary>
        /// کاربر سیستمی است یا نه
        /// </summary>
        public bool IsSystemAccount { get; set; }

        /// <summary>
        /// تعداد کل کاربران
        /// </summary>
        public string UsersCount { get; set; } = "۰";

        /// <summary>
        /// تعداد کل اساتید ثبت شده و یا ارسال شده به مدیریت
        /// </summary>
        public string ActiveToursCount { get; set; } = "۰";
        /// <summary>
        /// تعداد اساتید تأیید شده
        /// </summary>
        public string SalePolicyCount { get; set; } = "۰";
        /// <summary>
        /// تعداد کل مقالات
        /// </summary>
        public string CustomerCount { get; set; } = "۰";

        /// <summary>
        /// تعداد خطا های سیستم
        /// </summary>
        public string SystemErrorsCount { get; set; } = "۰";

        public string TotalToursCount { get; set; } = "۰";
    }
}
