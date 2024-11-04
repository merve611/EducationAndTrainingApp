using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EducationAndTrainingApp.WebApi.Filters
{
    public class DayAndTimeControlFilter : ActionFilterAttribute
    {

        // Sadece belirli bir gün (AllowedDay) ve belirli saat aralığında (StartTime - EndTime) erişime izin verir.
        // Sadece Pazartesi günleri saat 9:00 ile 17:00 arasında erişime izin ver
        public DayOfWeek AllowedDay { get; set; } = DayOfWeek.Sunday;
        public string StartTime { get; set; } = "17:00";
        public string EndTime { get; set; } = "20:00";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var currentTime = DateTime.Now;
            var currentDay = currentTime.DayOfWeek;
            var currentTimeOfDay = currentTime.TimeOfDay;

            if (currentDay == AllowedDay &&
                currentTimeOfDay >= TimeSpan.Parse(StartTime) &&
                currentTimeOfDay <= TimeSpan.Parse(EndTime))
            {
                base.OnActionExecuting(context);
            }
            else
            {
                context.Result = new ContentResult
                {
                    Content = $"Bu endpoint'e sadece {AllowedDay} günleri {StartTime} ile {EndTime} arasında erişim sağlanabilir.",
                    StatusCode = 403,
                };
            }
        }
    }
}
