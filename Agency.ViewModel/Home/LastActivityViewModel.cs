using System;

namespace Agency.ViewModel.Home
{
    public class LastActivityViewModel
    {
        /// <summary>
        /// نام کاربری کاربر ثبت کننده تغییرات
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// زمان
        /// </summary>
        public DateTime OperateDate { get; set; }
    }
}
