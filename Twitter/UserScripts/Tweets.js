$(document).ready(function() {
    $("#btn_update").on("click", function () {
        var msg = $("#tweetMsg").val();
        if (typeof msg != "undefined" && msg != "" && msg.length < 134) {
            $.ajax({
                url: "Home/Tweet",
                type: "POST",
                data: { "data": msg },
                success: function (data) {
                    if (data != null) {
                        $("#tweetMsg").val("");
                        $("#Tweets").html(data);
                    }
                },
                error: function(xhr) {
                    var error = $(this);
                }
            });
        }
    });

    $("#searchToFollow").on("click",
        function () {
            var msg = $(this).val();
            $.ajax({
                url: "Home/LoadUsers",
                type: "POST",
                data: { "data": msg },
                success: function (data) {
                    
                },
                error: function (xhr) {
                    var error = $(this);
                }
            });
        });
});