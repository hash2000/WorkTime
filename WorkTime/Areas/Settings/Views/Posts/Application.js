Ext.Loader.setPath('TimeNorms', Application.Path.Get('Areas/Settings/Views/TimeNorms'));
Ext.Loader.setPath('Vacations', Application.Path.Get('Areas/Settings/Views/Vacations'));


Ext.define('Posts.Application', {
    extend: 'Ext.app.Application',
    name: 'Posts',
    appFolder: Application.Path.Get('Areas/Settings/Views/Posts'),
    controllers: [
        'Home'
    ],
    singleton: true,
    autoCreateViewport: true,
    launch: function () {
        Ext.QuickTips.init();
    }
});

