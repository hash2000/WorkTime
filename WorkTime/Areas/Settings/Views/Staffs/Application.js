

Ext.define('Staffs.Application', {
    extend: 'Ext.app.Application',
    name: 'Staffs',
    appFolder: Application.Path.Get('Areas/Settings/Views/Staffs'),
    controllers: [
        'Home'
    ],
    singleton: true,
    autoCreateViewport: true,
    launch: function () {
        Ext.QuickTips.init();
    }
});