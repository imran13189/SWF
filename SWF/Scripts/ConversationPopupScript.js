var popup, 
    sendMessageForm,
    messagesDiv;

$(document).ready(function () {
    popup = $('.conversation-popup-container');
    messagesDiv = popup.find('.messages');
    sendMessageForm = popup.find('#frmSendMessage');

    if (messagesDiv.height() < messagesDiv[0].scrollHeight) {
        messagesDiv.animate({ scrollTop: messagesDiv[0].scrollHeight }, 1000);
    }

    var timer = setInterval(function () {
        var sender = sendMessageForm.find('[name="UserID"]'),
            receiver = sendMessageForm.find('[name="ToUserID"]'),
            lastMessageId = sendMessageForm.find('[name="LastReadMessageID"]');

        $.ajax({
            url: '/Conversation/GetRecentMessages',
            type: 'get',
            cache: false,
            data: { UserID: parseInt(sender.val(), 10), ToUserID: parseInt(receiver.val(), 10), LastReadMessageID: parseInt(lastMessageId.val(), 10) },
            success: function (htmlResult) {
                if (htmlResult.length > 0) {
                    loadChatMessages(htmlResult);
                }
            },
            complete: function () {
            }
        });
    }, 10000);

    window.onfocus = function () {
        var sender = sendMessageForm.find('[name="UserID"]'),
            receiver = sendMessageForm.find('[name="ToUserID"]'),
            lastMessageId = sendMessageForm.find('[name="LastReadMessageID"]');

        $.ajax({
            url: '/Conversation/UpdateMessageStatus',
            type: 'post',
            data: { UserID: parseInt(sender.val(), 10), ToUserID: parseInt(receiver.val(), 10), LastReadMessageID: parseInt(lastMessageId.val(), 10) },
            success: function () {
            }
        });
    };
});

function SetScrollPosition() {
    var div = document.getElementById('divMessages');
    div.scrollTop = 100000000000;
}

function SetToEnd(txtMessage) {
    if (txtMessage.createTextRange) {
        var fieldRange = txtMessage.createTextRange();
        fieldRange.moveStart('character', txtMessage.value.length);
        fieldRange.collapse();
        fieldRange.select();
    }
}

function ReplaceChars() {
    var txt = document.getElementById('txtMessage').value;
    var out = "<"; // replace this
    var add = ""; // with this
    var temp = "" + txt; // temporary holder

    while (temp.indexOf(out) > -1) {
        pos = temp.indexOf(out);
        temp = "" + (temp.substring(0, pos) + add +
        temp.substring((pos + out.length), temp.length));
    }

    document.getElementById('txtMessage').value = temp;
}

function FocusThisWindow(result, context) {
    // don't do anything here
}

function FocusMe() {
    FocusThisWindowCallBack('FocusThisWindow');
}

var loadChatMessages = function (data) {
    messagesDiv.append(data);

    if (messagesDiv.height() < messagesDiv[0].scrollHeight) {
        messagesDiv.animate({ scrollTop: messagesDiv[0].scrollHeight }, 1000);
    }

    updateLastReadMessageId();
};

function updateLastReadMessageId() {
    var lastMessage = messagesDiv.children().filter(':last');

    sendMessageForm.find('[name="LastReadMessageID"]').val(parseInt(lastMessage.data('id'), 10)).end()
        .find('[type="text"]').val('');
    window.focus();
}



