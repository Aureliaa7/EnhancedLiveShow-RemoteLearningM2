using System.ComponentModel;

namespace LiveShow.Services.Models.Notification
{
    public enum NotificationTypeDto
    {
        [Description("added show")]
        AddedShow = 0,
        [Description("updated show")]
        UpdatedShow = 1,
        [Description("canceled show")]
        CanceledShow = 2
    }
}
