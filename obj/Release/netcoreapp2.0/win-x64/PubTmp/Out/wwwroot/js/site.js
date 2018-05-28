// Write your JavaScript code.
window.onload = function () {
    var text = document.getElementsByClassName('txt');
    var textLeng = 100;
    for (var i = 0; text.length; i++) {
        var str = text[i].innerHTML;
        if (str.length > textLeng) {
            text[i].innerHTML = str.substring(0, textLeng) + "......";
        }
    }
}

function changeURLArg(url, arg, arg_val) {
    var pattern = arg + '=([^&]*)';
    var replaceText = arg + '=' + arg_val;
    if (url.match(pattern)) {
        var tmp = '/(' + arg + '=)([^&]*)/gi';
        tmp = url.replace(eval(tmp), replaceText);
        return tmp;
    } else {
        if (url.match('[\?]')) {
            return url + '&' + replaceText;
        } else {
            return url + '?' + replaceText;
        }
    }
    return url + '\n' + arg + '\n' + arg_val;
}
