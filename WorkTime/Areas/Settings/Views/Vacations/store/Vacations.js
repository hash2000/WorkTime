

Ext.define('Vacations.store.Vacations', {
    extend: 'Ext.data.Store',
    model: 'Vacations.model.Vacation',
    autoLoad: true,
    remoteFilter: true,
    autoSync: true

});