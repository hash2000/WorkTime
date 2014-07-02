


Ext.define('Common.view.Viewport', {
    extend: 'Ext.container.Viewport',
    width: '100%',
    height: '80%',

    initComponent: function () {

        if (Ext.isEmpty(this.headerText))
            this.headerText = Application.Info.title;
        else
            this.headerText = Application.Info.title + ' (' + this.headerText + ')';

        var contentItems = this.items;

        var containerItems = [{

            region: 'north',
            frame: true,
            height: 100,
            style: 'margin: 0 30px 10px 30px; font-size: 50pt',

            items: [{
                xtype: 'label',
                text: this.headerText,
                style: 'font-size: 30px; margin: 20px 0 0 40px'
            }],

            bbar: [{
                xtype: 'button',
                scale: 'medium',
                frame: false,
                text: 'На главную',
                href: Application.Path.Get(''),
                hrefTarget: ''
            }, /*{
                xtype: 'button',
                scale: 'medium',
                itemId: 'VacationButton',
                text: 'Расчет отпуска'
            },*/ {
                xtype: 'button',
                scale: 'medium',
                itemId: 'TableButton',
                text: 'Отчеты',
                href: Application.Path.Get('Reports/Index'),
                hrefTarget: ''
            }, {
                xtype: 'button',
                text: 'Справочники',
                menu: [{
                    text: 'Нормы времени',
                    href: Application.Path.Get('Settings/TimeNorms/Index'),
                    hrefTarget: ''
                }, {
                    text: 'Должности',
                    href: Application.Path.Get('Settings/Posts/Index'),
                    hrefTarget: ''
                }, {
                    text: 'Отпуска',
                    href: Application.Path.Get('Settings/Vacations/Index'),
                    hrefTarget: ''
                }, {
                    text: 'Сотрудники',
                    href: Application.Path.Get('Settings/Staffs/Index'),
                    hrefTarget: ''
                }]
            }, {
                xtype: 'button',
                itemId: 'AuthoricatedButton',
                disabled: true,
                text: 'Вход',

                logoffHandler: function () {
                    Ext.Ajax.request({
                        url: Application.Path.Get('Account/Logoff'),
                        scope: this,
                        callback: function (request, success, response) {
                            var authbutton = this,
                                responseData = Ext.decode(response.responseText);
                            if (!Ext.isEmpty(responseData) && success) {
                                if (!Ext.isEmpty(responseData.items)) {
                                    if (responseData.items[0] === true) {
                                        authbutton.setText('Вход');
                                        authbutton.removeListener('click', this.logoffHandler);
                                        authbutton.on('click', this.logonMsgHandler);
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

                logonMsgHandler: function () {
                    var dlg = Ext.create('Common.view.window.Login');
                    dlg.scope = this;
                    dlg.loginHandler = function (responseResult) {
                        var authbutton = this;

                        if (responseResult === true) {
                            authbutton.setText('Выход');
                            authbutton.removeListener('click', this.logonMsgHandler);
                            authbutton.on('click', this.logoffHandler);
                            return;
                        }

                        Ext.Msg.show({
                            title: 'Ошибка',
                            msg: 'Доступ запрещен',
                            width: 300,
                            buttons: Ext.Msg.OK,
                            icon: Ext.Msg.ERROR,
                        });
                    };
                    dlg.show();
                }
            }]
        }];

        Ext.Ajax.request({
            url: Application.Path.Get('Account/IsAuthenticated'),
            scope: this,
            callback: function (request, success, response) {

                var responseData = Ext.decode(response.responseText),
                    operationresult = false,
                    authbutton = this.down('[itemId=AuthoricatedButton]'),
                    authbuttonText = 'Вход',
                    authbuttonhandler = authbutton.logonMsgHandler;
                if (!Ext.isEmpty(responseData) && success) {
                    if (!Ext.isEmpty(responseData.items)) {
                        operationresult = responseData.items[0];
                        if (operationresult) {
                            authbuttonText = 'Выход';
                            authbuttonhandler = authbutton.logoffHandler;
                        }
                    }
                }

                authbutton.setDisabled(false);
                authbutton.setText(authbuttonText);
                authbutton.on('click', authbuttonhandler);

            }
        });

        if (!Ext.isEmpty(contentItems)) {
            containerItems.push({
                region: 'center',
                xtype: 'container',
                layout: 'fit',
                height: '80%',
                style: 'margin: 0 30px 10px 30px',
                items: contentItems
            });
        }

        this.items = containerItems;

        this.callParent(arguments);
    }


});