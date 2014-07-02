
Ext.define('Common.view.window.Login', {
    extend: 'Ext.window.Window',
    alias: 'widget.LoginWindow',
    modal: true,
    resizable: false,

    title: 'Авторизация',

    width: 200,
    height: 170,

    defaults: {
        margin: 5
    },

    items: [{
        xtype: 'label',
        text: 'Имя пользователя'
    }, {
        xtype: 'textfield',
        itemId: 'LoginNameText'
    }, {
        xtype: 'label',
        text: 'Пароль'
    }, {
        xtype: 'textfield',
        itemId: 'PasswordText',
        inputType: 'password'
    }],

    buttons: [{
        text: 'Ok',
        itemId: 'OkButton'
    }, {
        text: 'Отмена',
        handler: function () {
            this.up('window')
                .close();
        }
    }],

    loginHandler: undefined,
    scope: undefined,

    initComponent: function () {

        this.callParent(arguments);

        this.down('#OkButton').on('click', function () {

            var login = this.down('#LoginNameText').value,
                password = this.down('#PasswordText').value,
                window = this.down().up('window'),
                loginHandler = this.loginHandler;

            window.body.mask('Load');

            Ext.Ajax.request({
                url: Application.Path.Get('Account/Login'),
                params: {
                    username: login,
                    password: password,
                },
                scope: this,
                callback: function (request, success, response) {
                    var responseData = Ext.decode(response.responseText);
                    var result = false;
                    if (success && responseData.items[0]) {
                        result = true;
                    }

                    if (!Ext.isEmpty(this.loginHandler))
                        this.loginHandler.call(this.scope || this, result);

                    window.close();
                }
            });            

        }, this);       

    }
});
