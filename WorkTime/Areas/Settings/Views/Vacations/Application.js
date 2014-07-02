

Ext.define('Vacations.Application', {
    extend: 'Ext.app.Application',
    name: 'Vacations',
    appFolder: Application.Path.Get('Areas/Settings/Views/Vacations'),
    controllers: [
        'Home'
    ],
    singleton: true,
    autoCreateViewport: true,
    launch: function () {
        Ext.QuickTips.init();
    }
});