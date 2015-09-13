(function ($) {
    $.osharp = $.osharp || { version: 1.0 };
    $.osharp.easyui = $.osharp.easyui || {};
})(jQuery);
(function ($) {
    $.osharp.easyui.msg = {
        tip: function (content, title, timeout, showType) {
            $.messager.show({
                title: title || "消息提示",
                msg: content,
                timeout: timeout == undefined ? 3000 : timeout,
                showType: showType || "show"
            });
        },
        info: function (content, title) {
            $.messager.alert(title || "消息提示", content, "info");
        },
        warning: function (content, title) {
            $.messager.alert(title || "警告提示", content, "warning");
        },
        question: function (content, title) {
            $.messager.alert(title || "询问提示", content, "question");
        },
        error: function (content, title) {
            $.messager.alert(title || "错误提示", content, "error");
        },
        confirm: function (content, title, onOk, onCancel) {
            $.messager.confirm(title || "请选择", content, function (isOK) {
                if (isOK) {
                    if (onOk && (typeof onOk) == "function") {
                        onOk();
                        return;
                    }
                }
                if (onCancel && (typeof onCancel) == "function") {
                    onCancel();
                }
            });
        }
    };
})(jQuery);