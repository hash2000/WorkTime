



Ext.define('Vacations.controller.Home', {
    extend: 'Ext.app.Controller',

    requires: [
        'Ext.ux.grid.FiltersFeature'
    ],

    stores: [
        'Vacations.store.Vacations',
        'Vacations.store.VacationTypes'
    ]


});

