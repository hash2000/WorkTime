


Ext.define('Staffs.store.Staffs', {
    extend: 'Ext.data.Store',
    model: 'Staffs.model.Staff',
    autoLoad: true,
    remoteFilter: true,
    autoSync: true

});