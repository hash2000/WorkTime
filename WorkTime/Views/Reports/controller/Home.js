

Ext.define('Reports.controller.Home', {
    extend: 'Ext.app.Controller',

    refs: [{
        ref: 'DateFromDateField',
        selector: '#DateFromDateField'
    }, {
        ref: 'DateToDateField',
        selector: '#DateToDateField'
    }],


    init: function () {

        this.control({
            '#GenerateT13FormButton': {
                click: this.GenerateT13FormButton_clisk
            },
            '#GenerateInternalTableButton': {
                click: this.GenerateInternalTableButton_clisk
            }
        });
    },

    GenerateT13FormButton_clisk: function () {

        Ext.Ajax.request({
            url: Application.Path.Get('Account/IsAuthenticated'),
            scope: this,
            callback: function (request, success, response) {
                var responseData = Ext.decode(response.responseText);
                if (!Ext.isEmpty(responseData) && success) {
                    if (!Ext.isEmpty(responseData.items)) {
                        if (responseData.items[0] === true) {
                            // добавление к параметрам дат начала и окончания формирования 
                            var from = this.getDateFromDateField().getValue(),
                                to = this.getDateToDateField().getValue();
                            var parameters = '?dateFrom=' + Ext.Date.format(from, 'Y-m-d') + '&dateTo=' + Ext.Date.format(to, 'Y-m-d');
                            window.location = Application.Path.Get('T13Report/Generate') + parameters;
                            return;
                        }
                    }
                }

                Ext.Msg.show({
                    title: 'Ошибка',
                    msg: 'Доступ запрещен',
                    width: 300,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.ERROR,
                });

            }
        });

    },

    GenerateInternalTableButton_clisk: function () {

        Ext.Ajax.request({
            url: Application.Path.Get('Account/IsAuthenticated'),
            scope: this,
            callback: function (request, success, response) {
                var responseData = Ext.decode(response.responseText);
                if (!Ext.isEmpty(responseData) && success) {
                    if (!Ext.isEmpty(responseData.items)) {
                        if (responseData.items[0] === true) {
                            // добавление к параметрам дат начала и окончания формирования 
                            var from = this.getDateFromDateField().getValue(),
                                to = this.getDateToDateField().getValue();
                            var parameters = '?dateFrom=' + Ext.Date.format(from, 'Y-m-d') + '&dateTo=' + Ext.Date.format(to, 'Y-m-d');
                            window.location = Application.Path.Get('InternalReport/Generate') + parameters;
                            return;
                        }
                    }
                }

                Ext.Msg.show({
                    title: 'Ошибка',
                    msg: 'Доступ запрещен',
                    width: 300,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.ERROR,
                });
            }
        });
    }

});