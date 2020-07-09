$(function () {
    $('[data-toggle="tooltip"]').tooltip();
    $('[data-toggle="popover"]').popover({
        placement: 'bottom',
        content: function () {
            var content = $("#notification-content").html();

            return content;
        },
        html: true
    });

    $('body').append('<div id="notification-content" class="form-control" hidden></div>')

    function getNotifications() {
        var res = "<ul class='list-group'>";
        $.ajax({
            url: "/UserNotification/UnreadNotifications",
            method: "GET",
            success: function (result) {
                if (result.count != 0) {
                    $("#notificationCount").html(result.count);
                    $("#notificationCount").show('slow');
                } else {
                    $("#notificationCount").html();
                    $("#notificationCount").hide('slow');
                    $("#notificationCount").popover('hide');
                }
                console.log(result);
                var userNotifications = result.notifications;
                console.log(userNotifications);
                userNotifications.forEach(element => {
                    res = res + "<li class='list-group-item notification-text' data-id='" + element.notificationId + "'>" + element.info + "</li>";
                });
                res = res + "</ul>";
                $("#notification-content").html(res);
            },
            error: function (error) {
                console.log(error);
            }
        });
    }


    $(document).on('click', 'ul > li.notification-text', function (e) {
        var id = $('ul > li.notification-text').attr('data-id');
        var target = e.target;
        console.log(id);
        readNotification(id, target);
    })

    function readNotification(id, target) {
        $.ajax({
            url: "/UserNotification/MarkAsRead",
            method: "POST",
            data: { notificationId: id },
            success: function (result) {
                getNotifications(); // to update the notification count
                $(target).fadeOut('slow');
            },
            error: function (error) {
                console.log(error);
            }
        })
    }

    getNotifications();
});