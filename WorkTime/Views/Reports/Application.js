
Ext.Loader.setPath('Reports', Application.Path.Get('Views/Reports'));

Ext.define('Reports.Application', {
    extend: 'Ext.app.Application',
    name: 'Reports',
    appFolder: Application.Path.Get('Views/Reports'),
    controllers: [
        'Home'
    ],
    singleton: true,
    autoCreateViewport: true,
    launch: function () {
        Ext.QuickTips.init();
    }
});