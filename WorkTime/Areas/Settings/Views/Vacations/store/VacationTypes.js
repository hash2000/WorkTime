

Ext.define('Vacations.store.VacationTypes', {
    extend: 'Ext.data.Store',
    model: 'Vacations.model.VacationType',
    autoLoad: true,
    remoteFilter: true,
    autoSync: true
});

