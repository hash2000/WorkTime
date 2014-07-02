

Ext.define('Common.data.Ajax', {
    extend: Ext.data.Connection,

    singleton: true,
    autoAbort: false,

    requestPrompt: function (options) {
        options = options || {};
        var params = {
            username: options.username || this.username,
            password: options.password || this.password || '',
            title: options.title || '',
            msg: options.msg || '',
            msgSuccess: options.msgSuccess,
            titleSuccess: options.titleSuccess || 'Готово',
            msgFailure: options.msgFailure,
            titleFailure: options.titleFailure || 'Ошибка',
            url: options.url,
            params: options.params || {}
        };

        Ext.Msg.prompt({
            title: params.title,
            msg: params.msg,
            width: 300,
            scope: this,
            buttons: Ext.Msg.OKCANCEL,
            icon: Ext.Msg.QUESTION,
            params: params,
            fn: function (id, value, opt) {
                if (id === 'ok') {
                    Ext.Ajax.request({
                        url: params.url,
                        params: params.params,
                        username: params.username,
                        password: params.password,
                        scope: params,
                        callback: function (request, success, response) {
                            var responseData = Ext.decode(response.responseText);
                            var operationresult = true;
                            if (!Ext.isEmpty(responseData)) {
                                if (!Ext.isEmpty(responseData.items)) {
                                    operationresult = responseData.items[0];
                                }
                            }
                            if (success && responseData.success && operationresult !== false) {
                                Ext.Msg.show({
                                    title: this.titleSuccess,
                                    msg: this.msgSuccess,
                                    width: 300,
                                    buttons: Ext.Msg.OK,
                                    icon: Ext.Msg.INFO
                                });
                            } else {
                                Ext.Msg.show({
                                    title: this.titleFailure,
                                    msg: this.msgFailure,
                                    width: 300,
                                    buttons: Ext.Msg.OK,
                                    icon: Ext.Msg.ERROR,
                                });
                            }
                        } //end callback
                    });
                } // end if 
            } // end fn
        });
    }

});