
Ext.Loader.setPath('WorkTime', Application.Path.Get('Views/Home'));

Ext.define('WorkTime.Application', {
    extend: 'Ext.app.Application',
    name: 'WorkTime',
    appFolder: Application.Path.Get('Views/Home'),
    controllers: [
        'Home'
    ],
    singleton: true,
    autoCreateViewport: true,
    launch: function () {
        Ext.QuickTips.init();
    }
});

