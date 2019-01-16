function poststatus() {
    $.ajax({
        url: "/Home/Post",
        type: "post",
        data: $("#postForm").serialize(),
        success: function (result) {
            $("#partial").html(result);
        }
    });
}
