$(function () {

    //Proxy created on the fly
    var chat = $.connection.chatHub;

    // Declare a function on the chat hub so the server can invoke it
    chat.client.addPlayerTurnToPage = function (thisPlayerTurn) {
        $("#messages").append("Player " + thisPlayerTurn + " can play, refresh the page!");
    };

    $("#broadcast").click(function () {
        // call the chat method on the server
        chat.client.addPlayerTurnToPage($("#msg").val());
    
    });      

    $.connection.hub.start();
});

// This optional function html-encodes messages for display in the page.
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}
