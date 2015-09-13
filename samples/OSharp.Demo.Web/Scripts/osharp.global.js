(function ($) {
    $.osharp = $.osharp || { version: 1.0 };
})(jQuery);
(function ($) {
    //辅助工具类
    $.osharp.tools = {
        //Cookie操作
        cookies: {
            get: function (name) {
                var value = "";
                var search = name + "=";
                if (document.cookie.length == 0) {
                    return value;
                }
                var offset = document.cookie.indexOf(search);
                if (offset == -1) {
                    return value;
                }
                offset += search.length;
                var end = document.cookie.indexOf(";", offset);
                if (end == -1) {
                    end = document.cookie.length;
                }
                value = decodeURIComponent(document.cookie.substring(offset, end));
                return value;
            },
            set: function (name, value, expireDays) {
                var expires;
                if (expireDays == null) {
                    expireDays = 1;
                }
                expires = new Date((new Date()).getTime() + expireDays * 86400000);
                expires = ";expires=" + expires.toGMTString();
                document.cookie = name + "=" + encodeURIComponent(value) + ";path=/" + expires;
            },
            remove: function (name) {
                var expires;
                expires = new Date(new Date().getTime() - 1);
                expires = ";expires=" + expires.toGMTString();
                document.cookie = name + "=" + escape("") + ";path=/" + expires;
            }
        },
        //URL编码与解码
        url: {
            encode: function (url) {
                return encodeURIComponent(url);
            },
            decode: function (url) {
                return decodeURIComponent(url);
            }
        },
        //数据处理
        data: {
            valueToText: function (value, array, defaultText) {
                var text = defaultText == null ? value : defaultText;
                $.each(array, function () {
                    if (this.id != undefined && this.id == value) {
                        text = this.text;
                        return false;
                    }
                    if (this.id != undefined && this.id == value) {
                        text = this.text;
                        return false;
                    }
                    return true;
                });
                return text;
            }
        },
        //集合操作
        array: {
            expandAndToString: function (array, separator) {
                var result = "";
                if (!separator) {
                    separator = ";";
                }
                $.each(array, function (index, item) {
                    result = result + item.toString() + separator;
                });
                return result.substring(0, result.length - separator.length);
            }
        },
        //时间格式化
        formatDate:function(value, format) {
            if (!value) {
                return "";
            }
            format = format || "yyyy-MM-dd hh:mm:ss";
            return (new Date(parseInt(value.substring(value.indexOf('(') + 1, value.indexOf(')'))))).format(format);
        }
    };
    //查询条件类
    $.osharp.filter = {
        rule: function (field, value, operate) {
            this.Field = field;
            this.Value = value;
            this.Operate = operate || "equal";
        },
        group: function () {
            this.Rules = [];
            this.Operate = "and";
            this.Groups = [];
        }
    };
})(jQuery);

Date.prototype.format = function (format) {
    if (!format) {
        format = "yyyy-MM-dd hh:mm:ss";
    }
    var o = {
        "M+": this.getMonth() + 1, // month
        "d+": this.getDate(), // day
        "h+": this.getHours(), // hour
        "m+": this.getMinutes(), // minute
        "s+": this.getSeconds(), // second
        "q+": Math.floor((this.getMonth() + 3) / 3), // quarter
        "S": this.getMilliseconds()// millisecond
    };
    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
};