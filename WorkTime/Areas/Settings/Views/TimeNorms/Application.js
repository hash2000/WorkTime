
Ext.Loader.setPath('Staffs', Application.Path.Get('Areas/Settings/Views/Staffs'));



Ext.define('TimeNorms.Application', {
    extend: 'Ext.app.Application',
    name: 'TimeNorms',
    appFolder: Application.Path.Get('Areas/Settings/Views/TimeNorms'),
    controllers: [
        'Home'
    ],
    singleton: true,
    autoCreateViewport: true,
    launch: function () {
        Ext.QuickTips.init();
    }
});

