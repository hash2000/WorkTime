


Ext.define('Staffs.controller.Home', {
    extend: 'Ext.app.Controller',

    requires: [
        'Ext.ux.grid.FiltersFeature'
    ],

    stores: [
        'Staffs.store.Staffs'
    ]

});
