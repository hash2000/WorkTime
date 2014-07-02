

Ext.define('TimeNorms.store.Holidays', {
    extend: 'Ext.data.Store',
    model: 'TimeNorms.model.Holiday',
    autoLoad: true,
    remoteFilter: true,
    autoSync: true

});