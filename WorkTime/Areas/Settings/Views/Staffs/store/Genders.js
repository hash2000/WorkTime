
Ext.define('Staffs.store.Genders', {
    extend: 'Ext.data.Store',
    model: 'Staffs.model.Gender',
    autoLoad: true,
    remoteFilter: true,
    autoSync: true

});