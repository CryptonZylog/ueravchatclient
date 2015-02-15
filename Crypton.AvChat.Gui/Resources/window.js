function quoteText(dateRef) {
    var msgBody = $(dateRef).parent();
    window.external.quoteText(msgBody.text());
    return false;
}